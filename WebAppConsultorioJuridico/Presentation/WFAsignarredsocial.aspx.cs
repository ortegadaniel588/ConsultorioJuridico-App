using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAsignarredsocial : System.Web.UI.Page
    {
        AsignarredsocialLog objAsig = new AsignarredsocialLog();
        EmpresaLog objEmp = new EmpresaLog();  
        RedsocialLog objRed = new RedsocialLog();


        private int _id;
        private string _empresa_idempresa;
        private string _redsocial_idredsocial;
        private string _url;
        private bool execute = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showAsignarredsocial();
                showEmpresaDDL();
                showRedsocialDDL();

            }
        }

        private void showAsignarredsocial()
        {
            DataSet objData = new DataSet();
            objData = objAsig.showAsignarredsocial();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }
        private void showEmpresaDDL()
        {
            DDLEmpresa.DataSource = objEmp.showEmpresaDDL();
            DDLEmpresa.DataValueField = "idempresa";
            DDLEmpresa.DataValueField = "nombre";
            DDLEmpresa.DataBind();
            DDLEmpresa.Items.Insert(0, "Seleccione");
        }

        private void showRedsocialDDL()
        {
            DDLRedsocial.DataSource = objRed.showRedsocialDDL();
            DDLRedsocial.DataValueField = "idredsocial";
            DDLRedsocial.DataValueField = "nombre";
            DDLRedsocial.DataBind();
            DDLRedsocial.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _empresa_idempresa = Convert.ToInt32(DDLEmpresa_idempresa.Text);
            _redsocial_idredsocial = Convert.ToInt32(DDLRedsocial_idredsocial.Text);
            _url = TBUrl.Text;
            execute = objAsig.saveAsignarredsocial(_empresa_idempresa, _redsocial_idredsocial, _url);
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
            _id = Convert.ToInt32(TBId.Text);
            _empresa_idempresa = Convert.ToInt32(DDLEmpresa_idempresa.Text);
            _redsocial_idredsocial = Convert.ToInt32(DDLRedsocial_idredsocial.Text);
            _url = TBUrl.Text;
            execute = objAsig.updateAsignarredsocial(_empresa_idempresa, _redsocial_idredsocial, _url);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

        protected void GVAsignarredsocial_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBId.Text = GVAsignarredsocial.SelectedRow.Cells[0].Text;
            DDLEmpresa_idempresa.SelectedValue = GVAsignarredsocial.SelectedRow.Cells[1].Text;
            DDLRedsocial_idredsocial.SelectedValue = GVAsignarredsocial.SelectedRow.Cells[2].Text;
            TBUrl.Text = GVAsignarredsocial.SelectedRow.Cells[3].Text;

        }

        protected void GVAsignarredsocial_RowDeleting(object senderm, GridViewDeleteEventArgs e)
        {
            int _id = Convert.ToInt32(GVAsignarredsocial.DataKeys[e.RowIndex].Values[0]);
            execute = objAsig.deleteAsignarredsocial(_id);
            if (execute)
            {
                LblMsj.Text = "Se elimino exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al eliminar";
            }
        }

        protected void GVAsignarredsocial_RowDataBound(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Vsible = false;
                e.Row.Cells[1].Vsible = false;
                e.Row.Cells[2].Vsible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Vsible = false;
                e.Row.Cells[1].Vsible = false;
                e.Row.Cells[2].Vsible = false;
            }
        }
    }
}