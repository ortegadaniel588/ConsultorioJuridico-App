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
    public class EmpleadoLog
    {
        EmpleadoDat objEmp = new EmpleadoDat();

        // Método para mostrar lista desplegable de Empleados
        public DataSet showEmpleadoDDL()
        {
            return objEmp.showEmpleadoDDL();
        }

        // Método para mostrar todos los Empleados
        public DataSet showEmpleado()
        {
            return objEmp.showEmpleados();
        }

        // Método para guardar un nuevo Empleado
        public bool saveEmpleado(int _usuario_id, int _especialidad_id)
        {
            return objEmp.saveEmpleado(_usuario_id, _especialidad_id);
        }

        // Método para actualizar un Empleado
        public bool updateEmpleado(int _id, int _usuario_id, int _especialidad_id)
        {
            return objEmp.updateEmpleado(_id, _usuario_id, _especialidad_id);
        }

        // Método para eliminar un Empleado
        public bool deleteEmpleado(int _id)
        {
            return objEmp.deleteEmpleado(_id);
        }
    }
}