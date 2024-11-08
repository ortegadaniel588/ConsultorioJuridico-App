using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{

	public class PermisoDat
	{
		Persistence objPer = new Persistence();

		public DataSet showPermisos()
		{
			MySqlDataAdapter objAdapter = new MySqlDataAdapter();
			DataSet objData = new DataSet();

			MySqlCommand objSelectCmd = new MySqlCommand();
			objSelectCmd.Connection = objPer.openConnection();
			objSelectCmd.CommandText = "spSelectPermisos";
			objSelectCmd.CommandType = CommandType.StoredProcedure;
			objAdapter.SelectCommand = objSelectCmd;
			objAdapter.Fill(objData);
			objPer.closeConnection();
			return objData;
		}

		public bool savePermiso(string nombre, string descripcion)
		{
			bool executed = false;
			int row;

			MySqlCommand objInsertCmd = new MySqlCommand();
			objInsertCmd.Connection = objPer.openConnection();
			objInsertCmd.CommandText = "spInsertPermiso";
			objInsertCmd.CommandType = CommandType.StoredProcedure;

			objInsertCmd.Parameters.Add("p_nombre", MySqlDbType.VarString).Value = nombre;
			objInsertCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = descripcion;

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

		public bool updatePermiso(int idpermiso, string nombre, string descripcion)
		{
			bool executed = false;
			int row;

			MySqlCommand objUpdateCmd = new MySqlCommand();
			objUpdateCmd.Connection = objPer.openConnection();
			objUpdateCmd.CommandText = "spUpdatePermiso";
			objUpdateCmd.CommandType = CommandType.StoredProcedure;

			objUpdateCmd.Parameters.Add("p_idpermiso", MySqlDbType.Int32).Value = idpermiso;
			objUpdateCmd.Parameters.Add("p_nombre", MySqlDbType.VarString).Value = nombre;
			objUpdateCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = descripcion;

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

		public bool deletePermiso(int idpermiso)
		{
			bool executed = false;
			int row;

			MySqlCommand objDeleteCmd = new MySqlCommand();
			objDeleteCmd.Connection = objPer.openConnection();
			objDeleteCmd.CommandText = "spDeletePermiso";
			objDeleteCmd.CommandType = CommandType.StoredProcedure;

			objDeleteCmd.Parameters.Add("p_idpermiso", MySqlDbType.Int32).Value = idpermiso;

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