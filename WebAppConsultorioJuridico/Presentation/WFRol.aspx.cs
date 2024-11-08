using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFRol : System.Web.UI.Page
    {
        RolLog objRol = new RolLog();

        private int _idRol;
        private string _nombre, _descripcion;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowRoles();
            }
        }

        private void ShowRoles()
        {
            DataSet ds = objRol.showRoles();
            GVRoles.DataSource = ds;
            GVRoles.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objRol.saveRol(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El rol se guardó exitosamente!";
                ShowRoles();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idRol = Convert.ToInt32(TBId.Text);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objRol.updateRol(_idRol, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El rol se actualizó exitosamente!";
                ShowRoles();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void GVRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVRoles.Rows[index];
                TBId.Text = row.Cells[0].Text;
                TBNombre.Text = row.Cells[1].Text;
                TBDescripcion.Text = row.Cells[2].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVRoles.Rows[index];
                int idRol = Convert.ToInt32(row.Cells[0].Text);

                executed = objRol.deleteRol(idRol);

                if (executed)
                {
                    LblMsg.Text = "El rol se eliminó exitosamente!";
                    ShowRoles();
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }
    }
}