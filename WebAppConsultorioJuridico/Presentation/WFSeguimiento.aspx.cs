using Logic;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFSeguimiento : System.Web.UI.Page
    {
        SeguimientoLog objSeg = new SeguimientoLog();
        CasoLog objCas = new CasoLog();


        private int _id;
        private int _caso_id;
        private DateTime _fecha_actualizacion;
        private string _proceso;
        private string _descripcion;
        private string _estado;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //showSeguimiento();
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

            // Se obtiene un DataSet que contiene la lista de seguimiento desde la base de datos.
            var dataSet = objSeg.showSeguimiento();

            // Se crea una lista para almacenar los seguimiento que se van a devolver.
            var seguimientosList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un seguimiento).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                seguimientosList.Add(new
                {
                    SeguimientoID = row["idseguimiento"],
                    FKCaso = row["caso_idcaso"],
                    Casocode = row["caso_codigo"],
                    Fechaactualizacion = Convert.ToDateTime(row["fechaactualizacion"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Proceso = row["proceso"],
		            Descripcion = row["descripcion"],
                    Estado = row["estado"],
                    Asunto= row["asunto"],
                    Fechaapertura = row["fechadeapertura"],
                    Fechacierre = row["fechacierre"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de seguimiento.
            return new { data = seguimientosList };
        }

        // Comentado Eliminar por integridad de Datos
	    [WebMethod]
        public static bool DeleteSeguimiento(int id)
        {
            // Crear una instancia de la clase de lógica de seguimiento
            SeguimientoLog objSeg = new SeguimientoLog();

            // Invocar al método para eliminar el seguimiento y devolver el resultado
            return objSeg.deleteSeguimiento(id);
        }
        private void showCasoDDL()
        {
            DDCaso_idcaso.DataSource = objCas.showCasoDDL();
            DDCaso_idcaso.DataValueField = "idcaso";
            DDCaso_idcaso.DataTextField = "nombre";
            DDCaso_idcaso.DataBind();
            DDCaso_idcaso.Items.Insert(0, "Seleccione");
        }

        private void clear()
        {
            SeguimientoID.Value = "";
            DDCaso_idcaso.SelectedIndex = 0;
            TBFechaactualizacion.Text = "";
            TBProceso.Text = "";
            TBDescripcion.Text = "";
            TBEstado.Text = "";
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {

            _caso_id = Convert.ToInt32(DDCaso_idcaso.SelectedValue);
            _fecha_actualizacion = DateTime.Parse(TBFechaactualizacion.Text);
            _proceso = TBProceso.Text;
            _descripcion = TBDescripcion.Text;
            _estado = TBEstado.Text;
            executed = objSeg.saveSeguimiento(_caso_id, _fecha_actualizacion, _proceso, _descripcion, _estado);
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
            if (string.IsNullOrEmpty(SeguimientoID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            _id = Convert.ToInt32(SeguimientoID.Value);
            _caso_id = Convert.ToInt32(DDCaso_idcaso.SelectedValue);
            _fecha_actualizacion = DateTime.Parse(TBFechaactualizacion.Text);
            _proceso = TBProceso.Text;
            _descripcion = TBDescripcion.Text;
            _estado = TBEstado.Text;
            executed = objSeg.updateSeguimiento(_id, _caso_id, _fecha_actualizacion, _proceso, _descripcion, _estado);
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
