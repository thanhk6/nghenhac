using System;
using VSW.Core.Web;

namespace VSW.Core.Data
{
	public sealed class SQLTrans : SQLBase
	{
		internal SQLCmdTrans SQLCmdTrans { get; private set; }
		public void Begin()
		{
			this.Begin(ConnectionString.Default);
		}
		public void Begin(string connectionString)
		{
			if (this.SQLCmdTrans == null)
			{
				this.SQLCmdTrans = new SQLCmdTrans();
			}
			this.SQLCmdTrans.Begin(connectionString);
		}
		public void Commit()
		{
			if (this.SQLCmdTrans != null)
			{
				this.SQLCmdTrans.Commit();
				this.SQLCmdTrans = null;
			}
		}
		public int Insert(string tableName, bool identity, params object[] fields)
		{
			string text = SQLBase.GetInsertText(fields);
			text = string.Format("INSERT INTO {0}({1}) VALUES({2})" + (identity ? "; SELECT @@IDENTITY" : string.Empty), tableName, text.Replace("@", string.Empty), text.Replace("[", string.Empty).Replace("]", string.Empty));
			int result = (!identity) ? this.SQLCmdTrans.ExecuteNonQuery(text, fields) : this.SQLCmdTrans.ExecuteScalar(text, fields).ToInt();
			Cache.ClearTable(tableName);
			return result;
		}
		public int Update(string tableName, string whereClause, params object[] fields)
		{
			string text = SQLBase.GetUpdateText(fields);
			text = string.Format((whereClause == string.Empty) ? "UPDATE {0} SET {1}" : ("UPDATE {0} SET {1} WHERE " + whereClause), tableName, text);
			int result = this.SQLCmdTrans.ExecuteNonQuery(text, fields);
			Cache.ClearTable(tableName);
			return result;
		}
		public int Delete(string tableName, string whereClause, params object[] Params)
		{
			string command = "DELETE FROM " + tableName + ((whereClause == string.Empty) ? string.Empty : (" WHERE " + whereClause));
			int result = this.SQLCmdTrans.ExecuteNonQuery(command, Params);
			Cache.ClearTable(tableName);
			return result;
		}
		public void Rollback()
		{
			if (this.SQLCmdTrans != null)
			{
				this.SQLCmdTrans.Rollback();
				this.SQLCmdTrans = null;
			}
		}
		public void Rollback(string pointName)
		{
			if (this.SQLCmdTrans != null)
			{
				this.SQLCmdTrans.Rollback(pointName);
			}
		}
		public void Save(string pointName)
		{
			if (this.SQLCmdTrans != null)
			{
				this.SQLCmdTrans.Save(pointName);
			}
		}
	}
}
