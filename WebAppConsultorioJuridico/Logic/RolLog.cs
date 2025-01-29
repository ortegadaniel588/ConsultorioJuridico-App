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

        public DataSet showRoles()
        {
            return objRol.showRoles();
        }

        // Metodo para cargar los roles en el DDL
        public DataSet showRolesDDL()
        {
            return objRol.showRolesDDL();
        }


        //Metodo para guardar un Rol
        public bool saveRoles(string _nombre, string _descripcion)
        {
            return objRol.saveRoles(_nombre, _descripcion);

        }

        //Metodo para actualizar un Rol
        public bool updateRol(int _id, string _nombre, string _descripcion)
        {
            return objRol.updateRol(_id, _nombre, _descripcion);
        }

        //Metodo para eliminar un Rol
        public bool deleteRol(int _id)
        {
            return objRol.deleteRol(_id);
        }
    }
}