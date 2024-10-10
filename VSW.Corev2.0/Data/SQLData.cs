using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace VSW.Core.Data
{

	internal sealed class SQLData
	{
		private SQLData()
		{
		}
		private static void AddParameter(SqlCommand command, SqlParameter[] Params)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command = null");
			}
			if (Params != null)
			{
				foreach (SqlParameter sqlParameter in Params)
				{
					if (sqlParameter != null)
					{
						if ((sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Input) && sqlParameter.Value == null)
						{
							sqlParameter.Value = DBNull.Value;
						}
						command.Parameters.Add(sqlParameter);
					}
				}
			}
		}
		private static void CreateConnection(SqlCommand command, SqlConnection conn, SqlTransaction transaction, CommandType type, string query, SqlParameter[] Params, out bool bool_0)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command = null");
			}
			if (query == null || query.Length == 0)
			{
				throw new ArgumentNullException("commandText = null");
			}
			if (conn.State != ConnectionState.Open)
			{
				bool_0 = true;
				conn.Open();
			}
			else
			{
				bool_0 = false;
			}
			command.Connection = conn;
			command.CommandText = query;
			if (transaction != null)
			{
				if (transaction.Connection == null)
				{
					throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
				}
				command.Transaction = transaction;
			}
			command.CommandType = type;
			if (Params != null)
			{
				SQLData.AddParameter(command, Params);
			}
		}
		internal static DataSet GetDataSet(string connString, CommandType type, string query, params SqlParameter[] Params)
		{
			if (connString != null && connString.Length > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection(connString))
				{
					sqlConnection.Open();
					return SQLData.GetDataSet(sqlConnection, type, query, Params);
				}
			}
			throw new ArgumentNullException("connectionString");
		}
		internal static DataSet GetDataSet(SqlConnection conn, CommandType type, string query, params SqlParameter[] Params)
		{
			if (conn == null)
			{
				throw new ArgumentNullException("connection");
			}
			SqlCommand sqlCommand = new SqlCommand();
			bool flag;
			SQLData.CreateConnection(sqlCommand, conn, null, type, query, Params, out flag);
			DataSet result;
			using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
			{
				DataSet dataSet = new DataSet();
				sqlDataAdapter.Fill(dataSet);
				sqlCommand.Parameters.Clear();
				if (flag)
				{
					conn.Close();
				}
				result = dataSet;
			}
			return result;
		}
		internal static DataSet GetDataSet(SqlTransaction transaction, CommandType type, string query, params SqlParameter[] Params)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			SqlCommand sqlCommand = new SqlCommand();
			bool flag;
			SQLData.CreateConnection(sqlCommand, transaction.Connection, transaction, type, query, Params, out flag);
			DataSet result;
			using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
			{
				DataSet dataSet = new DataSet();
				sqlDataAdapter.Fill(dataSet);
				sqlCommand.Parameters.Clear();
				result = dataSet;
			}
			return result;
		}
		internal static SqlDataReader GetDataReader(string connString, CommandType type, string query, params SqlParameter[] Params)
		{
			if (connString == null || connString.Length == 0)
			{
				throw new ArgumentNullException("connectionString");
			}
			SqlConnection sqlConnection = new SqlConnection(connString);
			sqlConnection.Open();
			return SQLData.GetDataReader(sqlConnection, type, query, Params);
		}
		//internal static SqlDataReader GetDataReader(SqlConnection conn, CommandType type, string query, params SqlParameter[] Params)
		//{
		//	if (conn == null)
		//	{
		//		throw new ArgumentNullException("connection");
		//	}
		//	SqlCommand sqlCommand = new SqlCommand();
		//	bool flag = false;
		//	SqlDataReader result;
		//	try
		//	{
		//		SQLData.CreateConnection(sqlCommand, conn, null, type, query, Params, out flag);
		//		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		//		bool flag2 = true;
		//		using (IEnumerator enumerator = sqlCommand.Parameters.GetEnumerator())
		//		{
		//			while (enumerator.MoveNext())
		//			{
		//				if (((SqlParameter)enumerator.Current).Direction != ParameterDirection.Input)
		//				{
		//					flag2 = false;
		//				}
		//			}
		//		}
		//		if (flag2)
		//		{
		//			sqlCommand.Parameters.Clear();
		//		}
		//		result = sqlDataReader;
		//	}
		//	catch
		//	{
		//		conn.Close();
		//		throw;
		//	}
		//	return result;
		//}


		//internal static SqlDataReader GetDataReader(SqlTransaction transaction, CommandType type, string query, params SqlParameter[] Params)
		//{
		//	if (transaction == null)
		//	{
		//		throw new ArgumentNullException("transaction");
		//	}
		//	SqlCommand sqlCommand = new SqlCommand();
		//	bool flag;
		//	SQLData.CreateConnection(sqlCommand, transaction.Connection, transaction, type, query, Params, out flag);
		//	SqlDataReader result = sqlCommand.ExecuteReader();
		//	bool flag2 = true;
		//	using (IEnumerator enumerator = sqlCommand.Parameters.GetEnumerator())
		//	{
		//		while (enumerator.MoveNext())
		//		{
		//			if (((SqlParameter)enumerator.Current).Direction != ParameterDirection.Input)
		//			{
		//				flag2 = false;
		//			}
		//		}
		//	}
		//	if (flag2)
		//	{
		//		sqlCommand.Parameters.Clear();
		//	}
		//	return result;
		//}

		internal static SqlDataReader GetDataReader(SqlConnection conn, CommandType type, string query, params SqlParameter[] Params)
		{
			if (conn == null)
			{
				throw new ArgumentNullException("connection");
			}

			SqlDataReader data;
			var command = new SqlCommand();
			bool flag = false;
			try
			{
				CreateConnection(command, conn, null, type, query, Params, out flag);
				var reader = command.ExecuteReader();
				bool flag2 = true;

				foreach (SqlParameter parameter in command.Parameters)
				{
					if (parameter.Direction != ParameterDirection.Input)
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					command.Parameters.Clear();
				}

				data = reader;
			}
			catch
			{
				conn.Close();
				throw;
			}

			return data;
		}
		internal static SqlDataReader GetDataReader(SqlTransaction transaction, CommandType type, string query, params SqlParameter[] Params)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}

			var command = new SqlCommand();
			CreateConnection(command, transaction.Connection, transaction, type, query, Params, out bool flag);

			var reader = command.ExecuteReader();
			bool flag2 = true;
			foreach (SqlParameter parameter in command.Parameters)
			{
				if (parameter.Direction != ParameterDirection.Input)
				{
					flag2 = false;
				}
			}

			if (flag2)
			{
				command.Parameters.Clear();
			}

			return reader;
		}
		internal static int ExecuteNonQuery(string connString, CommandType type, string query, params SqlParameter[] Params)
		{
			if (connString != null && connString.Length > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection(connString))
				{
					sqlConnection.Open();
					return SQLData.ExecuteNonQuery(sqlConnection, type, query, Params);
				}
			}
			throw new ArgumentNullException("connectionString");
		}
		internal static int ExecuteNonQuery(SqlConnection conn, CommandType type, string query, params SqlParameter[] Params)
		{
			if (conn == null)
			{
				throw new ArgumentNullException("connection");
			}
			SqlCommand sqlCommand = new SqlCommand();
			bool flag;
			SQLData.CreateConnection(sqlCommand, conn, null, type, query, Params, out flag);
			int result = sqlCommand.ExecuteNonQuery();
			sqlCommand.Parameters.Clear();
			if (flag)
			{
				conn.Close();
			}
			return result;
		}
		internal static int ExecuteNonQuery(SqlTransaction transaction, CommandType type, string query, params SqlParameter[] Params)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			SqlCommand sqlCommand = new SqlCommand();
			bool flag;
			SQLData.CreateConnection(sqlCommand, transaction.Connection, transaction, type, query, Params, out flag);
			int result = sqlCommand.ExecuteNonQuery();
			sqlCommand.Parameters.Clear();
			return result;
		}
		internal static object ExecuteScalar(string connString, CommandType type, string query, params SqlParameter[] Params)
		{
			if (connString != null && connString.Length > 0)
			{
				using (SqlConnection sqlConnection = new SqlConnection(connString))
				{
					sqlConnection.Open();
					return SQLData.ExecuteScalar(sqlConnection, type, query, Params);
				}
			}
			throw new ArgumentNullException("connectionString");
		}
		internal static object ExecuteScalar(SqlConnection conn, CommandType type, string query, params SqlParameter[] Params)
		{
			if (conn == null)
			{
				throw new ArgumentNullException("connection");
			}
			SqlCommand sqlCommand = new SqlCommand();
			bool flag;
			SQLData.CreateConnection(sqlCommand, conn, null, type, query, Params, out flag);
			object result = sqlCommand.ExecuteScalar();
			sqlCommand.Parameters.Clear();
			if (flag)
			{
				conn.Close();
			}
			return result;
		}


		internal static object ExecuteScalar(SqlTransaction transaction, CommandType type, string query, params SqlParameter[] Params)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			SqlCommand sqlCommand = new SqlCommand();
			bool flag;
			SQLData.CreateConnection(sqlCommand, transaction.Connection, transaction, type, query, Params, out flag);
			object result = sqlCommand.ExecuteScalar();
			sqlCommand.Parameters.Clear();
			return result;
		}
	}
}
