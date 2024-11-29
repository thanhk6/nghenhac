using System;
using System.IO;
using System.Web;
namespace VSW.Core.Web
{
	public class HttpRequest
	{
		public static string Domain
		{
			get
			{
				return Scheme + "://" + Host + (IsLocal ? (":" + Port.ToString()) : string.Empty);
			}
		}
		public static bool CPMode
		{
			get
			{
				return RawUrl.StartsWith("/" + Setting.Sys_CPDir + "/", StringComparison.OrdinalIgnoreCase);
			}
		}
		public static string Scheme
		{
			get
			{
				return HttpContext.Current.Request.Url.Scheme;
			}
		}

		public static string Host
		{
			get
			{
				return HttpContext.Current.Request.Url.Host;
			}
		}
		public static int Port
		{
			get
			{
				return HttpContext.Current.Request.Url.Port;
			}
		}
		public static string Authority
		{
			get
			{
				return HttpContext.Current.Request.Url.Authority;
			}
		}
		public static string ApplicationPath
		{
			get
			{
				string applicationPath = HttpContext.Current.Request.ApplicationPath;
				string result;
				if (applicationPath == "/")
				{
					result = string.Empty;
				}
				else if (applicationPath != null)
				{
					result = applicationPath.Substring(1) + "/";
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
		}
		public static string Path
		{
			get
			{
				return HttpContext.Current.Request.Path;
			}
		}
		public static string AbsolutePath
		{
			get
			{
				return HttpContext.Current.Request.Url.AbsolutePath;
			}
		}
		public static string PathAndQuery
		{
			get
			{
				return HttpContext.Current.Request.Url.PathAndQuery;
			}
		}
		public static string Query
		{
			get
			{
				return HttpContext.Current.Request.Url.Query;
			}
		}
		public static string RawUrl
		{
			get
			{
				return HttpContext.Current.Request.RawUrl;
			}
		}
		public static string AbsoluteUri
		{
			get
			{
				return HttpContext.Current.Request.Url.AbsoluteUri;
			}
		}
		public static bool IsLocal
		{
			get
			{
				return HttpContext.Current.Request.IsLocal;
			}
		}
		public static bool IsSecureConnection
		{
			get
			{
				return HttpContext.Current.Request.IsSecureConnection;
			}
		}
		public static string IP
		{
			get
			{
				return HttpContext.Current.Request.UserHostAddress;
			}
		}
		//public static string CurrentURL
		//{
		//	get
		//	{
		//		string scheme = HttpRequest.Scheme;
		//		string host = HttpRequest.Host;
		//		int port = HttpRequest.Port;
		//		string applicationPath = HttpRequest.ApplicationPath;
		//		string result;
		//		if (port != 80)
		//		{
		//			result = string.Concat(new object[]
		//			{
		//				scheme,
		//				"://",
		//				host,
		//				":",
		//				port,
		//				applicationPath
		//			});
		//		}
		//		else
		//		{
		//			result = scheme + "://" + host + applicationPath;
		//		}
		//		return result;
		//	}
		//}

		public static string CurrentURL
		{
			get
			{
				return Domain + ApplicationPath;
			}
		}
		public static string Browser
		{
			get
			{
				return HttpContext.Current.Request.Browser.Browser;
			}
		}
		public static int BrowserVersion
		{
			get
			{
				return HttpContext.Current.Request.Browser.MajorVersion;
			}
		}
		public static bool Firefox
		{
			get
			{
				return Browser.ToLower() == "firefox";
			}
		}

		public static bool IE
		{
			get
			{
				return Browser.ToLower() == "ie";
			}
		}
		public static bool Safari
		{
			get
			{
				return  Browser.ToLower() == "applemac-safari";
			}
		}
		public static void Redirect301(string url)
		{
			HttpResponse response = HttpContext.Current.Response;
			response.Clear();
			response.Buffer = true;
			response.Status = "301 Moved Permanently";
			response.StatusCode = 301;
			response.AddHeader("Location", url);
			response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate");
			response.AppendHeader("Pragma", "no-cache");
			response.AppendHeader("Expires", "0");
			response.End();
		}
		public static void Redirect400(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}
		public static void Redirect401(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}	
		public static void Error404()
		{
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.StatusCode = 404;
			//HttpContext.Current.Response.Write(File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/HttpError/404.html")));
			Redirect301(Domain);
			HttpContext.Current.Response.End();
		}
		public static void Error500()
		{
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.StatusCode = 500;
			HttpContext.Current.Response.Write(File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/HttpError/500.html")));
			HttpContext.Current.Response.End();
		}
		public static void OnError(out Exception ex)
		{
			ex = HttpContext.Current.Server.GetLastError();
			if (!HttpRequest.IsLocal)
			{
				HttpContext.Current.Server.ClearError();
				HttpRequest.Error500();
			}
		}
	}
}
