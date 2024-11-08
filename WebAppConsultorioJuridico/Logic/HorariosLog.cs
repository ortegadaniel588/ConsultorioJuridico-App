using Data;
using System;
using System.Data;

namespace Logic
{
    public class HorarioLog
    {
        HorarioDat objHorario = new HorarioDat();

        public DataSet ShowHorariosDDL()
        {
            return objHorario.showHorariosDDL();
        }

        public DataSet ShowHorarios()
        {
            return objHorario.ShowHorarios();
        }

        public bool InsertHorario(int empleadoId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            return objHorario.InsertHorario(empleadoId, fecha, horaInicio, horaFin);
        }

        public bool UpdateHorario(int idHorario, int empleadoId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            return objHorario.UpdateHorario(idHorario, empleadoId, fecha, horaInicio, horaFin);
        }

        public bool DeleteHorario(int idHorario)
        {
            return objHorario.DeleteHorario(idHorario);
        }

        public DataSet ShowHorariosDisponibles(int especialidadId, DateTime fecha)
        {
            return objHorario.ShowHorariosDisponibles(especialidadId, fecha);
        }
    }
}