using Logic;
using Model;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class Default : System.Web.UI.Page
    {
        UsuariosLog objUserLog = new UsuariosLog();
        User objUser = new User();

        private string _usuario;
        private string _contrasena;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnIniciar_Click(object sender, EventArgs e)
        {
            // Muestra la imagen de cargando antes de procesar la encriptación
            ScriptManager.RegisterStartupScript(this, GetType(), "showLoading", "showLoading();", true);

            _usuario = TBUsuario.Text;
            _contrasena = TBContrasena.Text;

            if (string.IsNullOrEmpty(_usuario) || string.IsNullOrEmpty(_contrasena))
            {
                LblMsg.Text = "Por favor, ingrese su correo y contraseña.";
                ScriptManager.RegisterStartupScript(this, GetType(), "hideLoading", "hideLoading();", true);
                return;
            }

            ICryptoService cryptoService = new PBKDF2();
            objUser = objUserLog.showUsersMail(_usuario);// Busca el correo del usuario

            if (objUser != null)
            {
                if (objUser.State == "Activo")// Verifica si el usuario está activo
                {
                    // Almacena objUser en la sesión
                    Session["User"] = objUser;

                    string passEncryp = cryptoService.Compute(_contrasena, objUser.Salt);
                    if (cryptoService.Compare(objUser.Contrasena, passEncryp))
                    {
                        Response.Redirect("WFInicio.aspx");
                        TBUsuario.Text = "";
                        TBContrasena.Text = "";
                    }
                    else
                    {
                        LblMsg.Text = "Correo o Contraseña Incorrectos!";
                    }
                }
                else
                {
                    LblMsg.Text = "El usuario no está activo. Contacte al administrador.";
                }

            }
            else
            {
                LblMsg.Text = "Correo o Contraseña Incorrectos!";
            }
            // Oculta la imagen de cargando al terminar el procesamiento
            ScriptManager.RegisterStartupScript(this, GetType(), "hideLoading", "hideLoading();", true);
        }
    }
}