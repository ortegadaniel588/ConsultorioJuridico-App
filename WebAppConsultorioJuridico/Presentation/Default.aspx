<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio de sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="resources/css/StyleLogin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container d-flex justify-content-center align-items-center">
            <div class="login-card position-relative">
                <div id="loadingGif" class="loading-overlay" style="display: none;">
                    <span class="loader"></span>
                </div>
                <div class="text-center mb-4">
                    <img src="resources/images/logo.png" alt="Logo" class="img-fluid" style="max-width: 150px;" />
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
                <asp:Label ID="LblMsg" runat="server" CssClass="mt-3 d-block text-center text-danger"></asp:Label>
            </div>
        </div>
    </form>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        function showLoading() {
            document.getElementById("loadingGif").style.display = "flex";
        }
        function hideLoading() {
            document.getElementById("loadingGif").style.display = "none";
        }
    </script>
</body>
</html>