using Data;
using System;
using System.Data;

namespace Logic
{
    public class PermisoLog
    {
        PermisoDat objPermiso = new PermisoDat();

        public DataSet ShowPermisos()
        {
            return objPermiso.showPermisos();
        }

        public bool SavePermiso(string nombre, string descripcion)
        {
            return objPermiso.savePermiso(nombre, descripcion);
        }

        public bool UpdatePermiso(int idpermiso, string nombre, string descripcion)
        {
            return objPermiso.updatePermiso(idpermiso, nombre, descripcion);
        }

        public bool DeletePermiso(int idpermiso)
        {
            return objPermiso.deletePermiso(idpermiso);
        }
    }
}