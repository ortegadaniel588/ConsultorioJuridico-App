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


        private string _codigo;
        private int _empresa;
        private DateTime _fechaapertura;
        private DateTime _fechacierre;
        private string _asunto;
        private int _tipo;
        private int estado;
        private string _complejidad;
        private int _idempleado;




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

        private void showCaso()
        {
            DataSet objData = new DataSet();
            objData = objCas.showCaso();
            GVCaso.DataSource = objData;
            GVCaso.DataBind();
        }

        private void showEmpresaDDL() { 
            DDLCaso.DataSource = objCas.showCaso();
            DDLCaso.DataValueField = "_id";
            DDLCaso.DataValueField = "_codigo";
            DDLCaso.DataValueField = "_empresa";
            DDLCaso.DataValueField = "_fechaapertura";
            DDLCaso.DataValueField = "_fechacierre";
            DDLCaso.DataValueField = "_asunto";
            DDLCaso.DataValueField = "_tipo";
            DDLCaso.DataValueField = "_estado";
            DDLCaso.DataValueField = "_complejidad";
            DDLCaso.DataValueField = "_idempleado";










        }

        private void showEstadoDDL() { 
        }

        private void showTipoDDL() { 
        }

        private void showEmpleadoDDL() { 
        }
    }
}