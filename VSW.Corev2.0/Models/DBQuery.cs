using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using VSW.Core.Global;
using VSW.Core.Web;
namespace VSW.Core.Models
{
	public class DBQuery<T> where T : EntityBase
	{
		private object[] Params
		{
			get
			{
				object[] result;
				if (this._listItem.Count > 0)
				{
					result = this._listItem.ToArray();
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (value != null)
				{
					this._listItem.AddRange(value);
				}
			}
		}
		public int TotalRecord
		{
			get
			{
				int result;
				if (this._top > 0 && this._totalRecord > this._top)
				{
					result = this._top;
				}
				else
				{
					result = this._totalRecord;
				}
				return result;
			}
		}
		~DBQuery()
		{
			this._field = null;
			this._where = null;
			this._orderBy = null;
			this._groupBy = null;
			this._union = null;
			this._count = null;
			this._max = null;
			this._min = null;
			this._cacheKey = null;
		}

		private DBQuery()
		{
			this._listItem = new List<object>();
		}
		internal DBQuery(ServiceBase<T> service)
		{
			this._listItem = new List<object>();
			this._service = service;
			this._timeout = service.DBCacheTimeOut;
		}
		public DBQuery<T> AddParam(string name, object value)
		{
			this._listItem.Add(name);
			this._listItem.Add(value);
			return this;
		}
		public DBQuery<T> Clone()
		{
			return base.MemberwiseClone() as DBQuery<T>;
		}
		public DBQuery<T> Count()
		{
			this._count = "*";
			return this;
		}
		public DBQuery<T> Count(Expression<Func<T, object>> exp)
		{
			this._count = new DBToLinQ<T>().Select(exp);
			return this;
		}
		public DBQuery<T> Distinct()
		{
			this._distinct = true;
			return this;
		}

		
		public DBQuery<T> WithLock()
		{
			this._withlock = true;
			return this;
		}

		
		public DBQuery<T> GroupBy(Expression<Func<T, object>> exp)
		{
			this._groupBy = this._groupBy + (string.IsNullOrEmpty(this._groupBy) ? string.Empty : ",") + new DBToLinQ<T>().Select(exp);
			return this;
		}
		
		public DBQuery<T> GroupBy(string groupBy)
		{
			this._groupBy = this._groupBy + (string.IsNullOrEmpty(this._groupBy) ? string.Empty : ",") + groupBy;
			return this;
		}

		public DBQuery<T> Max(Expression<Func<T, object>> exp)
		{
			this._max = new DBToLinQ<T>().Select(exp);
			return this;
		}

		public DBQuery<T> Min(Expression<Func<T, object>> exp)
		{
			this._min = new DBToLinQ<T>().Select(exp);
			return this;
		}
		public DBQuery<T> OrderBy(string orderBy)
		{
			this._orderBy = this._orderBy + (string.IsNullOrEmpty(this._orderBy) ? string.Empty : ",") + orderBy;
			return this;
		}
		public DBQuery<T> OrderBy(bool condition, string orderBy)
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.OrderBy(orderBy);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBQuery<T> OrderByAsc(Expression<Func<T, object>> exp)
		{
			this._orderBy = this._orderBy + (string.IsNullOrEmpty(this._orderBy) ? string.Empty : ",") + new DBToLinQ<T>().OrderAsc(exp);
			return this;
		}
		public DBQuery<T> OrderByAsc(bool condition, Expression<Func<T, object>> exp)
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.OrderByAsc(exp);
			}
			else
			{
				result = this;
			}
			return result;
		}

		public DBQuery<T> OrderByDesc(Expression<Func<T, object>> predicate)
		{
			this._orderBy = this._orderBy + (string.IsNullOrEmpty(this._orderBy) ? string.Empty : ",") + new DBToLinQ<T>().OrderDesc(predicate);
			return this;
		}
		public DBQuery<T> OrderByDesc(bool condition, Expression<Func<T, object>> exp)
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.OrderByDesc(exp);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBQuery<T> Select(Expression<Func<T, object>> exp)
		{
			this._field = this._field + ((string.IsNullOrEmpty(this._field) || this._field == "*") ? string.Empty : ",") + new DBToLinQ<T>().Select(exp);
			return this;
		}

		public DBQuery<T> Select(string select)
		{
			this._field = this._field + ((string.IsNullOrEmpty(this._field) || this._field == "*") ? string.Empty : ",") + select;
			return this;
		}
		public DBQuery<T> Skip(int count)
		{
			this._paging = true;
			this._totalSkip = count;
			return this;
		}
		public DBQuery<T> Take(int count)
		{
			this._pageSize = count;
			return this;
		}
		public DBQuery<T> Union(DBQuery<T> dbQuery)
		{
			this._union = this._union + (string.IsNullOrEmpty(this._union) ? string.Empty : " UNION ") + dbQuery.BuildCommand();
			return this;
		}
		public DBQuery<T> Union(string union)
		{
			this._union = this._union + (string.IsNullOrEmpty(this._union) ? string.Empty : " UNION ") + union;
			return this;
		}
		public DBQuery<T> Where(Expression<Func<T, bool>> exp)
		{
			DBToLinQ<T> dbtoLinQ = new DBToLinQ<T>
			{
				IndexParams = this._listItem.Count / 2
			};
			this._where = string.Concat(new string[]
			{
				this._where,
				string.IsNullOrEmpty(this._where) ? string.Empty : " AND ",
				"(",
				dbtoLinQ.CreateQuery(exp),
				")"
			});
			this.Params = dbtoLinQ.Params;
			return this;
		}


		public DBQuery<T> Where(string where)
		{
			this._where = string.Concat(new string[]
			{
				this._where,
				string.IsNullOrEmpty(this._where) ? string.Empty : " AND ",
				"(",
				where,
				")"
			});
			return this;
		}
		public DBQuery<T> Where(bool condition, Expression<Func<T, bool>> exp)
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.Where(exp);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBQuery<T> Where(bool condition, string where)
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.Where(where);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBQuery<T> WhereIn<Tin>(Expression<Func<T, object>> exp, DBQuery<Tin> dbQuery) where Tin : EntityBase
		{
			this._where = string.Concat(new string[]
			{
				this._where,
				string.IsNullOrEmpty(this._where) ? string.Empty : " AND ",
				"(",
				new DBToLinQ<T>().Select(exp),
				" IN (",
				dbQuery.BuildCommand(),
				"))"
			});
			return this;
		}
		public DBQuery<T> WhereIn(Expression<Func<T, object>> exp, string list)
		{
			this._where = string.Concat(new string[]
			{
				this._where,
				string.IsNullOrEmpty(this._where) ? string.Empty : " AND ",
				"(",
				new DBToLinQ<T>().Select(exp),
				" IN (",
				list,
				"))"
			});
			return this;
		}
		public DBQuery<T> WhereIn<Tin>(bool condition, Expression<Func<T, object>> exp, DBQuery<Tin> dbQuery) where Tin : EntityBase
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.WhereIn<Tin>(exp, dbQuery);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBQuery<T> WhereIn(bool condition, Expression<Func<T, object>> exp, string list)
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.WhereIn(exp, list);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBQuery<T> WhereNotIn<Tin>(Expression<Func<T, object>> exp, DBQuery<Tin> dbQuery) where Tin : EntityBase
		{
			this._where = string.Concat(new string[]
			{
				this._where,
				string.IsNullOrEmpty(this._where) ? string.Empty : " AND ",
				"(",
				new DBToLinQ<T>().Select(exp),
				" NOT IN (",
				dbQuery.BuildCommand(),
				"))"
			});
			return this;
		}
		public DBQuery<T> WhereNotIn(Expression<Func<T, object>> exp, string list)
		{
			this._where = string.Concat(new string[]
			{
				this._where,
				string.IsNullOrEmpty(this._where) ? string.Empty : " AND ",
				"(",
				new DBToLinQ<T>().Select(exp),
				" NOT IN (",
				list,
				"))"
			});
			return this;
		}
		public DBQuery<T> WhereNotIn<Tin>(bool condition, Expression<Func<T, object>> exp, DBQuery<Tin> dbQuery) where Tin : EntityBase
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.WhereNotIn<Tin>(exp, dbQuery);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBQuery<T> WhereNotIn(bool condition, Expression<Func<T, object>> predicate, string list)
		{
			DBQuery<T> result;
			if (condition)
			{
				result = this.WhereNotIn(predicate, list);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public List<T> ToList()
		{
			List<T> result;
			if (this._service.DBExecuteMode == DBExecuteType.DataSet)
			{
				if (this._paging && this._pageSize > 0)
				{
					using (DataSet dataSet = this._service.ExecuteDataSet(this.BuildCommand(), this.Params))
					{
						if (dataSet == null)
						{
							result = null;
							return result;
						}
						this._totalRecord = VSW.Core.Global.Convert.ToInt(dataSet.Tables[0].Rows[0][0]);
						result = this._service.Populate(dataSet.Tables[1]);
						return result;
					}
				}
				result = this._service.Populate(this._service.ExecuteDataTable(this.BuildCommand(), this.Params));
			}
			else if (!this._paging || this._pageSize <= 0)
			{
				result = this._service.Populate(this._service.ExecuteReader(this.BuildCommand(), this.Params));
			}
			else
			{
				using (SqlDataReader sqlDataReader = this._service.ExecuteReader(this.BuildCommand(), this.Params))
				{
					try
					{
						if (sqlDataReader.Read())
						{
							this._totalRecord = sqlDataReader.GetInt32(0);
							sqlDataReader.NextResult();
						}
					}
					catch
					{
						sqlDataReader.Close();
						throw;
					}
					result = this._service.Populate(sqlDataReader);
				}
			}
			return result;
		}
		public List<T> ToList_Cache()
		{
			string text = Support.BitConverter(this.BuildCommand(), this.Params);
			string key = string.Concat(new string[]
			{
				"Data.",
				this._service.TableName,
				".ToList_Cache.",
				text,
				".",
				this._cacheKey
			});
			string key2 = string.Concat(new string[]
			{
				"Data.",
				this._service.TableName,
				".TotalRecord.",
				text,
				".",
				this._cacheKey
			});
			List<T> list = Cache.GetValue(key) as List<T>;
			List<T> result;
			if (list != null && list.Count > 0)
			{
				this._totalRecord = VSW.Core.Global.Convert.ToInt(Cache.GetValue(key2));
				result = list;
			}
			else
			{
				List<T> list2 = this.ToList();
				if (list2 == null || list2.Count < 1)
				{
					result = null;
				}
				else
				{
					Cache.SetValue(key, list2, this._timeout);
					Cache.SetValue(key2, this.TotalRecord, this._timeout);
					result = list2;
				}
			}
			return result;
		}
		public List<T> ToList_Cache(int cacheTimeOut)
		{
			this._timeout = cacheTimeOut;
			return this.ToList_Cache();
		}
		public List<T> ToList_Cache(string cacheKey)
		{
			this._cacheKey = cacheKey;
			return this.ToList_Cache();
		}
		public List<T> ToList_Cache(string cacheKey, int cacheTimeOut)
		{
			this._cacheKey = cacheKey;
			this._timeout = cacheTimeOut;
			return this.ToList_Cache();
		}
		public T ToSingle()
		{
			if (this._pageSize == 0)
			{
				this._pageSize = 1;
			}
			List<T> list = this.ToList();
			if (list == null || list.Count <= 0)
			{
				return default(T);
			}
			return list[0];
		}
		public T ToSingle_Cache()
		{
			string text = Support.BitConverter(this.BuildCommand(), this.Params);
			string key = string.Concat(new string[]
			{
				"Data.",
				this._service.TableName,
				".ToSingle_Cache.",
				text,
				".",
				this._cacheKey
			});
			T t = Cache.GetValue(key) as T;
			T result;
			if (t != null)
			{
				result = t;
			}
			else
			{
				T t2 = this.ToSingle();
				if (t2 == null)
				{
					result = default(T);
				}
				else
				{
					Cache.SetValue(key, t2, this._timeout);
					result = t2;
				}
			}
			return result;
		}
		public T ToSingle_Cache(int cacheTimeOut)
		{
			this._timeout = cacheTimeOut;
			return this.ToSingle_Cache();
		}
		public T ToSingle_Cache(string cacheKey)
		{
			this._cacheKey = cacheKey;
			return this.ToSingle_Cache();
		}
		public T ToSingle_Cache(string cacheKey, int cacheTimeOut)
		{
			this._cacheKey = cacheKey;
			this._timeout = cacheTimeOut;
			return this.ToSingle_Cache();
		}
		public VSW.Core.Global.Object ToValue()
		{
			return this._service.ExecuteScalar(this.BuildCommand(), this.Params);
		}

		public VSW.Core.Global.Object ToValue_Cache()
		{
			string key = string.Concat(new string[]
			{
				"Data.",
				this._service.TableName,
				".ToValue_Cache.",
				Support.BitConverter(this.BuildCommand(), this.Params),
				".",
				this._cacheKey
			});
			object value = Cache.GetValue(key);
			VSW.Core.Global.Object result;
			if (value != null)
			{
				result = (VSW.Core.Global.Object)value;
			}
			else
			{
				VSW.Core.Global.Object @object = this.ToValue();
				if (@object.Current == null)
				{
					result = null;
				}
				else
				{
					Cache.SetValue(key, @object, this._timeout);
					result = @object;
				}
			}
			return result;
		}
		public VSW.Core.Global.Object ToValue_Cache(int cacheTimeOut)
		{
			this._timeout = cacheTimeOut;
			return this.ToValue_Cache();
		}
		public VSW.Core.Global.Object ToValue_Cache(string cacheKey)
		{
			this._cacheKey = cacheKey;
			return this.ToValue_Cache();
		}
		public VSW.Core.Global.Object ToValue_Cache(string cacheKey, int cacheTimeOut)
		{
			this._cacheKey = cacheKey;
			this._timeout = cacheTimeOut;
			return this.ToValue_Cache();
		}
		private string SQL2000(string query)
		{
			int num = query.LastIndexOf('[');
			string result;
			if (num != -1)
			{
				result = query.Insert(num + 1, "#OBJ_");
			}
			else
			{
				result = "#OBJ_" + query.Replace(".", "_");
			}
			return result;
		}
		private string BuildCommand()
		{
			string tableName = this._service.TableName;
			string text2;
			if (this._paging && this._pageSize > 0)
			{
				string text = this.SQL2000(tableName);
				text2 = string.Concat(new string[]
				{
					"CREATE TABLE ",
					text,
					" ([TempID] [int] IDENTITY (1, 1) NOT NULL, [ID] [int]) INSERT INTO ",
					text,
					"([ID]) SELECT ",
					(this._top > 0) ? ("TOP " + this._top.ToString() + " ") : string.Empty,
					" [",
					this._service.PrimaryKey,
					"] FROM ",
					tableName,
					this._withlock ? " " : " with (NOLOCK) ",
					string.IsNullOrEmpty(this._where) ? string.Empty : (" WHERE " + this._where),
					string.IsNullOrEmpty(this._orderBy) ? string.Empty : (" ORDER BY " + this._orderBy),
					string.IsNullOrEmpty(this._groupBy) ? string.Empty : (" GROUP BY " + this._groupBy),
					" SELECT ISNULL(MAX([TEMPID]),0) AS [TEMPID] FROM ",
					text,
					" SELECT ",
					string.IsNullOrEmpty(this._field) ? "*" : this._field,
					" FROM ",
					tableName,
					this._withlock ? " " : " with (NOLOCK) ",
					" WHERE [",
					this._service.PrimaryKey,
					"] IN (SELECT TOP ",
					this._pageSize.ToString(),
					" [ID] FROM ",
					text,
					" WHERE [TempID] > ",
					this._totalSkip.ToString(),
					" ORDER BY [TempID])",
					string.IsNullOrEmpty(this._groupBy) ? string.Empty : (" GROUP BY " + this._groupBy),
					string.IsNullOrEmpty(this._orderBy) ? string.Empty : (" ORDER BY " + this._orderBy),
					" DROP TABLE ",
					text
				});
			}


			else
			{
				text2 = string.Concat(new string[]
				{
					this._delete ? "DELETE " : "SELECT ",
					string.IsNullOrEmpty(this._count) ? string.Empty : ("COUNT(" + this._count + ") "),
					string.IsNullOrEmpty(this._max) ? string.Empty : ("MAX(" + this._max + ") "),
					string.IsNullOrEmpty(this._min) ? string.Empty : ("MIN(" + this._min + ") "),
					this._distinct ? "DISTINCT " : string.Empty,
					(this._pageSize > 0) ? ("TOP " + this._pageSize.ToString() + " ") : string.Empty,
					(!string.IsNullOrEmpty(this._count) || !string.IsNullOrEmpty(this._max) || !string.IsNullOrEmpty(this._min)) ? string.Empty : (string.IsNullOrEmpty(this._field) ? "*" : this._field),
					" FROM ",
					tableName,
					this._withlock ? " " : " with (NOLOCK) ",
					string.IsNullOrEmpty(this._where) ? string.Empty : (" WHERE " + this._where),
					string.IsNullOrEmpty(this._groupBy) ? string.Empty : (" GROUP BY " + this._groupBy),
					string.IsNullOrEmpty(this._orderBy) ? string.Empty : (" ORDER BY " + this._orderBy)
				});
				if (this._union != null)
				{
					text2 = text2 + " UNION " + this._union;
				}
			}
			return text2;
		}
		private bool _paging;
		private bool _delete;
		private bool _distinct;
		private bool _withlock;
		private int _pageSize;
		private int _totalSkip;
		private int _top;
		private int _totalRecord;
		private int _timeout;
		private List<object> _listItem;
		private ServiceBase<T> _service;
		private string _field;
		private string _where;
		private string _orderBy;
		private string _groupBy;
		private string _union;
		private string _count;
		private string _max;
		private string _min;
		private string _cacheKey;
	}
}
