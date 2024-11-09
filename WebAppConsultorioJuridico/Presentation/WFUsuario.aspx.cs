using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFUsuario : System.Web.UI.Page
    {
        UsuarioLog objUsuario = new UsuarioLog();

        private int _idUsuario;
        private string _usuario, _contrasena, _estado;
        private int _rolId, _personaId;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aquí se pueden invocar métodos si es necesario
            }
        }

        [WebMethod]
        public static object ListUsuarios()
        {
            UsuarioLog objUsuario = new UsuarioLog();
            DataSet ds = objUsuario.ShowUsuarios();
            var usuariosList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                usuariosList.Add(new
                {
                    idusuario = row["idusuario"],
                    usuario = row["usuario"],
                    contrasena = row["contrasena"],
                    rolId = row["rolId"],
                    personaId = row["personaId"],
                    estado = row["estado"]
                });
            }

            return new { data = usuariosList };
        }

        [WebMethod]
        public static bool DeleteUsuario(int id)
        {
            UsuarioLog objUsu = new UsuarioLog();
            return objUsu.DeleteUsuario(id);
        }

        private void Clear()
        {
            TBId.Value = "";
            TBUsuario.Text = "";
            TBContrasena.Text = "";
            TBRolId.Text = "";
            TBPersonaId.Text = "";
            TBEstado.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _usuario = TBUsuario.Text;
            _contrasena = TBContrasena.Text;
            _rolId = Convert.ToInt32(TBRolId.Text);
            _personaId = Convert.ToInt32(TBPersonaId.Text);
            _estado = TBEstado.Text;

            executed = objUsuario.SaveUsuario(_usuario, _contrasena, _rolId, _personaId, _estado);

            if (executed)
            {
                LblMsg.Text = "El usuario se guardó exitosamente!";
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
                LblMsg.Text = "No se ha seleccionado un usuario para actualizar.";
                return;
            }
            _idUsuario = Convert.ToInt32(TBId.Value);
            _usuario = TBUsuario.Text;
            _contrasena = TBContrasena.Text;
            _rolId = Convert.ToInt32(TBRolId.Text);
            _personaId = Convert.ToInt32(TBPersonaId.Text);
            _estado = TBEstado.Text;

            executed = objUsuario.UpdateUsuario(_idUsuario, _usuario, _contrasena, _rolId, _personaId, _estado);

            if (executed)
            {
                LblMsg.Text = "El usuario se actualizó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}