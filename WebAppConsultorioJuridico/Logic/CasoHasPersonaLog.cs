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
    public class CasoHasPersonaLog
    {
        CasoHasPersonaDat objCp = new CasoHasPersonaDat();
        //Metodo para mostrar todas las caso has persona
        public DataSet showCasoHasPersona()
        {
            return objCp.showCasoHasPersona();
        }
        //Metodo para guardar un nuevo AsignarRedesSocial
        public bool saveCasoHasPersona(int _caso_idcaso, int _persona_idpersona)
        {
            return objCp.saveCasoHasPersona(_caso_idcaso, _persona_idpersona);
        }

        //Metodo para actulizar un AsignarRedesSocial
        public bool updateCasoHasPersona(int _id, int _caso_idcaso, int _persona_idpersona)
        {
            return objCp.updateCasoHasPersona(_id, _caso_idcaso, _persona_idpersona);
        }

        //Metodo para borrar una AsignarRedesSocial
        public bool deleteCasoHasPersona(int _id)
        {
            return objCp.deleteCasoHasPersona(_id);
        }
    }
}