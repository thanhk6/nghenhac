using System;
using System.Web;

namespace VSW.Core.Web
{
	public class Session
	{
		public static bool Exists(string key)
		{
			return HttpContext.Current.Session != null && HttpContext.Current.Session[key] != null;
		}
		public static void SetValue(string key, object value)
		{
			if (HttpContext.Current.Session != null)
			{
				HttpContext.Current.Session[key] = value;
			}
		}
		public static object GetValue(string key)
		{
			if (!Session.Exists(key))
			{
				return string.Empty;
			}
			return HttpContext.Current.Session[key];
		}
		public static void Remove(string key)
		{
			if (Session.Exists(key))
			{
				HttpContext.Current.Session[key] = null;
			}
		}
	}
}
