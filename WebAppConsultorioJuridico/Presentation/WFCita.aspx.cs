using Logic;
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
    public partial class WFCita : System.Web.UI.Page
    {
        CitaLog objCita = new CitaLog();

        private int _idCita;
        private int _horarioId;
        private string _asunto, _estado;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aquí se pueden invocar métodos si es necesario
            }
        }

        [WebMethod]
        public static object ListCitas()
        {
            CitaLog objCita = new CitaLog();
            DataSet ds = objCita.ShowCitas();
            var citasList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                citasList.Add(new
                {
                    idcita = row["idcita"],
                    horarioid = row["horarioid"],
                    asunto = row["asunto"],
                    estado = row["estado"]
                });
            }

            return new { data = citasList };
        }

        [WebMethod]
        public static bool DeleteCita(int id)
        {
            CitaLog objCita = new CitaLog();
            return objCita.DeleteCita(id);
        }

        // Método para limpiar los TextBox
        private void Clear()
        {
            TBId.Value = "";
            TBHorarioId.Text = "";
            TBAsunto.Text = "";
            TBEstado.Text = "";
        }

        // Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _horarioId = Convert.ToInt32(TBHorarioId.Text);
            _asunto = TBAsunto.Text;
            _estado = TBEstado.Text;

            executed = objCita.InsertCita(_horarioId, _asunto, _estado);

            if (executed)
            {
                LblMsg.Text = "La cita se guardó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idCita = Convert.ToInt32(TBId.Value);
            _horarioId = Convert.ToInt32(TBHorarioId.Text);
            _asunto = TBAsunto.Text;
            _estado = TBEstado.Text;

            executed = objCita.UpdateCita(_idCita, _horarioId, _asunto, _estado);

            if (executed)
            {
                LblMsg.Text = "La cita se actualizó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}