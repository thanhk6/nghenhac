using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.IO;

namespace VSW.Lib.Global
{
    public static class Static
    {
        public static string Tag(string rootRelativePath)
        {
            if (HttpRuntime.Cache[rootRelativePath] == null)
            {
                string absolute = HostingEnvironment.MapPath("~" + rootRelativePath);
                if (!Global.File.Exists(absolute)) return string.Empty;

                DateTime date = System.IO.File.GetLastWriteTime(absolute);
                int index = rootRelativePath.LastIndexOf('/');

                string result = HttpContext.Current.Request.IsLocal ? rootRelativePath : (rootRelativePath + "?v=" + date.Ticks);
                HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolute));
            }
            return HttpRuntime.Cache[rootRelativePath] as string;






        }
    }
}