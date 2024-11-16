﻿using Logic;
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
                //showAsignarredsocial();
                showEmpresaDDL();
                showRedsocialDDL();

            }
        }

        /*private void showAsignarredsocial()
        {
            DataSet objData = new DataSet();
            objData = objAsig.showAsignarredsocial();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/

        [WebMethod]
        public static object ListAsignarredessociales()
        {
            AsignarredsocialLog objAsig = new AsignarredsocialLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objAsig.showAsignarredsocial();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var asignarredessocialesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un caso).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                asignarredessocialesList.Add(new
                {
                    AsignarredsocialID = row["idasignarredsocial"],
                    Empresa = row["empresa_idempresa"],
                    Redsocial = row["redsocial_idredsocial"],
		            Url = row["url"],
                    
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = asignarredessocialesList };
        }

        /* Comentado Eliminar por integridad de Datos
	[WebMethod]
        public static bool DeleteAsignarredsocial(int id)
        {
            // Crear una instancia de la clase de lógica de asignarredsocial
            AsignarredsocialLog objAsig = new AsignarredsocialLog();

            // Invocar al método para eliminar el asignar red social y devolver el resultado
            return objAsig.deleteAsignarredsocial(id);
        }*/
        private void showEmpresaDDL()
        {
            DDLEmpresa_idempresa.DataSource = objEmp.showEmpresaDDL();
            DDLEmpresa_idempresa.DataValueField = "idempresa";
            DDLEmpresa_idempresa.DataValueField = "nombre";
            DDLEmpresa_idempresa.DataBind();
            DDLEmpresa_idempresa.Items.Insert(0, "Seleccione");
        }

        private void showRedsocialDDL()
        {
            DDLRedsocial_idredsocial.DataSource = objRed.showRedsocialDDL();
            DDLRedsocial_idredsocial.DataValueField = "idredsocial";
            DDLRedsocial_idredsocial.DataValueField = "nombre";
            DDLRedsocial_idredsocial.DataBind();
            DDLRedsocial_idredsocial.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _empresa_idempresa = Convert.ToInt32(DDLEmpresa_idempresa.SelectedValue);
            _redsocial_idredsocial = Convert.ToInt32(DDLRedsocial_idredsocial.SelectedValue);
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
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(AsignarredsocialID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }

            _id = Convert.ToInt32(AsignarredsocialID.Value);
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

    }
}