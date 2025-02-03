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
    public partial class WFEmpleado : System.Web.UI.Page
    {
        EmpleadoLog empleadoLog = new EmpleadoLog();
        EspecialidadLog especialidadLog = new EspecialidadLog();
        UsuariosLog usuariosLog = new UsuariosLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showUsuariosDDL();
                showEspecialidadesDDL();
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
                            FrmEmpleado.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmEmpleado.Visible = true;
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
                masterPage.linkEmpleados.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "ACTUALIZAR":
                            FrmEmpleado.Visible = true;
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
                            FrmEmpleado.Visible = true;
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

        // Método para mostrar los usuarios en el DDL
        private void showUsuariosDDL()
        {
            DDLUsuarios.DataSource = usuariosLog.showUsers().Tables[0];
            DDLUsuarios.DataValueField = "UserID"; // Nombre de la llave primaria
            DDLUsuarios.DataTextField = "Mail";
            DDLUsuarios.DataBind();
            DDLUsuarios.Items.Insert(0, "Seleccione");
        }

        // Método para mostrar las especialidades en el DDL
        private void showEspecialidadesDDL()
        {
            DDLEspecialidades.DataSource = especialidadLog.showEspecialidad().Tables[0];
            DDLEspecialidades.DataValueField = "EspecialidadID"; // Nombre de la llave primaria
            DDLEspecialidades.DataTextField = "Nombre";
            DDLEspecialidades.DataBind();
            DDLEspecialidades.Items.Insert(0, "Seleccione");
        }

        [WebMethod]
        public static object ListEmpleados()
        {
            EmpleadoLog empleadoLog = new EmpleadoLog();
            DataSet ds = empleadoLog.showEmpleado();
            var empleadosList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                empleadosList.Add(new
                {
                    idempleado = row["idempleado"],
                    usuario = row["usuario"],
                    especialidad = row["especialidad"],
                    usuarioId = row["usuarioId"],
                    especialidadId = row["especialidadId"]
                });
            }

            return new { data = empleadosList };
        }

        [WebMethod]
        public static bool DeleteEmpleado(int id)
        {
            EmpleadoLog empleadoLog = new EmpleadoLog();
            return empleadoLog.deleteEmpleado(id);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int usuarioId = int.Parse(DDLUsuarios.SelectedValue);
            int especialidadId = int.Parse(DDLEspecialidades.SelectedValue);

            bool result = empleadoLog.saveEmpleado(usuarioId, especialidadId);
            LblMsg.Text = result ? "Empleado guardado exitosamente." : "Error al guardar el empleado.";
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFEmpleadoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un empleado para actualizar.";
                return;
            }

            int empleadoId = int.Parse(HFEmpleadoID.Value);
            int usuarioId = int.Parse(DDLUsuarios.SelectedValue);
            int especialidadId = int.Parse(DDLEspecialidades.SelectedValue);

            bool result = empleadoLog.updateEmpleado(empleadoId, usuarioId, especialidadId);
            LblMsg.Text = result ? "Empleado actualizado exitosamente." : "Error al actualizar el empleado.";
        }
    }
}