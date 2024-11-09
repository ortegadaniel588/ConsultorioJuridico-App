<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsuario.aspx.cs" Inherits="Presentation.WFUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="TBId" runat="server" />
    
    <div>
        <table>
            <tr>
                <td>Usuario:</td>
                <td>
                    <asp:TextBox ID="TBUsuario" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Contraseña:</td>
                <td>
                    <asp:TextBox ID="TBContrasena" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Rol ID:</td>
                <td>
                    <asp:TextBox ID="TBRolId" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Persona ID:</td>
                <td>
                    <asp:TextBox ID="TBPersonaId" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Estado:</td>
                <td>
                    <asp:TextBox ID="TBEstado" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server"></asp:Label>
    </div>

    <div>
        <table id="DTUsuarios">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Usuario</th>
                    <th>Contraseña</th>
                    <th>Rol ID</th>
                    <th>Persona ID</th>
                    <th>Estado</th>
                    <th>Acciones</th>
                </tr>
            </thead>
        </table>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#DTUsuarios').DataTable({
                "ajax": {
                    "url": "WFUsuario.aspx/ListUsuarios",
                    "type": "POST",
                    "datatype": "json",
                    "contentType": "application/json; charset=utf-8"
                },
                "columns": [
                    { "data": "idusuario" },
                    { "data": "usuario" },
                    { "data": "contrasena" },
                    { "data": "rolId" },
                    { "data": "personaId" },
                    { "data": "estado" },
                    {
                        "data": "idusuario",
                        "render": function (data) {
                            return "<button type='button' onclick='editUsuario(" + data + ")'>Editar</button> " +
                                "<button type='button' onclick='deleteUsuario(" + data + ")'>Eliminar</button>";
                        }
                    }
                ]
            });
        });

        function editUsuario(id) {
            var table = $('#DTUsuarios').DataTable();
            var data = table.rows().data();
            var usuarioData = data.filter(function (item) { return item.idusuario == id; })[0];

            $('#<%= TBId.ClientID %>').val(usuarioData.idusuario);
            $('#<%= TBUsuario.ClientID %>').val(usuarioData.usuario);
            $('#<%= TBContrasena.ClientID %>').val(usuarioData.contrasena);
            $('#<%= TBRolId.ClientID %>').val(usuarioData.rolId);
            $('#<%= TBPersonaId.ClientID %>').val(usuarioData.personaId);
            $('#<%= TBEstado.ClientID %>').val(usuarioData.estado);
        }

        function deleteUsuario(id) {
            if (confirm('¿Está seguro de eliminar este usuario?')) {
                $.ajax({
                    type: "POST",
                    url: "WFUsuario.aspx/DeleteUsuario",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            alert('Usuario eliminado exitosamente');
                            $('#DTUsuarios').DataTable().ajax.reload();
                        } else {
                            alert('Error al eliminar el usuario');
                        }
                    },
                    error: function (error) {
                        alert('Error al procesar la solicitud');
                    }
                });
            }
        }
    </script>
</asp:Content>