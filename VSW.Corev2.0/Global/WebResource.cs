using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using VSW.Core.Web;
namespace VSW.Core.Global
{
	public static class WebResource
	{
		public static string Css(string path)
		{
			string path2 = HttpUtility.UrlDecode(HttpContext.Current.Server.MapPath(path));
			string result;
			if (!File.Exists(path2))
			{
				result = string.Empty;
			}
			else
			{
				DateTime lastWriteTime = File.GetLastWriteTime(path2);
				result = string.Concat(new string[]
				{
					"<link type=\"text/css\" href=\"",
					path,
					"?v=",
					string.Format("{0:ddMMyyyyhhmmss}", lastWriteTime),
					"\" rel=\"stylesheet\" />"
				});
			}
			return result;
		}
		public static string Js(string path)
		{
			string result;
			if (!File.Exists(HttpContext.Current.Server.MapPath("~" + path)))
			{
				result = string.Empty;
			}
			else
			{
				result = string.Concat(new string[]
				{
					"<script type=\"text/javascript\" src=\"",
					path,
					"\" />"
				});
				result = path;
			}
			return result;
		}
		public static string GetCss(string path)
		{
			return GetContent1("style", path);
		}
		public static string GetCss1(string path)
		{
			return GetContent("style", path);
		}

		public static string GetJs(string path)
		{
			return GetContent1("script", path);
		}
		public static string GetContent(string tag, string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			string result;
			if (tag == "css")
			{
				result = WebResource.Css(path);
			}

			else if (tag == "js")
			{
				result = WebResource.Js(path);
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		public static string GetContent1(string tag, string path)
		{
			string string_ = path;
			if (!path.StartsWith("~"))
			{
				path = "~" + path;
			}

			path = HttpUtility.UrlDecode(HttpContext.Current.Server.MapPath(path));
			string result;
			var a=File.Exists(path);

			if (!File.Exists(path))
			{
				result = string.Empty;
			}

			else
			{
				DateTime lastWriteTime = File.GetLastWriteTime(path);
				string key = Cache.CreateKey("Resource_" + Device.Mode.ToString(), "GetResource." + Class636.smethod_0(path + "." + lastWriteTime.ToString()));
				object value = Cache.GetValue(key);
				if (value != null)
				{
					result = value.ToString();
				}
				else
				{
					string text = ReadFile(path);
					if (tag == "style")
					{
						text = smethod_0(smethod_1(string_, path), text);
						text = CompressCss(text);
					}
					else if (tag == "script")
					{
						text = CompressJs1(text);
					}


					text = string.Concat(new string[]
					{
						"<",
						tag,
						">",
						text,
						"</",
						tag,
						">"
					});

					Cache.SetValue(key, text);
					result = text;
				}
			}
			return result;
		}





		public static string CompressCss(string css)
		{
			css = Regex.Replace(css, "[a-zA-Z]+#", "#");
			css = Regex.Replace(css, "[\\n\\r]+\\s*", string.Empty);
			css = Regex.Replace(css, "\\s+", " ");
			css = Regex.Replace(css, "\\s?([:,;{}])\\s?", "$1");
			css = css.Replace(";}", "}");
			css = Regex.Replace(css, "([\\s:]0)(px|pt|%|em)", "$1");
			css = Regex.Replace(css, "/\\*[\\d\\D]*?\\*/", string.Empty);
			return css;
		}




		private static string smethod_1(string string_0, string string_1)
		{
			return string_0.Replace(Path.GetFileName(string_1), "-");
		}
		public static string ReadFile(string path)
		{
			string text = string.Empty;
			string result;
			if (!File.Exists(path))
			{
				result = text;
			}
			else
			{
				using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
				{
					text = streamReader.ReadToEnd();
					result = text;
				}
			}
			return result;
		}


		public static string CompressJs(string strScript)
		{
			return Regex.Replace(strScript, "/\\*.*?\\*/", string.Empty, RegexOptions.Compiled | RegexOptions.Singleline);
		}


		public static string CompressJs1(string js)
		{
			return new Class628().method_0(js).Replace("\n", "\r\n");
		}

		private static string smethod_0(string string_0, string string_1)
		{
			WebResource.Class632 @class = new WebResource.Class632();
			@class.string_0 = string_0;

			return Regex.Replace(string_1, "url\\((.+)\\)", new MatchEvaluator(@class.method_0));
		}


		public static string GetCssmin(string path)
		{
			return GetContent2("style", path);
		}

		public static string GetContent2(string tag, string path)
		{
			string string_ = path;
			if (!path.StartsWith("~"))
			{
				path = "~" + path;
			}
			path = HttpUtility.UrlDecode(HttpContext.Current.Server.MapPath(path));
			string result;
			var a = File.Exists(path);
			if (!File.Exists(path))
			{
				result = string.Empty;
			}
			else
			{
				DateTime lastWriteTime = File.GetLastWriteTime(path);
				string key = Cache.CreateKey("Resource_" + Device.Mode.ToString(), "GetResource." + Class636.smethod_0(path + "." + lastWriteTime.ToString()));
				object value = Cache.GetValue(key);
				if (value != null)
				{
					result = value.ToString();
				}
				else
				{
					string text = ReadFile(path);
					if (tag == "style")
					{
						text = smethod_0(smethod_1(string_, path), text);
					}
					else if (tag == "script")
					{
						text = CompressJs1(text);
					}
					text = string.Concat(new string[]
					{
						"<",
						tag,
						">",
						text,
						"</",
						tag,
						">"
					});

					Cache.SetValue(key, text);
					result = text;
				}
			}
			return result;
		}

		private sealed class Class632
		{
			internal string method_0(Match match_0)
			{
				string result;
				if (match_0.Groups.Count == 2)
				{
					string text = match_0.Groups[1].Value.Trim(new char[]
					{
						'\'',
						'"'
					});
					result = string.Format("url(\"{0}\")", (text.StartsWith("http") || text.StartsWith("data:") || text.StartsWith(this.string_0)) ? text : (VSW.Core.Web.HttpRequest.Domain + this.string_0 + text));
				}
				else
				{
					result = match_0.Value;
				}
				return result;
			}
			public string string_0;
		}
		internal sealed class Class636
        {
			internal static string smethod_0(string string_0)
			{
				byte[] bytes = new UnicodeEncoding().GetBytes(string_0);
				return BitConverter.ToString(((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes));
			}

		}




	}
}
