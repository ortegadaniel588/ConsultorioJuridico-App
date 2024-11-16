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
    public partial class WFPersona : System.Web.UI.Page
    {
        PersonaLog objPersona = new PersonaLog();

        private int _idPersona;
        private string _nombres, _apellidos, _tipoDocumento, _documento, _genero,
                      _estadoCivil, _lugarNacimiento, _telefono1, _telefono2, _correo,
                      _direccion, _estrato, _ocupacion, _nivelEducacion;
        private DateTime _fechaNacimiento;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aquí se pueden invocar métodos si es necesario
            }
        }

        [WebMethod]
        public static object ListPersonas()
        {
            PersonaLog objPersona = new PersonaLog();
            DataSet ds = objPersona.showPersonas();
            var personasList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                personasList.Add(new
                {
                    idpersona = row["idpersona"],
                    nombres = row["nombres"],
                    apellidos = row["apellidos"],
                    tipodocumento = row["tipodocumento"],
                    documento = row["documento"],
                    genero = row["genero"],
                    estadocivil = row["estadocivil"],
                    lugarNacimiento = row["lugarNacimiento"],
                    fechaNacimiento = row["fechaNacimiento"],
                    telefono = row["telefono"],
                    telefono2 = row["telefono2"],
                    correo = row["correo"],
                    direccion = row["direccion"],
                    estrato = row["estrato"],
                    ocupacion = row["ocupacion"],
                    nivelescolaridad = row["nivelescolaridad"]
                });
            }

            return new { data = personasList };
        }

        [WebMethod]
        public static bool DeletePersona(int id)
        {
            PersonaLog objPer = new PersonaLog();
            return objPer.DeletePersona(id);
        }

        // Método para limpiar los TextBox
        private void Clear()
        {
            HFPersonaID.Value = "";
            TBNames.Text = "";
            TBLastNames.Text = "";
            DDLDocumentTypes.SelectedIndex = 0;
            TBDocument.Text = "";
            DDLGender.SelectedIndex = 0;
            DDLMaritalStatus.SelectedIndex = 0;
            TBBirthPlace.Text = "";
            TBBirthDate.Text = "";
            TBPhone1.Text = "";
            TBPhone2.Text = "";
            TBEmail.Text = "";
            TBAddress.Text = "";
            DDLSocioeconomicStatus.SelectedIndex = 0;
            TBOccupation.Text = "";
            DDLEducationLevel.SelectedIndex = 0;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombres = TBNames.Text;
            _apellidos = TBLastNames.Text;
            _tipoDocumento = DDLDocumentTypes.SelectedValue;
            _documento = TBDocument.Text;
            _genero = DDLGender.SelectedValue;
            _estadoCivil = DDLMaritalStatus.SelectedValue;
            _lugarNacimiento = TBBirthPlace.Text;
            _fechaNacimiento = Convert.ToDateTime(TBBirthDate.Text);
            _telefono1 = TBPhone1.Text;
            _telefono2 = TBPhone2.Text;
            _correo = TBEmail.Text;
            _direccion = TBAddress.Text;
            _estrato = DDLSocioeconomicStatus.SelectedValue;
            _ocupacion = TBOccupation.Text;
            _nivelEducacion = DDLEducationLevel.SelectedValue;

            executed = objPersona.savePersona(_nombres, _apellidos, _tipoDocumento,
                _documento, _genero, _estadoCivil, _lugarNacimiento, _fechaNacimiento,
                _telefono1, _telefono2, _correo, _direccion, _estrato, _ocupacion, _nivelEducacion);

            if (executed)
            {
                LblMsg.Text = "La persona se guardó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFPersonaID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una persona para actualizar.";
                return;
            }

            _idPersona = Convert.ToInt32(HFPersonaID.Value);
            _nombres = TBNames.Text;
            _apellidos = TBLastNames.Text;
            _tipoDocumento = DDLDocumentTypes.SelectedValue;
            _documento = TBDocument.Text;
            _genero = DDLGender.SelectedValue;
            _estadoCivil = DDLMaritalStatus.SelectedValue;
            _lugarNacimiento = TBBirthPlace.Text;
            _fechaNacimiento = Convert.ToDateTime(TBBirthDate.Text);
            _telefono1 = TBPhone1.Text;
            _telefono2 = TBPhone2.Text;
            _correo = TBEmail.Text;
            _direccion = TBAddress.Text;
            _estrato = DDLSocioeconomicStatus.SelectedValue;
            _ocupacion = TBOccupation.Text;
            _nivelEducacion = DDLEducationLevel.SelectedValue;

            executed = objPersona.updatePersona(_idPersona, _nombres, _apellidos,
                _tipoDocumento, _documento, _genero, _estadoCivil, _lugarNacimiento,
                _fechaNacimiento, _telefono1, _telefono2, _correo, _direccion, _estrato,
                _ocupacion, _nivelEducacion);

            if (executed)
            {
                LblMsg.Text = "La persona se actualizó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}