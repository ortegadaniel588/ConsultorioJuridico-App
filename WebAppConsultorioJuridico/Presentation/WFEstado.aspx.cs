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
        //Crear los objetos
        EstadoLog objEst = new EstadoLog();
        
        private int idestado;
        private string nombre, descripcion;
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
            }
        }
        //Metodo para mostrar todos los estado
        /*
      * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
      * parte de un servicio web, lo que significa que puede ser invocado de manera
      * remota a través de HTTP.
      */
       / [WebMethod]
        public static object ListEstado()
        {
            EstadoLog objEst = new EstadoLog();

            // Se obtiene un DataSet que contiene la lista de estados desde la base de datos.
            var dataSet = objEst.showEstado();

            // Se crea una lista para almacenar los estados que se van a devolver.
            var EstadoList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Estado).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                EstadoList.Add(new
                {
                    EstadoID = row["est_id"],
                    Nombre = row["est_Nombre"],
                    Descripcion = row["est_descripcion"],
                 });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de Estado.
            return new { data = EstadoList };
        }

        [WebMethod]
        public static bool deleteEstado(int id)
        {
            // Crear una instancia de la clase de lógica de estado
            EstadoLog objEst = new EstadoLog();

            // Invocar al método para eliminar el estado y devolver el resultado
            return objEst.deleteEstado (id);
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFEstadoID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
           
        }
        //Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            
            executed = objEst.saveEstado(nombre, descripcion);

            if (executed)
            {
                LblMsg.Text = "El estado se guardo exitosamente!";

            }
            else
            {
                LblMsg.Text = "Error al guardar el estado";
            }
        }
        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFEstadoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un estado para actualizar.";
                return;
            }
            idestad = Convert.ToInt32(HFEstadoID.Value);
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            
            executed = objEst.updateEstado(int idestado, string nombre, string descripcion);

            if (executed)
            {
                LblMsg.Text = "El estado se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar el estado";
            }
        }
    }
}