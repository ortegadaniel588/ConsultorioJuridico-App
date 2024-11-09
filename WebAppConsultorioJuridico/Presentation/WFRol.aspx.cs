using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
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
                // Aquí se pueden invocar métodos si es necesario
            }
        }

        [WebMethod]
        public static object ListRoles()
        {
            RolLog objRol = new RolLog();
            DataSet ds = objRol.showRoles();
            var rolesList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                rolesList.Add(new
                {
                    idrol = row["idrol"],
                    nombre = row["nombre"],
                    descripcion = row["descripcion"]
                });
            }

            return new { data = rolesList };
        }

        [WebMethod]
        public static bool DeleteRole(int id)
        {
            RolLog objRol = new RolLog();
            return objRol.deleteRol(id);
        }

        // Método para limpiar los TextBox
        private void Clear()
        {
            TBId.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        // Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objRol.saveRol(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El rol se guardó exitosamente!";
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
                LblMsg.Text = "No se ha seleccionado un rol para actualizar.";
                return;
            }
            _idRol = Convert.ToInt32(TBId.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objRol.updateRol(_idRol, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El rol se actualizó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}