using Logic;
using System;
using System.Data;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using System.Linq;

namespace Presentation
{
    public partial class WFCita : System.Web.UI.Page
    {
        CitaLog objCita = new CitaLog();
        HorarioLog objHorario = new HorarioLog();
        public bool _showEditButton { get; set; } = true;
        public bool _showDeleteButton { get; set; } = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowHorariosDDL();
                LoadEstadosDDL();
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
                            FrmCita.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmCita.Visible = true;
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
                            FrmCita.Visible = true;
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

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmCita.Visible = true;
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

        private void ShowHorariosDDL()
        {
            try
            {
                DDLHorarios.DataSource = objHorario.ShowHorariosDDL();
                DDLHorarios.DataValueField = "idhorario";
                DDLHorarios.DataTextField = "fecha";
                DDLHorarios.DataBind();
                DDLHorarios.Items.Insert(0, "Seleccione");
            }
            catch (Exception ex)
            {
                LblMsg.Text = "Error al cargar horarios: " + ex.Message;
            }
        }

        private void LoadEstadosDDL()
        {
            DDLEstado.Items.Clear();
            DDLEstado.Items.Add(new ListItem("Seleccione", ""));
            DDLEstado.Items.Add(new ListItem("Programada", "programada"));
            DDLEstado.Items.Add(new ListItem("Confirmada", "confirmada"));
            DDLEstado.Items.Add(new ListItem("Pendiente", "pendiente"));
            DDLEstado.Items.Add(new ListItem("Cancelada", "cancelada"));
            DDLEstado.Items.Add(new ListItem("Reprogramada", "reprogramada"));
            DDLEstado.Items.Add(new ListItem("No Asistida", "no asistida"));
            DDLEstado.Items.Add(new ListItem("En Curso", "en curso"));
            DDLEstado.Items.Add(new ListItem("Completada", "completada"));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int horarioId = Convert.ToInt32(DDLHorarios.SelectedValue);
                string asunto = TBAsunto.Text;
                string estado = DDLEstado.SelectedValue;

                bool result = objCita.InsertCita(horarioId, asunto, estado);

                if (result)
                {
                    LblMsg.Text = "Cita guardada exitosamente!";
                    Clear();
                }
                else
                {
                    LblMsg.Text = "Error al guardar la cita";
                }
            }
            catch (Exception ex)
            {
                LblMsg.Text = "Error: " + ex.Message;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int idCita = Convert.ToInt32(TBId.Value);
                int horarioId = Convert.ToInt32(DDLHorarios.SelectedValue);
                string asunto = TBAsunto.Text;
                string estado = DDLEstado.SelectedValue;

                bool result = objCita.UpdateCita(idCita, horarioId, asunto, estado);

                if (result)
                {
                    LblMsg.Text = "Cita actualizada exitosamente!";
                    Clear();
                }
                else
                {
                    LblMsg.Text = "Error al actualizar la cita";
                }
            }
            catch (Exception ex)
            {
                LblMsg.Text = "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static object ListCitas()
        {
            CitaLog objCita = new CitaLog();
            DataSet ds = objCita.ShowCitas();
            List<object> citas = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    // Conversiones seguras de tipos
                    DateTime fecha = Convert.ToDateTime(row["fecha"]);
                    TimeSpan horaInicio = (TimeSpan)row["horainicio"];
                    TimeSpan horaFin = (TimeSpan)row["horafin"];

                    // Crear objeto fecha completa para formateo
                    DateTime fechaInicio = fecha.Date.Add(horaInicio);
                    DateTime fechaFin = fecha.Date.Add(horaFin);

                    citas.Add(new
                    {
                        id = row["idcita"],
                        horarioId = row["horario_idhorario"],
                        horario = string.Format("{0:dd/MM/yyyy} {1:hh:mm tt} - {2:hh:mm tt}",
                            fecha,
                            fechaInicio,
                            fechaFin),
                        asunto = row["asunto"]?.ToString() ?? "",
                        estado = row["estado"]?.ToString() ?? ""
                    });
                }
                catch (Exception ex)
                {
                    // Log del error si es necesario
                    System.Diagnostics.Debug.WriteLine($"Error al procesar fila: {ex.Message}");
                    continue;
                }
            }

            return new { data = citas };
        }
        [WebMethod]
        public static bool DeleteCita(int id)
        {
            try
            {
                CitaLog objCita = new CitaLog();
                return objCita.DeleteCita(id);
            }
            catch
            {
                return false;
            }
        }

        private void Clear()
        {
            TBId.Value = "";
            DDLHorarios.SelectedIndex = 0;
            TBAsunto.Text = "";
            DDLEstado.SelectedIndex = 0;
            LblMsg.Text = "";
        }
    }
}