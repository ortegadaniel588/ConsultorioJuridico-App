using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class RolLog
    {
        RolDat objRol = new RolDat();

        // Metodo para cargar los roles en el DDL
        public DataSet showRolesDDL()
        {
            return objRol.showRolesDDL();
        }
    }
}