using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class EmpleadoDat
    {
        private readonly Persistence objPer = new Persistence();

        // Mostrar empleados para DropDownList
        public DataSet showEmpleadoDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand
            {
                Connection = objPer.openConnection(),
                CommandText = "spSelectEmpleadoDDL",
                CommandType = CommandType.StoredProcedure
            };
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Mostrar todos los empleados
        public DataSet showEmpleados()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand
            {
                Connection = objPer.openConnection(),
                CommandText = "spSelectEmpleados",
                CommandType = CommandType.StoredProcedure
            };
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Guardar nuevo empleado
        public bool saveEmpleado(int usuarioId, int especialidadId)
        {
            bool executed = false;
            MySqlCommand objSelectCmd = new MySqlCommand
            {
                Connection = objPer.openConnection(),
                CommandText = "spInsertEmpleado",
                CommandType = CommandType.StoredProcedure
            };

            objSelectCmd.Parameters.Add("p_usuario_id", MySqlDbType.Int32).Value = usuarioId;
            objSelectCmd.Parameters.Add("p_especialidad_id", MySqlDbType.Int32).Value = especialidadId;

            try
            {
                int row = objSelectCmd.ExecuteNonQuery();
                if (row == 1) executed = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            finally
            {
                objPer.closeConnection();
            }
            return executed;
        }

        // Actualizar empleado
        public bool updateEmpleado(int id, int usuarioId, int especialidadId)
        {
            bool executed = false;
            MySqlCommand objSelectCmd = new MySqlCommand
            {
                Connection = objPer.openConnection(),
                CommandText = "spUpdateEmpleado",
                CommandType = CommandType.StoredProcedure
            };

            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;
            objSelectCmd.Parameters.Add("p_usuario_id", MySqlDbType.Int32).Value = usuarioId;
            objSelectCmd.Parameters.Add("p_especialidad_id", MySqlDbType.Int32).Value = especialidadId;

            try
            {
                int row = objSelectCmd.ExecuteNonQuery();
                if (row == 1) executed = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            finally
            {
                objPer.closeConnection();
            }
            return executed;
        }

        // Eliminar empleado
        public bool deleteEmpleado(int id)
        {
            bool executed = false;
            MySqlCommand objSelectCmd = new MySqlCommand
            {
                Connection = objPer.openConnection(),
                CommandText = "spDeleteEmpleado",
                CommandType = CommandType.StoredProcedure
            };

            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = id;

            try
            {
                int row = objSelectCmd.ExecuteNonQuery();
                if (row == 1) executed = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            finally
            {
                objPer.closeConnection();
            }
            return executed;
        }
        //Metodo para mostrar la cantidad de Usuarios
        public int showCountEmpleado()
        {
            int totalEmpleados;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCountEmpleados";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            // Agregar el parámetro de salida
            objSelectCmd.Parameters.Add(new MySqlParameter("@total_empleados", MySqlDbType.Int32));
            objSelectCmd.Parameters["@total_empleados"].Direction = ParameterDirection.Output;
            // Ejecutar el comando
            objSelectCmd.ExecuteNonQuery();
            // Obtener el valor del parámetro de salida
            totalEmpleados = Convert.ToInt32(objSelectCmd.Parameters["@total_empleados"].Value);
            return totalEmpleados;
        }
    }
}