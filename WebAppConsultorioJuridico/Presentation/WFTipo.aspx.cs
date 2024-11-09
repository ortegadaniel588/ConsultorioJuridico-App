using Logic;
using System;
using System.CodeDom.Compiler;
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
        //Crear los objetos
        TipoLog objTipo = new TipoLog();

        private int _idtipo;
        private string _nombre, _descripcion;

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
        public static object ListTipo()
        {
            TipoLog objTipo = new TipoLog();

            // Se obtiene un DataSet que contiene la lista de Tipo desde la base de datos.
            var dataSet = objTipo.ShowTipo();

            // Se crea una lista para almacenar los Tipo que se van a devolver.
            var TipoList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Tipo).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                TipoList.Add(new
                {
                    TipoID = row["tipo_id"],
                    Nombre = row["tipo_Nombre"],
                    Descripcion = row["tipo_descripcion"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de Tipo.
            return new { data = TipoList };
        }

        [WebMethod]
        public static bool DeleteTipo(int _idtipo)
        {
            // Crear una instancia de la clase de lógica de Tipo
            TipoLog objTipo = new TipoLog();

            // Invocar al método para eliminar el tipo y devolver el resultado
            return objTipo.DeleteTipo(id);
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFTipoID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";

        }
        //Eventos que se ejecutan cuando se da clic en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objTipo.SaveTipo(string _nombre, string _descripcion);

            if (executed)
            {
                LblMsg.Text = "El tipo se guardo exitosamente!";

            }
            else
            {
                LblMsg.Text = "Error al guardar el tipo";
            }
        }
        // Evento del boton actualizar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFTipoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un tipo para actualizar.";
                return;
            }
            _idtipo = Convert.ToInt32(HFTipoID.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objTipo.UpdateTipo(int _idtipo, string _nombre, string _descripcion);

            if (executed)
            {
                LblMsg.Text = "El tipo se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar el tipo";
            }
        }
    }
}