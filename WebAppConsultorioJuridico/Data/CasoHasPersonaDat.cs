using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class CasoHasPersonaDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todas las caso has persona
        public DataSet showCasoHasPersona()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCasoHasPersonas";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        public DataSet showCasoHasPersonaByIdCaso(int _id)
        {
            // Instancias necesarias para la conexión y manejo de datos
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();

            try
            {
                // Abrir la conexión
                objSelectCmd.Connection = objPer.openConnection();

                // Configurar el comando con el procedimiento almacenado
                objSelectCmd.CommandText = "spSelectCasoHasPersonasByIdCaso"; // Nombre del procedimiento
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                // Agregar el parámetro del procedimiento almacenado
                objSelectCmd.Parameters.AddWithValue("p_idCaso", _id);

                // Asociar el comando al adaptador
                objAdapter.SelectCommand = objSelectCmd;

                // Llenar el DataSet con los datos obtenidos
                objAdapter.Fill(objData);
            }
            catch (Exception ex)
            {
                // Manejar errores en caso de ocurrir
                throw new Exception("Error al obtener los datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión en cualquier caso
                objPer.closeConnection();
            }

            // Devolver el conjunto de datos llenado
            return objData;
        }


        //Metodo para guardar un nuevo AsignarRedesSocial
        public bool saveCasoHasPersona(int _caso_idcaso, int _persona_idpersona)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertCasoHasPersona";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_caso_idcaso", MySqlDbType.Int32).Value = _caso_idcaso;
            objSelectCmd.Parameters.Add("p_persona_idpersona", MySqlDbType.Int32).Value = _persona_idpersona;



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
        public bool updateCasoHasPersona(int _id, int _caso_idcaso, int _persona_idpersona)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateCasoHasPersona";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_caso_idcaso", MySqlDbType.Int32).Value = _caso_idcaso;
            objSelectCmd.Parameters.Add("p_persona_idpersona", MySqlDbType.Int32).Value = _persona_idpersona;

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
        public bool deleteCasoHasPersona(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteCasoHasPersona"; //nombre del procedimiento almacenado
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

        //Metodo para mostrar la cantidad de Usuarios
        public int showCountClientes()
        {
            int totalClientes;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCountClientes";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            // Agregar el parámetro de salida
            objSelectCmd.Parameters.Add(new MySqlParameter("@total_clientes", MySqlDbType.Int32));
            objSelectCmd.Parameters["@total_clientes"].Direction = ParameterDirection.Output;
            // Ejecutar el comando
            objSelectCmd.ExecuteNonQuery();
            // Obtener el valor del parámetro de salida
            totalClientes = Convert.ToInt32(objSelectCmd.Parameters["@total_clientes"].Value);
            return totalClientes;
        }

    }
}