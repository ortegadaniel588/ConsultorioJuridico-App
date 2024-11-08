using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPersona : System.Web.UI.Page
    {
        PersonaLog objPersona = new PersonaLog();

        private int _idPersona;
        private string _nombres, _apellidos, _tipodocumento, _documento, _genero, _estadocivil, _lugarNacimiento, _telefono, _celular, _correo, _direccion, _estado, _ocupacion, _nivelEducacion;
        private DateTime _fechaNacimiento;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowPersonas();
            }
        }

        private void ShowPersonas()
        {
            DataSet ds = objPersona.ShowPersonas();
            GVPersonas.DataSource = ds;
            GVPersonas.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombres = TBNombres.Text;
            _apellidos = TBApellidos.Text;
            _tipodocumento = DDLTBTipodocumento.SelectedValue;
            _documento = TBDocumento.Text;
            _genero = DDLTBGénero.SelectedValue;
            _estadocivil = DDLTBEstadoCivil.SelectedValue;
            _lugarNacimiento = TBLugarNacimiento.Text;
            _fechaNacimiento = Convert.ToDateTime(TBFechaNacimiento.Text);
            _telefono = TBTeléfono.Text;
            _celular = TBCelular.Text;
            _correo = TBCorreo.Text;
            _direccion = TBDirección.Text;
            _estado = DDLTBEstado.SelectedValue;
            _ocupacion = TBOcupación.Text;
            _nivelEducacion = DDLTBNivelEducación.SelectedValue;

            executed = objPersona.SavePersona(_nombres, _apellidos, _tipodocumento, _documento, _genero, _estadocivil, _lugarNacimiento, _fechaNacimiento, _telefono, _celular, _correo, _direccion, _estado, _ocupacion, _nivelEducacion);

            if (executed)
            {
                LblMsg.Text = "La persona se guardó exitosamente!";
                ShowPersonas();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            _idPersona = Convert.ToInt32(TBId.Text);
            _nombres = TBNombres.Text;
            _apellidos = TBApellidos.Text;
            _tipodocumento = DDLTBTipodocumento.SelectedValue;
            _documento = TBDocumento.Text;
            _genero = DDLTBGénero.SelectedValue;
            _estadocivil = DDLTBEstadoCivil.SelectedValue;
            _lugarNacimiento = TBLugarNacimiento.Text;
            _fechaNacimiento = Convert.ToDateTime(TBFechaNacimiento.Text);
            _telefono = TBTeléfono.Text;
            _celular = TBCelular.Text;
            _correo = TBCorreo.Text;
            _direccion = TBDirección.Text;
            _estado = DDLTBEstado.SelectedValue;
            _ocupacion = TBOcupación.Text;
            _nivelEducacion = DDLTBNivelEducación.SelectedValue;

            executed = objPersona.UpdatePersona(_idPersona, _nombres, _apellidos, _tipodocumento, _documento, _genero, _estadocivil, _lugarNacimiento, _fechaNacimiento, _telefono, _celular, _correo, _direccion, _estado, _ocupacion, _nivelEducacion);

            if (executed)
            {
                LblMsg.Text = "La persona se actualizó exitosamente!";
                ShowPersonas();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void GVPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVPersonas.Rows[index];
                TBId.Text = row.Cells[0].Text;
                TBNombres.Text = row.Cells[1].Text;
                TBApellidos.Text = row.Cells[2].Text;
                DDLTBTipodocumento.SelectedValue = row.Cells[3].Text;
                TBDocumento.Text = row.Cells[4].Text;
                DDLTBGénero.SelectedValue = row.Cells[5].Text;
                DDLTBEstadoCivil.SelectedValue = row.Cells[6].Text;
                TBLugarNacimiento.Text = row.Cells[7].Text;
                TBFechaNacimiento.Text = row.Cells[8].Text;
                TBTeléfono.Text = row.Cells[9].Text;
                TBCelular.Text = row.Cells[10].Text;
                TBCorreo.Text = row.Cells[11].Text;
                TBDirección.Text = row.Cells[12].Text;
                DDLTBEstado.SelectedValue = row.Cells[13].Text;
                TBOcupación.Text = row.Cells[14].Text;
                DDLTBNivelEducación.SelectedValue = row.Cells[15].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVPersonas.Rows[index];
                int idPersona = Convert.ToInt32(row.Cells[0].Text);

                executed = objPersona.DeletePersona(idPersona);

                if (executed)
                {
                    LblMsg.Text = "La persona se eliminó exitosamente!";
                    ShowPersonas();
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }
    }
}