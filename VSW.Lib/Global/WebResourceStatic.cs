using System;
using System.Web;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using VSW.Core.Web;
namespace VSW.Lib.Global
{
    public static class WebResourceStatic
    {
        public static string Css(string path)
        {
            string full_path = HttpContext.Current.Server.MapPath(path);
            bool flag = !File.Exists(full_path);
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                result = full_path;
            }
            return result;
        }
        public static string Js(string path)
        {
            string full_path = HttpContext.Current.Server.MapPath(path);
            bool flag = !File.Exists(full_path);
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                result = full_path;
            }
            return result;
        }
        public static string GetCss(string path)
        {
            return WebResourceStatic.GetContent("css", path);
        }
        public static string GetJs(string path)
        {
            return WebResourceStatic.GetContent("js", path);
        }

        public static string GetContent(string tag, string path)
        {
            object value = Cache.GetValue(path);
            bool flag = value != null;
            string text = "";
            if (flag)
            {
                text = value.ToString();
            }
            else
            {
                string full_path = HttpContext.Current.Server.MapPath(path);
                bool flag1 = !File.Exists(full_path);
                if (flag1)
                {
                    text = string.Empty;
                }
                else
                {
                    text = WebResourceStatic.ReadFile(path);
                    Cache.SetValue(path, text);
                }
            }
            string result = "";
            if (!string.IsNullOrEmpty(text))
            {
                if (tag == "css")
                {
                    result = @"<style type=""text/css"">" + text + "</style>";
                }
                else if (tag == "js")
                {
                    result = @"<script type=""text/javascript"">" + text + " </script>";
                }
            }
            return result;
        }

        public static string ReadFile(string path)
        {
            string text = string.Empty;
            string full_path = HttpContext.Current.Server.MapPath(path);
            bool flag = File.Exists(full_path);
            string result;
            if (!flag)
            {
                result = text;
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(full_path))
                {
                    text = streamReader.ReadToEnd();
                    result = text;
                }
            }
            return result;
        }
    }
}
