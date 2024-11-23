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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showCountUsers();
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListCountProductsCategories()
        {
            ProductsLog objProd = new ProductsLog();

            // Se obtiene un DataSet que contiene la lista de productos que existen por categoria
            var dataSet = objProd.showCountProductsCategories();

            // Se crea una lista para almacenar las cantidades que de productos x categorias 
            var prodCatList = new List<object>();

            // Se itera sobre cada fila del DataSet.
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                prodCatList.Add(new
                {
                    CategoryName = row["Categoria"],
                    TotalProducts = row["TotalProductos"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos x categorias.
            return new { data = prodCatList };
        }

        // Metodo para validar permisos roles
        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("Default.aspx");
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
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        case "ACTUALIZAR":
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        case "MOSTRAR":
                            // Aqui van las acciones para los elementos del WFInicio
                            //LblMsg.Text += " Tienes permiso de Mostrar!";

                            break;
                        case "ELIMINAR":
                            // Aqui van las acciones para los elementos del WFInicio
                            //LblMsg.Text += " Tienes permiso de Eliminar!";

                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Abogado")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUser.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermission.Visible = false;
                masterPage.linkPermissionRol.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        case "ACTUALIZAR":
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        case "MOSTRAR":
                            // Aqui van las acciones para los elementos del WFInicio
                            //LblMsg.Text += " Tienes permiso de Mostrar!";

                            break;
                        case "ELIMINAR":
                            // Aqui van las acciones para los elementos del WFInicio
                            //LblMsg.Text += " Tienes permiso de Eliminar!";

                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Secretaria")
            {
                //LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkUser.Visible = false;
                masterPage.linkPermission.Visible = false;
                masterPage.linkPermissionRol.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        case "ACTUALIZAR":
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        case "MOSTRAR":
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        case "ELIMINAR":
                            // Aqui van las acciones para los elementos del WFInicio

                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }
        }
        private void showCountUsers()
        {
            int count = objUsu.showCountUsers();
            LblCantUsu.Text = count.ToString();
        }
    }
}