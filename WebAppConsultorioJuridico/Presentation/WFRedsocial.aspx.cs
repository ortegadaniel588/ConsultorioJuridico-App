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
    public partial class WFRedsocial : System.Web.UI.Page
    {
        RedsocialLog objReds = new  RedsocialLog();
        private string _nombre;
        private string _descripcion;
        private int _id;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showRedsocial();
            }
        }

        /*private void showRedsocial() 
        {
            DataSet objData = new DataSet();
            objData = objReds.showRedsocial();
            GVRedsocial.DataSource = objData;
            GVRedsocial.DataBind();
        }*/

	[WebMethod]
        public static object ListRedesSociales()
        {
            RedsocialLog objCas = new RedsocialLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objRed.showRedsocial();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var redessocialesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un caso).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                redessocialesList.Add(new
                {
                    RedsocialID = row["idredsocial"],
                    Nombre = row["nombre"],
                    Descripcion = row["descripcion"],
                    
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = redessocialesList};
        }

        /* Comentado Eliminar por integridad de Datos
	[WebMethod]
        public static bool DeleteRedsocial(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            RedsocialLog objRed = new RedsocialLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objRed.deleteRedsocial(id);
        }*/

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;
            execute = objReds.saveRedsocial(_nombre, _descripcion);
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
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;
            execute = objReds.saveRedsocial(_nombre, _descripcion);
            if (execute)
            {
                LblMsj.Text = "Se actualizo exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al actualizar";
            }
        }

        protected void GVRedsocial_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBId.Text = GVRedsocial.SelectedRow.Cells[0].Text;
            TBNombre.Text = GVRedsocial.SelectedRow.Cells[1].Text;
            TBDescripcion.Text = GVRedsocial.SelectedRow.Cells[2].Text;
        }

        protected void GVRedsocial_RowDeleting(object senderm, GridViewDeleteEventArgs e) 
        {
            int _idRedsocial = Convert.ToInt32(GVRedsocial.DataKeys[e.RowIndex].Values[0]);
            execute = objReds.deleteRedsocial(_idRedsocial);
            if (execute)
            {
                LblMsj.Text = "Se eliminó exitosamente";
            }
            else
            {
                LblMsj.Text = "Error al eliminar";
            }
        }


    }
}
