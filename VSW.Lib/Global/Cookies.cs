using System;
using System.Web;

namespace VSW.Lib.Global
{
    public static class Cookies
    {
        private static string SiteID => Core.Web.Setting.Sys_SiteID + "_";

        public static bool Exists(string key)
        {
            key = SiteID + key;

            return HttpContext.Current.Request.Cookies[key] != null;
        }

        public static void SetValue(string key, string value, int minutes, bool secure)
        {
            key = SiteID + key;

            var IP = Core.Web.HttpRequest.IP;

            var response = HttpContext.Current.Response;

            if (IP != "127.0.0.1" && Core.Web.Setting.Mod_DomainCookies != string.Empty)
                response.Cookies[key].Domain = Core.Web.Setting.Mod_DomainCookies;

            if (secure)
                response.Cookies[key].Value = Core.Global.CryptoString.Encrypt(IP + "_VSW_" + key + value);
            else
                response.Cookies[key].Value = value;

            if (minutes > 0)
                response.Cookies[key].Expires = DateTime.Now.AddMinutes(minutes);
        }

        public static void SetValue(string key, string value, int minutes)
        {
            SetValue(key, value, minutes, false);
        }

        public static void SetValue(string key, string value, bool secure)
        {
            SetValue(key, value, 0, secure);
        }

        public static void SetValue(string key, string value)
        {
            SetValue(key, value, 0, false);
        }

        public static string GetValue(string key, bool secure)
        {
            if (!Exists(key))
                return string.Empty;

            string tempKey = SiteID + key;

            if (!secure)
                return HttpContext.Current.Request.Cookies[tempKey].Value;

            var IP = Core.Web.HttpRequest.IP;
            string value = Core.Global.CryptoString.Decrypt(HttpContext.Current.Request.Cookies[tempKey].Value).Replace(IP + "_VSW_" + tempKey, string.Empty);

            if (value.IndexOf("_VSW_" + tempKey) > -1)
            {
                Remove(key);

                return string.Empty;
            }

            return value;
        }

        public static string GetValue(string key)
        {
            return GetValue(key, false);
        }

        public static void Remove(string key)
        {
            if (!Exists(key)) return;

            key = SiteID + key;

            //ip
            var IP = Core.Web.HttpRequest.IP;

            var response = HttpContext.Current.Response;

            //local
            if (IP != "127.0.0.1" && Core.Web.Setting.Mod_DomainCookies != string.Empty)
                response.Cookies[key].Domain = Core.Web.Setting.Mod_DomainCookies;

            //remove
            response.Cookies[key].Value = string.Empty;
            response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
        }
    }
}