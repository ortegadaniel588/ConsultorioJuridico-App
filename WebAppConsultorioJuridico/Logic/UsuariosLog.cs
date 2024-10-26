using Data;
using System;
using System.Data;

namespace Logic
{
    public class UsuarioLog
    {
        UsuarioDat objUsuario = new UsuarioDat();

        public DataSet ShowUsuariosDDL()
        {
            return objUsuario.showUsuariosDDL();
        }

        public DataSet ShowUsuarios()
        {
            return objUsuario.ShowUsuarios();
        }

        public bool SaveUsuario(string usuario, string contrasena, int rolId, int personaId, string estado)
        {
            return objUsuario.SaveUsuario(usuario, contrasena, rolId, personaId, estado);
        }

        public bool UpdateUsuario(int id, string usuario, string contrasena, int rolId, int personaId, string estado)
        {
            return objUsuario.UpdateUsuario(id, usuario, contrasena, rolId, personaId, estado);
        }

        public bool DeleteUsuario(int id)
        {
            return objUsuario.DeleteUsuario(id);
        }
    }
}