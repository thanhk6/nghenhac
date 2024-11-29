using System;
using System.Web;
using VSW.Core.Global;

namespace VSW.Core.Web
{
	
	public class HttpQueryString
	{
		public static string[] AllKeys
		{
			get
			{
				string[] result;
				try
				{
					
					result = HttpContext.Current.Request.QueryString.AllKeys;
				}
				catch
				{
					result = new string[0];
				}
				return result;
			}
		}

		public static bool Exists(string key)
		{
			return HttpContext.Current != null && HttpContext.Current.Request.QueryString[key] != null;
		}
		public static VSW.Core.Global.Object GetValue(string key)
		{
			VSW.Core.Global.Object result;
			if (!Exists(key))
			{
				result = new VSW.Core.Global.Object();
			}
			else
			{
				result = new VSW.Core.Global.Object(HttpQueryString.GetByName(key));
			}
			return result;
		}
		public static VSW.Core.Global.Object GetValues(string key)
		{
			VSW.Core.Global.Object result;
			if (!Exists(key))
			{
				result = new VSW.Core.Global.Object();
			}
			else
			{
				result = new VSW.Core.Global.Object(VSW.Core.Global.Array.ToString(HttpQueryString.GetByTagName(key)));
			}
			return result;
		}
		private static string GetByName(string key)
		{
			return HttpQueryString.GetByName(key, string.Empty);
		}
		private static string GetByName(string key, string defaut)
		{
			string result;
			if (HttpQueryString.Exists(key))
			{
				result = HttpContext.Current.Request.QueryString[key];
			}
			else
			{
				result = defaut;
			}
			return result;
		}
		private static string[] GetByTagName(string key)
		{
			string[] result;

			if (HttpQueryString.Exists(key))
			{
				result = HttpContext.Current.Request.QueryString.GetValues(key);
			}
			else
			{
				result = new string[0];
			}
			return result;
		}
	}
}
