using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;

namespace Logic
{
    public class RolLog
    {
        RolDat objRol = new RolDat();

        public DataSet showRolesDDL()
        {
            return objRol.showRolesDDL();
        }

        public DataSet showRoles()
        {
            return objRol.showRoles();
        }

        public bool saveRol(string nombre, string descripcion)
        {
            return objRol.saveRol(nombre, descripcion);
        }

        public bool updateRol(int idrol, string nombre, string descripcion)
        {

            return objRol.updateRol(idrol, nombre, descripcion);
        }

        public bool deleteRol(int idrol)
        {

            return objRol.deleteRol(idrol);
        }
    }
}