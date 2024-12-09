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
    public partial class WFAsignarSeguimiento : System.Web.UI.Page
    {
        SeguimientoLog objSeg = new SeguimientoLog();
        CasoLog objCas = new CasoLog();


        private int _id;
        private int _caso_id;
        private DateTime _fecha_actualizacion;
        private string _proceso;
        private string _descripcion;
        private string _estado;
        private static int _idcaso;
        private static string _nombre_caso;
        private bool executed = false;


        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmAsignarSeguimiento.Visible = false;
                PanelAdmin.Visible = false;
                //showSeguimiento();
                LBNombrecaso.Text = _nombre_caso;
                showCasoDDL();

            }
            validatePermissionRol();
        }

        /*/private void showSeguimiento()
        {
            DataSet objData = new DataSet();
            objData = objSeg.showSeguimiento();
            GVEmpresa.DataSource = objData;
            GVEmpresa.DataBind();
        }*/
        [WebMethod]
        public static object ListSeguimientos()
        {
            SeguimientoLog objSeg = new SeguimientoLog();

            // Se obtiene un DataSet que contiene la lista de seguimiento desde la base de datos.
            var dataSet = objSeg.showSeguimientoByIdCaso(_idcaso);

            // Se crea una lista para almacenar los seguimiento que se van a devolver.
            var seguimientosList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un seguimiento).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                seguimientosList.Add(new
                {
                    SeguimientoID = row["idseguimiento"],
                    FKCaso = row["caso_idcaso"],
                    Casocode = row["caso_codigo"],
                    Fechaactualizacion = Convert.ToDateTime(row["fechaactualizacion"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Proceso = row["proceso"],
                    Descripcion = row["descripcion"],
                    Estado = row["estado"],
                    Asunto = row["asunto"],
                    Fechaapertura = Convert.ToDateTime(row["fechadeapertura"]).ToString("yyyy-MM-dd"),// Formato de fecha específico
                    Fechacierre = Convert.ToDateTime(row["fechacierre"]).ToString("yyyy-MM-dd"),// Formato de fecha específico

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de seguimiento.
            return new { data = seguimientosList };
        }

        // Comentado Eliminar por integridad de Datos
        [WebMethod]
        public static bool DeleteSeguimiento(int id)
        {
            // Crear una instancia de la clase de lógica de seguimiento
            SeguimientoLog objSeg = new SeguimientoLog();

            // Invocar al método para eliminar el seguimiento y devolver el resultado
            return objSeg.deleteSeguimiento(id);
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
                            FrmAsignarSeguimiento.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarSeguimiento.Visible = true;
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
                masterPage.linkSeguimiento.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkSeguimiento.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAsignarSeguimiento.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarSeguimiento.Visible = true;
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
                            FrmAsignarSeguimiento.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAsignarSeguimiento.Visible = true;
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
            //DDCaso_idcaso.Items.Insert(0, "Seleccione");
        }

        private void clear()
        {
            SeguimientoID.Value = "";
            //DDCaso_idcaso.SelectedIndex = 0;
            TBFechaactualizacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBProceso.Text = "";
            TBDescripcion.Text = "";
            TBEstado.SelectedIndex = 0;
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {

           // _caso_id = Convert.ToInt32(DDCaso_idcaso.SelectedValue);
            _fecha_actualizacion = DateTime.Parse(TBFechaactualizacion.Text);
            _proceso = TBProceso.Text;
            _descripcion = TBDescripcion.Text;
            _estado = TBEstado.Text;
            executed = objSeg.saveSeguimiento(_idcaso, _fecha_actualizacion, _proceso, _descripcion, _estado);
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
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(SeguimientoID.Value))
            {
                LblMsj.Text = "No se ha seleccionado un segumiento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(SeguimientoID.Value);
            _caso_id = Convert.ToInt32(DDCaso_idcaso.SelectedValue);
            _fecha_actualizacion = DateTime.Parse(TBFechaactualizacion.Text);
            _proceso = TBProceso.Text;
            _descripcion = TBDescripcion.Text;
            _estado = TBEstado.Text;
            executed = objSeg.updateSeguimiento(_id, _caso_id, _fecha_actualizacion, _proceso, _descripcion, _estado);
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