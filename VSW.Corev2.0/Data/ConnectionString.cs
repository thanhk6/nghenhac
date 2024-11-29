using System;
using System.Configuration;
namespace VSW.Core.Data
{
	public sealed class ConnectionString
	{
		public static string Default
		{
			get
			{
				return GetByConfigKey(DefaultConfigKey);
			}
		}
		public static string GetByConfigKey(string configKey)
		{
			if (string.IsNullOrEmpty(configKey))
			{
				configKey = DefaultConfigKey;
			}
			string result;
			if (ConfigurationManager.ConnectionStrings[configKey] != null)
			{
				result = ConfigurationManager.ConnectionStrings[configKey].ConnectionString;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		public static string DefaultConfigKey = "DBConnection";
	}
}
