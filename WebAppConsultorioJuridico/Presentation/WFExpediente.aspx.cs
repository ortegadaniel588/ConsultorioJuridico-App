using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFExpediente : System.Web.UI.Page
    {
        ExpedienteLog objExp = new ExpedienteLog();
        CasoLog objCas = new CasoLog();


        private string _id;
        private string _codigo;
        private int _caso_idcaso;
        private string _accionrealizada;
        private string _razon;
        private string _relevancia;
        private string _evidencia;
        private string _comentario;
        private string _estado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showExpediente();
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
        public static object ListCasos()
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
                    ExpedienteID = row["idexpediente"],
		    Caso = row["caso_idcaso"],
                    Codigo = row["codigo"],
                    Fechacreacion = row["creacionfecha"],
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

        /* Comentado Eliminar por integridad de Datos
	[WebMethod]
        public static bool DeleteExpediente(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ExpedienteLog objExp = new ExpedienteLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objExp.deleteExpediente(id);
        }*/
        private void showCasoDDL()
        {
            DDLEmpresa.DataSource = objCas.showCasoDDL();
            DDLEmpresa.DataValueField = "idcaso";
            DDLEmpresa.DataValueField = "codigo";
            DDLEmpresa.DataBind();
            DDLEmpresa.Items.Insert(0, "Seleccione");
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _codigo = Convert.ToInt32(DDCaso_idcaso.Text);
            _caso_idcaso = TBCodigo.Text;
            _accionrealizada = TBCracionfecha.Text;
            _razon = TBAccionrealizada.Text;
            _relevancia = TBRazon.Text;
            _evidencia = TBRelevancia.Text;
            _comentario = TBEvidencia.Text;
            _estado = TBComentario.Text;
            execute = objCas.saveExpediente(_codigo, _caso_idcaso, _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
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
            _id = TBid.Text;
            _codigo = Convert.ToInt32(DDCaso_idcaso.Text);
            _caso_idcaso = TBCodigo.Text;
            _accionrealizada = TBCracionfecha.Text;
            _razon = TBAccionrealizada.Text;
            _relevancia = TBRazon.Text;
            _evidencia = TBRelevancia.Text;
            _comentario = TBEvidencia.Text;
            _estado = TBComentario.Text;
            execute = objCas.updateExpediente(_id, _codigo, _caso_idcaso, _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

        protected void GVExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {

            TBid.Text = GVExpediente.SelectedRow.Cells[0].Text;
            DDCaso_idcaso.SelectedValue = GVExpediente.SelectedRow.Cells[1].Text;
            TBCodigo.Text = GVExpediente.SelectedRow.Cells[2].Text;
            TBCracionfecha.Text = GVExpediente.SelectedRow.Cells[3].Text;
            TBAccionrealizada.Text = GVExpediente.SelectedRow.Cells[4].Text;
            TBRazon.Text = GVExpediente.SelectedRow.Cells[5].Text;
            TBRelevancia.Text = GVExpediente.SelectedRow.Cells[6].Text;
            TBEvidencia.Text = GVExpediente.SelectedRow.Cells[7].Text;
            TBComentario.Text = GVExpediente.SelectedRow.Cells[8].Text;
        }

        protected void GVExpediente_RowDeleting(object senderm, GridViewDeleteEventArgs e)
        {
            int _id = Convert.ToInt32(GVExpediente.DataKeys[e.RowIndex].Values[0]);
            execute = objCas.deleteExpediente(_id);
            if (execute)
            {
                LblMsj.Text = "Se elimino exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al eliminar";
            }
        }

        protected void GVExpediente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Vsible = false;
                e.Row.Cells[1].Vsible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Vsible = false;
                e.Row.Cells[1].Vsible = false;
            }
        }
    }
}