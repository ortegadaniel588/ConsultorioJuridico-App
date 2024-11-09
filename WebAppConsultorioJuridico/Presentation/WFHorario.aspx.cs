using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFHorario : System.Web.UI.Page
    {
        HorarioLog objHorario = new HorarioLog();
        EmpleadoLog objEmpleado = new EmpleadoLog();

        private int _idHorario, _fkEmpleado;
        private DateTime _fecha;
        private TimeSpan _horaInicio, _horaFin;
        private string _estado;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowHorarios();
                ShowEmpleadosDDL();
                ShowEstadosDDL();
            }
        }

        private void ShowHorarios()
        {
            DataSet ds = objHorario.ShowHorarios();
            GVHorarios.DataSource = ds;
            GVHorarios.DataBind();
        }

        private void ShowEmpleadosDDL()
        {
            DDLEmpleados.DataSource = objEmpleado.showEmpleadoDDL();
            DDLEmpleados.DataValueField = "emp_id";
            DDLEmpleados.DataTextField = "emp_nombre";
            DDLEmpleados.DataBind();
            DDLEmpleados.Items.Insert(0, "Seleccione");
        }

        private void ShowEstadosDDL()
        {
            DDLEstado.Items.Add(new ListItem("Activo", "Activo"));
            DDLEstado.Items.Add(new ListItem("Inactivo", "Inactivo"));
            DDLEstado.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkEmpleado = Convert.ToInt32(DDLEmpleados.SelectedValue);
            _fecha = Convert.ToDateTime(TBFecha.Text);
            _horaInicio = TimeSpan.Parse(TBHoraInicio.Text);
            _horaFin = TimeSpan.Parse(TBHoraFin.Text);
            _estado = DDLEstado.SelectedValue;

            executed = objHorario.InsertHorario(_fkEmpleado, _fecha, _horaInicio, _horaFin);

            if (executed)
            {
                LblMsg.Text = "El horario se guardó exitosamente!";
                ShowHorarios();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idHorario = Convert.ToInt32(TBId.Text);
            _fkEmpleado = Convert.ToInt32(DDLEmpleados.SelectedValue);
            _fecha = Convert.ToDateTime(TBFecha.Text);
            _horaInicio = TimeSpan.Parse(TBHoraInicio.Text);
            _horaFin = TimeSpan.Parse(TBHoraFin.Text);
            _estado = DDLEstado.SelectedValue;

            executed = objHorario.UpdateHorario(_idHorario, _fkEmpleado, _fecha, _horaInicio, _horaFin);

            if (executed)
            {
                LblMsg.Text = "El horario se actualizó exitosamente!";
                ShowHorarios();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}