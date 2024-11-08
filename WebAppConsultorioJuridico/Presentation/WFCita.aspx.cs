using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCita : System.Web.UI.Page
    {
        CitaLog objCita = new CitaLog();
        HorarioLog objHorario = new HorarioLog(); // Asumiendo que tienes una clase para horarios

        private int _idCita, _horarioId;
        private string _asunto, _estado;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowCitas();
                ShowHorariosDDL();
            }
        }

        private void ShowCitas()
        {
            DataSet ds = objCita.ShowCitas();
            GVCitas.DataSource = ds;
            GVCitas.DataBind();
        }

        private void ShowHorariosDDL()
        {
            DDLHorarios.DataSource = objHorario.ShowHorariosDDL();
            DDLHorarios.DataValueField = "idhorario";
            DDLHorarios.DataTextField = "descripcion"; // Ajusta esto según el campo que quieras mostrar
            DDLHorarios.DataBind();
            DDLHorarios.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _horarioId = Convert.ToInt32(DDLHorarios.SelectedValue);
            _asunto = TBAsunto.Text;
            _estado = DDLEstado.SelectedValue;

            executed = objCita.InsertCita(_horarioId, _asunto, _estado);

            if (executed)
            {
                LblMsg.Text = "La cita se guardó exitosamente!";
                ShowCitas();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idCita = Convert.ToInt32(TBId.Text);
            _horarioId = Convert.ToInt32(DDLHorarios.SelectedValue);
            _asunto = TBAsunto.Text;
            _estado = DDLEstado.SelectedValue;

            executed = objCita.UpdateCita(_idCita, _horarioId, _asunto, _estado);

            if (executed)
            {
                LblMsg.Text = "La cita se actualizó exitosamente!";
                ShowCitas();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void GVCitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVCitas.Rows[index];
                TBId.Text = row.Cells[0].Text;
                DDLHorarios.SelectedValue = row.Cells[1].Text;
                TBAsunto.Text = row.Cells[2].Text;
                DDLEstado.SelectedValue = row.Cells[3].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVCitas.Rows[index];
                int idCita = Convert.ToInt32(row.Cells[0].Text);

                executed = objCita.DeleteCita(idCita);

                if (executed)
                {
                    LblMsg.Text = "La cita se eliminó exitosamente!";
                    ShowCitas();
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }
    }
}