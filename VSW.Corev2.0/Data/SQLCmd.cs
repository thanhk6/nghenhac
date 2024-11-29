using System;
using System.Data;
using System.Data.SqlClient;
using VSW.Core.Global;

namespace VSW.Core.Data
{
	public abstract class SQLCmd : SQLBase
	{
		public static DataSet ExecuteDataSet(string connectionString, string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			DataSet dataSet;
			try
			{
				dataSet = SQLData.GetDataSet(connectionString, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter);
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return dataSet;
		}
		public static DataTable ExecuteDataTable(string connectionString, string command, params object[] Params)
		{
			return SQLBase.GetFirstTable(SQLCmd.ExecuteDataSet(connectionString, command, Params));
		}
		public static int ExecuteNonQuery(string connectionString, string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			int result;
			try
			{
				result = SQLData.ExecuteNonQuery(connectionString, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter);
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return result;
		}
		public static SqlDataReader ExecuteReader(string connectionString, string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			SqlDataReader dataReader;
			try
			{
				dataReader = SQLData.GetDataReader(connectionString, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter);
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return dataReader;
		}
		public static VSW.Core.Global.Object ExecuteScalar(string connectionString, string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			VSW.Core.Global.Object result;
			try
			{
				result = new VSW.Core.Global.Object(SQLData.ExecuteScalar(connectionString, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter));
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return result;
		}
	}
}
