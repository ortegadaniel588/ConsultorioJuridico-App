using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFEstado : System.Web.UI.Page
    {
        //Crear los objetos
        EstadoLog objEst = new EstadoLog();
        
        private int _idestado;
        private string _nombre, _descripcion;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showEstado();
            }
        }

        private void showEstado()
        {
            DataSet ds = objEst.showEstado();
            GVEstado.DataSource = ds;
            GVEstado.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEst.saveEstado(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El Estado se guardó exitosamente!";
                showEstado();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idestado = Convert.ToInt32(TBId.Text);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEst.updateEstado(_idestado, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El Estado se actualizó exitosamente!";
                showEstado();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void GVEstado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVEstado.Rows[index];
                TBId.Text = row.Cells[0].Text;
                TBNombre.Text = row.Cells[1].Text;
                TBDescripcion.Text = row.Cells[2].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVEstado.Rows[index];
                int idestado = Convert.ToInt32(row.Cells[0].Text);

                executed = objEst.deleteEstado(idestado);

                if (executed)
                {
                    LblMsg.Text = "El Estado se eliminó exitosamente!";
                    showEstado();
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }
    }
}