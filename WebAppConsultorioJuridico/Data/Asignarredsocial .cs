using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class CasoDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todas las AsignarRedesSocial
        public DataSet showCaso()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectAsignarRedesSociales";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para guardar un nuevo AsignarRedesSocial
        public bool saveCaso(int _empresa_idempresa, int _redsocial_idredsocial, string p_url)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertAsignarRedSocial";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_empresa_idempresa", MySqlDbType.Int32).Value = _empresa_idempresa;
            objSelectCmd.Parameters.Add("p_redsocial_idredsocial", MySqlDbType.Int32).Value = _redsocial_idredsocial;
            objSelectCmd.Parameters.Add("p_url", MySqlDbType.VarString).Value = _url;
            


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

        //Metodo para actulizar un AsignarRedesSocial
        public bool updateCaso(int _id, string _empresa_idempresa, int _redsocial_idredsocial, string _url)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateAsignarRedSocial";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_empresa_idempresa", MySqlDbType.Int32).Value = _empresa_idempresa;
            objSelectCmd.Parameters.Add("p_redsocial_idredsocial", MySqlDbType.Int32).Value = _redsocial_idredsocial;
            objSelectCmd.Parameters.Add("p_url", MySqlDbType.VarString).Value = _url;

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

        //Metodo para borrar una AsignarRedesSocial
        public bool deleteCaso(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteAsignarRedSocial"; //nombre del procedimiento almacenado
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
