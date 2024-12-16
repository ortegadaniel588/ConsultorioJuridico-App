<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Inicio de sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="resources/css/StyleLogin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container d-flex justify-content-center align-items-center">
            <div class="login-card">
                <div id="loadingGif" class="loading-overlay text-center" style="display: none;">
                    <img src="resources/images/loading-7528_128.gif" alt="Cargando..." />
                </div>
                <h2 class="login-title text-center">Bienvenido</h2>
                <div class="mb-4">
                    <asp:Label ID="Label1" CssClass="form-label" runat="server">Correo electrónico</asp:Label>
                    <asp:TextBox ID="TBUsuario" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                </div>
                <div class="mb-4">
                    <asp:Label ID="Label3" CssClass="form-label" runat="server">Contraseña</asp:Label>
                    <asp:TextBox ID="TBContrasena" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="mb-4">
                    <a href="#" class="forgot-password">¿Olvidaste tu contraseña?</a>
                </div>
                <asp:Button ID="BtnIniciar" CssClass="btn btn-login btn-lg w-100 text-white" runat="server" Text="Iniciar sesión" OnClick="BtnIniciar_Click" OnClientClick="showLoading();" />
                <asp:Label ID="LblMsg" runat="server" CssClass="mt-3 d-block text-center"></asp:Label>
            </div>
        </div>
    </form>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        function showLoading() {
            document.getElementById("loadingGif").style.display = "block";
        }
        function hideLoading() {
            document.getElementById("loadingGif").style.display = "none";
        }
    </script>
</body>
</html>