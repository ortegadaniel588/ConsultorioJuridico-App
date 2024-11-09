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
    public partial class WFCaso : System.Web.UI.Page
    {
        CasoLog objCas = new CasoLog();
        EmpresaLog objEmp = new EmpresaLog();
        TipoLog objTip = new TipoLog();
        EstadoLog objEst = new EstadoLog();
        EmpleadoLog objEmpl = new EmpleadoLog();

        private int _id;
        private string _codigo;
        private int _empresa;
        private string _fechaapertura;
        private string _fechacierre;
        private string _asunto;
        private int _tipo;
        private int estado;
        private string _complejidad;
        private int _idempleado;
        private bool execute = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showCaso();
                showEmpresaDDL();
                showEstadoDDL();
                showTipoDDL();
                showEmpleadoDDL();

            }
        }
        /*Se elimino este mtodo para añadir el datatable JavaScrit*/
        /*private void showCaso()
        {
            DataSet objData = new DataSet();
            objData = objCas.showAsignarredsocial();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/
        
        [WebMethod]
        public static object ListCasos()
        {
            CasoLog objCas = new CasoLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objCas.showCaso();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var casosList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un caso).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                casosList.Add(new
                {
                    CasoID = row["idcaso"],
                    Codigo = row["codigo"],
                    Empresa = row["empresa_idempresa"],
		    Fechaapertura = row["fechadeapertura"],
		    Fechacierra = row["fechacierre"],
                    Asunto = row["asunto"],
                    Tipo = row["tipo_idtipo"],
                    Estado = row["estado_idestado"],
                    Complejidad = row["complejidad"],
                    Empleado = row["empleado_idempleado"],
                    
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = casosList };
        }

        /* Comentado Eliminar por integridad de Datos
	[WebMethod]
        public static bool DeleteCaso(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            CasoLog objCas = new CasoLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objCas.deleteCaso(id);
        }*/
        private void showEmpresaDDL()
        {
            DDLEmpresa.DataSource = objEmp.showEmpresaDDL();
            DDLEmpresa.DataValueField = "idempresa";
            DDLEmpresa.DataValueField = "nombre";
            DDLEmpresa.DataBind();
            DDLEmpresa.Items.Insert(0, "Seleccione");
        }

        private void showEstadoDDL()
        {
            DDLEstado.DataSource = objEst.showEstadoDDL();
            DDLEstado.DataValueField = "idestado";
            DDLEstado.DataValueField = "nombre";
            DDLEstado.DataBind();
            DDLEstado.Items.Insert(0, "Seleccione");
        }

        private void showTipoDDL()
        {
            DDLEstado.DataSource = objTip.showTipoDDL();
            DDLEstado.DataValueField = "idtipo";
            DDLEstado.DataValueField = "nombre";
            DDLEstado.DataBind();
            DDLEstado.Items.Insert(0, "Seleccione");
        }

        private void showEmpleadoDDL()
        {
            DDLEstado.DataSource = objEmpl.showTipoDDL();
            DDLEstado.DataValueField = "idempleado";
            DDLEstado.DataValueField = "usuario_idusuario";
            DDLEstado.DataBind();
            DDLEstado.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _codigo = TBCodigo.Text;
            _empresa_id = Convert.ToInt32(DDLEpresa.Text);
            _fechacierre = TBFechacierre.Text;
            _asunto = TBAsunto.Text;
            _tipo_id = Convert.ToInt32(DDLTipo.Text);
            _estado_id = Convert.ToInt32(DDLEstado.Text);
            _complejidad = DDLComplejidad.Text;
            _empleado_id = Convert.ToInt32(DDLEmpleado.Text);
            execute = objCas.saveCaso(_codigo, _empresa_id, _fechacierre, _asunto, _tipo_id, _estado_id, _complejidad, _empleado_id);
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
            _codigo = TBCodigo.Text;
            _empresa_id = Convert.ToInt32(DDLEpresa.Text);
            _fechacierre = TBFechacierre.Text;
            _asunto = TBAsunto.Text;
            _tipo_id = Convert.ToInt32(DDLTipo.Text);
            _estado_id = Convert.ToInt32(DDLEstado.Text);
            _complejidad = DDLComplejidad.Text;
            _empleado_id = DDLEmpleado.Text;
            execute = objCas.updateCaso(_id, _codigo, _empresa_id, _fechacierre, _asunto, _tipo_id, _estado_id, _complejidad, _empleado_id);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

        protected void GVCaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBId.Text = GVCaso.SelectedRow.Cells[0].Text;
            TBCodigo.Text = GVCaso.SelectedRow.Cells[1].Text;
            DDLEpresa.SelectedValue = GVCaso.SelectedRow.Cells[2].Text;
            TBFechacierre.Text = GVCaso.SelectedRow.Cells[3].Text;
            TBAsunto.Text = GVCaso.SelectedRow.Cells[4].Text;
            DDLTipo.SelectedValue = GVCaso.SelectedRow.Cells[5].Text;
            DDLComplejidad.SelectedValue = GVCaso.SelectedRow.Cells[6].Text;
            DDLEmpleado.SelectedValue = GVCaso.SelectedRow.Cells[7].Text;

        }

        protected void GVCaso_RowDeleting(object senderm, GridViewDeleteEventArgs e)
        {
            int _id = Convert.ToInt32(GVCaso.DataKeys[e.RowIndex].Values[0]);
            execute = objCas.deleteCaso(_id);
            if (execute)
            {
                LblMsj.Text = "Se elimino exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al eliminar";
            }
        }

        protected void GVCaso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Vsible = false;
                e.Row.Cells[2].Vsible = false;
                e.Row.Cells[5].Vsible = false;
                e.Row.Cells[6].Vsible = false;
                e.Row.Cells[7].Vsible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Vsible = false;
                e.Row.Cells[2].Vsible = false;
                e.Row.Cells[5].Vsible = false;
                e.Row.Cells[6].Vsible = false;
                e.Row.Cells[7].Vsible = false;
            }
        }
    }
}