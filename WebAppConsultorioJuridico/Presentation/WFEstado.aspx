<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEstado.aspx.cs" Inherits="Presentation.WFEstado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmEstado" runat="server">
        <asp:HiddenField ID="EstadoID" runat="server" />
        <%--Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox><br />
        <%--Descripción--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox><br />
    
        <%--Botones Guardar y Actualizar--%>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label><br />
    </form>

    <%--lista de productos--%>
    <h2>Lista de los estados</h2>
    <table id="EstadoTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>EstadoID</th>
                <th>Nombre</th>
                <th>Descripción</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#EstadoTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEstado.aspx/ListEstado",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "EstadoID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.EstadoID}">Editar</button>
                              <button class="delete-btn" data-id="${row.EstadoID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            //Editar un Estado
            $('#EstadoTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#EstadoTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadEstadoData(rowData);
            });

            //Eliminar un Estado
            $('#EstadoTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Estado
                if (confirm("¿Estás seguro de que deseas eliminar este Estado?")) {
                    deleteEstado(id);// Invoca a la función para eliminar el Estado
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadEstadoData(rowData) {
            $('#<%= EstadoID.ClientID %>').val(rowData.EstadoID);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Descripcion);
        }

        //Función para eliminar un Estado
        function deleteEstado(idestado) {
            $.ajax({
                type: "POST",
                url: "WFEstado.aspx/deleteEstado",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idestado: idestado }),
                success: function (response) {
                    $('#EstadoTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Estado eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Estado.");
                }
            });
        }
    </script>
</asp:Content>
