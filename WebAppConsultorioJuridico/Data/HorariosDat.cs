using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class HorarioDat
    {
        Persistence objPer = new Persistence();

        public DataSet ShowHorarios()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectHorarios";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool InsertHorario(int empleadoId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertHorario";
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_empleado_idempleado", MySqlDbType.Int32).Value = empleadoId;
            objInsertCmd.Parameters.Add("p_fecha", MySqlDbType.Date).Value = fecha;
            objInsertCmd.Parameters.Add("p_horainicio", MySqlDbType.Time).Value = horaInicio;
            objInsertCmd.Parameters.Add("p_horafin", MySqlDbType.Time).Value = horaFin;

            try
            {
                row = objInsertCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        public bool UpdateHorario(int idHorario, int empleadoId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateHorario";
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_idhorario", MySqlDbType.Int32).Value = idHorario;
            objUpdateCmd.Parameters.Add("p_empleado_idempleado", MySqlDbType.Int32).Value = empleadoId;
            objUpdateCmd.Parameters.Add("p_fecha", MySqlDbType.Date).Value = fecha;
            objUpdateCmd.Parameters.Add("p_horainicio", MySqlDbType.Time).Value = horaInicio;
            objUpdateCmd.Parameters.Add("p_horafin", MySqlDbType.Time).Value = horaFin;

            try
            {
                row = objUpdateCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        public bool DeleteHorario(int idHorario)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteHorario";
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_idhorario", MySqlDbType.Int32).Value = idHorario;

            try
            {
                row = objDeleteCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para seleccionar horarios disponibles por especialidad y fecha
        public DataSet ShowHorariosDisponibles(int especialidadId, DateTime fecha)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectHorariosDisponibles";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_especialidad_id", MySqlDbType.Int32).Value = especialidadId;
            objSelectCmd.Parameters.Add("p_fecha", MySqlDbType.Date).Value = fecha;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}