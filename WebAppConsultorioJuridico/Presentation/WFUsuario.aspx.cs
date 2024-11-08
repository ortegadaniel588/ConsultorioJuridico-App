using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFUsuario : System.Web.UI.Page
    {
        UsuarioLog objUsuario = new UsuarioLog();
        RolLog objRol = new RolLog(); // Asumiendo que tienes una clase para roles
        PersonaLog objPersona = new PersonaLog(); // Asumiendo que tienes una clase para personas

        private int _idUsuario, _rolId, _personaId;
        private string _usuario, _contrasena, _estado;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowUsuarios();
                ShowRolesDDL();
                ShowPersonasDDL();
            }
        }

        private void ShowUsuarios()
        {
            DataSet ds = objUsuario.ShowUsuarios();
            GVUsuarios.DataSource = ds;
            GVUsuarios.DataBind();
        }

        private void ShowRolesDDL()
        {
            DDLRol.DataSource = objRol.showRolesDDL();
            DDLRol.DataValueField = "idrol";
            DDLRol.DataTextField = "nombre";
            DDLRol.DataBind();
            DDLRol.Items.Insert(0, "Seleccione");
        }

        private void ShowPersonasDDL()
        {
            DDLPersona.DataSource = objPersona.ShowPersonasDDL();
            DDLPersona.DataValueField = "idpersona";
            DDLPersona.DataTextField = "nombres";
            DDLPersona.DataBind();
            DDLPersona.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _usuario = TBUsuario.Text;
            _contrasena = TBContrasena.Text;
            _rolId = Convert.ToInt32(DDLRol.SelectedValue);
            _personaId = Convert.ToInt32(DDLPersona.SelectedValue);
            _estado = DDLEstado.SelectedValue;

            executed = objUsuario.SaveUsuario(_usuario, _contrasena, _rolId, _personaId, _estado);

            if (executed)
            {
                LblMsg.Text = "El usuario se guardó exitosamente!";
                ShowUsuarios();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idUsuario = Convert.ToInt32(TBId.Text);
            _usuario = TBUsuario.Text;
            _contrasena = TBContrasena.Text;
            _rolId = Convert.ToInt32(DDLRol.SelectedValue);
            _personaId = Convert.ToInt32(DDLPersona.SelectedValue);
            _estado = DDLEstado.SelectedValue;

            executed = objUsuario.UpdateUsuario(_idUsuario, _usuario, _contrasena, _rolId, _personaId, _estado);

            if (executed)
            {
                LblMsg.Text = "El usuario se actualizó exitosamente!";
                ShowUsuarios();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void GVUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVUsuarios.Rows[index];
                TBId.Text = row.Cells[0].Text;
                TBUsuario.Text = row.Cells[1].Text;
                DDLRol.SelectedValue = row.Cells[2].Text;
                DDLPersona.SelectedValue = row.Cells[3].Text;
                DDLEstado.SelectedValue = row.Cells[4].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVUsuarios.Rows[index];
                int idUsuario = Convert.ToInt32(row.Cells[0].Text);

                executed = objUsuario.DeleteUsuario(idUsuario);

                if (executed)
                {
                    LblMsg.Text = "El usuario se eliminó exitosamente!";
                    ShowUsuarios();
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }
    }
}