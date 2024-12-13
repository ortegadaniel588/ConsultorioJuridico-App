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
        EmpleadoLog objEmp = new EmpleadoLog();
        CasoHasPersonaLog objCp = new CasoHasPersonaLog();
        CasoLog objCas = new CasoLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showCountUsers();
                showCountEmpleados();   
                showCountClientes();
                showCountCasos();
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListCountCasosEstados()
        {
            CasoLog objCas = new CasoLog();
            // Se obtiene un DataSet que contiene la lista de casos que existen por estado
            var dataSet = objCas.showCountCasosEstados();
            // Se crea una lista para almacenar las cantidades que de productos x categorias 
            var casosEstadoList = new List<object>();
            // Se itera sobre cada fila del DataSet.
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                casosEstadoList.Add(new
                {
                    EstadoName = row["Nombre"],
                    TotalCasos = row["TotalCasos"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos x categorias.
            return new { data = casosEstadoList };
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
        private void showCountUsers()
        {
            int count = objUsu.showCountUsers();
            LblCantUsu.Text = count.ToString();
        }

        private void showCountEmpleados()
        {
            int count = objEmp.showCountEmpleados();
            LblCantEmp.Text = count.ToString();
        }
        private void showCountClientes()
        {
            int count = objCp.showCountClientes();
            LblCantClient.Text = count.ToString();
        }

        private void showCountCasos()
        {
            int count = objCas.showCountCasos();
            LblCantCasos.Text = count.ToString();
        }

    }
}