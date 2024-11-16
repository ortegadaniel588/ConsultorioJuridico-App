using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class UsuariosDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todos los Usuarios
        public DataSet showUsers()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUsers";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Metodo que retorna un objeto con el usuario encontrado por el correo
        //public User showUsersMail(string mail)
        //{
        //    User objUser = null;
        //    MySqlCommand objSelectCmd = new MySqlCommand();
        //    objSelectCmd.Connection = objPer.openConnection();
        //    objSelectCmd.CommandText = "spSelectUserMail";
        //    objSelectCmd.CommandType = CommandType.StoredProcedure;
        //    objSelectCmd.Parameters.Add("p_mail", MySqlDbType.VarString).Value = mail;
        //    MySqlDataReader reader = objSelectCmd.ExecuteReader();
        //    if (!reader.HasRows)
        //    {
        //        return objUser;
        //    }
        //    else
        //    {
        //        while (reader.Read())
        //        {
        //            objUser = new User(reader["usu_correo"].ToString(),
        //            reader["usu_contrasena"].ToString(), reader["usu_salt"].ToString(),
        //            reader["usu_estado"].ToString(), reader["rol_nombre"].ToString(), Convert.ToInt32(reader["per_id"]));
        //        }
        //    }
        //    objPer.closeConnection();
        //    return objUser;
        //}

        // Metodo modificado que retorna un objeto con el usuario encontrado por el correo
        public User showUsersMail(string mail)
        {
            User objUser = null;
            List<Permission> permisos = new List<Permission>();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUserMail";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_mail", MySqlDbType.VarString).Value = mail;
            MySqlDataReader reader = objSelectCmd.ExecuteReader();

            if (!reader.HasRows)
            {
                return objUser;
            }

            while (reader.Read())
            {
                // Si el objeto User es nulo, inicializarlo (solo se hace una vez)
                if (objUser == null)
                {
                    // Inicializar rol
                    Rol userRol = new Rol(
                        id: Convert.ToInt32(reader["rol_id"]), // Si tienes el ID del rol
                        nombre: reader["rol_nombre"].ToString(),
                        descripcion: reader["rol_descripcion"].ToString() // Ajusta según tu estructura
                    );

                    // Crear objeto User con los datos iniciales
                    objUser = new User(
                        correo: reader["usu_correo"].ToString(),
                        contrasena: reader["usu_contrasena"].ToString(),
                        salt: reader["usu_salt"].ToString(),
                        state: reader["usu_estado"].ToString(),
                        rol: userRol,
                        permisos: permisos // Inicialmente vacío, luego se irá llenando
                    );
                }
                // Crear permiso y agregarlo a la lista de permisos
                Permission permiso = new Permission(
                    id: Convert.ToInt32(reader["per_id"]), // Si tienes el ID del permiso
                    nombre: reader["per_nombre"].ToString(),
                    descripcion: reader["per_descripcion"].ToString() // Ajusta según tu estructura
                );

                permisos.Add(permiso);
            }
            objPer.closeConnection();
            return objUser;
        }


        //Metodo para guardar un nuevo Usuario
        public bool saveUsers(string _mail, string _password, string _salt, string _state, DateTime _date, int _fkrol, int _fkpersona)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_mail", MySqlDbType.VarString).Value = _mail;
            objSelectCmd.Parameters.Add("p_password", MySqlDbType.VarString).Value = _password;
            objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("p_state", MySqlDbType.VarString).Value = _state;
            objSelectCmd.Parameters.Add("p_date", MySqlDbType.Date).Value = _date;
            objSelectCmd.Parameters.Add("p_fkrol", MySqlDbType.Int32).Value = _fkrol;
            objSelectCmd.Parameters.Add("p_fkpersona", MySqlDbType.Int32).Value = _fkpersona;


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

        //Metodo para actualizar un Usuario
        public bool updateUsers(int _id, string _mail, string _password, string _salt, string _state, DateTime _date, int _fkrol, int _fkpersona)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarString).Value = _mail;
            objSelectCmd.Parameters.Add("p_contrasena", MySqlDbType.VarString).Value = _password;
            objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("p_estado", MySqlDbType.VarString).Value = _state;
            objSelectCmd.Parameters.Add("p_fecha_creacion", MySqlDbType.Date).Value = _date;
            objSelectCmd.Parameters.Add("p_fkrol", MySqlDbType.Int32).Value = _fkrol;
            objSelectCmd.Parameters.Add("p_fkpersona", MySqlDbType.Int32).Value = _fkpersona;

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