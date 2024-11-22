using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class EmpresaLog
    {
        EmpresaDat objEmp = new EmpresaDat();
        //Metodo para mostrar todas las Empresa
        public DataSet showEmpresa()
        {
            return objEmp.showEmpresa();
        }

        public DataSet showEmpresaDDL()
        {
            return objEmp.showEmpresaDDL();
        }

        public bool saveEmpresa(string _numeronit, string _nombre, string _mision, string _vision, string _direccion, string _telefono, string _telefono2, string _correo)
        {
            return objEmp.saveEmpresa(_numeronit, _nombre, _mision, _vision, _direccion, _telefono, _telefono2, _correo);
        }

        public bool updateEmpresa(int _id, string _numeronit, string _nombre, string _mision, string _vision, string _direccion, string _telefono, string _telefono2, string _correo)
        {
            return objEmp.updateEmpresa(_id, _numeronit, _nombre, _mision, _vision, _direccion, _telefono, _telefono2, _correo);
        }

        public bool deleteEmpresa(int _id)
        {
            return objEmp.deleteEmpresa(_id);
        }
    }
}