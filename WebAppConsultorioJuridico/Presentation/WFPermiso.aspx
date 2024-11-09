<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermiso.aspx.cs" Inherits="Presentation.WFPermiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--ID--%>
        <asp:HiddenField ID="TBId" runat="server" />
        <%--Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
        <br />
        <%--Descripción--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la Descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
        <br />
        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--Lista de Permisos--%>
    <div>
        <table id="permissionsTable" class="display">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Descripción</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#permissionsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPermiso.aspx/ListPermissions", // Se invoca el WebMethod Listar Permisos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista 
                    }
                },
                "columns": [
                    { "data": "idpermiso" },
                    { "data": "nombre" },
                    { "data": "descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.idpermiso}">Editar</button>
                               <button class="delete-btn" data-id="${row.idpermiso}">Eliminar</button>`;
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

            $('#permissionsTable').on('click', '.edit-btn', function () {
                var rowData = $('#permissionsTable').DataTable().row($(this).parents('tr')).data();
                loadPermissionData(rowData);
            });

            $('#permissionsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del permiso
                if (confirm("¿Estás seguro de que deseas eliminar este permiso?")) {
                    deletePermission(id);// Invoca a la función para eliminar el permiso
                }
            });
        });

        // Cargar los datos en los TextBox para actualizar
        function loadPermissionData(rowData) {
            $('#<%= TBId.ClientID %>').val(rowData.idpermiso);
            $('#<%= TBNombre.ClientID %>').val(rowData.nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.descripcion);
        }

        // Función para eliminar un permiso
        function deletePermission(id) {
            $.ajax({
                type: "POST",
                url: "WFPermiso.aspx/DeletePermission", // Se invoca el WebMethod Eliminar un Permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#permissionsTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    alert("Permiso eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el permiso.");
                }
            });
        }
    </script>
</asp:Content>