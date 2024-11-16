using Data;
using System;
using System.Data;

namespace Logic
{
    public class PersonaLog
    {
        PersonaDat objPersona = new PersonaDat();

        public DataSet showPersonasDDL()
        {
            return objPersona.showPersonasDDL();
        }

        public DataSet showPersonas()
        {
            return objPersona.showPersonas();
        }

        public bool savePersona(string nombres, string apellidos, string tipodocumento, string documento, string genero, string estadocivil, string lugarNacimiento, DateTime fechaNacimiento, string telefono, string telefono2, string correo, string direccion, string estrato, string ocupacion, string nivelEducacion)
        {
            return objPersona.savePersona(nombres, apellidos, tipodocumento, documento, genero, estadocivil, lugarNacimiento, fechaNacimiento, telefono, telefono2, correo, direccion, estrato, ocupacion, nivelEducacion);
        }

        public bool updatePersona(int idpersona, string nombres, string apellidos, string tipodocumento, string documento, string genero, string estadocivil, string lugarNacimiento, DateTime fechaNacimiento, string telefono, string telefono2, string correo, string direccion, string estrato, string ocupacion, string nivelEducacion)
        {
            return objPersona.updatePersona(idpersona, nombres, apellidos, tipodocumento, documento, genero, estadocivil, lugarNacimiento, fechaNacimiento, telefono, telefono2, correo, direccion, estrato, ocupacion, nivelEducacion);
        }

        public bool DeletePersona(int idpersona)
        {
            return objPersona.DeletePersona(idpersona);
        }
    }
}