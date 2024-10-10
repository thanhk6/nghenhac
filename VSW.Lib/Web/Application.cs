using System;
using System.Collections.Generic;
using System.Reflection;

using System.Web;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Web
{

    public class Application : HttpApplication
    {
        #region private
        private void Redirection()
        {
            var absoluteUri = Request.Url.AbsoluteUri.ToString();
            if (absoluteUri.Length >= 500) Response.Redirect(Core.Web.HttpRequest.Domain);

            var listRedirection = WebRedirectionService.Instance.CreateQuery().ToList_Cache();
            if (listRedirection == null) return;
            var index = listRedirection.FindIndex(o => o.Url == absoluteUri);
            if (index <= -1 || string.IsNullOrEmpty(listRedirection[index].Redirect)) return;            
                Core.Web.HttpRequest.Redirect301(listRedirection[index].Redirect);           
        }

        #endregion private
        public static List<CPModuleInfo> CPModules { get; set; }
        public new static List<ModuleInfo> Modules { get; set; }
        protected void Application_Start(object sender, EventArgs e)
        {
            //signalR
            //RouteTable.Routes.MapHubs();
            //license excel
            var licenseFile = HttpContext.Current.Server.MapPath("~/bin/Aspose.Cells.lic");
            if (System.IO.File.Exists(licenseFile))
            {
                //Aspose.Cells.License license = new Aspose.Cells.License();
                //license.SetLicense(licenseFile);
            }
            if (CPModules != null) return;

            CPModules = new List<CPModuleInfo>();
            Modules = new List<ModuleInfo>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(CPModuleInfo), true);
                if (attributes.GetLength(0) == 0)
                {
                    attributes = type.GetCustomAttributes(typeof(ModuleInfo), true);
                    if (attributes.GetLength(0) == 0)
                        continue;
                    if (attributes[0] is ModuleInfo moduleInfo && Modules.Find(o => o.Code == moduleInfo.Code) == null)
                    {
                        moduleInfo.ModuleType = type;

                        Modules.Add(moduleInfo);
                    }
                    continue;
                }
                {
                    if (!(attributes[0] is CPModuleInfo moduleInfo)) continue;

                    if (CPModules.Find(o => o.Code == moduleInfo.Code) == null)
                        CPModules.Add(moduleInfo);
                }
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string rawUrl = HttpContext.Current.Request.RawUrl;
            if (rawUrl.Contains("?gidzl"))
                Core.Web.HttpRequest.Redirect301(rawUrl.Split('?')[0]);
            if(rawUrl.Contains("&gidzl"))
                Core.Web.HttpRequest.Redirect301(rawUrl.Split('&')[0]);
            Redirection();
           
            Core.Web.Application.BeginRequest();
           
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Global.Application.OnError();
        }

        //protected void application_prerequesthandlerexecute(object sender, eventargs e)
        //{
        //    core.web.application.prerequesthandlerexecute(sender as httpapplication);
        //}
    }
}