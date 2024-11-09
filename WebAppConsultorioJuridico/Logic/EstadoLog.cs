using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class EstadoLog
    {
        EstadoDat objEst = new EstadoDat();

        public DataSet ShowEstadoDDL()
        {
            return objEstado.showEstadoDDL();
        }
                
        public DataSet showEstado()
        {
            return objEst.showEstado();
        }
        //Metodo para guardar un nuevo Estado
        public bool saveEstado(string nombre,string descripcion)
        {
            return objEst.saveEstado(nombre,descripcion);
        }

        //Metodo para actulizar un Estado
        public bool updateEstado(int idestado, string nombre, string descripcion)
        {
            return objEst.updateEstado(idestado,nombre,descripcion);
        }

        //Metodo para borrar un Estado
        public bool deleteEstado(int idestado)
        {
            return objEst.deleteEstado(idestado);
        }
    }
}