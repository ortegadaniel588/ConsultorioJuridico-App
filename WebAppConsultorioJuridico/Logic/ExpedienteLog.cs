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
    public class ExpedienteLog
    {
        ExpedienteDat objExp = new ExpedienteDat();
        public DataSet showExpediente()
        {
            return objExp.showExpediente();
        }
        //Metodo para guardar un nuevo Expediente
        public bool saveExpediente(string _codigo, int _caso_idcaso, string _accionrealizada, string _razon, string _relevancia, string _evidencia, string _comentario, string _estado)
        {
            return objExp.saveExpediente( _codigo, _caso_idcaso, _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
        }

        //Metodo para actulizar un Expediente
        public bool updateExpediente(int _id, string _codigo, int _caso_idcaso, string _accionrealizada, string _razon, string _relevancia, string _evidencia, string _comentario, string _estado)
        {
            return objExp.updateExpediente(_id, _codigo, _caso_idcaso, _accionrealizada, _razon, _relevancia, _evidencia, _comentario, _estado);
        }

        //Metodo para borrar una Expediente
        public bool deleteExpediente(int _id)
        {
            return objExp.deleteExpediente(_id);
        }
    }
}