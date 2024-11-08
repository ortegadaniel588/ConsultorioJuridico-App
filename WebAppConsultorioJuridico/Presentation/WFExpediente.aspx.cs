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

        private void showExpediente()
        {
            DataSet objData = new DataSet();
            objData = objExp.showExpediente();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }
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