using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using VSW.Core.MVC;

namespace VSW.Core.Web
{

    public sealed class Application
    {
        private static List<object> list_0;
        private static readonly List<string> list_1 = new List<string>
        {
            "/bin/",
            "/content/",
            "/data/",
            "/logs/",
            "/tools/",
            "/view/"
        };


        private static readonly List<string> list_2 = new List<string>
        {
            "/.aspx/",
            "/.ascx/",
            "/.asmx/",
            "/.ashx/",
            Setting.Sys_PageExt
        };
        private static List<object> listHideDirectory
        {
            get
            {
                List<object> result;
                if ((result = list_0) == null)
                {
                    result = (list_0 = Setting.listHideDirectory);
                }
                return result;
            }
        }
        public static ViewPage CurrentViewPage
        {
            get
            {
                return HttpContext.Current.Handler as ViewPage;
            }
        }
        private static bool smethod_0(string string_1)
        {
            foreach (string text in Application.list_2)
            {
                if (!string.IsNullOrEmpty(text) && !text.Equals(string_1, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
        private static bool smethod_1(string string_1)
        {
            foreach (string value in list_1)
            {
                if (string_1.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool smethod_2(string string_1)
        {
            return string_1.Equals("/") || string_1.StartsWith("/default.aspx", StringComparison.OrdinalIgnoreCase) || string_1.Equals("/" + Setting.Sys_CPDir, StringComparison.OrdinalIgnoreCase) || string_1.Equals("/" + Setting.Sys_CPDir + "/", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/" + Setting.Sys_CPDir + "/default.aspx", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/" + Setting.Sys_CPDir + "/Content/ckfinder/core/connector", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/bin/", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/content/", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/data/", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/logs/", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/tools/", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/signalr", StringComparison.OrdinalIgnoreCase) || string_1.StartsWith("/" + Setting.Sys_CPDir + "/signalr", StringComparison.OrdinalIgnoreCase) || File.Exists(HttpContext.Current.Server.MapPath(string_1));
        }
        private static void smethod_3(string string_1)
        {
            if (!Setting.Sys_WWWMode && string_1.IndexOf("www.", StringComparison.OrdinalIgnoreCase) > -1)
            {
                HttpRequest.Redirect301(string_1.Replace("www.", ""));
            }
        }

        private static void smethod_4(string string_1)
        {
            if (Setting.Sys_SSLMode)
            {
                if (string_1.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                {
                    HttpRequest.Redirect301(string_1.Replace("http://", "https://"));
                }
            }
            else if (string_1.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                HttpRequest.Redirect301(string_1.Replace("https://", "http://"));
            }
        }
        private static void smethod_5(string string_1)
        {
            if (string_1.StartsWith("/default", StringComparison.OrdinalIgnoreCase))
            {
                HttpRequest.Redirect301(HttpRequest.Domain);
            }
            if (string_1.StartsWith("/" + Setting.Sys_CPDir + "/default", StringComparison.OrdinalIgnoreCase))
            {
                HttpRequest.Redirect301(HttpRequest.Domain + "/" + Setting.Sys_CPDir + "/");
            }
        }
        public static void BeginRequest()
        {
            HttpContext httpContext = HttpContext.Current;
            string text = httpContext.Request.Path.ToString();
            string rawUrl = HttpRequest.RawUrl;
            string currentExecutionFilePathExtension = HttpContext.Current.Request.CurrentExecutionFilePathExtension;
            string path = HttpRequest.Path;
            string absoluteUri = HttpRequest.AbsoluteUri;
            if (!File.Exists(HttpContext.Current.Server.MapPath(text)) && !File.Exists(HttpContext.Current.Server.MapPath(text) + "/default.aspx") && !text.ToLower().EndsWith(".axd") && text.ToLower().Contains("sitemap") && text.ToLower().EndsWith("xml"))
            {
                httpContext.RewritePath("~/Default.aspx", false);
                httpContext.Items["VSW_VQS"] = text.Replace("/", "");
                httpContext.Items["VSW_PARAMS"] = "";
                return;
            }
            if (!File.Exists(HttpContext.Current.Server.MapPath(text)) && !File.Exists(HttpContext.Current.Server.MapPath(text) + "/default.aspx") && !text.ToLower().EndsWith(".axd") && text.ToLower().Contains("rss") && text.ToLower().EndsWith("rss"))
            {
                httpContext.RewritePath("~/Default.aspx", false);
                httpContext.Items["VSW_VQS"] = text.Replace("/", "");
                httpContext.Items["VSW_PARAMS"] = "";
                return;
            }
            smethod_5(rawUrl);
            smethod_3(absoluteUri);
            //// smethod_4(absoluteUri);
            if (!smethod_1(text) && !smethod_2(path) && !smethod_0(currentExecutionFilePathExtension))
            {
                string query = httpContext.Request.Url.Query;
                smethod12(text);
                smethod_6(text, query);
               //smethod_15(text, query);
                smethod_7(text, currentExecutionFilePathExtension);
                smethod_8(text, currentExecutionFilePathExtension);
                smethod_11(rawUrl, currentExecutionFilePathExtension);

                RemoveSpecial(text);
                smethod_13(text);

                string[] array = new string[]
                {
                    "^/" + HttpRequest.ApplicationPath + Setting.Sys_CPDir + "/([0-9a-zA-Z-_//]+).aspx([0-9a-zA-Z-_//]*)",
                    "~/" + Setting.Sys_CPDir + "/Default.aspx",
                    string.Concat(new string[]
                    {
                        "^/",
                        HttpRequest.ApplicationPath,
                        "([0-9a-zA-Z-_//]+)",
                        Setting.Sys_PageExt,
                        "([0-9a-zA-Z-_//]*)"
                    }),
                    "~/Default.aspx"
                };
                for (int i = 0; i < array.Length; i += 2)
                {
                    Match match = new Regex(array[i], RegexOptions.IgnoreCase).Match(HttpContext.Current.Request.Path);
                    if (match.Groups.Count > 1)
                    {
                        string value = match.Groups[1].Captures[0].ToString();
                        string value2 = string.Empty;
                        if (match.Groups.Count > 2)
                        {
                            value2 = match.Groups[2].Captures[0].ToString();
                        }
                        string path2 = array[i + 1] + query;
                        httpContext.RewritePath(path2, false);
                        httpContext.Items["VSW_VQS"] = value;
                        httpContext.Items["VSW_PARAMS"] = value2;
                        return;
                    }
                }
            }
        }
        private static void smethod_6(string string_1, string string_2)
        {
            if (string_1.IndexOf("%20", StringComparison.OrdinalIgnoreCase) > -1)
            {
                string_1 = string_1.Trim().Replace(" ", "").Replace("%20", "") + ((!string.IsNullOrEmpty(string_2)) ? ("?" + string_2) : "");
                HttpRequest.Redirect301(HttpRequest.Domain + string_1);
            }

        }
        private static void smethod_15(string string_1, string string_2)
        {
            if (string_1.IndexOf("https", StringComparison.OrdinalIgnoreCase) > -1)
            {
                string_1 = string_1.Trim().Replace(" ", "").Replace("https", "") + ((!string.IsNullOrEmpty(string_2)) ? ("?" + string_2) : "");
                HttpRequest.Redirect301(HttpRequest.Domain + string_1);
            }

        }
        private static void smethod_7(string string_1, string string_2)
        {
            if (string_1.StartsWith("/sitemap", StringComparison.OrdinalIgnoreCase) && string_2 != ".xml")
            {
                HttpRequest.Redirect301(string_1.Replace(string_2, ".xml"));
            }
        }
        private static void smethod_8(string string_1, string string_2)
        {
            if (string_1.StartsWith("/rss", StringComparison.OrdinalIgnoreCase) && string_2 != ".rss")
            {
                HttpRequest.Redirect301(string_1.Replace(string_2, ".rss"));
            }
        }
        private static void smethod_13(string string_1)
        {
            if (string_1.StartsWith("/data/", StringComparison.OrdinalIgnoreCase))
            {
                int num = 0;
                while (listHideDirectory != null && num < listHideDirectory.Count)
                {
                    string value = listHideDirectory[num].ToString();
                    if (!string.IsNullOrEmpty(value) && string_1.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                    {
                        HttpRequest.Error404();
                        break;
                    }
                    num++;
                }
            }
        }
        private static void smethod_11(string string_1, string string_2)
        {
            if (!string_1.Equals("/") && !string_1.StartsWith("/default", StringComparison.OrdinalIgnoreCase) && !string_1.StartsWith("/" + Setting.Sys_CPDir, StringComparison.OrdinalIgnoreCase) && !string_1.StartsWith("/sitemap", StringComparison.OrdinalIgnoreCase) && !string_1.StartsWith("/rss", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(string_2))
                {
                    if (!string.IsNullOrEmpty(Setting.Sys_PageExt))
                    {
                        string[] array = string_1.Split(new char[]
                        {
                            '?'
                        });
                        string_1 = array[0] + Setting.Sys_PageExt + ((array.Length > 1) ? ("?" + array[1]) : string.Empty);

                        HttpRequest.Redirect301(HttpRequest.Domain + string_1);
                    }
                }

                else if (!string_2.Equals(Setting.Sys_PageExt, StringComparison.OrdinalIgnoreCase))
                {
                    string_1 = string_1.Replace(string_2, Setting.Sys_PageExt);
                    HttpRequest.Redirect301(HttpRequest.Domain + string_1);
                }
            }
        }
        private static void smethod12(string text)
        {
            string text2 = text;
            int a = text2.IndexOf("html");
            string b;

            if (a > -1)
            {
                if ((a + 4) < text2.Length)
                {
                    b = text2.Substring(a + 4);
                    HttpRequest.Redirect301(HttpRequest.Domain + "/" + b);
                }
                else
                {
                    b = text2;
                }
            }
            else
            {
                b = text2;
            }
              

        }
        private static void Smetho14(string query)
        {

        }
        internal static void RemoveSpecial(string absolutePath)
        {
            absolutePath = HttpUtility.UrlDecode(absolutePath);
            string text = absolutePath;
            if (absolutePath.IndexOf("\"", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("\"", "");
            }
            if (absolutePath.IndexOf("??", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("??", "?");
            }
            if (absolutePath.IndexOf("//", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("//", "/");
            }
            if (absolutePath.IndexOf("..", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("..", ".");
            }
            if (absolutePath.IndexOf(",", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace(",", "");
            }
            if (absolutePath.IndexOf("&", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("&", "");
            }
            if (absolutePath.IndexOf(">", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace(">", "");
            }
            if (absolutePath.IndexOf("<", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("<", "");
            }
            if (absolutePath.IndexOf("*", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("*", "");
            }
            if (absolutePath.IndexOf(":", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace(":", "");
            }
            if (absolutePath.IndexOf("/.", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("/.", ".");
            }
            if (absolutePath.Equals("/" + Setting.Sys_PageExt, StringComparison.OrdinalIgnoreCase))
            {
                text = string.Empty;
            }
            if (absolutePath.IndexOf("//", StringComparison.Ordinal) > -1)
            {
                text = absolutePath.Replace("//", "/");
            }
            if (!text.Equals(absolutePath, StringComparison.OrdinalIgnoreCase))
            {
                HttpRequest.Redirect301(HttpRequest.Domain + text);
            }


        }

        public static void PreRequestHandlerExecute(HttpApplication app)
        {
        }
    }
}
