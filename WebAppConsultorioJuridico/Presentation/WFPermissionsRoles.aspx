<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermissionsRoles.aspx.cs" Inherits="Presentation.WFPermissionsRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Permisos y Roles
        </div>
        <div class="card-body">
            <form id="FrmPermissionRol" runat="server">
                <asp:HiddenField ID="HFRolPermisoID" runat="server" />
                
                <div class="row m-1">
                    <div class="col-4">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Roles:"></asp:Label>
                        <asp:DropDownList ID="DDLRoles" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVRoles" 
                            runat="server" 
                            ControlToValidate="DDLRoles" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione un rol">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Permisos:"></asp:Label>
                        <asp:DropDownList ID="DDLPermisos" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVPermisos" 
                            runat="server" 
                            ControlToValidate="DDLPermisos" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione un permiso">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Fecha de Asignación:"></asp:Label>
                        <asp:TextBox ID="TBDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDate" 
                            runat="server" 
                            ControlToValidate="TBDate" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione una fecha">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" CssClass="form-label text-success" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <div class="card-header">
            Lista de Permisos por Rol
        </div>
        <div class="card-body">
            <table id="permissionRolTable" class="table table-hover display" style="width: 100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Rol</th>
                        <th>Permiso</th>
                        <th>Fecha Asignación</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <script src="resources/js/datatables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#permissionRolTable').DataTable({
                "ajax": {
                    "url": "WFPermissionsRoles.aspx/ListPermissionsRoles",
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);
                    },
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "RolPermisoID" },
                    { "data": "NameRol" },
                    { "data": "NamePermission" },
                    { "data": "Date" },
                    {
                        "data": null,
                        "render": function (row) {
                            return `<button class="btn btn-info edit-btn" data-id="${row.RolPermisoID}">Editar</button>
                             <button class="btn btn-danger delete-btn" data-id="${row.RolPermisoID}">Eliminar</button>`;
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
                        "previous": "Anterior",
                    }
                }
            });

            $('#permissionRolTable').on('click', '.edit-btn', function () {
                const rowData = $('#permissionRolTable').DataTable().row($(this).parents('tr')).data();
                loadPermissionRolData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            $('#permissionRolTable').on('click', '.delete-btn', function () {
                if (confirm("¿Está seguro de eliminar este permiso del rol?")) {
                    deletePermissionRol($(this).data('id'));
                }
            });
        });

        function loadPermissionRolData(rowData) {
            $('#<%= HFRolPermisoID.ClientID %>').val(rowData.RolPermisoID);
            $('#<%= DDLRoles.ClientID %>').val(rowData.RolID);
            $('#<%= DDLPermisos.ClientID %>').val(rowData.PermisoID);
            $('#<%= TBDate.ClientID %>').val(rowData.FechaAsignacion);
        }

        function deletePermissionRol(id) {
            $.ajax({
                type: "POST",
                url: "WFPermissionsRoles.aspx/DeletePermissionRol",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#permissionRolTable').DataTable().ajax.reload();
                    alert("Permiso eliminado del rol exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el permiso del rol.");
                }
            });
        }
    </script>
</asp:Content>