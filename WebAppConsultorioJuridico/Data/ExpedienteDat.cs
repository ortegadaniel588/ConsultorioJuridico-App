using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class ExpedienteDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todas las Expediente
        public DataSet showExpediente()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectExpendientes";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para guardar un nuevo Expediente
        public bool saveExpediente(int _caso_idcaso, string _codigo, string _accionrealizada, string _razon, string _relevancia, string _evidencia, string _comentario, string _estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertExpendiente";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_caso_idcaso", MySqlDbType.Int32).Value = _caso_idcaso;
            objSelectCmd.Parameters.Add("p_codigo", MySqlDbType.VarString).Value = _codigo;
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

        //Metodo para actulizar un Expediente
        public bool updateExpediente(int _id, int _caso_idcaso, string _codigo,  string _accionrealizada, string _razon, string _relevancia, string _evidencia, string _comentario, string _estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateExpendiente";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_idexpendiente", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_caso_idcaso", MySqlDbType.Int32).Value = _caso_idcaso;
            objSelectCmd.Parameters.Add("p_codigo", MySqlDbType.VarString).Value = _codigo;
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

        //Metodo para borrar una Expediente
        public bool deleteExpediente(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteExpendiente"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_idexpendiente", MySqlDbType.Int32).Value = _id;
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
