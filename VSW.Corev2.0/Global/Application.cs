using System;
using System.Web;

namespace VSW.Core.Global
{
	public class Application
	{
		public static string BaseDirectory
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}
		public static bool IsWebApplication
		{
			get
			{
				return HttpContext.Current != null;
			}
		}
		public static bool IsWinFormApplication
		{
			get
			{
				return !Application.IsWebApplication;
			}
		}
		public static string CryptoStringIV = Config.GetValue("Sys.CryptoStringIV").ToString();
		public static string CryptoStringKey = Config.GetValue("Sys.CryptoStringKey").ToString();

		public static bool Debug = Config.GetValue("Sys.Debug").ToBool();


		public static int TimeOutCache = Config.GetValue("Sys.TimeOutCache").ToInt();
	}
}
