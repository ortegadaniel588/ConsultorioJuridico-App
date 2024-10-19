using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class UsuarioDat
    {
        Persistence objPer = new Persistence();

        public DataSet showUsuariosDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUsuarioDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet ShowUsuarios()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUsuarios";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool SaveUsuario(string usuario, string contrasena, int rolId, int personaId, string estado)
        {
            bool executed = false;
            int row = 0;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertUsuario";
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_usuario", MySqlDbType.VarChar).Value = usuario;
            objInsertCmd.Parameters.Add("p_contrasena", MySqlDbType.Text).Value = contrasena;
            objInsertCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = rolId;
            objInsertCmd.Parameters.Add("p_persona_id", MySqlDbType.Int32).Value = personaId;
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

        public bool UpdateUsuario(int id, string usuario, string contrasena, int rolId, int personaId, string estado)
        {
            bool executed = false;
            int row = 0;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateUsuario";
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;
            objUpdateCmd.Parameters.Add("p_usuario", MySqlDbType.VarChar).Value = usuario;
            objUpdateCmd.Parameters.Add("p_contrasena", MySqlDbType.Text).Value = contrasena;
            objUpdateCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = rolId;
            objUpdateCmd.Parameters.Add("p_persona_id", MySqlDbType.Int32).Value = personaId;
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

        public bool DeleteUsuario(int id)
        {
            bool executed = false;
            int row = 0;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteUsuario";
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;

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