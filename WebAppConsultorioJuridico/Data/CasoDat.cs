﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class CasoDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todas las Caso
        public DataSet showCaso()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCasos";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para guardar un nuevo Caso
        public bool saveCaso(string _codigo, int _empresa_id, string _fechacierre, string _asunto, int _tipo_id, int _estado_id, string _complejidad, int _empleado_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertCaso";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_codigo", MySqlDbType.VarString).Value = _codigo;
            objSelectCmd.Parameters.Add("p_empresa_id", MySqlDbType.Int32).Value = _empresa_id;
            objSelectCmd.Parameters.Add("p_fechacierre", MySqlDbType.VarString).Value = _fechacierre;
            objSelectCmd.Parameters.Add("p_asunto", MySqlDbType.VarString).Value = _asunto;
            objSelectCmd.Parameters.Add("p_tipo_id", MySqlDbType.Int32).Value = _tipo_id;
            objSelectCmd.Parameters.Add("p_estado_id", MySqlDbType.Int32).Value = _estado_id;
            objSelectCmd.Parameters.Add("p_complejidad", MySqlDbType.VarString).Value = _complejidad; // Asumiendo que _complejidad es un string como 'alta', 'media', 'baja'
            objSelectCmd.Parameters.Add("p_empleado_id", MySqlDbType.Int32).Value = _empleado_id;

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

        //Metodo para actulizar un Caso
        public bool updateCaso(int _id, string _codigo, int _empresa_id, string _fechacierre, string _asunto, int _tipo_id, int _estado_id, string _complejidad, int _empleado_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateCaso";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_codigo", MySqlDbType.VarString).Value = _codigo;
            objSelectCmd.Parameters.Add("p_empresa_id", MySqlDbType.Int32).Value = _empresa_id;
            objSelectCmd.Parameters.Add("p_fechacierre", MySqlDbType.VarString).Value = _fechacierre;
            objSelectCmd.Parameters.Add("p_asunto", MySqlDbType.VarString).Value = _asunto;
            objSelectCmd.Parameters.Add("p_tipo_id", MySqlDbType.Int32).Value = _tipo_id;
            objSelectCmd.Parameters.Add("p_estado_id", MySqlDbType.Int32).Value = _estado_id;
            objSelectCmd.Parameters.Add("p_complejidad", MySqlDbType.VarString).Value = _complejidad;
            objSelectCmd.Parameters.Add("p_empleado_id", MySqlDbType.Int32).Value = _empleado_id;

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

        //Metodo para borrar una Caso
        public bool deleteCaso(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteCaso"; //nombre del procedimiento almacenado
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