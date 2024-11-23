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
    public partial class WFEstado : System.Web.UI.Page
    {
        EstadoLog objEst = new EstadoLog();
        private string nombre;
        private string descripcion;
        private int idestado;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showEstado();
            }
        }

        private void showEstado() 
        {
            DataSet objData = new DataSet();
            objData = objEst.showEstado();
            GVEstado.DataSource = objData;
            GVEstado.DataBind();
        }

        [WebMethod]
        public static object ListEstado()
        {
            EstadoLog objEst = new EstadoLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objEst.showEstado();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var EstadoList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Estado).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                EstadoList.Add(new
                {
                    EstadoID = row["idestado"],
                    Nombre = row["nombre"],
                    Descripcion = row["descripcion"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = EstadoList };
        }

        //Comentado Eliminar por integridad de Datos
	[WebMethod]
        public static bool deleteEstado(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            EstadoLog objEst = new EstadoLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objEst.deleteEstado(id);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objEst.saveEstado(nombre, descripcion);
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
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(EstadoID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }

            idestado = Convert.ToInt32(EstadoID.Value);
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objEst.saveEstado(nombre, descripcion);
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