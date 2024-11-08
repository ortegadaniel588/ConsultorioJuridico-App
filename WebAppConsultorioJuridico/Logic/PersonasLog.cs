using Data;
using System;
using System.Data;

namespace Logic
{
    public class PersonaLog
    {
        PersonaDat objPersona = new PersonaDat();

        public DataSet ShowPersonasDDL()
        {
            return objPersona.showPersonasDDL();
        }

        public DataSet ShowPersonas()
        {
            return objPersona.ShowPersonas();
        }

        public bool SavePersona(string nombres, string apellidos, string tipodocumento, string documento, string genero, string estadocivil, string lugarNacimiento, DateTime fechaNacimiento, string telefono, string celular, string correo, string direccion, string estado, string ocupacion, string nivelEducacion)
        {
            return objPersona.SavePersona(nombres, apellidos, tipodocumento, documento, genero, estadocivil, lugarNacimiento, fechaNacimiento, telefono, celular, correo, direccion, estado, ocupacion, nivelEducacion);
        }

        public bool UpdatePersona(int idpersona, string nombres, string apellidos, string tipodocumento, string documento, string genero, string estadocivil, string lugarNacimiento, DateTime fechaNacimiento, string telefono, string celular, string correo, string direccion, string estado, string ocupacion, string nivelEducacion)
        {
            return objPersona.UpdatePersona(idpersona, nombres, apellidos, tipodocumento, documento, genero, estadocivil, lugarNacimiento, fechaNacimiento, telefono, celular, correo, direccion, estado, ocupacion, nivelEducacion);
        }

        public bool DeletePersona(int idpersona)
        {
            return objPersona.DeletePersona(idpersona);
        }
    }
}