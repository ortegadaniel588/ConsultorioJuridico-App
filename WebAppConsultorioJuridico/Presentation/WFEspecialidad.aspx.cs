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
    public partial class WFEspecialidad : System.Web.UI.Page
    {
        EspecialidadLog objEsp = new EspecialidadLog();

        private int _idEspecialidad;
        private string _nombre, _descripcion;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aquí se pueden invocar métodos si es necesario
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
                    idespecialidad = row["idespecialidad"],
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

        // Método para limpiar los TextBox
        private void Clear()
        {
            TBId.Text = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        // Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEsp.saveEspecialidad(_nombre, _descripcion);

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
            _idEspecialidad = Convert.ToInt32(TBId.Text);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEsp.updateEspecialidad(_idEspecialidad, _nombre, _descripcion);

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

        protected void GVEspecialidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVEspecialidades.Rows[index];
                TBId.Text = row.Cells[0].Text;
                TBNombre.Text = row.Cells[1].Text;
                TBDescripcion.Text = row.Cells[2].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVEspecialidades.Rows[index];
                int idEspecialidad = Convert.ToInt32(row.Cells[0].Text);

                executed = objEsp.deleteEspecialidad(idEspecialidad);

                if (executed)
                {
                    LblMsg.Text = "La especialidad se eliminó exitosamente!";
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }
    }
}