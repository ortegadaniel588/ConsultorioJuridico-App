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
    public partial class WFExpediente : System.Web.UI.Page
    {
        ExpedienteLog objEsp = new ExpedienteLog();
        CasoLog objCas = new CasoLog();


        private int _id;
        private int _caso_idcaso;
        private string _codigo;
        private string _accionrealizada;
        private string _razon;
        private string _relevancia;
        private string _evidencia;
        private string _comentario;
        private string _estado;

        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //showExpediente();
                showCasoDDL();

            }
        }

        /*private void showExpediente()
        {
            DataSet objData = new DataSet();
            objData = objExp.showExpediente();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/
	[WebMethod]
        public static object ListExpedientes()
        {
            ExpedienteLog objExp = new ExpedienteLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objExp.showExpediente();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var expedienteList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un caso).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                expedienteList.Add(new
                {
                    ExpedienteID = row["idexpendiente"],
                    FKCaso = row["caso_idcaso"],
                    Caso = row["caso_nombre"],
                    Codigo = row["codigo"],
                    Fechacreacion = row["cracionfecha"],
		            Accionrealizada = row["accionrealizada"],
		            Razon = row["razon"],
                    Relevancia = row["relevancia"],
                    Evidencia = row["evidencia"],
                    Comentario = row["comentario"],
                    Estado = row["estado"],
                    
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = expedienteList };
        }

        // Comentado Eliminar por integridad de Datos
	    [WebMethod]
        public static bool DeleteExpediente(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ExpedienteLog objExp = new ExpedienteLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objExp.deleteExpediente(id);
        }
        private void showCasoDDL()
        {
            DDCaso_idcaso.DataSource = objCas.showCasoDDL();
            DDCaso_idcaso.DataValueField = "idcaso";
            DDCaso_idcaso.DataTextField = "nombre";
            DDCaso_idcaso.DataBind();
            DDCaso_idcaso.Items.Insert(0, "Seleccione");
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            ExpedienteID.Value = "";
            DDCaso_idcaso.SelectedIndex = 0;
            TBCodigo.Text = "";
            TBAccionrealizada.Text = "";
            TBRazon.Text = "";
            DDLRelevancia.SelectedIndex = 0;
            TBEvidencia.Text = "";
            TBComentario.Text = "";
            DDLEstado.SelectedIndex = 0;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _caso_idcaso = Convert.ToInt32(DDCaso_idcaso.SelectedValue);
            _codigo = TBCodigo.Text;
            _accionrealizada = TBAccionrealizada.Text;
            _razon = TBRazon.Text;
            _relevancia = DDLRelevancia.Text;
            _evidencia = TBEvidencia.Text;
            _comentario = TBComentario.Text;
            _estado = DDLEstado.Text;
            executed = objEsp.saveExpediente(_caso_idcaso, _codigo,  _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
            if (executed)
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
            if (string.IsNullOrEmpty(ExpedienteID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un expediente para actualizar.";
                return;
            }

            _id = Convert.ToInt32(ExpedienteID.Value);
            _caso_idcaso = Convert.ToInt32(DDCaso_idcaso.Text);
            _codigo = TBCodigo.Text;
            _accionrealizada = TBAccionrealizada.Text;
            _razon = TBRazon.Text;
            _relevancia = DDLRelevancia.Text;
            _evidencia = TBEvidencia.Text;
            _comentario = TBComentario.Text;
            _estado = DDLEstado.Text;
            executed = objEsp.updateExpediente(_id, _caso_idcaso, _codigo, _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
            if (executed)
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