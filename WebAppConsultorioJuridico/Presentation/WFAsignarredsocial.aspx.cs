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
    public partial class WFAsignarredsocial : System.Web.UI.Page
    {
        AsignarredsocialLog objAsig = new AsignarredsocialLog();
        EmpresaLog objEmp = new EmpresaLog();  
        RedsocialLog objRed = new RedsocialLog();


        private int _id;
        private int _empresa_idempresa;
        private int _redsocial_idredsocial;
        private string _url;
        private bool execute = false;

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmAsignarRedSocial.Visible = false;
                PanelAdmin.Visible = false;
                //showAsignarredsocial();
                showEmpresaDDL();
                showRedsocialDDL();

            }
            validatePermissionRol();
        }

        /*private void showAsignarredsocial()
        {
            DataSet objData = new DataSet();
            objData = objAsig.showAsignarredsocial();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/

        [WebMethod]
        public static object listAsignarredessociales()
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
                    FkEmpresa = row["empresa_idempresa"],
                    EmpresaNombre = row["empresa_nombre"], // Nombre de la empresa
                    FKRedsocial = row["redsocial_idredsocial"],
                    RedsocialNombre = row["redsocial_nombre"], // Nombre de la red social
                    Url = row["url"]

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = asignarredessocialesList };
        }


        //Comentado Eliminar por integridad de Datos
	    [WebMethod]
        public static bool DeleteAsignarredsocial(int id)
        {
            // Crear una instancia de la clase de lógica de asignarredsocial
            AsignarredsocialLog objAsig = new AsignarredsocialLog();

            // Invocar al método para eliminar el asignar red social y devolver el resultado
            return objAsig.deleteAsignarredsocial(id);
        }

        // Metodo validar permisos roles
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
                            FrmAsignarRedSocial.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarRedSocial.Visible = true;
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
            else if (userRole == "Gerente")
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
                            FrmAsignarRedSocial.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarRedSocial.Visible = true;
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
                            FrmAsignarRedSocial.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarRedSocial.Visible = true;
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

        private void showEmpresaDDL()
        {
            DDLEmpresa_idempresa.DataSource = objEmp.showEmpresaDDL();
            DDLEmpresa_idempresa.DataValueField = "idempresa";  // ID numérico
            DDLEmpresa_idempresa.DataTextField = "nombre";      // Nombre visible
            DDLEmpresa_idempresa.DataBind();
            DDLEmpresa_idempresa.Items.Insert(0, "Seleccione");
        }

        private void showRedsocialDDL()
        {
            DDLRedsocial_idredsocial.DataSource = objRed.showRedsocialDDL();
            DDLRedsocial_idredsocial.DataValueField = "idredsocial";  // ID numérico
            DDLRedsocial_idredsocial.DataTextField = "nombre";
            DDLRedsocial_idredsocial.DataBind();
            DDLRedsocial_idredsocial.Items.Insert(0, "Seleccione");
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            AsignarredsocialID.Value = "";
            DDLEmpresa_idempresa.SelectedIndex = 0;
            DDLRedsocial_idredsocial.SelectedIndex = 0;
            TBUrl.Text = "";


        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                _empresa_idempresa = Convert.ToInt32(DDLEmpresa_idempresa.SelectedValue);
                _redsocial_idredsocial = Convert.ToInt32(DDLRedsocial_idredsocial.SelectedValue);
                _url = TBUrl.Text;
                execute = objAsig.saveAsignarredsocial(_empresa_idempresa, _redsocial_idredsocial, _url);
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
            execute = objAsig.updateAsignarredsocial(_id, _empresa_idempresa, _redsocial_idredsocial, _url);
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