using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class PersonaDat
    {
        Persistence objPer = new Persistence();

        public DataSet ShowPersonas()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPersonas";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool SavePersona(string nombres, string apellidos, string tipodocumento, string documento, string genero, string estadocivil, string lugarNacimiento, DateTime fechaNacimiento, string telefono, string celular, string correo, string direccion, string estado, string ocupacion, string nivelEducacion)
        {
            bool executed = false;
            int row = 0;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertPersona";
            objInsertCmd.CommandType = CommandType.StoredProcedure;
            objInsertCmd.Parameters.Add("p_nombres", MySqlDbType.VarChar).Value = nombres;
            objInsertCmd.Parameters.Add("p_apellidos", MySqlDbType.VarChar).Value = apellidos;
            objInsertCmd.Parameters.Add("p_tipodocumento", MySqlDbType.Enum).Value = tipodocumento;
            objInsertCmd.Parameters.Add("p_documento", MySqlDbType.VarChar).Value = documento;
            objInsertCmd.Parameters.Add("p_genero", MySqlDbType.Enum).Value = genero;
            objInsertCmd.Parameters.Add("p_estadocivil", MySqlDbType.Enum).Value = estadocivil;
            objInsertCmd.Parameters.Add("p_lugarNacimiento", MySqlDbType.VarChar).Value = lugarNacimiento;
            objInsertCmd.Parameters.Add("p_fechaNacimiento", MySqlDbType.Date).Value = fechaNacimiento;
            objInsertCmd.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = telefono;
            objInsertCmd.Parameters.Add("p_celular", MySqlDbType.VarChar).Value = celular;
            objInsertCmd.Parameters.Add("p_correo", MySqlDbType.VarChar).Value = correo;
            objInsertCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = direccion;
            objInsertCmd.Parameters.Add("p_estado", MySqlDbType.Enum).Value = estado;
            objInsertCmd.Parameters.Add("p_ocupacion", MySqlDbType.VarChar).Value = ocupacion;
            objInsertCmd.Parameters.Add("p_nivelEducacion", MySqlDbType.Enum).Value = nivelEducacion;

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

        public bool UpdatePersona(int idpersona, string nombres, string apellidos, string tipodocumento, string documento, string genero, string estadocivil, string lugarNacimiento, DateTime fechaNacimiento, string telefono, string celular, string correo, string direccion, string estado, string ocupacion, string nivelEducacion)
        {
            bool executed = false;
            int row = 0;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdatePersona";
            objUpdateCmd.CommandType = CommandType.StoredProcedure;
            objUpdateCmd.Parameters.Add("p_idpersona", MySqlDbType.Int32).Value = idpersona;
            objUpdateCmd.Parameters.Add("p_nombres", MySqlDbType.VarChar).Value = nombres;
            objUpdateCmd.Parameters.Add("p_apellidos", MySqlDbType.VarChar).Value = apellidos;
            objUpdateCmd.Parameters.Add("p_tipodocumento", MySqlDbType.Enum).Value = tipodocumento;
            objUpdateCmd.Parameters.Add("p_documento", MySqlDbType.VarChar).Value = documento;
            objUpdateCmd.Parameters.Add("p_genero", MySqlDbType.Enum).Value = genero;
            objUpdateCmd.Parameters.Add("p_estadocivil", MySqlDbType.Enum).Value = estadocivil;
            objUpdateCmd.Parameters.Add("p_lugarNacimiento", MySqlDbType.VarChar).Value = lugarNacimiento;
            objUpdateCmd.Parameters.Add("p_fechaNacimiento", MySqlDbType.Date).Value = fechaNacimiento;
            objUpdateCmd.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = telefono;
            objUpdateCmd.Parameters.Add("p_celular", MySqlDbType.VarChar).Value = celular;
            objUpdateCmd.Parameters.Add("p_correo", MySqlDbType.VarChar).Value = correo;
            objUpdateCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = direccion;
            objUpdateCmd.Parameters.Add("p_estado", MySqlDbType.Enum).Value = estado;
            objUpdateCmd.Parameters.Add("p_ocupacion", MySqlDbType.VarChar).Value = ocupacion;
            objUpdateCmd.Parameters.Add("p_nivelEducacion", MySqlDbType.Enum).Value = nivelEducacion;

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

        public bool DeletePersona(int idpersona)
        {
            bool executed = false;
            int row = 0;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeletePersona";
            objDeleteCmd.CommandType = CommandType.StoredProcedure;
            objDeleteCmd.Parameters.Add("p_idpersona", MySqlDbType.Int32).Value = idpersona;

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