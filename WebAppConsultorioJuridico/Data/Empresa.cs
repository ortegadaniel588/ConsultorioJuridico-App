using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class CasoDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todas las Empresa
        public DataSet showCaso()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteEmpresa";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para guardar un nuevo Empresa
        public bool saveCaso(string _numeronit, string _nombre, string _mision, string _vision, string _dirrecion, string _telefono, string _telefono2, string _correo)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertEmpresa";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_numeronit", MySqlDbType.VarString).Value = _numeronit;
            objSelectCmd.Parameters.Add("p_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_mision", MySqlDbType.VarString).Value = _mision;
            objSelectCmd.Parameters.Add("p_vision", MySqlDbType.VarString).Value = _vision;
            objSelectCmd.Parameters.Add("p_dirrecion", MySqlDbType.VarString).Value = _dirrecion;
            objSelectCmd.Parameters.Add("p_telefono", MySqlDbType.VarString).Value = _telefono;
            objSelectCmd.Parameters.Add("p_telefono2", MySqlDbType.VarString).Value = _telefono2; // Asumiendo que _complejidad es un string como 'alta', 'media', 'baja'
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarString).Value = _correo;

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

        //Metodo para actulizar un Empresa
        public bool updateCaso(int _id, string _codigo, int _empresa_id, string _fechacierre, string _asunto, int _tipo_id, int _estado_id, string _complejidad, int _empleado_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateEmpresa";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_numeronit", MySqlDbType.VarString).Value = _numeronit;
            objSelectCmd.Parameters.Add("p_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_mision", MySqlDbType.VarString).Value = _mision;
            objSelectCmd.Parameters.Add("p_vision", MySqlDbType.VarString).Value = _vision;
            objSelectCmd.Parameters.Add("p_dirrecion", MySqlDbType.VarString).Value = _dirrecion;
            objSelectCmd.Parameters.Add("p_telefono", MySqlDbType.VarString).Value = _telefono;
            objSelectCmd.Parameters.Add("p_telefono2", MySqlDbType.VarString).Value = _telefono2; // Asumiendo que _complejidad es un string como 'alta', 'media', 'baja'
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarString).Value = _correo;

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

        //Metodo para borrar una Empresa
        public bool deleteCaso(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteEmpresa"; //nombre del procedimiento almacenado
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
