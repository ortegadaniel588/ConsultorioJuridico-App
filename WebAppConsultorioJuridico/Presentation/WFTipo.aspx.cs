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
    public partial class WFTipo : System.Web.UI.Page
    {
        TipoLog objTipo = new TipoLog();
        private string nombre, descripcion;
        private int idtipo;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
            validatePermissionRol();
        }

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
                            FrmTipo.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmTipo.Visible = true;
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

                masterPage.linkUser.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermissions.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkPermissionsRoles.Visible = false;// Se oculta el enlace de Permiso Rol
                masterPage.linkConfiguration.Visible = false;
                masterPage.linkPersonas.Visible = false;
                masterPage.linkEspecialidad.Visible = false;
                masterPage.linkEmpresa.Visible = false;
                masterPage.linkAsignarRedsocial.Visible = false;
                masterPage.linkRedsocial.Visible = false;
                masterPage.linkEstado.Visible = false;
                masterPage.linkTipo.Visible = false;
                masterPage.linkSecurity.Visible = false;
                masterPage.linkRol.Visible = false;
                masterPage.linkEmpleados.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "ACTUALIZAR":
                            FrmTipo.Visible = true;
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
                masterPage.linkEmpleados.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmTipo.Visible = true;
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


        [WebMethod]
        public static object ListTipo()
        {
            TipoLog objTipo = new TipoLog();

            // Se obtiene un DataSet que contiene la lista de Tipo desde la base de datos.
            var dataSet = objTipo.showTipo();

            // Se crea una lista para almacenar los Tipos que se van a devolver.
            var TipoList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Tipo).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TipoList.Add(new
                {
                    TipoID = row["idtipo"],
                    Nombre = row["nombre"],
                    Descripcion = row["descripcion"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de Tipos.
            return new { data = TipoList };
        }
        private void clear()
        {
            TipoID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";



        }
        [WebMethod]
        public static bool deleteTipo(int idtipo)
        {
            // Crear una instancia de la clase de lógica de productos
            TipoLog objTipo = new TipoLog();

            // Invocar al método para eliminar el Tipo y devolver el resultado
            return objTipo.deleteTipo(idtipo);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objTipo.saveTipo(nombre, descripcion);
            if (execute)
            {
                LblMsg.Text = "Se guardo exitosamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un tipo para actualizar
            if (string.IsNullOrEmpty(TipoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un Tipo para actualizar.";
                return;
            }

            idtipo = Convert.ToInt32(TipoID.Value);
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objTipo.updateTipo(idtipo, nombre, descripcion);
            if (execute)
            {
                LblMsg.Text = "Se actualizo exitosamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

    }
}