<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRol.aspx.cs" Inherits="Presentation.WFRol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Roles
        </div>
        <div class="card-body">
            <form id="FrmRol" runat="server" class="container mt-4">
                <asp:HiddenField ID="HFRol" runat="server" />
                
                <div class="row m-1">
                    <div class="col-4">
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Seleccione el Rol:"></asp:Label>
                        <asp:DropDownList ID="DDLNombreRol" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Administrador">Administrador</asp:ListItem>
                            <asp:ListItem Value="Secretario">Secretario</asp:ListItem>
                            <asp:ListItem Value="Abogado">Abogado</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVNombreRol" runat="server" ControlToValidate="DDLNombreRol" InitialValue="0" ErrorMessage="Debes seleccionar un Rol." ForeColor="Red" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Descripción del Rol:"></asp:Label>
                        <asp:TextBox ID="TBRol_descripcion" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescripcion" runat="server" ControlToValidate="TBRol_descripcion" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio" CssClass="form-text text-danger"></asp:RequiredFieldValidator>
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

    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card m-1">
            <div class="card-header">
                Lista de Roles
            </div>
            <div class="card-body">
                <table id="rolTable" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombre Rol</th>
                            <th>Descripción</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </asp:Panel>

    <script src="resources/js/datatables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#rolTable').DataTable({
                "ajax": {
                    "url": "WFRol.aspx/ListRoles",
                    "type": "POST",
                    "contentType": "application/json",
                    "dataType": "json",
                    "dataSrc": "d.data"
                },
                "columns": [
                    { "data": "ID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `
                                        <button class="btn btn-link btn-lg px-0 edit-btn" data-id="${row.ID}" title="Editar" style="color:#fd7e14">
                                            <i class="lni lni-pencil-1"></i>
                                        </button>
                                        <button class="btn btn-link btn-lg text-danger px-0 delete-btn" data-id="${row.ID}" title="Eliminar">
                                            <i class="lni lni-trash-3"></i>
                                        </button>`;
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

            $('#rolTable').on('click', '.edit-btn', function () {
                var rowData = $('#rolTable').DataTable().row($(this).parents('tr')).data();
                loadRolData(rowData);
            });

            $('#rolTable').on('click', '.delete-btn', function () {
                if (confirm('¿Está seguro de eliminar este rol?')) {
                    deleteRol($(this).data('id'));
                }
            });
        });

        function loadRolData(rowData) {
            $('#<%= HFRol.ClientID %>').val(rowData.ID);
            $('#<%= DDLNombreRol.ClientID %>').val(rowData.Nombre);
            $('#<%= TBRol_descripcion.ClientID %>').val(rowData.Descripcion);
        }

        function deleteRol(id) {
            $.ajax({
                type: "POST",
                url: "WFRol.aspx/DeleteRol",
                data: JSON.stringify({ id: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        $('#rolTable').DataTable().ajax.reload();
                        alert('Rol eliminado correctamente');
                    }
                }
            });
        }
    </script>
</asp:Content>