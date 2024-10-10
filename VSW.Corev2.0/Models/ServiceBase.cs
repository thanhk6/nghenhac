using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using VSW.Core.Data;
using VSW.Core.Global;
namespace VSW.Core.Models
{
	public abstract class ServiceBase<T> where T : EntityBase
	{
		public int DBCacheTimeOut { get; protected set; }

		public string DBConfigKey
		{
			get
			{
				return this._dbConfigKey;
			}
			protected set
			{
				this._dbConfigKey = value;
				this.DBConnectString = ConnectionString.GetByConfigKey(this.DBConfigKey);
			}
		}
		public string DBConnectString
		{
			get
			{
				if (this._dbConnectString == null)
				{
					this._dbConnectString = ConnectionString.GetByConfigKey(this.DBConfigKey);
				}
				return this._dbConnectString;
			}
			private set
			{
				this._dbConnectString = value;
			}
		}


		public DBExecuteType DBExecuteMode { get; protected set; }
		public DBType DBType { get; protected set; }
		public Type EntityType { get; private set; }
		public string PrimaryKey { get; private set; }
		public string TableName { get; private set; }
		public ServiceBase(string tableName) : this(tableName, "ID")
		{
		}
		public ServiceBase(string tableName, string primaryKey)
		{
			this.TableName = tableName;
			this.PrimaryKey = primaryKey;
			this.EntityType = typeof(T);
			this.DBCacheTimeOut = Application.TimeOutCache;
			this.DBExecuteMode = DBExecuteType.DataSet;
			this.DBType = DBType.SQL2000;
		}
		public virtual DBQuery<T> CreateQuery()
		{
			return new DBQuery<T>(this);
		}
		public virtual void Delete(List<T> list)
		{
			int num = 0;
			while (list != null && num < list.Count)
			{
				if (list[num] != null)
				{
					this.Delete(list[num]);
				}
				num++;
			}
		}
		public virtual void Delete(int id)
		{
			SQL.Delete(this.DBConnectString, this.TableName, string.Concat(new object[]
			{
				"[",
				this.PrimaryKey,
				"]=",
				id
			}), new object[0]);
		}


		public virtual void Delete(T item)
		{
			if (item != null)
			{
				this.Delete(item.ID);
			}
		}
		public int Delete(Expression<Func<T, bool>> exp)
		{
			DBToLinQ<T> dbtoLinQ = new DBToLinQ<T>();
			return this.Delete(dbtoLinQ.CreateQuery(exp), dbtoLinQ.Params);
		}
		public int Delete(string whereClause, params object[] Params)
		{
			int result;
			if (this._transaction != null)
			{
				result = this._transaction.Delete(this.TableName, whereClause, Params);
			}
			else
			{
				result = SQL.Delete(this.DBConnectString, this.TableName, whereClause, Params);
			}
			return result;
		}
		public DataSet ExecuteDataSet(string command, params object[] Params)
		{
			DataSet result;
			if (this._transaction != null)
			{
				result = this._transaction.SQLCmdTrans.ExecuteDataSet(command, Params);
			}
			else
			{
				result = SQLCmd.ExecuteDataSet(this.DBConnectString, command, Params);
			}
			return result;
		}

		public DataTable ExecuteDataTable(string command, params object[] Params)
		{
			DataTable result;
			if (this._transaction != null)
			{
				result = this._transaction.SQLCmdTrans.ExecuteDataTable(command, Params);
			}
			else
			{
				result = SQLCmd.ExecuteDataTable(this.DBConnectString, command, Params);
			}
			return result;
		}

		public int ExecuteNonQuery(string command, params object[] Params)
		{
			int result;
			if (this._transaction != null)
			{
				result = this._transaction.SQLCmdTrans.ExecuteNonQuery(command, Params);
			}
			else
			{
				result = SQLCmd.ExecuteNonQuery(this.DBConnectString, command, Params);
			}
			return result;
		}
		public SqlDataReader ExecuteReader(string command, params object[] Params)
		{
			SqlDataReader result;
			if (this._transaction != null)
			{
				result = this._transaction.SQLCmdTrans.ExecuteReader(command, Params);
			}
			else
			{
				result = SQLCmd.ExecuteReader(this.DBConnectString, command, Params);
			}
			return result;
		}
		public Global.Object ExecuteScalar(string command, params object[] Params)
		{
			VSW.Core.Global.Object result;
			if (this._transaction != null)
			{
				result = this._transaction.SQLCmdTrans.ExecuteScalar(command, Params);
			}
			else
			{
				result = SQLCmd.ExecuteScalar(this.DBConnectString, command, Params);
			}
			return result;
		}
		public int Insert(bool identity, params object[] Fields)
		{
			int result;
			if (this._transaction != null)
			{
				result = this._transaction.Insert(this.TableName, identity, Fields);
			}
			else
			{
				result = SQL.Insert(this.DBConnectString, this.TableName, identity, Fields);
			}
			return result;
		}
		private int Save(T item, string[] propertyExec, string[] propertyNotExec, Dictionary<string, object> customField)
		{
			if (item == null)
			{
				throw new Exception("item = null");
			}
			Class @class = new Class(item);
			bool flag = false;
			List<object> list = new List<object>();
			foreach (PropertyInfo propertyInfo in @class.GetPropertiesInfo())
			{
				object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DataInfo), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					if (propertyInfo.Name == this.PrimaryKey)
					{
						flag = true;
					}
					else if ((propertyExec == null || System.Array.IndexOf<string>(propertyExec, propertyInfo.Name) != -1) && (propertyNotExec == null || System.Array.IndexOf<string>(propertyNotExec, propertyInfo.Name) <= -1))
					{
						object obj = @class.GetProperty(propertyInfo.Name);
						if (item.ID != 0 || !Support.IsNull(obj))
						{
							if (item.ID > 0 && Support.IsNull(obj))
							{
								obj = null;
							}
							list.Add("@" + propertyInfo.Name);
							list.Add(obj);
						}
					}
				}
			}
			if (customField != null)
			{
				string[] array = new string[customField.Keys.Count];
				customField.Keys.CopyTo(array, 0);
				for (int j = 0; j < array.Length; j++)
				{
					list.Add("@" + array[j]);
					list.Add(customField[array[j]]);
				}
			}
			int result;
			if (item.ID == 0)
			{
				result = (item.ID = this.Insert(flag, list.ToArray()));
			}
			else if (flag && item.ID >= 1)
			{
				this.Update(string.Concat(new object[]
				{
					"[",
					this.PrimaryKey,
					"]=",
					item.ID
				}), list.ToArray());
				result = item.ID;
			}
			else
			{
				result = -1;
			}
			return result;
		}
		public List<T> Populate(DataTable dataTable)
		{
			return DAO<T>.Populate(dataTable);
		}
		public List<T> Populate(SqlDataReader reader)
		{
			return DAO<T>.Populate(reader);
		}
		public virtual int Save(T item)
		{
			Expression<Func<T, object>> propertyExec = null;
			Expression<Func<T, object>> propertyNotExec = null;
			return this.Save(item, propertyExec, propertyNotExec, null);
		}
		public virtual int Save(T item, Expression<Func<T, object>> propertyExec)
		{
			return this.Save(item, propertyExec, null, null);
		}
		public virtual int Save(T item, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec)
		{
			return this.Save(item, propertyExec, propertyNotExec, null);
		}
		public virtual int Save(T item, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec, Dictionary<string, object> customFields)
		{
			string[] propertyExec2 = null;
			string[] propertyNotExec2 = null;
			if (propertyExec != null)
			{
				propertyExec2 = new DBToLinQ<T>().Select(propertyExec).Replace("[", string.Empty).Replace("]", string.Empty).Split(new char[]
				{
					','
				});
			}
			if (propertyNotExec != null)
			{
				propertyNotExec2 = new DBToLinQ<T>().Select(propertyNotExec).Replace("[", string.Empty).Replace("]", string.Empty).Split(new char[]
				{
					','
				});
			}
			return this.Save(item, propertyExec2, propertyNotExec2, customFields);
		}
		public virtual void Save(List<T> list)
		{
			this.Save(list, null, null, null);
		}
		public virtual void Save(List<T> list, Expression<Func<T, object>> propertyExec)
		{
			this.Save(list, propertyExec, null, null);
		}
		public virtual void Save(List<T> list, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec)
		{
			this.Save(list, propertyExec, propertyNotExec, null);
		}
		public virtual void Save(List<T> list, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec, Dictionary<string, object> customFields)
		{
			int num = 0;
			while (list != null && num < list.Count)
			{
				if (list[num] != null)
				{
					this.Save(list[num], propertyExec, propertyNotExec, customFields);
				}
				num++;
			}
		}
		public void Trans_Begin()
		{
			this._transaction = new SQLTrans();
			this._transaction.Begin(this.DBConnectString);
		}
		public void Trans_Commit()
		{
			if (this._transaction != null)
			{
				this._transaction.Commit();
				this._transaction = null;
			}
		}
		public void Trans_Rollback()
		{
			if (this._transaction != null)
			{
				this._transaction.Rollback();
				this._transaction = null;
			}
		}
		public void Trans_Rollback(string pointName)
		{
			if (this._transaction != null)
			{
				this._transaction.Rollback(pointName);
			}
		}
		public void Trans_Save(string pointName)
		{
			if (this._transaction != null)
			{
				this._transaction.Save(pointName);
			}
		}
		public int Update(Expression<Func<T, bool>> exp, params object[] fields)
		{
			DBToLinQ<T> dbtoLinQ = new DBToLinQ<T>();
			return this.Update(dbtoLinQ.CreateQuery(exp), fields);
		}
		public int Update(string whereClause, params object[] fields)
		{
			int result;
			if (this._transaction != null)
			{
				result = this._transaction.Update(this.TableName, whereClause, fields);
			}
			else
			{
				result = SQL.Update(this.DBConnectString, this.TableName, whereClause, fields);
			}
			return result;
		}
		private SQLTrans _transaction;
		private string _dbConfigKey;
		private string _dbConnectString;
	}
}
