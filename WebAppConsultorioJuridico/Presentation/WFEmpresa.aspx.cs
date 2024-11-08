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
    public partial class WFEmpresa : System.Web.UI.Page
    {
        EmpresaLog objEmp = new EmpresaLog();
        
        private int _id; 
        private string _numeronit;
        private string _nombre;
        private string _mision;
        private string _vision;
        private string _dirrecion;
        private string _telefono;
        private string _telefono2;
        private string _correo;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showEmpresa();
            }
        }

        private void showEmpresa()
        {
            DataSet objData = new DataSet();
            objData = objEmp.showEmpresa();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _numeronit = TBnumeronit.Text;
            _nombre = TBnombre.Text;
            _mision = TBmision.Text;
            _vision = TBvision.Text;
            _dirrecion = TBdirrecion.Text;
            _telefono = TBTlefono.Text;
            _telefono2 = TBTelefono2.Text;
            _correo = TBCorreo.Text;
            execute = objEmp.saveEmpresa(_numeronit, _nombre, _mision, _vision, _dirrecion, _telefono, _telefono2, _correo);
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
            _id = Convert.ToInt32(TBid.Text);
            _numeronit = TBnumeronit.Text;
            _nombre = TBnombre.Text;
            _mision = TBmision.Text;
            _vision = TBvision.Text;
            _dirrecion = TBdirrecion.Text;
            _telefono = TBTlefono.Text;
            _telefono2 = TBTelefono2.Text;
            _correo = TBCorreo.Text;
            execute = objEmp.updateEmpresa(_id, _numeronit, _nombre, _mision, _vision, _dirrecion, _telefono, _telefono2, _correo);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

        protected void GVEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBId.Text = GVEmpresa.SelectedRow.Cells[1].Text;
            TBnumeronit.Text = GVEmpresa.SelectedRow.Cells[2].Text;
            TBnombre.Text = GVEmpresa.SelectedRow.Cells[3].Text;
            TBmision.Text = GVEmpresa.SelectedRow.Cells[4].Text;
            TBvision.Text = GVEmpresa.SelectedRow.Cells[5].Text;
            TBdirrecion.Text = GVEmpresa.SelectedRow.Cells[6].Text;
            TBTlefono.Text = GVEmpresa.SelectedRow.Cells[7].Text;
            TBTelefono2.Text = GVEmpresa.SelectedRow.Cells[8].Text;
            TBCorreo.Text = GVEmpresa.SelectedRow.Cells[9].Text;
        }

        protected void GVEmpresa_RowDeleting(object senderm, GridViewDeleteEventArgs e)
        {
            int _id = Convert.ToInt32(GVEmpresa.DataKeys[e.RowIndex].Values[1]);
            execute = objEmp.deleteEmpresa(_id);
            if (execute)
            {
                LblMsj.Text = "La red social se elimino exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al eliminar la red social";
            }
        }

    }
}