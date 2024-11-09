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
                      _estadoCivil, _lugarNacimiento, _telefono, _celular, _correo,
                      _direccion, _estado, _ocupacion, _nivelEducacion;
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
            DataSet ds = objPersona.ShowPersonas();
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
                    celular = row["celular"],
                    correo = row["correo"],
                    direccion = row["direccion"],
                    estado = row["estado"],
                    ocupacion = row["ocupacion"],
                    nivelEducacion = row["nivelEducacion"]
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
            TBId.Value = "";
            TBNombres.Text = "";
            TBApellidos.Text = "";
            TBTipoDocumento.Text = "";
            TBDocumento.Text = "";
            TBGenero.Text = "";
            TBEstadoCivil.Text = "";
            TBLugarNacimiento.Text = "";
            TBFechaNacimiento.Text = "";
            TBTelefono.Text = "";
            TBCelular.Text = "";
            TBCorreo.Text = "";
            TBDireccion.Text = "";
            TBEstado.Text = "";
            TBOcupacion.Text = "";
            TBNivelEducacion.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombres = TBNombres.Text;
            _apellidos = TBApellidos.Text;
            _tipoDocumento = TBTipoDocumento.Text;
            _documento = TBDocumento.Text;
            _genero = TBGenero.Text;
            _estadoCivil = TBEstadoCivil.Text;
            _lugarNacimiento = TBLugarNacimiento.Text;
            _fechaNacimiento = Convert.ToDateTime(TBFechaNacimiento.Text);
            _telefono = TBTelefono.Text;
            _celular = TBCelular.Text;
            _correo = TBCorreo.Text;
            _direccion = TBDireccion.Text;
            _estado = TBEstado.Text;
            _ocupacion = TBOcupacion.Text;
            _nivelEducacion = TBNivelEducacion.Text;

            executed = objPersona.SavePersona(_nombres, _apellidos, _tipoDocumento,
                _documento, _genero, _estadoCivil, _lugarNacimiento, _fechaNacimiento,
                _telefono, _celular, _correo, _direccion, _estado, _ocupacion, _nivelEducacion);

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
            if (string.IsNullOrEmpty(TBId.Value))
            {
                LblMsg.Text = "No se ha seleccionado una persona para actualizar.";
                return;
            }

            _idPersona = Convert.ToInt32(TBId.Value);
            _nombres = TBNombres.Text;
            _apellidos = TBApellidos.Text;
            _tipoDocumento = TBTipoDocumento.Text;
            _documento = TBDocumento.Text;
            _genero = TBGenero.Text;
            _estadoCivil = TBEstadoCivil.Text;
            _lugarNacimiento = TBLugarNacimiento.Text;
            _fechaNacimiento = Convert.ToDateTime(TBFechaNacimiento.Text);
            _telefono = TBTelefono.Text;
            _celular = TBCelular.Text;
            _correo = TBCorreo.Text;
            _direccion = TBDireccion.Text;
            _estado = TBEstado.Text;
            _ocupacion = TBOcupacion.Text;
            _nivelEducacion = TBNivelEducacion.Text;

            executed = objPersona.UpdatePersona(_idPersona, _nombres, _apellidos,
                _tipoDocumento, _documento, _genero, _estadoCivil, _lugarNacimiento,
                _fechaNacimiento, _telefono, _celular, _correo, _direccion, _estado,
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