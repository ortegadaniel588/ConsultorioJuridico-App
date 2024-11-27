using Logic;
using System;
using System.Data;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;

namespace Presentation
{
    public partial class WFHorario : System.Web.UI.Page
    {
        HorarioLog objHorario = new HorarioLog();
        EmpleadoLog objEmpleado = new EmpleadoLog();
        public bool _showEditButton { get; set; } = true;
        public bool _showDeleteButton { get; set; } = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowEmpleadosDDL();
                BtnUpdate.Visible = false;
            }
        }

        private void ShowEmpleadosDDL()
        {
            DDLEmpleados.DataSource = objEmpleado.showEmpleadoDDL();
            DDLEmpleados.DataValueField = "idempleado";  
            DDLEmpleados.DataTextField = "nombre";       
            DDLEmpleados.DataBind();
            DDLEmpleados.Items.Insert(0, "Seleccione");
        }

        [WebMethod]
        public static object ListHorarios()
        {
            HorarioLog objHor = new HorarioLog();
            DataSet ds = objHor.ShowHorarios();
            List<object> horarios = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                horarios.Add(new
                {
                    id = row["idhorario"],                    // Cambiado de id_horario
                    empleadoId = row["empleado_idempleado"],  // Cambiado de fk_empleado
                    empleado = ObtenerNombreEmpleado(Convert.ToInt32(row["empleado_idempleado"])),
                    fecha = Convert.ToDateTime(row["fecha"]).ToString("yyyy-MM-dd"),
                    horaInicio = ((TimeSpan)row["horainicio"]).ToString(@"hh\:mm"), // Cambiado de hora_inicio
                    horaFin = ((TimeSpan)row["horafin"]).ToString(@"hh\:mm")        // Cambiado de hora_fin
                });
            }

            return new { data = horarios };
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int empleadoId = Convert.ToInt32(DDLEmpleados.SelectedValue);
                DateTime fecha = Convert.ToDateTime(TBFecha.Text);
                TimeSpan horaInicio = TimeSpan.Parse(TBHoraInicio.Text);
                TimeSpan horaFin = TimeSpan.Parse(TBHoraFin.Text);

                bool result = objHorario.InsertHorario(empleadoId, fecha, horaInicio, horaFin);

                if (result)
                {
                    LblMsg.Text = "Horario guardado exitosamente!";
                    Clear();
                }
                else
                {
                    LblMsg.Text = "Error al guardar el horario";
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
                int idHorario = Convert.ToInt32(HFHorarioID.Value);
                int empleadoId = Convert.ToInt32(DDLEmpleados.SelectedValue);
                DateTime fecha = Convert.ToDateTime(TBFecha.Text);
                TimeSpan horaInicio = TimeSpan.Parse(TBHoraInicio.Text);
                TimeSpan horaFin = TimeSpan.Parse(TBHoraFin.Text);

                bool result = objHorario.UpdateHorario(idHorario, empleadoId, fecha, horaInicio, horaFin);

                if (result)
                {
                    LblMsg.Text = "Horario actualizado exitosamente!";
                    Clear();
                }
                else
                {
                    LblMsg.Text = "Error al actualizar el horario";
                }
            }
            catch (Exception ex)
            {
                LblMsg.Text = "Error: " + ex.Message;
            }
        }

        [WebMethod]
        public static bool DeleteHorario(int id)
        {
            try
            {
                HorarioLog objHor = new HorarioLog();
                return objHor.DeleteHorario(id);
            }
            catch
            {
                return false;
            }
        }

        private void Clear()
        {
            HFHorarioID.Value = "";
            DDLEmpleados.SelectedIndex = 0;
            TBFecha.Text = "";
            TBHoraInicio.Text = "";
            TBHoraFin.Text = "";
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            LblMsg.Text = "";
        }

        private static string ObtenerNombreEmpleado(int empleadoId)
        {
            EmpleadoLog objEmpleado = new EmpleadoLog();
            DataSet ds = objEmpleado.showEmpleadoDDL();
            
            DataRow[] rows = ds.Tables[0].Select($"idempleado = {empleadoId}");
            return rows.Length > 0 ? rows[0]["nombre"].ToString() : "No disponible";
        }
    }
}