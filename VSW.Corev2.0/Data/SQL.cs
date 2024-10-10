using System;
using System.Data;
using VSW.Core.Web;
namespace VSW.Core.Data
{
	public abstract class SQL : SQLBase
	{
		public static DataTable GetTableWithPage(string connectionString, string tableName, string fieldName, string selectClause, string whereClause, string orderClause, int PageSize, int pageIndex, bool _withlock, ref int totalRecord, params object[] Params)
		{
			string tempTableText = SQLBase.GetTempTableText(tableName);
			if (whereClause != string.Empty)
			{
				whereClause = whereClause.Insert(0, " WHERE ");
			}
			if (orderClause != string.Empty)
			{
				orderClause = orderClause.Insert(0, " ORDER BY ");
			}
			string command = string.Format("\r\n CREATE TABLE {0} ([TempID] [int] IDENTITY (1, 1) NOT NULL, {1} [int]) ", tempTableText, fieldName) + string.Format("\r\n INSERT INTO {4}({1}) SELECT {1} FROM {0} " + (_withlock ? " " : " with (NOLOCK) ") + " {2} {3}", new object[]
			{
				tableName,
				fieldName,
				whereClause,
				orderClause,
				tempTableText
			}) + string.Format("\r\n SELECT {6} FROM {0} " + (_withlock ? " " : " with (NOLOCK) ") + " WHERE {1} IN (SELECT TOP {2} {1} FROM {5} WHERE TempID > {3} ORDER BY TempID) {4} ", new object[]
			{
				tableName,
				fieldName,
				PageSize,
				PageSize * pageIndex,
				orderClause,
				tempTableText,
				selectClause
			}) + string.Format("\r\n SELECT ISNULL(MAX(TEMPID),0) AS TEMPID FROM {0}", tempTableText);
			return SQLBase.GetTablePage(SQLCmd.ExecuteDataSet(connectionString, command, Params), ref totalRecord);
		}
		public static int Insert(string connectionString, string tableName, bool identity, params object[] fields)
		{
			string text = SQLBase.GetInsertText(fields);
			text = string.Format("INSERT INTO {0}({1}) VALUES({2})" + (identity ? "; SELECT @@IDENTITY" : string.Empty), tableName, text.Replace("@", string.Empty), text.Replace("[", string.Empty).Replace("]", string.Empty));

			int result = (!identity) ? SQLCmd.ExecuteNonQuery(connectionString, text, fields) : SQLCmd.ExecuteScalar(connectionString, text, fields).ToInt();
			Cache.ClearTable(tableName);
			return result;
		}



		public static int Update(string connectionString, string tableName, string whereClause, params object[] fields)
		{
			string text = SQLBase.GetUpdateText(fields);
			text = string.Format((whereClause == string.Empty) ? "UPDATE {0} SET {1}" : ("UPDATE {0} SET {1} WHERE " + whereClause), tableName, text);
			int result = SQLCmd.ExecuteNonQuery(connectionString, text, fields);
			Cache.ClearTable(tableName);
			return result;
		}
		public static int Delete(string connectionString, string tableName, string whereClause, params object[] Params)
		{
			string command = "DELETE FROM " + tableName + ((whereClause == string.Empty) ? string.Empty : (" WHERE " + whereClause));
			int result = SQLCmd.ExecuteNonQuery(connectionString, command, Params);
			Cache.ClearTable(tableName);
			return result;
		}
	}
}
