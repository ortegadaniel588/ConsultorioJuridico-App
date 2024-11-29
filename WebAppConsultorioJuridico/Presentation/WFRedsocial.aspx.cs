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
    public partial class WFRedsocial : System.Web.UI.Page
    {
        RedsocialLog objReds = new  RedsocialLog();
        private string _nombre;
        private string _descripcion;
        private int _id;
        private bool execute = false;

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmRdsocial.Visible = false;
                PanelAdmin.Visible = false;
                //showRedsocial();
            }
            validatePermissionRol();
        }

        /*private void showRedsocial() 
        {
            DataSet objData = new DataSet();
            objData = objReds.showRedsocial();
            GVRedsocial.DataSource = objData;
            GVRedsocial.DataBind();
        }*/

        [WebMethod]
        public static object ListRedessociales()
        {
            RedsocialLog objRed = new RedsocialLog();

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

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            RedsocialID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";



        }

        // Comentado Eliminar por integridad de Datos
        [WebMethod]
        public static bool deleteRedsocial(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            RedsocialLog objRed = new RedsocialLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objRed.deleteRedsocial(id);
        }

        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("WFDefault.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;

            if (userRole == "Administrador")
            {
                //LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRdsocial.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmRdsocial.Visible = true;
                            BtnUpdate.Visible = true;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = true;// Se pone visible el panel
                            _showEditButton = true;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsj.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Abogado")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUser.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermissions.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkPermissionsRoles.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRdsocial.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmRdsocial.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsj.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Secretario")
            {
                //LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkUser.Visible = false;
                masterPage.linkPermissions.Visible = false;
                masterPage.linkPermissionsRoles.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRdsocial.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmRdsocial.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsj.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsj.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;
            execute = objReds.saveRedsocial(_nombre, _descripcion);
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
            // Verifica si se ha seleccionado un tipo para actualizar
            if (string.IsNullOrEmpty(RedsocialID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un Tipo para actualizar.";
                return;
            }

            _id = Convert.ToInt32(RedsocialID.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;
            execute = objReds.updateRedsocial(_id, _nombre, _descripcion);
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
