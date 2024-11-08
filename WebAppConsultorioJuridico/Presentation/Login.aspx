<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentation.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--Inputs de Inicio de sesión--%>
            <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
            <asp:TextBox ID="TxUsuario" runat="server"></asp:TextBox>

            <asp:Label ID="Label2" runat="server" Text="Contraseña"></asp:Label>
            <asp:TextBox ID="TxContrasena" runat="server"></asp:TextBox>

            <asp:Button ID="BtnIniciar" runat="server" Text="Iniciar" />
            <asp:Label ID="LblMensaje" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
