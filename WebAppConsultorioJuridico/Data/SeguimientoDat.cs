using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        public bool saveSeguimiento(int _caso_id, DateTime _fecha_actualizacion, string _proceso, string _descripcion, string _estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertSeguimiento";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_caso_id", MySqlDbType.Int32).Value = _caso_id;
            objSelectCmd.Parameters.Add("p_fecha_actualizacion", MySqlDbType.DateTime).Value = _fecha_actualizacion;
            objSelectCmd.Parameters.Add("p_proceso", MySqlDbType.VarString).Value = _proceso;
            objSelectCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = _descripcion;
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
        public bool updateSeguimiento(int _id, int _caso_id, DateTime _fecha_actualizacion, string _proceso, string _descripcion, string _estado)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateSeguimiento";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_caso_id", MySqlDbType.Int32).Value = _caso_id;
            objSelectCmd.Parameters.Add("p_fecha_actualizacion", MySqlDbType.DateTime).Value = _fecha_actualizacion;
            objSelectCmd.Parameters.Add("p_proceso", MySqlDbType.VarString).Value = _proceso;
            objSelectCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = _descripcion;
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
            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
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
