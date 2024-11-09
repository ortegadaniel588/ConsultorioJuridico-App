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

        public bool UpdateTipo(int _idtipo, string _nombre, string _descripcion)
        {
            return objTipo.UpdateTipo(_idtipo, _nombre,_descripcion);
        }

        public bool DeleteTipo(int _idtipo)
        {
            return objUsuario.DeleteUsuario(_idtipo);
        }
    }
}