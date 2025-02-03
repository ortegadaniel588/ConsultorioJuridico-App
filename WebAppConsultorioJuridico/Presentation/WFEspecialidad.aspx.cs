using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;

namespace Presentation
{
    public partial class WFEspecialidad : System.Web.UI.Page
    {
        EspecialidadLog objEspecialidad = new EspecialidadLog();
        private int _id;
        private string _nombre, _descripcion;
        private bool executed = false;

        /*
    *  Variables de tipo pública que indiquen si el usuario tiene
    *  permiso para ver los botones editar y eliminar.
    */
        public bool _showEditButton { get; set; } = true;
        public bool _showDeleteButton { get; set; } = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializaciones si son necesarias
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
                            FrmEspecialidad.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmEspecialidad.Visible = true;
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

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "ACTUALIZAR":
                            FrmEspecialidad.Visible = true;
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
                            FrmEspecialidad.Visible = true;
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
        public static object ListEspecialidades()
        {
            EspecialidadLog objEsp = new EspecialidadLog();
            DataSet ds = objEsp.showEspecialidad();
            var especialidadesList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                especialidadesList.Add(new
                {
                    id = row["idespecialidad"],
                    nombre = row["nombre"],
                    descripcion = row["descripcion"]
                });
            }

            return new { data = especialidadesList };
        }

        [WebMethod]
        public static bool DeleteEspecialidad(int id)
        {
            EspecialidadLog objEsp = new EspecialidadLog();
            return objEsp.deleteEspecialidad(id);
        }

        private void Clear()
        {
            HFEspecialidadID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEspecialidad.saveEspecialidad(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "La especialidad se guardó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFEspecialidadID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una especialidad para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFEspecialidadID.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEspecialidad.updateEspecialidad(_id, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "La especialidad se actualizó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}