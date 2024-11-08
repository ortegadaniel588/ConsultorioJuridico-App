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
    public class RedsocialLog
    {
        RedsocialDat objRed = new RedsocialDat();
        public DataSet showRedsocialDDL()
        {
            return objRed.showRedsocialDDL();
        }

        //Metodo para mostrar todas las RedesSociales
        public DataSet showRedsocial()
        {
            return objRed.showRedsocial();
        }
        //Metodo para guardar un nuevo RedesSociales
        public bool saveRedsocial(string _nombre, string _descripcion)
        {
            return objRed.saveRedsocial(_nombre, _descripcion);
        }

        //Metodo para actulizar un RedesSociales
        public bool updateRedsocial(int _id, string _nombre, int _descripcion)
        {
            return objRed.updateRedsocial(_id, _nombre, _descripcion);
        }

        //Metodo para borrar una RedesSociales
        public bool deleteRedsocial(int _id)
        {
            return objRed.deleteRedsocial(_id);
        }
    }
}