using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class SeguimientoDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todas las Seguimiento
        public DataSet showSeguimiento()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectSeguimientos";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para guardar un nuevo Seguimiento
        public bool saveSeguimiento(string _codigo, string _fecha, string _accionrealizada, string _relevancia, string _evidencia, string _comentario, string _estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertSeguimiento";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            
            objSelectCmd.Parameters.Add("p_codigo", MySqlDbType.VarString).Value = _codigo;
            objSelectCmd.Parameters.Add("p_fecha", MySqlDbType.VarString).Value = _fecha;
            objSelectCmd.Parameters.Add("p_accionrealizada", MySqlDbType.VarString).Value = _accionrealizada;
            objSelectCmd.Parameters.Add("p_razon", MySqlDbType.VarString).Value = _razon;
            objSelectCmd.Parameters.Add("p_relevancia", MySqlDbType.VarString).Value = _relevancia;
            objSelectCmd.Parameters.Add("p_evidencia", MySqlDbType.VarString).Value = _evidencia;
            objSelectCmd.Parameters.Add("p_comentario", MySqlDbType.VarString).Value = _comentario;
            objSelectCmd.Parameters.Add("p_estado", MySqlDbType.VarString).Value = _estado; 

            try
            {
                row = objSelectCmd.ExecuteNonQuery();

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

        //Metodo para actulizar un Seguimiento
        public bool updateSeguimiento(int _caso_id, string _codigo, string _fecha, string _accionrealizada, string _relevancia, string _evidencia, string _comentario, string _estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateSeguimiento";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_caso_id", MySqlDbType.Int32).Value = _caso_id;
            objSelectCmd.Parameters.Add("p_fecha", MySqlDbType.VarString).Value = _fecha;
            objSelectCmd.Parameters.Add("p_accionrealizada", MySqlDbType.VarString).Value = _accionrealizada;
            objSelectCmd.Parameters.Add("p_razon", MySqlDbType.VarString).Value = _razon;
            objSelectCmd.Parameters.Add("p_relevancia", MySqlDbType.VarString).Value = _relevancia;
            objSelectCmd.Parameters.Add("p_evidencia", MySqlDbType.VarString).Value = _evidencia;
            objSelectCmd.Parameters.Add("p_comentario", MySqlDbType.VarString).Value = _comentario;
            objSelectCmd.Parameters.Add("p_estado", MySqlDbType.VarString).Value = _estado;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
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

        //Metodo para borrar una Seguimiento
        public bool deleteSeguimiento(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteSeguimiento"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_caso_id", MySqlDbType.Int32).Value = _id;
            try
            {
                row = objSelectCmd.ExecuteNonQuery();
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