<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTipo.aspx.cs" Inherits="Presentation.WFTipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:HiddenField ID="TipoID" runat="server" />
        <%--Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox><br />
        <%--Descripción--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox><br />
    </div>
    
    <div>
        <%--Botones Guardar y Actualizar--%>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label><br />
    </div>
    <%--lista de Tipos--%>
    <h2>Lista de Tipos</h2>
    <table id="TipoTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>TipoID</th>
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
            $('#TipoTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFTipo.aspx/ListTipo",// Se invoca el WebMethod Listar Tipo
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
                    { "data": "TipoID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.TipoID}">Editar</button>
                              <button class="delete-btn" data-id="${row.TipoID}">Eliminar</button>`;
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

            // Editar un Tipo
            $('#TipoTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#TipoTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadCasoData(rowData);
            });

            // Eliminar un Tipo
            $('#TipoTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del tipo
                if (confirm("¿Estás seguro de que deseas eliminar este tipo?")) {
                    deleteCaso(id);// Invoca a la función para eliminar el tipo
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadTipoData(rowData) {
            $('#<%= TipoID.ClientID %>').val(rowData.TipoID);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Descripcion);
        }

        // Función para eliminar un Tipo
        function deleteTipo(idtipo) {
            $.ajax({
                type: "POST",
                url: "WFTipo.aspx/deleteTipo",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idtipo: idtipo }),
                success: function (response) {
                    $('#TipoTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Producto eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Tipo.");
                }
            });
        }*/
    </script>
</asp:Content>