<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermission.aspx.cs" Inherits="Presentation.WFPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Permisos
        </div>
        <div class="card-body">
            <form id="FrmPermission" runat="server">
                <asp:HiddenField ID="HFPermisoID" runat="server" />

                <div class="row m-1">
                    <div class="col-6">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Permiso:"></asp:Label>
                        <asp:DropDownList ID="DDLNombrePer" CssClass="form-control" runat="server">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="CREAR">Crear</asp:ListItem>
                            <asp:ListItem Value="ACTUALIZAR">Actualizar</asp:ListItem>
                            <asp:ListItem Value="MOSTRAR">Mostrar</asp:ListItem>
                            <asp:ListItem Value="ELIMINAR">Eliminar</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVNombrePer"
                            runat="server"
                            ControlToValidate="DDLNombrePer"
                            InitialValue="0"
                            ForeColor="Red"
                            Display="Dynamic"
                            ErrorMessage="Debe seleccionar un permiso">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-6">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Descripción:"></asp:Label>
                        <asp:TextBox ID="TBDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescripcion"
                            runat="server"
                            ControlToValidate="TBDescripcion"
                            ForeColor="Red"
                            Display="Dynamic"
                            ErrorMessage="Este campo es obligatorio">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" />
                        <asp:Label ID="LblMsg" CssClass="form-label text-success" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <asp:Panel ID="PanelAdmin" runat="server">

            <div class="card-header">
                Lista de Permisos
            </div>
            <div class="card-body">

                <table id="permissionTable" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre</th>
                            <th>Descripción</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </asp:Panel>

    </div>

    <script src="resources/js/datatables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';

            $('#permissionTable').DataTable({
                "ajax": {
                    "url": "WFPermission.aspx/ListPermissions",
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
                    { "data": "PermisoID" },
                    { "data": "NamePermiso" },
                    { "data": "Description" },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button type="button" class="btn btn-info btn-sm edit-btn" data-id="${row.PermisoID}">
                                            <i class="fas fa-edit"></i> Editar
                                          </button> `;
                            }
                            if (showDeleteButton) {
                                buttons += `<button type="button" class="btn btn-danger btn-sm delete-btn" data-id="${row.PermisoID}">
                                            <i class="fas fa-trash"></i> Eliminar
                                          </button>`;
                            }
                            return buttons;
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

            $('#permissionTable').on('click', '.edit-btn', function () {
                const rowData = $('#permissionTable').DataTable().row($(this).parents('tr')).data();
                permissionsData(rowData);
                $('#<%= BtnSave.ClientID %>').hide();
                $('#<%= BtnUpdate.ClientID %>').show();
            });

            $('#permissionTable').on('click', '.delete-btn', function () {
                if (confirm("¿Está seguro de eliminar este permiso?")) {
                    deletePermissions($(this).data('id'));
                }
            });
        });

        function permissionsData(rowData) {
            $('#<%= HFPermisoID.ClientID %>').val(rowData.PermisoID);
            $('#<%= DDLNombrePer.ClientID %>').val(rowData.NamePermiso);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Description);
        }

        function deletePermissions(id) {
            $.ajax({
                type: "POST",
                url: "WFPermission.aspx/DeletePermission",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#permissionTable').DataTable().ajax.reload();
                    alert("Permiso eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el permiso.");
                }
            });
        }
    </script>
</asp:Content>
