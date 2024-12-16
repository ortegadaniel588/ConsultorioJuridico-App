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
    public partial class WFAsignarPersona : System.Web.UI.Page
    {
        CasoHasPersonaLog objCp = new CasoHasPersonaLog();
        CasoLog objCas = new CasoLog();
        PersonaLog objPer = new PersonaLog();


        private int _id;
        private int _caso_idcaso;
        private int _persona_idpersona;
        private static int _idcaso;
        private static string _nombre_caso;
        private bool execute = false;


        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmAsignarPersona.Visible = false;
                PanelAdmin.Visible = false;
                //showAsignarredsocial();
                LBNombrecaso.Text = _nombre_caso;
                showCasoDDL();
                showPersonaDDL();


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
        public static object listCasoHasPersona()
        {
            CasoHasPersonaLog objCp = new CasoHasPersonaLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objCp.showCasoHasPersonaByIdCaso(_idcaso);

            // Se crea una lista para almacenar los productos que se van a devolver.
            var casohaspersonaList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un caso).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                casohaspersonaList.Add(new
                {
                    CasoHasPersonaID = row["idcaso_has_persona"],
                    FkCaso = row["caso_idcaso"],
                    Caso = row["caso_nombre"], // Nombre del caso
                    FKPersona = row["idpersona"], // ID de la persona asociada
                    Nombres = row["nombres"],
                    Apellidos = row["apellidos"],
                    TipoDocumento = row["tipodocumento"],
                    Documento = row["documento"],
                    Genero = row["genero"],
                    EstadoCivil = row["estadocivil"],
                    LugarNacimiento = row["lugar_nacimiento"],
                    FechaNacimiento = Convert.ToDateTime(row["fecha_nacimiento"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Telefono = row["telefono"],
                    TelefonoAlternativo = row["telefono_alternativo"],
                    Correo = row["correo"],
                    Direccion = row["direccion"],
                    Estrato = row["estrato"],
                    Ocupacion = row["ocupacion"],
                    NivelEscolaridad = row["nivel_escolaridad"]

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = casohaspersonaList };
        }


        //Comentado Eliminar por integridad de Datos
        [WebMethod]
        public static bool DeleteCasoHasPersona(int id)
        {
            // Crear una instancia de la clase de lógica de asignarredsocial
            CasoHasPersonaLog objCp = new CasoHasPersonaLog();

            // Invocar al método para eliminar el asignar red social y devolver el resultado
            return objCp.deleteCasoHasPersona(id);
        }

        [WebMethod]
        public static int extraerIdCaso(int id, string nombre)
        {
            // Escribir el ID en la consola para fines de depuración
            _idcaso = id;

            if (nombre != null)
            {
                _nombre_caso = nombre;

            }
            else
            {
                _nombre_caso = "";
            }
            System.Diagnostics.Debug.WriteLine("IDcaso: " + id + " Nombre: " + nombre);
            return id;

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
                            FrmAsignarPersona.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarPersona.Visible = true;
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
                            FrmAsignarPersona.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarPersona.Visible = true;
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
                            FrmAsignarPersona.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarPersona.Visible = true;
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

        private void showCasoDDL()
        {
            DDLCaso_idcaso.DataSource = objCas.showCasoDDL();
            DDLCaso_idcaso.DataValueField = "idcaso";  // ID numérico
            DDLCaso_idcaso.DataTextField = "nombre";      // Nombre visible
            DDLCaso_idcaso.DataBind();
            //DDLCaso_idcaso.Items.Insert(0, "Seleccione");
            DDLCaso_idcaso.SelectedValue = "" + _idcaso;
        }

        private void showPersonaDDL()
        {
            DDLPersona_idpersona.DataSource = objPer.showPersonasDDL();
            DDLPersona_idpersona.DataValueField = "idpersona";  // ID numérico
            DDLPersona_idpersona.DataTextField = "nombre_completo";
            DDLPersona_idpersona.DataBind();
            DDLPersona_idpersona.Items.Insert(0, "Seleccione");
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            CasoHasPersonaID.Value = "";
            //DDLCaso_idcaso.SelectedIndex = 0;
            DDLPersona_idpersona.SelectedIndex = 0;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //_caso_idcaso = Convert.ToInt32(DDLCaso_idcaso.SelectedValue);
                _persona_idpersona = Convert.ToInt32(DDLPersona_idpersona.SelectedValue);
                execute = objCp.saveCasoHasPersona(_idcaso, _persona_idpersona);
                if (execute)
                {
                    LblMsj.Style["color"] = "green";
                    LblMsj.Text = "Se guardo exitosamente";
                    clear();
                }
                else
                {
                    LblMsj.Style["color"] = "red";
                    LblMsj.Text = "Error al guardar";
                }
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(CasoHasPersonaID.Value))
            {
                LblMsj.Style["color"] = "red";
                LblMsj.Text = "No se ha seleccionado un implicado";
                return;
            }

            _id = Convert.ToInt32(CasoHasPersonaID.Value);
            _caso_idcaso = Convert.ToInt32(DDLCaso_idcaso.Text);
            _persona_idpersona = Convert.ToInt32(DDLPersona_idpersona.Text);
            execute = objCp.updateCasoHasPersona(_id, _caso_idcaso, _persona_idpersona);
            if (execute)
            {
                LblMsj.Style["color"] = "green";
                LblMsj.Text = "Se actualizo exitosamente";
                clear();
            }
            else
            {
                LblMsj.Style["color"] = "red";
                LblMsj.Text = "Error al actualizar";
            }
        }
    }
}