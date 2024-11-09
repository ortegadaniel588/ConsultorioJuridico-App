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
    public class CitaLog
    {
        CitaDat objCita = new CitaDat();

        public DataSet ShowCitas()
        {
            return objCita.ShowCitas();
        }

        public bool InsertCita(int horarioId, string asunto, string estado)
        {
            return objCita.InsertCita(horarioId, asunto, estado);
        }

        public bool UpdateCita(int idCita, int horarioId, string asunto, string estado)
        {
            return objCita.UpdateCita(idCita, horarioId, asunto, estado);
        }

        public bool DeleteCita(int idCita)
        {
            return objCita.DeleteCita(idCita);
        }
    }
}