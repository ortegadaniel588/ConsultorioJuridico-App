<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAsignarredsocial.aspx.cs" Inherits="Presentation.WFAsignarredsocial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div>
            <%--Asignarredsocial ID--%>
            <asp:HiddenField ID="AsignarredsocialID" runat="server" />
            <br />
            <%--Empresa_idempresa--%>
            <asp:Label ID="Label1" runat="server" Text="Seleccione la empresa"></asp:Label>
            <asp:DropDownList ID="DDLEmpresa_idempresa" runat="server" CssClass="form-select"></asp:DropDownList>
            <br />
            <%--Redsocial_idredsocial--%>
            
            <asp:Label ID="Label2" runat="server" Text="Seleccione la red social"></asp:Label>
            <asp:DropDownList ID="DDLRedsocial_idredsocial" runat="server" CssClass="form-select"></asp:DropDownList>
            <br />
            <%--Url--%>
            <asp:Label ID="Label3" runat="server" Text="Ingrese la url del perfil"></asp:Label>
            <asp:TextBox ID="TBUrl" runat="server" Visible="false"></asp:TextBox>
            <br />
        </div>

        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
            <br />
            <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--lista de productos--%>
    <h2>Lista de los Asignarredsocial</h2>
    <table id="asignarredsocialesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>AsignarredsocialID</th>
                <th>Empresa</th>
                <th>Redsocial</th>
                <th>Url</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#asignarredessocialesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFAsignarredsocial.aspx/ListAsignarredessociales",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de asignarredessociales del resultado
                    }
                },
                "columns": [
                    { "data": "AsignarredsocialID" },
                    { "data": "Empresa", "visible": false },
                    { "data": "Redsocial", "visible": false },
                    { "data": "Url" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.AsignarredsocialID}">Editar</button>
                              <button class="delete-btn" data-id="${row.AsignarredsocialID}">Eliminar</button>`;
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

            // Editar un caso
            $('#asignarredessocialesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#asignarredessocialesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadAsignarredsocialData(rowData);
            });

            // Eliminar un caso
            $('#asignarredessocialesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del caso
                if (confirm("¿Estás seguro de que deseas eliminar esta asignación de red social?")) {
                    deleteAsignarredsocial(id);// Invoca a la función para eliminar el asignarredsocial
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadAsignarredsocialData(rowData) {
            $('#<%= AsignarredsocialID.ClientID %>').val(rowData.AsignarredsocialID);
            $('#<%= DDLEmpresa_idempresa.ClientID %>').val(rowData.Empresa);
            $('#<%= DDLRedsocial_idredsocial.ClientID %>').val(rowData.Redsocial);
            $('#<%= TBUrl.ClientID %>').val(rowData.Url);
        }

        // Función para eliminar un producto
        /*function deleteAsignarredsocial(id) {
            $.ajax({
                type: "POST",
                url: "WFAsignarredsocial.aspx/DeleteAsignarredsocial",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#asignarredessocialesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Producto eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el producto.");
                }
            });
        }*/
    </script>

</asp:Content>
