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
    public class CasoLog
    {
        CasoDat objCas = new CasoDat();
        public DataSet showCasoDDL()
        {
            return objCas.showCasoDDL();
        }

        //Metodo para mostrar todas las Caso
        public DataSet showCaso()
        {
            return objCas.showCaso();
        }
        //Metodo para guardar un nuevo Caso
        public bool saveCaso(string _codigo, string _nombre, int _empresa_id, DateTime _fechacierre, string _asunto, int _tipo_id, int _estado_id, string _complejidad, int _empleado_id)
        {
            return objCas.saveCaso(_codigo, _nombre, _empresa_id,  _fechacierre, _asunto, _tipo_id, _estado_id, _complejidad, _empleado_id);
        }

        //Metodo para actulizar un Caso
        public bool updateCaso(int _id, string _codigo, string _nombre, int _empresa_id, DateTime _fechacierre, string _asunto, int _tipo_id, int _estado_id, string _complejidad, int _empleado_id)
        {
            return objCas.updateCaso( _id,  _codigo, _nombre,  _empresa_id,  _fechacierre,  _asunto,  _tipo_id,  _estado_id,  _complejidad,  _empleado_id);
        }

        //Metodo para borrar una Caso
        public bool deleteCaso(int _id)
        {
            return objCas.deleteCaso(_id);
        }
    }
}