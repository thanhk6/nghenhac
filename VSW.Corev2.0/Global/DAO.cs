using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using VSW.Core.Models;

namespace VSW.Core.Global
{
	public class DAO<T> where T : EntityBase
	{
		public static T Populate(DataRow dataRow)
		{
			T result;
			if (dataRow == null)
			{
				result = default(T);
			}
			else
			{
				Class @class = new Class(typeof(T));
				T t = (T)((object)@class.Instance);
				for (int i = 0; i < dataRow.Table.Columns.Count; i++)
				{
					string columnName = dataRow.Table.Columns[i].ColumnName;
					object obj = dataRow[i];
					if (obj != null && obj != DBNull.Value)
					{
						if (columnName == "Custom")
						{
							DAO<T>.GetCustom(t.Items, obj.ToString());
						}
						if (!@class.ExistsProperty(columnName))
						{
							t.Items.SetValue(columnName, obj);
						}
						else
						{
							PropertyInfo propertyInfo = @class.GetPropertyInfo(columnName);
							object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DataInfo), true);
							if (customAttributes != null && customAttributes.Length != 0)
							{
								if (propertyInfo.CanWrite)
								{
									@class.SetProperty(columnName, obj);
								}
							}
							else
							{
								t.Items.SetValue(columnName, obj);
							}
						}
					}
				}
				result = t;
			}
			return result;
		}
		public static List<T> Populate(DataTable dataTable)
		{
			List<T> result;
			if (dataTable == null || dataTable.Rows.Count == 0)
			{
				result = null;
			}
			else
			{
				List<T> list = new List<T>();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					list.Add(DAO<T>.Populate(dataTable.Rows[i]));
				}
				result = list;
			}
			return result;
		}
		public static List<T> Populate(SqlDataReader reader)
		{
			List<T> result;
			if (reader == null)
			{
				result = null;
			}
			else
			{
				List<T> list = new List<T>();
				try
				{
					while (reader.Read())
					{
						Class @class = new Class(typeof(T));
						T t = (T)((object)@class.Instance);
						for (int i = 0; i < reader.FieldCount; i++)
						{
							string name = reader.GetName(i);
							object obj = reader[i];
							if (obj != null && obj != DBNull.Value)
							{
								if (name == "Custom")
								{
									DAO<T>.GetCustom(t.Items, obj.ToString());
								}
								if (!@class.ExistsProperty(name))
								{
									t.Items.SetValue(name, obj);
								}
								else
								{
									PropertyInfo propertyInfo = @class.GetPropertyInfo(name);
									object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DataInfo), true);
									if (customAttributes != null && customAttributes.Length != 0)
									{
										if (propertyInfo.CanWrite)
										{
											@class.SetProperty(name, obj);
										}
									}
									else
									{
										t.Items.SetValue(name, obj);
									}
								}
							}
						}
						list.Add(t);
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					reader.Close();
				}
				if (list.Count == 0)
				{
					result = null;
				}
				else
				{
					result = list;
				}
			}
			return result;
		}
		private static void GetCustom(Custom item, string value)
		{
			if (!(value == string.Empty))
			{
				string[] array = value.Split(new char[]
				{
					'\n'
				});
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim();
					if (!(text == string.Empty) && !text.StartsWith("//"))
					{
						if (text == "/*")
						{
							flag = true;
						}
						else if (text == "*/")
						{
							flag = false;
						}
						else if (!flag)
						{
							int num = text.IndexOf('=');
							if (num > -1)
							{
								string value2 = text.Substring(num + 1);
								text = text.Substring(0, num);
								item.SetValue(text, value2);
							}
						}
					}
				}
			}
		}
	}
}
