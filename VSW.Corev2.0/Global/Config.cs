using System;
using System.Configuration;

namespace VSW.Core.Global
{
	public abstract class Config
	{
		
		public static bool Exists(string key)
		{
			return ConfigurationManager.AppSettings[key] != null;
		}
		public static Object GetValue(string key)
		{
			Object result;
			if (!Exists(key))
			{
				result = new Object();
			}
			else
			{
				result = new Object(GetByConfiKey(key));
			}
			return result;
		}
		private static string GetByConfiKey(string configKey)
		{
			string result;
			if (Exists(configKey))
			{
				result = ConfigurationManager.AppSettings[configKey];
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
