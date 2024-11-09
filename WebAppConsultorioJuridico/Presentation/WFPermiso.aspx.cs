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
    public partial class WFPermiso : System.Web.UI.Page
    {
        PermisoLog objPermiso = new PermisoLog();

        private int _idPermiso;
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
        public static object ListPermissions()
        {
            PermisoLog objPermiso = new PermisoLog();
            DataSet ds = objPermiso.ShowPermisos();
            var permissionsList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                permissionsList.Add(new
                {
                    idpermiso = row["idpermiso"],
                    nombre = row["nombre"],
                    descripcion = row["descripcion"]
                });
            }

            return new { data = permissionsList };
        }

        [WebMethod]
        public static bool DeletePermission(int id)
        {
            PermisoLog objPer = new PermisoLog();
            return objPer.DeletePermiso(id);
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

            executed = objPermiso.SavePermiso(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El permiso se guardó exitosamente!";
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
                LblMsg.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            _idPermiso = Convert.ToInt32(TBId.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objPermiso.UpdatePermiso(_idPermiso, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "El permiso se actualizó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
        /*
        protected void GVPermisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVPermisos.Rows[index];
                TBId.Text = row.Cells[0].Text;
                TBNombre.Text = row.Cells[1].Text;
                TBDescripcion.Text = row.Cells[2].Text;
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVPermisos.Rows[index];
                int idPermiso = Convert.ToInt32(row.Cells[0].Text);

                executed = objPermiso.DeletePermiso(idPermiso);

                if (executed)
                {
                    LblMsg.Text = "El permiso se eliminó exitosamente!";
                }
                else
                {
                    LblMsg.Text = "Error al eliminar";
                }
            }
        }*/
    }
}