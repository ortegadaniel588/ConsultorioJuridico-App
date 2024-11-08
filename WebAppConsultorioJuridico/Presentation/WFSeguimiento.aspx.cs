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
    public partial class WFSeguimiento : System.Web.UI.Page
    {
        SeguimientoLog objSeg = new SeguimientoLog();
        CasoLog objCas = new CasoLog();


        private string _id;
        private string _caso_id;
        private string _fecha_actualizacion;
        private string _proceso;
        private string _descripcion;
        private string _estado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showSeguimiento();
                showCasoDDL();

            }
        }

        /*/private void showSeguimiento()
        {
            DataSet objData = new DataSet();
            objData = objSeg.showSeguimiento();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/
                [WebMethod]
        public static object ListSeguimientos()
        {
            SeguimientoLog objSeg = new SeguimientoLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objSeg.showSeguimiento();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var seguimientosList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un caso).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                seguimientosList.Add(new
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

            _caso_id = Convert.ToInt32(DDCaso_idcaso.Text);
            _fecha_actualizacion = TBFechaactualizacion.Text;
            _proceso = TBProceso.Text;
            _descripcion = TBDescripcion.Text;
            _estado = TBEstado.Text;
            execute = objCas.saveSeguimiento(_caso_id, _fecha_actualizacion, _proceso, _estado);
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
            _caso_id = Convert.ToInt32(DDCaso_idcaso.Text);
            _fecha_actualizacion = TBFechaactualizacion.Text;
            _proceso = TBProceso.Text;
            _descripcion = TBDescripcion.Text;
            _estado = TBEstado.Text;
            execute = objCas.updateSeguimiento(_id, _caso_id, _fecha_actualizacion, _proceso, _estado);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

        protected void GVSeguimiento_SelectedIndexChanged(object sender, EventArgs e)
        {

            TBid.Text = GVSeguimiento.SelectedRow.Cells[0].Text;
            DDCaso_idcaso.SelectedValue = GVSeguimiento.SelectedRow.Cells[1].Text;
            TBFechaactualizacion.Text = GVSeguimiento.SelectedRow.Cells[2].Text;
            TBProceso.Text = GVSeguimiento.SelectedRow.Cells[3].Text;
            TBDescripcion.Text = GVSeguimiento.SelectedRow.Cells[4].Text;
            TBEstado.Text = GVSeguimiento.SelectedRow.Cells[5].Text;

        }

        protected void GVSeguimiento_RowDeleting(object senderm, GridViewDeleteEventArgs e)
        {
            int _id = Convert.ToInt32(GVSeguimiento.DataKeys[e.RowIndex].Values[0]);
            execute = objSeg.deleteSeguimiento(_id);
            if (execute)
            {
                LblMsj.Text = "Se elimino exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al eliminar";
            }
        }

        protected void Seguimiento_RowDataBound(object sender, GridViewRowEventArgs e)
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
