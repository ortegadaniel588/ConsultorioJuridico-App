using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;

namespace Presentation
{
    public partial class WFEspecialidad : System.Web.UI.Page
    {
        EspecialidadLog objEspecialidad = new EspecialidadLog();
        private int _id;
        private string _nombre, _descripcion;
        private bool executed = false;

        /*
    *  Variables de tipo pública que indiquen si el usuario tiene
    *  permiso para ver los botones editar y eliminar.
    */
        public bool _showEditButton { get; set; } = true;
        public bool _showDeleteButton { get; set; } = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializaciones si son necesarias
            }
        }

        [WebMethod]
        public static object ListEspecialidades()
        {
            EspecialidadLog objEsp = new EspecialidadLog();
            DataSet ds = objEsp.showEspecialidad();
            var especialidadesList = new List<object>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                especialidadesList.Add(new
                {
                    id = row["idespecialidad"],
                    nombre = row["nombre"],
                    descripcion = row["descripcion"]
                });
            }

            return new { data = especialidadesList };
        }

        [WebMethod]
        public static bool DeleteEspecialidad(int id)
        {
            EspecialidadLog objEsp = new EspecialidadLog();
            return objEsp.deleteEspecialidad(id);
        }

        private void Clear()
        {
            HFEspecialidadID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEspecialidad.saveEspecialidad(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "La especialidad se guardó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFEspecialidadID.Value))
            {
                LblMsg.Text = "No se ha seleccionado una especialidad para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFEspecialidadID.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objEspecialidad.updateEspecialidad(_id, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "La especialidad se actualizó exitosamente!";
                Clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}