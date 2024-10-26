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
    public class SeguimientoLog
    {
        SeguimientoDat objSeg = new SeguimientoDat();
        public DataSet showSeguimiento()
        {
            return objSeg.showSeguimiento();
        }
        //Metodo para guardar un nuevo Seguimiento
        public bool saveSeguimiento(int _caso_id, string _fecha_actualizacion, string _proceso, string _descripcion, string _estado)
        {
            return objSeg.saveSeguimiento(_caso_id, _fecha_actualizacion, _proceso, _descripcion, _estado);
        }

        //Metodo para actulizar un Seguimiento
        public bool updateSeguimiento(int _id, int _caso_id, string _fecha_actualizacion, string _proceso, string _descripcion, string _estado)
        {
            return objSeg.updateSeguimiento(_id, _caso_id, _fecha_actualizacion, _proceso, _descripcion, _estado);
        }

        //Metodo para borrar una Seguimiento
        public bool deleteSeguimiento(int _id)
        {
            return objSeg.deleteSeguimiento(_id);
        }
    }
}