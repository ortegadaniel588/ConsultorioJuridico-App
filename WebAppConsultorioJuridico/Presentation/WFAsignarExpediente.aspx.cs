using Logic;
using Model;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAsignarExpediente : System.Web.UI.Page
    {
        ExpedienteLog objEsp = new ExpedienteLog();
        CasoLog objCas = new CasoLog();



        private int _id;
        private int _caso_idcaso;
        private string _codigo;
        private string _accionrealizada;
        private string _razon;
        private string _relevancia;
        private string _evidencia;
        private string _comentario;
        private string _estado;
        private static string _nombre_caso;
        private static int _idcaso;
        private string rutaEvidencia;

        private bool executed = false;

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmAsignarExpediente.Visible = false;
                PanelAdmin.Visible = false;
                //showExpediente();
                showCasoDDL();
                LBNombrecaso.Text = _nombre_caso;
            }
            validatePermissionRol();
        }

        /*private void showExpediente()
        {
            DataSet objData = new DataSet();
            objData = objExp.showExpediente();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/
        [WebMethod]
        public static object ListExpedientes()
        {
            ExpedienteLog objExp = new ExpedienteLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objExp.showExpedienteByIdCaso(_idcaso);

            // Se crea una lista para almacenar los productos que se van a devolver.
            var expedienteList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Expediente).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                expedienteList.Add(new
                {
                    ExpedienteID = row["idexpendiente"],
                    FKCaso = row["caso_idcaso"],
                    Caso = row["caso_nombre"],
                    Codigo = row["codigo"],
                    Fechacreacion = Convert.ToDateTime(row["cracionfecha"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Accionrealizada = row["accionrealizada"],
                    Razon = row["razon"],
                    Relevancia = row["relevancia"],
                    Evidencia = row["evidencia"],
                    Comentario = row["comentario"],
                    Estado = row["estado"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = expedienteList };
        }

        // Comentado Eliminar por integridad de Datos
        [WebMethod]
        public static bool DeleteExpediente(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ExpedienteLog objExp = new ExpedienteLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objExp.deleteExpediente(id);
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
            else {
                _nombre_caso = "";
            }
            System.Diagnostics.Debug.WriteLine("IDcaso: " + id + " Nombre: " + nombre);
            return id;

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
                            FrmAsignarExpediente.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarExpediente.Visible = true;
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
                masterPage.linkConfiguration.Visible = false;
                masterPage.linkPersonas.Visible = false;
                masterPage.linkEspecialidad.Visible = false;
                masterPage.linkEmpresa.Visible = false;
                masterPage.linkAsignarRedsocial.Visible = false;
                masterPage.linkRedsocial.Visible = false;
                masterPage.linkEstado.Visible = false;
                masterPage.linkTipo.Visible = false;
                masterPage.linkSecurity.Visible = false;
                masterPage.linkRol.Visible = false;
                masterPage.linkEmpleados.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAsignarExpediente.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarExpediente.Visible = true;
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
                masterPage.linkPermissions.Visible = false;
                masterPage.linkPermissionsRoles.Visible = false;
                masterPage.linkSecurity.Visible = false;
                masterPage.linkEmpleados.Visible = false;
                masterPage.linkCasos.Visible = true;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAsignarExpediente.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarExpediente.Visible = true;
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
            DDCaso_idcaso.DataSource = objCas.showCasoDDL();
            DDCaso_idcaso.DataValueField = "idcaso";
            DDCaso_idcaso.DataTextField = "nombre";
            DDCaso_idcaso.DataBind();
            DDCaso_idcaso.SelectedValue = "" + _idcaso;
            //DDCaso_idcaso.Items.Insert(0, _nombre_caso);
        }


        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            ExpedienteID.Value = "";
            //DDCaso_idcaso.SelectedIndex = 0;
            TBCodigo.Text = "";
            TBAccionrealizada.Text = "";
            TBRazon.Text = "";
            DDLRelevancia.SelectedIndex = 0;
            TBEvidencia.Text = "";
            TBComentario.Text = "";
            DDLEstado.SelectedIndex = 0;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //_caso_idcaso = Convert.ToInt32(DDCaso_idcaso.SelectedValue);
            try
            {
                // Validar que se haya seleccionado un archivo
                if (TBEvidencia2.HasFile)
                {
                    // Obtener el archivo
                    HttpPostedFile file = TBEvidencia2.PostedFile;

                    // Validar el tamaño del archivo (opcional)
                    //if (file.ContentLength > 0 && file.ContentLength < 10485760) // 10 MB
                    if (file.ContentLength > 0)
                    {
                        // Guardar el archivo en el servidor
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Server.MapPath("~/resources/uploads/") + fileName;
                        file.SaveAs(filePath);

                        // Aquí puedes guardar la ruta del archivo en la base de datos
                        rutaEvidencia = "/resources/uploads/" + fileName;

                        // Llamar a tu método para guardar el expediente en la base de datos
                        // GuardarExpediente(rutaEvidencia);
                    }
                    else
                    {
                        LblMsj.Text = "El archivo es demasiado grande o no se ha seleccionado ningún archivo.";
                        LblMsj.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    LblMsj.Text = "Por favor, seleccione un archivo de evidencia.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al subir el archivo: " + ex.Message;
                LblMsj.ForeColor = System.Drawing.Color.Red;
            }
            _codigo = TBCodigo.Text;
            _accionrealizada = TBAccionrealizada.Text;
            _razon = TBRazon.Text;
            _relevancia = DDLRelevancia.Text;
            _evidencia = rutaEvidencia;
            _comentario = TBComentario.Text;
            _estado = DDLEstado.Text;
            executed = objEsp.saveExpediente(_idcaso, _codigo, _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
            if (executed)
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


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se haya seleccionado un archivo
                if (TBEvidencia2.HasFile)
                {
                    // Obtener el archivo
                    HttpPostedFile file = TBEvidencia2.PostedFile;

                    // Validar el tamaño del archivo (opcional)
                    //if (file.ContentLength > 0 && file.ContentLength < 10485760) // 10 MB
                    if (file.ContentLength > 0)
                    {
                        // Guardar el archivo en el servidor
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Server.MapPath("~/resources/uploads/") + fileName;
                        file.SaveAs(filePath);

                        // Aquí puedes guardar la ruta del archivo en la base de datos
                        rutaEvidencia = "/resources/uploads/" + fileName;

                        // Llamar a tu método para actualizar el expediente en la base de datos
                        // ActualizarExpediente(rutaEvidencia);
                    }
                    else
                    {
                        LblMsj.Text = "El archivo es demasiado grande o no se ha seleccionado ningún archivo.";
                        LblMsj.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    LblMsj.Text = "Por favor, seleccione un archivo de evidencia.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al subir el archivo: " + ex.Message;
                LblMsj.ForeColor = System.Drawing.Color.Red;
            }
            // Verifica si se ha seleccionado un expediente para actualizar
            if (string.IsNullOrEmpty(ExpedienteID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un expediente para actualizar.";
                return;
            }
            
            _id = Convert.ToInt32(ExpedienteID.Value);
            _caso_idcaso = Convert.ToInt32(DDCaso_idcaso.SelectedValue);
            _codigo = TBCodigo.Text;
            _accionrealizada = TBAccionrealizada.Text;
            _razon = TBRazon.Text;
            _relevancia = DDLRelevancia.Text;
            
            if (rutaEvidencia != null) {
                _evidencia = rutaEvidencia;
            }
            else {
                _evidencia = TBEvidencia.Text;
            }
            
            _comentario = TBComentario.Text;
            _estado = DDLEstado.Text;
            executed = objEsp.updateExpediente(_id, _caso_idcaso, _codigo, _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
            
            if (executed)
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