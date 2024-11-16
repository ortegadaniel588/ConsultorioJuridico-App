using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Model;

namespace Logic
{
    public class UsuarioLog
    {
        UsuariosDat objUse = new UsuariosDat();

        //Metodo para mostrar todos los Usuarios
        public DataSet showUsers()
        {
            return objUse.showUsers();
        }

        //Metodo para mostrar el Usuarios pasandole el correo
        public User showUsersMail(string mail)
        {
            return objUse.showUsersMail(mail);
        }

        //Metodo para guardar un nuevo Usuario
        public bool saveUsers(string _mail, string _password, string _salt, string _state, DateTime _date, int _fkrol, int _fkpersona)
        {
            return objUse.saveUsers(_mail, _password, _salt, _state, _date, _fkrol, _fkpersona);
        }

        //Metodo para actualizar un Usuario
        public bool updateUsers(int _id, string _mail, string _password, string _salt, string _state, DateTime _date, int _fkrol, int _fkpersona)
        {
            return objUse.updateUsers(_id, _mail, _password, _salt, _state, _date, _fkrol, _fkpersona);
        }
    }
}