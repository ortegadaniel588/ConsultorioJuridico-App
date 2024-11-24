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
    public partial class WFRedsocial : System.Web.UI.Page
    {
        RedsocialLog objReds = new  RedsocialLog();
        private string _nombre;
        private string _descripcion;
        private int _id;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //showRedsocial();
            }
        }

        /*private void showRedsocial() 
        {
            DataSet objData = new DataSet();
            objData = objReds.showRedsocial();
            GVRedsocial.DataSource = objData;
            GVRedsocial.DataBind();
        }*/

	[WebMethod]
        public static object ListRedessociales()
        {
            RedsocialLog objRed = new RedsocialLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objRed.showRedsocial();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var redessocialesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un caso).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                redessocialesList.Add(new
                {
                    RedsocialID = row["idredsocial"],
                    Nombre = row["nombre"],
                    Descripcion = row["descripcion"],
                    
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = redessocialesList};
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            RedsocialID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";



        }

        // Comentado Eliminar por integridad de Datos
        [WebMethod]
        public static bool deleteRedsocial(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            RedsocialLog objRed = new RedsocialLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objRed.deleteRedsocial(id);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;
            execute = objReds.saveRedsocial(_nombre, _descripcion);
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
            if (string.IsNullOrEmpty(RedsocialID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un Tipo para actualizar.";
                return;
            }

            _id = Convert.ToInt32(RedsocialID.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;
            execute = objReds.updateRedsocial(_id, _nombre, _descripcion);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
                clear();
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

    }
}
