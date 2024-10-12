using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class CitaDat
    {
        Persistence objPer = new Persistence();

        public DataSet ShowCitas()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCitas";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool InsertCita(int horarioId, string asunto, string estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertCita";
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_horario_idhorario", MySqlDbType.Int32).Value = horarioId;
            objInsertCmd.Parameters.Add("p_asunto", MySqlDbType.MediumText).Value = asunto;
            objInsertCmd.Parameters.Add("p_estado", MySqlDbType.Enum).Value = estado;

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

        public bool UpdateCita(int idCita, int horarioId, string asunto, string estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateCita";
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_idcita", MySqlDbType.Int32).Value = idCita;
            objUpdateCmd.Parameters.Add("p_horario_idhorario", MySqlDbType.Int32).Value = horarioId;
            objUpdateCmd.Parameters.Add("p_asunto", MySqlDbType.MediumText).Value = asunto;
            objUpdateCmd.Parameters.Add("p_estado", MySqlDbType.Enum).Value = estado;

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

        public bool DeleteCita(int idCita)
        {
            bool executed = false;
            int row;

            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteCita";
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_idcita", MySqlDbType.Int32).Value = idCita;

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
    }
}