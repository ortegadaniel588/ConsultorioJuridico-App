using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class EspecialidadLog
    {
        EspecialidadDat objEsp = new EspecialidadDat();

        //M�todo para mostrar todas las Especialidades
        public DataSet showEspecialidad()
        {
            return objEsp.showEspecialidad();
        }

        //M�todo para guardar una nueva Especialidad
        public bool saveEspecialidad(string _nombre, string _descripcion)
        {
            return objEsp.saveEspecialidad(_nombre, _descripcion);
        }

        //M�todo para actualizar una Especialidad
        public bool updateEspecialidad(int _id, string _nombre, string _descripcion)
        {
            return objEsp.updateEspecialidad(_id, _nombre, _descripcion);
        }

        //M�todo para eliminar una Especialidad
        public bool deleteEspecialidad(int _id)
        {
            return objEsp.deleteEspecialidad(_id);
        }
    }
}