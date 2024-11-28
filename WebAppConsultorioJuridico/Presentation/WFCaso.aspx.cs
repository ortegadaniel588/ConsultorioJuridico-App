﻿using Logic;
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
    public partial class WFCaso : System.Web.UI.Page
    {
        CasoLog objCas = new CasoLog();
        EmpresaLog objEmp = new EmpresaLog();
        TipoLog objTip = new TipoLog();
        EstadoLog objEst = new EstadoLog();
        EmpleadoLog objEmpl = new EmpleadoLog();

        private int _id;
        private string _codigo;
        private string _nombre;
        private int _empresa;
        private DateTime _fechacierre;
        private string _asunto;
        private int _tipo;
        private int _estado;
        private string _complejidad;
        private int _empleado;
        private bool execute = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //showCaso();
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
            objData = objCas.showCaso();
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
                    Nombre = row["nombre"],
                    FKEmpresa = row["empresa_idempresa"],
                    Empresa = row["nombre_empresa"],
                    Fechaapertura = Convert.ToDateTime(row["fechadeapertura"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Fechacierra = Convert.ToDateTime(row["fechacierre"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Asunto = row["asunto"],
                    FKTipo = row["tipo_idtipo"],
                    Tipo = row["nombre_tipo"],
                    FKEstado = row["estado_idestado"],
                    Estado = row["nombre_estado"],
                    Complejidad = row["complejidad"],
                    FKEmpleado = row["idempleado"],
                    Empleado = row["nombre_persona"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = casosList };
        }

        //Comentado Eliminar por integridad de Datos
	    [WebMethod]
        public static bool DeleteCaso(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            CasoLog objCas = new CasoLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objCas.deleteCaso(id);
        }

        private void showEmpresaDDL()
        {
            DDLEpresa.DataSource = objEmp.showEmpresaDDL();
            DDLEpresa.DataValueField = "idempresa";
            DDLEpresa.DataTextField = "nombre";
            DDLEpresa.DataBind();
            DDLEpresa.Items.Insert(0, "Seleccione");
        }

        private void showEstadoDDL()
        {
            DDLEstado.DataSource = objEst.showEstadoDDL();
            DDLEstado.DataValueField = "idestado";
            DDLEstado.DataTextField = "nombre";
            DDLEstado.DataBind();
            DDLEstado.Items.Insert(0, "Seleccione");
        }

        private void showTipoDDL()
        {
            DDLTipo.DataSource = objTip.showTipoDDL();
            DDLTipo.DataValueField = "idtipo";
            DDLTipo.DataTextField = "nombre";
            DDLTipo.DataBind();
            DDLTipo.Items.Insert(0, "Seleccione");
        }

        private void showEmpleadoDDL()
        {
            DDLEmpleado.DataSource = objEmpl.showEmpleadoDDL();
            DDLEmpleado.DataValueField = "idempleado";
            DDLEmpleado.DataTextField = "nombre";
            DDLEmpleado.DataBind();
            DDLEmpleado.Items.Insert(0, "Seleccione");
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            CasoID.Value = "";
            TBCodigo.Text = "";
            TBNombre.Text = "";
            DDLEpresa.SelectedIndex = 0;
            TBFechacierre.Text = "";
            TBAsunto.Text = "";
            DDLTipo.SelectedIndex = 0;
            DDLEstado.SelectedIndex = 0;
            DDLComplejidad.SelectedIndex = 0;
            DDLEmpleado.SelectedIndex = 0;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _codigo = TBCodigo.Text;
            _nombre = TBNombre.Text;
            _empresa = Convert.ToInt32(DDLEpresa.SelectedValue);
            if (!string.IsNullOrWhiteSpace(TBFechacierre.Text))
            {
                _fechacierre = DateTime.Parse(TBFechacierre.Text);
            }
            
            
            _asunto = TBAsunto.Text;
            _tipo = Convert.ToInt32(DDLTipo.SelectedValue);
            _estado = Convert.ToInt32(DDLEstado.SelectedValue);
            _complejidad = DDLComplejidad.Text;
            _empleado = Convert.ToInt32(DDLEmpleado.SelectedValue);
            execute = objCas.saveCaso(_codigo, _nombre, _empresa, _fechacierre, _asunto, _tipo, _estado, _complejidad, _empleado);
            if (execute)
            {
                LblMsj.Text = "Se guardo exitosamente";
                clear();
            }
            else
            {
                LblMsj.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CasoID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            _id = Convert.ToInt32(CasoID.Value);
            _codigo = TBCodigo.Text;
            _nombre = TBNombre.Text;
            _empresa = Convert.ToInt32(DDLEpresa.SelectedValue);
            _fechacierre = DateTime.Parse(TBFechacierre.Text);
            _asunto = TBAsunto.Text;
            _tipo = Convert.ToInt32(DDLTipo.SelectedValue);
            _estado = Convert.ToInt32(DDLEstado.SelectedValue);
            _complejidad = DDLComplejidad.Text;
            _empleado = Convert.ToInt32(DDLEmpleado.SelectedValue);
            execute = objCas.updateCaso(_id, _codigo, _nombre, _empresa, _fechacierre, _asunto, _tipo, _estado, _complejidad, _empleado);
            if (execute)
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