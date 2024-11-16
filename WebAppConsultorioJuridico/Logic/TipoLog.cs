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
    public class TipoLog
    {
        TipoDat objTipo = new TipoDat();

        public DataSet showTipoDDL()
        {
            return objTipo.showTipo();
        }

        public DataSet showTipo()
        {
            return objTipo.showTipo();
        }

        public bool saveTipo(string nombre, string descripcion)
        {
            return objTipo.saveTipo(nombre,descripcion);
        }

        public bool updateTipo(int idtipo, string nombre, string descripcion)
        {

            return objTipo.updateTipo(idtipo,nombre,descripcion);
        }

        public bool deleteTipo(int idtipo)
        {

            return objTipo.deleteTipo(idtipo);
        }
    }
}