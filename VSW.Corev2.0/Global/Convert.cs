using System;

namespace VSW.Core.Global
{
	
	public class Convert
	{
		
		public static object AutoValue(object value, Type type)
		{
			object result;
			if (type == typeof(int))
			{
				result = ToInt(value);
			}
			else if (type == typeof(long))
			{
				result =ToLong(value);
			}
			else if (type == typeof(double))
			{
				result =ToDouble(value);
			}
			else if (type == typeof(decimal))
			{
				result =ToDecimal(value);
			}
			else if (type == typeof(DateTime))
			{
				result =ToDateTime(value);
			}
			else if (type == typeof(bool))
			{
				result =ToBool(value);
			}
			else
			{
				result = value;
			}
			return result;
		}
		public static bool ToBool(object o)
		{
			return ToBool(o, false);
		}
		public static bool ToBool(object o, bool defalt)
		{
			bool result;
			if (o == null || o == DBNull.Value || o is DateTime)
			{
				result = defalt;
			}
			else if (o is bool)
			{
				result = (bool)o;
			}
			else
			{
				result = (ToInt(o) > 0);
			}
			return result;
		}

		public static DateTime ToDateTime(object o)
		{
			return ToDateTime(o, DateTime.MinValue);
		}
		public static DateTime ToDateTime(object o, DateTime defalt)
		{
			if (o != null && o != DBNull.Value)
			{
				if (o is DateTime)
				{
					return (DateTime)o;
				}
				try
				{
					return DateTime.Parse(o.ToString());
				}
				catch
				{
				}
				return defalt;
			}
			return defalt;
		}

		public static decimal ToDecimal(object o)
		{
			return ToDecimal(o, 0m);
		}
		public static decimal ToDecimal(object o, decimal defalt)
		{
			if (o != null && o != DBNull.Value && !(o is DateTime))
			{
				if (o is decimal)
				{
					return (decimal)o;
				}
				string text = o.ToString().Trim().ToLower();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 != null && text2.Length == 0)
					{
						return defalt;
					}
					if (text2 == "true")
					{
						return 1m;
					}
					if (text2 == "false")
					{
						return 0m;
					}
				}
				try
				{
					return ToDecimal(text);
				}
				catch
				{
				}
				return defalt;
			}
			return defalt;
		}

		public static double ToDouble(object o)
		{
			return ToDouble(o, 0.0);
		}
		public static double ToDouble(object o, double defalt)
		{
			if (o != null && o != DBNull.Value && !(o is DateTime))
			{
				if (o is double)
				{
					return (double)o;
				}
				string text = o.ToString().Trim().ToLower();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 != null && text2.Length == 0)
					{
						return defalt;
					}
					if (text2 == "true")
					{
						return 1.0;
					}
					if (text2 == "false")
					{
						return 0.0;
					}
				}
				try
				{
					return Convert.ToDouble(text);
				}
				catch
				{
				}
				return defalt;
			}
			return defalt;
		}


		public static int ToInt(object o)
		{
			return Convert.ToInt(o, -1);
		}


		public static int ToInt(object o, int defalt)
		{
			if (o != null && o != DBNull.Value && !(o is DateTime))
			{
				if (o is int)
				{
					return (int)o;
				}
				string text = o.ToString().Trim().ToLower();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 != null && text2.Length == 0)
					{
						return defalt;
					}
					if (text2 == "true")
					{
						return 1;
					}
					if (text2 == "false")
					{
						return 0;
					}
				}
				try
				{
					return System.Convert.ToInt32(text);
				}
				catch
				{
				}
				return defalt;
			}
			return defalt;
		}
		public static long ToLong(object o)
		{
			return Convert.ToLong(o, -1L);
		}
		public static long ToLong(object o, long defalt)
		{
			if (o != null && o != DBNull.Value && !(o is DateTime))
			{
				if (o is long)
				{
					return (long)o;
				}
				string text = o.ToString().Trim().ToLower();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 != null && text2.Length == 0)
					{
						return defalt;
					}
					if (text2 == "true")
					{
						return 1L;
					}
					if (text2 == "false")
					{
						return 0L;
					}
				}
				try
				{
					return System.Convert.ToInt64(text);
				}
				catch
				{
				}
				return defalt;
			}
			return defalt;
		}
		public static string ToString(object o)
		{
			return ToString(o, string.Empty);
		}
		public static string ToString(object o, string defalt)
		{
			string result;
			if (o != null && o != DBNull.Value)
			{
				result = o.ToString();
			}
			else
			{
				result = defalt;
			}
			return result;
		}
	}
}
