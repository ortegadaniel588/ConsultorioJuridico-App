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
    public partial class WFTipo : System.Web.UI.Page
    {
        TipoLog objTipo = new TipoLog();
        private string nombre;
        private string descripcion;
        private int idtipo;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showTipo();
            }
        }

        private void showTipo() 
        {
            DataSet objData = new DataSet();
            objData = objTipo.showTipo();
            GVTipo.DataSource = objData;
            GVTipo.DataBind();
        }

        [WebMethod]
        public static object ListTipo()
        {
            TipoLog objTipo = new TipoLog();

            // Se obtiene un DataSet que contiene la lista de Tipo desde la base de datos.
            var dataSet = objTipo.showTipo();

            // Se crea una lista para almacenar los Tipos que se van a devolver.
            var TipoList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Tipo).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TipoList.Add(new
                {
                    TipoID = row["idtipo"],
                    Nombre = row["nombre"],
                    Descripcion = row["descripcion"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de Tipos.
            return new { data = TipoList };
        }

        //Comentado Eliminar por integridad de Datos
	[WebMethod]
        public static bool deleteTipo(int idtipo)
        {
            // Crear una instancia de la clase de lógica de productos
            TipoLog objTipo = new TipoLog();

            // Invocar al método para eliminar el Tipo y devolver el resultado
            return objTipo.deleteTipo(idtipo);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objTipo.saveTipo(nombre, descripcion);
            if (execute)
            {
                LblMsj.Text = "Se guardo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un tipo para actualizar
            if (string.IsNullOrEmpty(TipoID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un Tipo para actualizar.";
                return;
            }

            idtipo = Convert.ToInt32(TipoID.Value);
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objTipo.saveTipo(_nombre, _descripcion);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

    }
}