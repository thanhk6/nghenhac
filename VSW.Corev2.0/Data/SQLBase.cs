using System;
using System.Data;
using System.Data.SqlClient;
using VSW.Core.Global;
namespace VSW.Core.Data
{
	public abstract class SQLBase
	{
		protected static object[] AddParameter(object[] Params, string name, object value)
		{
			object[] result;
			if (Params != null && Params.Length != 0)
			{
				System.Array.Resize<object>(ref Params, Params.Length + 2);
				Params[Params.Length - 2] = name;
				Params[Params.Length - 1] = value;
				result = Params;
			}
			else
			{
				result = new object[]
				{
					name,
					value
				};
			}
			return result;
		}
		public static DataTable GetFirstTable(DataSet dataSet)
		{
			DataTable result;
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				dataSet.Tables.Remove(dataTable);
				dataSet.Dispose();
				result = dataTable;
			}
			else
			{
				result = null;
			}
			return result;
		}
		protected static string GetInsertText(params object[] Fields)
		{
			if (Fields == null || Fields.Length % 2 > 0)
			{
				throw new Exception("Fields is null or Fields % 2 = 1");
			}
			string text = string.Empty;
			for (int i = 0; i < Fields.Length; i += 2)
			{
				text = string.Concat(new object[]
				{
					text,
					(i == 0) ? string.Empty : ", ",
					"[",
					Fields[i],
					"]"
				});
			}
			return text;
		}
		protected static SqlParameter[] GetSqlParameter(params object[] Params)
		{
			SqlParameter[] result;
			if (Params == null)
			{
				result = null;
			}
			else
			{
				if (Params.Length % 2 > 0)
				{
					throw new Exception("Params % 2 = 1");
				}
				SqlParameter[] array = new SqlParameter[Params.Length];
				for (int i = 0; i < Params.Length; i += 2)
				{
					array[i] = new SqlParameter(Params[i].ToString(), Params[i + 1]);
				}
				result = array;
			}
			return result;
		}
		protected static DataTable GetTablePage(DataSet dataSet, ref int totalRecord)
		{
			if (dataSet != null && dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
			{
				totalRecord = VSW.Core.Global.Convert.ToInt(dataSet.Tables[1].Rows[0][0]);
			}
			else
			{
				totalRecord = 0;
			}
			return SQLBase.GetFirstTable(dataSet);
		}
		protected static string GetTempTableText(string tableName)
		{
			int num = tableName.LastIndexOf('[');
			string result;
			if (num != -1)
			{
				result = tableName.Insert(num + 1, "#");
			}
			else
			{
				result = "#" + tableName.Replace(".", "_");
			}
			return result;
		}
		protected static string GetUpdateText(params object[] fields)
		{
			if (fields == null || fields.Length % 2 > 0)
			{
				throw new Exception("Fields is null or Fields % 2 = 1");
			}
			string text = string.Empty;
			for (int i = 0; i < fields.Length; i += 2)
			{
				text = text + ((i == 0) ? string.Empty : ", ") + string.Format("[{0}]={1}", fields[i].ToString().Substring(1), fields[i]);
			}
			return text;
		}
		protected static SqlParameter[] SqlCommandToSqlParameter(SqlCommand sqlCommand)
		{
			SqlParameter[] array = new SqlParameter[sqlCommand.Parameters.Count];
			for (int i = 0; i < sqlCommand.Parameters.Count; i++)
			{
				array[i] = new SqlParameter(sqlCommand.Parameters[i].ParameterName, sqlCommand.Parameters[i].Value);
			}
			return array;
		}
	}
}
