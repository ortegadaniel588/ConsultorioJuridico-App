using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFInicio : System.Web.UI.Page
    {
        //Crear los objetos
        UsuariosLog objUsu = new UsuariosLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            validatePermissionRol();
        }

        // Metodo para validar permisos roles
        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            var userRole = objUser.Rol.Nombre;
            if (objUser.Permisos == null || !objUser.Permisos.Any())
            {
                LblMsg.Text = "El usuario no tiene permisos asignados.";
                return;
            }

            if (userRole == "Administrador")
            {
                LblMsg.Text = "Bienvenido, Administrador!";
                // Tiene acceso a todo, no necesita ocultar nada

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmInicio.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmInicio.Visible = true;
                            break;
                        case "MOSTRAR":
                            // Configuración para mostrar
                            break;
                        case "ELIMINAR":
                            // Configuración para eliminar
                            break;
                        default:
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Abogado")
            {
                LblMsg.Text = "Bienvenido, Abogado!";
                
                masterPage.linkUser.Visible = false;
                masterPage.linkPermissions.Visible = false;
                masterPage.linkPermissionsRoles.Visible = false;
                masterPage.linkSecurity.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "ACTUALIZAR":
                            FrmInicio.Visible = true;
                            break;
                        case "MOSTRAR":
                            // Configuración para mostrar casos
                            break;
                        default:
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Secretario")
            {
                LblMsg.Text = "Bienvenido, Secretario!";
                
                masterPage.linkPermissions.Visible = false;
                masterPage.linkPermissionsRoles.Visible = false;
                masterPage.linkSecurity.Visible = false;
                masterPage.linkCasos.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmInicio.Visible = true;
                            break;
                        case "MOSTRAR":
                            // Configuración para mostrar registros
                            break;
                        default:
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                LblMsg.Text = "Rol no reconocido.";
                Response.Redirect("WFInicio.aspx");
            }
        }

    }
}