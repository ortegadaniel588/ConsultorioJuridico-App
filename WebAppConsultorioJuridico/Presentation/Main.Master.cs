using System;
using System.Web.UI.HtmlControls;

namespace Presentation
{
    public partial class Main : System.Web.UI.MasterPage
    {
        // Enlaces principales
        public HtmlAnchor linkInicio;
        public HtmlAnchor linkCasos;
        public HtmlAnchor linkExpedientes;
        public HtmlAnchor linkAsignarExpedientes;
        public HtmlAnchor linkSeguimiento;
        public HtmlAnchor linkCitas;
        public HtmlAnchor linkHorarios;
        public HtmlAnchor linkPersonas;
        public HtmlAnchor linkEmpresa;
        public HtmlAnchor linkEspecialidad;
        public HtmlAnchor linkEstado;
        public HtmlAnchor linkTipo;
        public HtmlAnchor linkRedsocial;
        public HtmlAnchor linkAsignarRedsocial;

        // Enlaces de seguridad
        public HtmlAnchor linkSecurity;
        public HtmlAnchor linkUser;
        public HtmlAnchor linkPermissions;
        public HtmlAnchor linkPermissionsRoles;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}