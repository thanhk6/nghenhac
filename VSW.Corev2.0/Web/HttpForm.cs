using System;
using System.Web;
using VSW.Core.Global;

namespace VSW.Core.Web
{
	public class HttpForm
	{
		public static string[] AllKeys
		{
			get
			{
				string[] result;
				try
				{
					result = HttpContext.Current.Request.Form.AllKeys;
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
			return HttpContext.Current != null && HttpContext.Current.Request.Form[key] != null;
		}
		public static VSW.Core.Global.Object GetValue(string key)
		{
			VSW.Core.Global.Object result;
			if (!HttpForm.Exists(key))
			{
				result = new VSW.Core.Global.Object();
			}
			else
			{
				result = new VSW.Core.Global.Object(HttpForm.GetByName(key));
			}
			return result;
		}
		public static VSW.Core.Global.Object GetValues(string key)
		{
			VSW.Core.Global.Object result;
			if (!HttpForm.Exists(key))
			{
				result = new VSW.Core.Global.Object();
			}
			else
			{
				result = new VSW.Core.Global.Object(VSW.Core.Global.Array.ToString(HttpForm.GetByTagName(key)));
			}
			return result;
		}
		private static string GetByName(string key)
		{
			return HttpForm.GetByName(key, string.Empty);
		}
		private static string GetByName(string key, string defaut)
		{
			string result;
			if (HttpForm.Exists(key))
			{
				result = HttpContext.Current.Request.Form[key];
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
			if (HttpForm.Exists(key))
			{
				result = HttpContext.Current.Request.Form.GetValues(key);
			}
			else
			{
				result = new string[0];
			}
			return result;
		}
	}
}
