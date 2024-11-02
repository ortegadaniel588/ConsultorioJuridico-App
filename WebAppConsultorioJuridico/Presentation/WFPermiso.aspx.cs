using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPermiso : System.Web.UI.Page
    {
        PermisoLog objPermiso = new PermisoLog();

        private int _idPermiso;
        private string _nombre, _descripcion;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowPermisos();
            }
        }

        private void ShowPermisos()
        {
            DataSet ds = objPermiso.ShowPermisos();
            GVPermisos.DataSource = ds;
            GVPermisos.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objPermiso.SavePermiso(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El permiso se guardó exitosamente!";
                ShowPermisos();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idPermiso = Convert.ToInt32(TBId.Text);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objPermiso.UpdatePermiso(_idPermiso, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El permiso se actualizó exitosamente!";
                ShowPermisos();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void GVPermisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVPermisos.Rows[index];
                TBId.Text = row.Cells[0].Text;
                TBNombre.Text = row.Cells[1].Text;
                TBDescripcion.Text = row.Cells[2].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVPermisos.Rows[index];
                int idPermiso = Convert.ToInt32(row.Cells[0].Text);

                executed = objPermiso.DeletePermiso(idPermiso);

                if (executed)
                {
                    LblMsg.Text = "El permiso se eliminó exitosamente!";
                    ShowPermisos();
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }
    }
}