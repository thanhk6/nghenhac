using System.Globalization;
using System.Web;
using VSW.Lib.Models;
namespace VSW.Lib.Global
{
    public static class WebResource
    {
        private static string CurrentCode => CultureInfo.CurrentCulture.Name;

        public static string GetValue(string code)
        {
            return GetValue(code, string.Empty);
        }

        public static string GetValue(string code, string defalt)
        {
            var lang = SysLangService.Instance.CreateQuery()
                                    .Where(o => o.Code == CurrentCode)
                                    .ToSingle_Cache();

            if (lang == null)
                return defalt;

            var resource = WebResourceService.Instance.GetByCode_Cache(code, lang.ID);

            if (resource != null)
                return resource.Value;

            var iniResourceService = new IniResourceService(HttpContext.Current.Server.MapPath("~/Views/Lang/" + lang.Code + ".ini"));
            return iniResourceService.VSW_Core_GetByCode(code, defalt);
        }
    }
}