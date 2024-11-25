﻿using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
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
        private string _direccion;
        private string _telefono;
        private string _telefono2;
        private string _correo;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //showEmpresa();
            }
        }

        /*private void showEmpresa()
        {
            DataSet objData = new DataSet();
            objData = objEmp.showEmpresa();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/

	[WebMethod]
        public static object listEmpresas()
        {
            EmpresaLog objEmp = new EmpresaLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objEmp.showEmpresa();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var empresasList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un empresa).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                empresasList.Add(new
                {
                    EmpresaID = row["idempresa"],
                    Numeronit = row["numeronit"],
                    Nombre = row["nombre"],
		            Mision = row["mision"],
                    Vision = row["vision"],
                    Direccion = row["direccion"],
                    Telefono = row["telefono"],
                    Telefono2 = row["telefono2"],
                    Correo = row["correo"],
                    
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = empresasList };
        }

        private void clear()
        {
            EmpresaID.Value = "";
            TBNumeronit.Text = "";
            TBNombre.Text = "";
            TBMision.Text = "";
            TBVision.Text = "";
            TBDireccion.Text = "";
            TBTelefono.Text = "";
            TBTelefono2.Text = "";
            TBCorreo.Text = "";



        }




        //Comentado Eliminar por integridad de Datos
        [WebMethod]
        public static bool deleteEmpresa(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            EmpresaLog objEmp = new EmpresaLog();

            // Invocar al método para eliminar empresa y devolver el resultado
            return objEmp.deleteEmpresa(id);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _numeronit = TBNumeronit.Text;
            _nombre = TBNombre.Text;
            _mision = TBMision.Text;
            _vision = TBVision.Text;
            _direccion = TBDireccion.Text;
            _telefono = TBTelefono.Text;
            _telefono2 = TBTelefono2.Text;
            _correo = TBCorreo.Text;
            execute = objEmp.saveEmpresa(_numeronit, _nombre, _mision, _vision, _direccion, _telefono, _telefono2, _correo);
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
            if (string.IsNullOrEmpty(EmpresaID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            _id = Convert.ToInt32(EmpresaID.Value);
            _numeronit = TBNumeronit.Text;
            _nombre = TBNombre.Text;
            _mision = TBMision.Text;
            _vision = TBVision.Text;
            _direccion = TBDireccion.Text;
            _telefono = TBTelefono.Text;
            _telefono2 = TBTelefono2.Text;
            _correo = TBCorreo.Text;
            execute = objEmp.updateEmpresa(_id, _numeronit, _nombre, _mision, _vision, _direccion, _telefono, _telefono2, _correo);
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