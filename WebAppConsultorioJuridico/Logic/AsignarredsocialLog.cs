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
    public class AsignarredsocialLog
    {
        AsignarredsocialDat objAsig = new AsignarredsocialDat();
        public DataSet showAsignarredsocial()
        {
            return objAsig.showAsignarredsocial();
        }
        //Metodo para guardar un nuevo AsignarRedesSocial
        public bool saveAsignarredsocial(int _empresa_idempresa, int _redsocial_idredsocial, string _url)
        {
            return objAsig.saveAsignarredsocial(_empresa_idempresa, _redsocial_idredsocial, _url);
        }

        //Metodo para actulizar un AsignarRedesSocial
        public bool updateAsignarredsocial(int _id, int _empresa_idempresa, int _redsocial_idredsocial, string _url)
        {

            return objAsig.updateAsignarredsocial(_id,_empresa_idempresa, _redsocial_idredsocial, _url);
        }

        //Metodo para borrar una AsignarRedesSocial
        public bool deleteAsignarredsocial(int _id)
        {
            return objAsig.deleteAsignarredsocial(_id);
        }
    }
}