using Data;
using System;
using System.Data;

namespace Logic
{
    public class TipoLog
    {
        TipoDat objTipo = new TipoDat();

        public DataSet ShowTipoDDL()
        {
            return objTipo.showTipoDDL();
        }

        public DataSet ShowTipo()
        {
            return objTipo.ShowTipo();
        }

        public bool SaveTipo(string _nombre, string _descripcion)
        {
            return objTipo.SaveTipo(_nombre,_descripcion);
        }

        public bool UpdateTipo(int _id, string _nombre, string _descripcion)
        {
            return objTipo.UpdateTipo(_id, _nombre,_descripcion);
        }

        public bool DeleteTipo(int _id)
        {
            return objUsuario.DeleteUsuario(_id);
        }
    }
}