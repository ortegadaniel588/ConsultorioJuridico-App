using Logic;
using Model;
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
    public partial class WFEstado : System.Web.UI.Page
    {
        EstadoLog objEst = new EstadoLog();
        private string nombre;
        private string descripcion;
        private int idestado;
        private bool execute = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            var userRole = objUser.Rol.Nombre;
            if (objUser.Permisos == null || !objUser.Permisos.Any())
            {
                LblMsg.Text = "El usuario no tiene permisos asignados.";
                return;
            }

            if (userRole == "Administrador")
            {
                LblMsg.Text = "Bienvenido, Administrador!";
                // Tiene acceso a todo, no necesita ocultar nada

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmEstado.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmEstado.Visible = true;
                            break;
                        case "MOSTRAR":
                            // Configuración para mostrar
                            break;
                        case "ELIMINAR":
                            // Configuración para eliminar
                            break;
                        default:
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Abogado")
            {
                LblMsg.Text = "Bienvenido, Abogado!";

                masterPage.linkUser.Visible = false;
                masterPage.linkPermissions.Visible = false;
                masterPage.linkPermissionsRoles.Visible = false;
                masterPage.linkSecurity.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "ACTUALIZAR":
                            FrmEstado.Visible = true;
                            break;
                        case "MOSTRAR":
                            // Configuración para mostrar casos
                            break;
                        default:
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Secretario")
            {
                LblMsg.Text = "Bienvenido, Secretario!";

                masterPage.linkPermissions.Visible = false;
                masterPage.linkPermissionsRoles.Visible = false;
                masterPage.linkSecurity.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmEstado.Visible = true;
                            break;
                        case "MOSTRAR":
                            // Configuración para mostrar registros
                            break;
                        default:
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                LblMsg.Text = "Rol no reconocido.";
                Response.Redirect("WFInicio.aspx");
            }
        }


        [WebMethod]
        public static object ListEstado()
        {
            EstadoLog objEst = new EstadoLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objEst.showEstado();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var EstadoList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Estado).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                EstadoList.Add(new
                {
                    EstadoID = row["idestado"],
                    Nombre = row["nombre"],
                    Descripcion = row["descripcion"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = EstadoList };
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            EstadoID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        //Comentado Eliminar por integridad de Datos
        [WebMethod]
        public static bool deleteEstado(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            EstadoLog objEst = new EstadoLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objEst.deleteEstado(id);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objEst.saveEstado(nombre, descripcion);
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
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(EstadoID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }

            idestado = Convert.ToInt32(EstadoID.Value);
            nombre = TBNombre.Text;
            descripcion = TBDescripcion.Text;
            execute = objEst.updateEstado(idestado, nombre, descripcion);
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
