using System;
using System.Web;
using VSW.Core.Global;

namespace VSW.Core.Web
{
	public class Cookies
	{
		private static string SiteID
		{
			get
			{
				return Setting.Sys_SiteID + "_";
			}
		}

		public static bool Exists(string key)
		{
			key = Cookies.SiteID + key;
			return HttpContext.Current.Request.Cookies[key] != null;
		}
		public static void SetValue(string key, string value, int minutes, bool secure)
		{
			key = Cookies.SiteID + key;
			string userHostAddress = HttpContext.Current.Request.UserHostAddress;
			if (userHostAddress != "127.0.0.1" && Setting.Sys_DomainCookies != string.Empty)
			{
				HttpContext.Current.Response.Cookies[key].Domain = Setting.Sys_DomainCookies;
			}
			if (secure)
			{
				HttpContext.Current.Response.Cookies[key].Value = CryptoString.Encrypt(userHostAddress + "_VSW_" + key + value);
			}
			else
			{
				HttpContext.Current.Response.Cookies[key].Value = value;
			}
			if (minutes > 0)
			{
				HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddMinutes((double)minutes);
			}
		}
		public static void SetValue(string key, string value, int minutes)
		{
			Cookies.SetValue(key, value, minutes, false);
		}
		public static void SetValue(string key, string value, bool secure)
		{
			Cookies.SetValue(key, value, 0, secure);
		}
		public static void SetValue(string key, string value)
		{
			Cookies.SetValue(key, value, 0, false);
		}
		public static string GetValue(string key, bool secure)
		{
			string result;
			if (Cookies.Exists(key))
			{
				key = Cookies.SiteID + key;
				if (secure)
				{
					string userHostAddress = HttpContext.Current.Request.UserHostAddress;
					string text = CryptoString.Decrypt(HttpContext.Current.Request.Cookies[key].Value).Replace(userHostAddress + "_VSW_" + key, string.Empty);
					if (text.IndexOf("_VSW_" + key) > -1)
					{
						Cookies.Remove(key);
						result = string.Empty;
					}
					else
					{
						result = text;
					}
				}
				else
				{
					result = HttpContext.Current.Request.Cookies[key].Value;
				}
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
		public static string GetValue(string Key)
		{
			return Cookies.GetValue(Key, false);
		}
		public static void Remove(string Key)
		{
			if (Cookies.Exists(Key))
			{
				Key = Cookies.SiteID + Key;
				if (HttpContext.Current.Request.UserHostAddress != "127.0.0.1" && Setting.Sys_DomainCookies != string.Empty)
				{
					HttpContext.Current.Response.Cookies[Key].Domain = Setting.Sys_DomainCookies;
				}
				HttpContext.Current.Response.Cookies[Key].Value = string.Empty;
				HttpContext.Current.Response.Cookies[Key].Expires = DateTime.Now.AddDays(-1.0);
			}
		}
	}
}
