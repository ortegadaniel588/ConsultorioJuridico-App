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
    public partial class WFInicio : System.Web.UI.Page
    {
        //Crear los objetos
        UsuariosLog objUsu = new UsuariosLog();
        EmpleadoLog objEmp = new EmpleadoLog();
        CasoHasPersonaLog objCp = new CasoHasPersonaLog();
        CasoLog objCas = new CasoLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showCountUsers();
                showCountEmpleados();   
                showCountClientes();
                showCountCasos();
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListCountCasosEstados()
        {
            CasoLog objCas = new CasoLog();
            // Se obtiene un DataSet que contiene la lista de casos que existen por estado
            var dataSet = objCas.showCountCasosEstados();
            // Se crea una lista para almacenar las cantidades que de productos x categorias 
            var casosEstadoList = new List<object>();
            // Se itera sobre cada fila del DataSet.
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                casosEstadoList.Add(new
                {
                    EstadoName = row["Nombre"],
                    TotalCasos = row["TotalCasos"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos x categorias.
            return new { data = casosEstadoList };
        }

        // Metodo para validar permisos roles
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
                            FrmInicio.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmInicio.Visible = true;
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
                            FrmInicio.Visible = true;
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
                masterPage.linkCasos.Visible = true;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmInicio.Visible = true;
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
        private void showCountUsers()
        {
            int count = objUsu.showCountUsers();
            LblCantUsu.Text = count.ToString();
        }

        private void showCountEmpleados()
        {
            int count = objEmp.showCountEmpleados();
            LblCantEmp.Text = count.ToString();
        }
        private void showCountClientes()
        {
            int count = objCp.showCountClientes();
            LblCantClient.Text = count.ToString();
        }

        private void showCountCasos()
        {
            int count = objCas.showCountCasos();
            LblCantCasos.Text = count.ToString();
        }

        //[WebMethod]
        //public static object GetTendenciaCasosCerrados()
        //{
        //    CasoLog objCasoLogic = new CasoLog();

        //    // Se obtiene un DataSet con los datos de tendencia desde la capa lógica
        //    var dataSet = objCasoLogic.spTendenciaCasosCerradosPorMes();

        //    // Lista para almacenar los datos procesados
        //    var casosList = new List<object>();

        //    // Se recorre cada fila del DataSet
        //    foreach (DataRow row in dataSet.Tables[0].Rows)
        //    {
        //        casosList.Add(new
        //        {
        //            Anio = row["anio"],
        //            Mes = row["mes"],
        //            TotalCasos = row["total_casos"]
        //        });
        //    }

        //    // Devuelve los datos en formato JSON
        //    return new { data = casosList };
        //}

        //[WebMethod]
        //public static object GetAsignacionCitasPorMes()
        //{
        //    // Crear una instancia de la capa lógica o de acceso a datos
        //    CasoLog objCasoLogic = new CasoLog();

        //    // Obtener los datos desde el procedimiento almacenado
        //    var dataSet = objCasoLogic.spAsignacionCitasPorMes();

        //    // Crear una lista para los datos que se van a devolver
        //    var citasList = new List<object>();

        //    // Iterar sobre cada fila del DataSet
        //    foreach (DataRow row in dataSet.Tables[0].Rows)
        //    {
        //        citasList.Add(new
        //        {
        //            MesActual = row["MesActual"], // Nombre de la columna en el procedimiento almacenado
        //            TotalCitasAsignadas = row["TotalCitasAsignadas"] // Nombre de la columna en el resultado
        //        });
        //    }

        //    // Devolver un objeto en formato JSON con la lista de citas
        //    return new { data = citasList };
        //}


        [WebMethod]
        public static object GetTendenciaCasosCerrados()
        {
            // Datos de prueba simulados
            var casosTendencia = new List<object>
            {
                new { Mes = "Enero", Anio = "2024", TotalCasos = 3 },
                new { Mes = "Febrero", Anio = "2024", TotalCasos = 5 },
                new { Mes = "Marzo", Anio = "2024", TotalCasos = 4 },
                new { Mes = "Abril", Anio = "2024", TotalCasos = 7 },
                new { Mes = "Mayo", Anio = "2024", TotalCasos = 2 },
            };

            return new { data = casosTendencia };
        }

        [WebMethod]
        public static object GetAsignacionCitasPorMes()
        {
            var datosPrueba = new List<object>()
            {
                new { MesActual = "2024-01", TotalCitasAsignadas = 25 },
                new { MesActual = "2024-02", TotalCitasAsignadas = 30 },
                new { MesActual = "2024-03", TotalCitasAsignadas = 15 },
                new { MesActual = "2024-04", TotalCitasAsignadas = 40 }
            };

            return new { data = datosPrueba };
        }



    }
}