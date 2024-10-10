using System;
using System.Data;
using System.Data.SqlClient;
using VSW.Core.Global;

namespace VSW.Core.Data
{
	public sealed class SQLCmdTrans : SQLBase
	{
		public void Begin()
		{
			this.Begin(ConnectionString.Default);
		}
		public void Begin(string ConnectionString)
		{
			SqlConnection sqlConnection = new SqlConnection(ConnectionString);
			sqlConnection.Open();
			this._transaction = sqlConnection.BeginTransaction();
		}
		public void Commit()
		{
			if (this._transaction != null)
			{
				this._transaction.Commit();
				this._transaction = null;
			}
		}
		public DataSet ExecuteDataSet(string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			DataSet dataSet;
			try
			{
				dataSet = SQLData.GetDataSet(this._transaction, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter);
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return dataSet;
		}		
		public DataTable ExecuteDataTable(string command, params object[] Params)
		{
			return SQLBase.GetFirstTable(this.ExecuteDataSet(command, Params));
		}
		public int ExecuteNonQuery(string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			int result;
			try
			{
				result = SQLData.ExecuteNonQuery(this._transaction, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter);
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return result;
		}
		public SqlDataReader ExecuteReader(string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			SqlDataReader dataReader;
			try
			{
				dataReader = SQLData.GetDataReader(this._transaction, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter);
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return dataReader;
		}
		public VSW.Core.Global.Object ExecuteScalar(string command, params object[] Params)
		{
			SqlParameter[] sqlParameter = SQLBase.GetSqlParameter(Params);
			VSW.Core.Global.Object result;
			try
			{
				result = new VSW.Core.Global.Object(SQLData.ExecuteScalar(this._transaction, (command.IndexOf(' ') > -1) ? CommandType.Text : CommandType.StoredProcedure, command, sqlParameter));
			}
			catch (Exception exception)
			{
				throw new SQLException(exception, command, sqlParameter);
			}
			return result;
		}
		public void Rollback()
		{
			if (this._transaction != null)
			{
				this._transaction.Rollback();
				this._transaction = null;
			}
		}
		public void Rollback(string pointName)
		{
			if (this._transaction != null)
			{
				this._transaction.Rollback(pointName);
			}
		}
		public void Save(string pointName)
		{
			if (this._transaction != null)
			{
				this._transaction.Save(pointName);
			}
		}
		private SqlTransaction _transaction;
	}
}
