using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class PermisoLog
    {
        PermisoDat objPerm = new PermisoDat();

        //Metodo para mostrar todos los Permisos
        public DataSet showPermission()
        {
            return objPerm.showPermission();
        }
        //Metodo para mostrar unicamente el id y el nombre de los Permisos
        public DataSet showPermissionDDL()
        {
            return objPerm.showPermissionDDL();
        }
        //Metodo para guardar un nuevo Permiso
        public bool savePermission(string _name, string _description)
        {
            return objPerm.savePermission(_name, _description);
        }
        //Metodo para actualizar un Permiso
        public bool updatePermission(int _idPermission, string _name, string _description)
        {
            return objPerm.updatePermission(_idPermission, _name, _description);
        }
        //Metodo para borrar un Permiso
        public bool deletePermission(int _idPermission)
        {
            return objPerm.deletePermission(_idPermission);
        }
    }
}