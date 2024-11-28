<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeFile="WFUsuario.aspx.cs" Inherits="Presentation.WFUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Usuarios
        </div>
        <div class="card-body">
            <form runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
                <asp:HiddenField ID="HFUserId" runat="server" />
                
                <div class="row m-1">
                    <div class="col-4">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Correo:"></asp:Label>
                        <asp:TextBox ID="TBMail" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVMail" 
                            runat="server" 
                            ControlToValidate="TBMail" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Ingrese un correo">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Contraseña:"></asp:Label>
                        <asp:TextBox ID="TBContrasena" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPassword" 
                            runat="server" 
                            ControlToValidate="TBContrasena" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Ingrese una contraseña">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Estado:"></asp:Label>
                        <asp:DropDownList ID="DDLState" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Seleccione</asp:ListItem>
                            <asp:ListItem Value="Activo">Activo</asp:ListItem>
                            <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVState" 
                            runat="server" 
                            ControlToValidate="DDLState" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione un estado">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col-4">
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Fecha de creación:"></asp:Label>
                        <asp:TextBox ID="TBDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDate" 
                            runat="server" 
                            ControlToValidate="TBDate" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione una fecha">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Rol:"></asp:Label>
                        <asp:DropDownList ID="DDLRol" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVRol" 
                            runat="server" 
                            ControlToValidate="DDLRol" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione un rol">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Persona:"></asp:Label>
                        <asp:DropDownList ID="DDLPersona" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVPersona" 
                            runat="server" 
                            ControlToValidate="DDLPersona" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione una persona">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" 
                            OnClick="BtnSave_Click" OnClientClick="if(!confirm('¿Desea guardar este usuario?')) return false;" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" 
                            OnClick="BtnUpdate_Click" OnClientClick="if(!confirm('¿Desea actualizar este usuario?')) return false;" />
                        <asp:Label ID="LblMsg" CssClass="form-label text-success" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <div class="card-header">
            Lista de Usuarios
        </div>
        <div class="card-body">
            <table id="usersTable" class="table table-hover display" style="width: 100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Correo</th>
                        <th>Estado</th>
                        <th>Fecha</th>
                        <th>Rol</th>
                        <th>Persona</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <script src="resources/js/datatables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#usersTable').DataTable({
                "ajax": {
                    "url": "WFUsuario.aspx/ListUsers",
                    "type": "POST",
                    "contentType": "application/json",
                    "dataType": "json",
                    "dataSrc": "d.data"
                },
                "columns": [
                    { "data": "UserID" },
                    { "data": "Mail" },
                    { "data": "State" },
                    { "data": "Date" },
                    { "data": "NameRol" },
                    { "data": "NamePersona" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button type="button" class="btn btn-info btn-sm edit-btn" data-id="${row.UserID}">
                                        <i class="fas fa-edit"></i> Editar
                                    </button>`;
                        }
                    }
                ],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json"
                }
            });

            $('#usersTable').on('click', '.edit-btn', function () {
                var rowData = $('#usersTable').DataTable().row($(this).parents('tr')).data();
                loadUsersData(rowData);
            });
        });

        function loadUsersData(rowData) {
            $('#<%= HFUserId.ClientID %>').val(rowData.UserID);
            $('#<%= TBMail.ClientID %>').val(rowData.Mail);
            $('#<%= DDLState.ClientID %>').val(rowData.State);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= DDLRol.ClientID %>').val(rowData.FkRol);
            $('#<%= DDLPersona.ClientID %>').val(rowData.FkPersona);
            $('#<%= BtnSave.ClientID %>').hide();
            $('#<%= BtnUpdate.ClientID %>').show();
        }
    </script>
</asp:Content>