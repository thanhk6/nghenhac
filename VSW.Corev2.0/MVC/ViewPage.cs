using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.UI;
using VSW.Core.Global;
using VSW.Core.Interface;
using VSW.Core.Models;
using VSW.Core.Web;
namespace VSW.Core.MVC
{
    public class ViewPage : ViewPageBase
    {
        public string ContentPath
        {
            get
            {
                return base.ApplicationPath + "Content";
            }
        }
        public List<Controller> Controllers
        {
            get
            {
                return this.list_0;
            }
        }
        public string CPPath
        {
            get
            {
                return base.ApplicationPath + Setting.Sys_CPDir;
            }
        }
        public Custom CurrentCustom { get; private set; }
        public IPageInterface CurrentPage { get; private set; }
        public ISiteInterface CurrentSite { get; private set; }
        public ITemplateInterface CurrentTemplate { get; private set; }
        public string DataPath
        {
            get
            {
                return base.ApplicationPath + "Data";
            }
        }
        public ILangServiceInterface LangService { get; protected set; }
        public IModuleServiceInterface ModuleService { get; protected set; }
        public string PageExt { get; private set; }
        public IPageServiceInterface PageService { get; protected set; }
        public string ReturnPath { get; private set; }
        public ISiteServiceInterface SiteService { get; protected set; }
        public ITemplateServiceInterface TemplateService { get; protected set; }
        public string ToolsPath
        {
            get
            {
                return base.ApplicationPath + "Tools";
            }
        }
        public string URLBase { get; private set; }
        public string ViewsPath
        {
            get
            {
                return base.ApplicationPath + "Views";
            }
        }
        public bool MobileMode
        {
            get
            {
                return DeviceDetect.Device == "Mobile";
            }
        }
        public bool TabletMode
        {
            get
            {
                return DeviceDetect.Device == "Tablet";
            }
        }
        public void ChangeTemplate(ITemplateInterface template)
        {
            this.CurrentTemplate = template;
            this.bool_1 = true;
        }
        //public string GetURL(params object[] values)
        //{
        //    string text = this.URLBase;
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        if (values[i] != null)
        //        {
        //            if (values[i] is DateTime)
        //            {
        //                text = text + string.Format("{0:yyyy-MM-dd}", values[i]) + "/";
        //            }
        //            else
        //            {
        //                text = text + values[i].ToString().Trim() + "/";
        //            }
        //        }
        //    }
        //    if (text.EndsWith("/"))
        //    {
        //        text = text.Substring(0, text.Length - 1);
        //    }
        //    return (text + this.PageExt).Replace("//", "/").Replace("//", "/");
        //}
        public string GetURL(params object[] values)
        {
            string result;
            if (values == null)
            {
                result = "#";
            }
            else
            {
                if (string.IsNullOrEmpty(this.PageExt))
                {
                    this.PageExt = "/";
                }
                string text = (!Setting.Sys_MultiSite) ? ("/" + base.ApplicationPath) : ("/" + base.ApplicationPath + this.CurrentSite.Code);
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] != null)
                    {
                        string text2 = values[i].ToString();
                        if (!string.IsNullOrEmpty(text2) && !text2.StartsWith(".", StringComparison.OrdinalIgnoreCase))
                        {
                            if (values[i] is DateTime)
                            {
                                text = text + "/" + string.Format("{0:yyyy-MM-dd}", values[i]);
                            }
                            else
                            {
                                text = text + "/" + text2.ToString().Trim();
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(text))
                {
                    result = "#";
                }

                else
                {
                    text += this.PageExt;
                    text = text.Replace("//", "/");
                    if (VSW.Core.Web.HttpRequest.IsLocal)
                    {
                        result = string.Concat(new string[]
                        {
                            VSW.Core.Web.HttpRequest.Scheme,
                            "://",
                            VSW.Core.Web.HttpRequest.Host,
                            ":",
                            VSW.Core.Web.HttpRequest.Port.ToString(),
                            text
                        });
                    }
                    else
                    {
                        result = VSW.Core.Web.HttpRequest.Scheme + "://" + VSW.Core.Web.HttpRequest.Host + text;
                    }
                }
            }
            return result;
        }


        protected sealed override void InitializeCulture()
        {
            base.nguyenthanh_0();
            this.PageExt = Setting.Sys_PageExt;
            string s = base.ActionForm = base.Request.RawUrl;
            this.ReturnPath = base.Server.UrlEncode(s);
            if (Setting.Sys_MultiSite && base.CurrentVQS.Count > 0)
            {
                string @string = base.CurrentVQS.GetString(0);
                this.CurrentSite = this.SiteService.VSW_Core_GetByCode(@string);
                base.CurrentVQS.nguyenthanh_0();
            }
            if (!Setting.Sys_MultiSite || (Setting.Sys_MultiSite && base.CurrentVQS.Count == 0))
            {
                this.CurrentSite = this.SiteService.VSW_Core_GetDefault();
            }
            if (this.CurrentSite == null)
            {
                this.CurrentSite = this.SiteNotFound();
            }
            if (this.CurrentSite == null)
            {
                throw new Exception("CurrentSite = null");
            }
            base.CurrentLang = this.LangService.VSW_Core_GetByID(this.CurrentSite.LangID);
            if (base.CurrentLang == null)
            {
                throw new Exception("CurrentLang = null");
            }
            if (base.CurrentVQS.Count == 0)
            {
                this.CurrentPage = this.PageService.VSW_Core_GetByID(this.CurrentSite.PageID);
                base.CurrentVQSMVC = base.CurrentVQS;
            }
            else
            {
                base.CurrentVQSMVC = new VQS(string.Empty);
                this.CurrentPage = this.PageService.VSW_Core_CurrentPage(this);
            }
            if (this.CurrentPage == null)
            {
                this.CurrentPage = this.PageNotFound();
            }
            if (this.CurrentPage == null)
            {
                throw new Exception("CurrentPage = null");
            }
            if (this.MobileMode)
            {
                this.CurrentTemplate = this.TemplateService.VSW_Core_GetByID(this.CurrentPage.TemplateMobileID);
            }
            else if (this.TabletMode)
            {
                this.CurrentTemplate = this.TemplateService.VSW_Core_GetByID(this.CurrentPage.TemplateTabletID);
            }
            else
            {
                this.CurrentTemplate = this.TemplateService.VSW_Core_GetByID(this.CurrentPage.TemplateID);
            }
            this.SetCustom();
            if (!string.IsNullOrEmpty(this.CurrentPage.ModuleCode))
            {
                this.nguyenthanh_6();
            }
            base.nguyenthanh_1();
            base.nguyenthanh_2(true);
            base.nguyenthanh_4(string.Empty);
            if (this.CurrentTemplate == null)
            {
                throw new Exception("CurrentTemplate = null");
            }

            base.InitializeCulture();
            this.Page.Culture = base.CurrentLang.Code;
            this.Page.UICulture = base.CurrentLang.Code;
            if (this.bool_1)
            {
                this.SetCustom();
            }
            this.GetURL();
        }
        private Custom GetCustom(string moduleCode, string vswID)
        {
            if (vswID == string.Empty)
            {
                vswID = moduleCode;
            }
            Custom custom = new Custom();
            string[] allKeys = this.CurrentCustom.AllKeys;
            for (int i = 0; i < allKeys.Length; i++)
            {
                if (allKeys[i].StartsWith(vswID + "."))
                {
                    custom.SetValue(allKeys[i].Replace(vswID + ".", string.Empty), this.CurrentCustom.GetValue(allKeys[i]).Current);
                }
            }
            return custom;
        }
        private void InitPage(Control control)
        {
            foreach (object obj in control.Controls)
            {
                Control control2 = (Control)obj;

                if (control2 is StaticControl)
                {
                    StaticControl staticControl = control2 as StaticControl;
                    this.RenderHtml(staticControl, staticControl.Code, staticControl.VSWID, staticControl.DefaultLayout, staticControl.DefaultAction, staticControl.DefaultProperties);
                }

                else if (control2 is DynamicControl)
                {
                    DynamicControl dynamicControl = control2 as DynamicControl;
                    this.nguyenthanh_12(control2, this.CurrentCustom.GetValue("Template_" + dynamicControl.Code).ToString());
                }
                else if (control2.HasControls())
                {
                    this.InitPage(control2);
                }
            }
        }
        private void nguyenthanh_12(Control control, string custom)
        {
            string[] array = custom.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                string[] array2 = array[i].Split(new char[]
                {
                    '|'
                });
                string text = array2[0].Trim();
                string vswID = (array2.Length == 2) ? array2[1] : string.Empty;
                if (text != string.Empty)
                {
                    if (text == "VSWMODULE")
                    {
                        this.nguyenthanh_14(control);
                    }
                    else
                    {
                        this.RenderHtml(control, text, vswID, null, null, null);
                    }
                }
            }
        }
        private void RenderHtml(Control control, string controlCode, string vswID, string viewLayout, string action, string defaultProperty)
        {
            IModuleInterface moduleInterface = this.ModuleService.VSW_Core_GetByCode(controlCode);
            if (moduleInterface != null)
            {
                Predicate<string> predicate = null;
                ViewPage.DynamicAction dynamicAction = new ViewPage.DynamicAction();
                Class @class = new Class(moduleInterface.ModuleType);
                dynamicAction.controller = (@class.Instance as Controller);
                this.Controllers.Add(dynamicAction.controller);
                dynamicAction.controller.ViewLayout = viewLayout;
                dynamicAction.controller.IndexAction = action;
                if (!string.IsNullOrEmpty(defaultProperty))
                {
                    string[] array = defaultProperty.Split(new char[]
                    {
                        ','
                    });
                    for (int i = 0; i < array.Length; i++)
                    {
                        int num = array[i].IndexOf('=');
                        if (num > -1)
                        {
                            string fieldName = array[i].Substring(0, num);
                            string value = array[i].Substring(num + 1, array[i].Length - num - 1);
                            FieldInfo fieldInfo = @class.GetFieldInfo(fieldName);
                            if (fieldInfo != null)
                            {
                                try
                                {
                                    @class.SetField(fieldInfo.Name,Global.Convert.AutoValue(value, fieldInfo.FieldType));
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }


                dynamicAction.controller.InitPage(this);
                dynamicAction.controller.SetCustom(this.GetCustom(moduleInterface.Code, vswID));
                dynamicAction.controller.OnLoad();
                if (!string.IsNullOrEmpty(dynamicAction.controller.IndexAction))
                {
                    if (predicate == null)
                    {
                        predicate = new Predicate<string>(dynamicAction.Exists);
                    }
                    string text = @class.GetMethodsName().Find(predicate);

                    if (text != null)
                    {
                        ViewPageRender.RenderPage(this, dynamicAction.controller, @class, text, null);
                    }
                }

                if (string.IsNullOrEmpty(dynamicAction.controller.ViewLayout))
                {
                    throw new Exception(string.Concat(new string[]
                    {
                        "The control '",
                        moduleInterface.Code,
                        ":",
                        vswID,
                        "' not set Layout"
                    }));
                }
                string text2 = string.Concat(new string[]
                {
                    "~/Views/",
                    moduleInterface.Code,
                    "/",
                    dynamicAction.controller.ViewLayout,
                    ".ascx"
                });


                if (!File.Exists(base.Server.MapPath(text2)))
                {
                    throw new Exception("The file '" + text2 + "' not found");
                }

                ViewControl viewControl = base.LoadControl(text2) as ViewControl;
                dynamicAction.controller.ViewControl = viewControl;
                viewControl.VSWID = vswID;
                control.Controls.Add(viewControl);
                if (base.IsPostBack)
                {
                    Predicate<string> predicate2 = null;
                    ViewPage.StaticAction staticAction = new ViewPage.StaticAction
                    {
                        _dynamicAction = dynamicAction,
                        FuncNamePOST = base.FuncNamePOST
                    };
                    if (!string.IsNullOrEmpty(staticAction.FuncNamePOST) && staticAction.FuncNamePOST.IndexOf(controlCode + "-") > -1 && staticAction.FuncNamePOST.IndexOf("-" + dynamicAction.controller.ViewLayout) > -1)
                    {
                        string[] array2 = staticAction.FuncNamePOST.Split(new char[]
                        {
                            '-'
                        });
                        if (array2.Length == 3)
                        {
                            if (array2[0] == controlCode && array2[2] == dynamicAction.controller.ViewLayout)
                            {
                                staticAction.FuncNamePOST = array2[1];
                            }
                            else
                            {
                                staticAction.FuncNamePOST = null;
                            }
                        }
                        else
                        {
                            staticAction.FuncNamePOST = null;
                        }
                    }
                    else
                    {
                        staticAction.FuncNamePOST = null;
                    }
                    if (!string.IsNullOrEmpty(staticAction.FuncNamePOST))
                    {
                        if (predicate2 == null)
                        {
                            predicate2 = new Predicate<string>(staticAction.Exists);
                        }
                        string text3 = @class.GetMethodsName().Find(predicate2);
                        if (text3 != null)
                        {
                            ViewPageRender.RenderPage(this, dynamicAction.controller, @class, text3, base.FuncParamPOST);
                        }
                    }
                }
                viewControl.Controller = dynamicAction.controller;
                viewControl.ViewData = dynamicAction.controller.ViewData;
                viewControl.ViewBag = dynamicAction.controller.ViewBag;
                this.InitPage(viewControl);
            }
        }
        private void nguyenthanh_14(Control control)
        {
            if (base.ViewControl != null)
            {
                control.Controls.Add(base.ViewControl);
                this.InitPage(base.ViewControl);
            }
        }
        private void nguyenthanh_6()
        {
            base.CurrentModule = this.ModuleService.VSW_Core_GetByCode(this.CurrentPage.ModuleCode);
            base.CurrentModule = this.OnPreLoadModule(base.CurrentModule);
            if (base.CurrentModule == null)
            {
                throw new Exception("CurrentModule = null");
            }
            Class @class = new Class(base.CurrentModule.ModuleType);
            base.Controller = (@class.Instance as Controller);
            base.Controller.InitPage(this);
            base.Controller.SetCustom(this.GetCustom(base.CurrentModule.Code, base.CurrentModule.Code));
            base.Controller.OnLoad();
            base.nguyenthanh_3(@class, base.Controller.DefaultAction);
        }
        private void SetMeta(IPageInterface entityBase)
        {
            if (entityBase is EntityBase)
            {
                base.SetMeta(entityBase as EntityBase);
            }
        }
        private void GetURL()
        {
            if (!Setting.Sys_MultiSite)
            {
                this.URLBase = "/" + base.ApplicationPath;
                return;
            }
            this.URLBase = "/" + base.ApplicationPath + this.CurrentSite.Code + "/";
        }

        private void SetCustom()
        {
            this.CurrentCustom = new Custom();
            if (this.CurrentTemplate != null)
            {
                string[] allKeys = this.CurrentTemplate.Items.AllKeys;
                for (int i = 0; i < allKeys.Length; i++)
                {
                    this.CurrentCustom.SetValue(allKeys[i], this.CurrentTemplate.Items.GetValue(allKeys[i]).Current);
                }
            }
            string[] allKeys2 = this.CurrentPage.Items.AllKeys;
            for (int j = 0; j < allKeys2.Length; j++)
            {
                this.CurrentCustom.SetValue(allKeys2[j], this.CurrentPage.Items.GetValue(allKeys2[j]).Current);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetMeta(this.CurrentPage);
            this.InitPage(this);
        }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            string text = "~/Views/Shared/" + this.CurrentTemplate.File;

            if (!File.Exists(base.Server.MapPath(text)))
            {
                throw new Exception("The file : '" + text + "' not found");
            }
            this.MasterPageFile = text;
        }

        protected virtual IModuleInterface OnPreLoadModule(IModuleInterface CurrentModule)
        {
            return CurrentModule;
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.nguyenthanh_2(false);

            foreach (Controller controller in this.Controllers)
            {
                controller.OnUnLoad();
            }

            if (base.Controller != null)
            {
                base.Controller.OnUnLoad();
            }
            bool flag = !string.IsNullOrEmpty(this.CurrentPage.PageTitle);
            string content = "";
            if (flag)
            {
                base.Title = this.CurrentPage.PageTitle;
                content = this.CurrentPage.PageTitle;
            }
            else if (!string.IsNullOrEmpty(this.CurrentPage.Name))
            {
                base.Title = this.CurrentPage.Name;
                content = this.CurrentPage.Name;
            }
            base.SetMeta("description", this.CurrentPage.PageDescription);
            base.SetMeta("keywords", this.CurrentPage.PageKeywords);
            base.SetMeta("schema_name", content);
            base.SetMeta("schema_image", this.CurrentPage.PageFile);
            base.SetMeta("schema_description", this.CurrentPage.PageDescription);
            base.SetMeta("og_title", content);
            base.SetMeta("og_url", this.CurrentPage.PageURL);
            base.SetMeta("og_image", this.CurrentPage.PageFile);
            base.SetMeta("og_image_url", this.CurrentPage.PageFile);
            base.SetMeta("og_description", this.CurrentPage.PageDescription);
            if (base.CurrentVQS.Count > 0)
            {
                string @string = base.CurrentVQS.GetString(0);
                string GetQuery = HttpRequest.Query;
                base.SetLink("canonical_name", HttpRequest.Domain + this.URLBase + @string + Setting.Sys_PageExt);
                if (GetQuery.Contains("page"))
                {
                    int Checckpage = 0;
                    
                    if (!GetQuery.Contains("sort")||!GetQuery.Contains("keyword"))
                    {
                       Checckpage = System.Convert.ToInt32(GetQuery.Substring(GetQuery.Length - 1));
                    }    
                    
                    if (Checckpage < 2)
                    {
                        //base.SetLink("prev_name", HttpRequest.Domain + this.URLBase + @string + Setting.Sys_PageExt + "/page/" + (a - 1));
                        base.SetLink("next_name", HttpRequest.Domain + this.URLBase + @string + Setting.Sys_PageExt + "/page/" + (Checckpage + 1));
                    }
                    else
                    {
                        base.SetLink("prev_name", HttpRequest.Domain + this.URLBase + @string + Setting.Sys_PageExt + "/page/" + (Checckpage - 1));
                        base.SetLink("next_name", HttpRequest.Domain + this.URLBase + @string + Setting.Sys_PageExt + "/page/" + (Checckpage + 1));
                    }
                }
                base.SetLink("hreflang_name", HttpRequest.Domain + this.URLBase + @string + Setting.Sys_PageExt, "hreflang", base.CurrentLang.Code.ToLower());
            }

            else
            {
                base.SetLink("canonical_name", HttpRequest.Domain);
                base.SetLink("hreflang_name", HttpRequest.Domain, "hreflang", base.CurrentLang.Code.ToLower());
            }

            base.OnPreRender(e);
        }
        protected override string OnRender(string html)
        {
            html = html.Replace("{ReturnPath}", this.ReturnPath);
            html = html.Replace("{ActionForm}", base.ActionForm);
            html = html.Replace("{ApplicationPath}", base.ApplicationPath);
            html = html.Replace("{ViewsPath}", this.ViewsPath);
            html = html.Replace("{DataPath}", this.DataPath);
            html = html.Replace("{ToolsPath}", this.ToolsPath);
            html = html.Replace("{ContentPath}", this.ContentPath);
            if (Setting.Sys_MultiPath)
            {
                string[] allKeys = this.CurrentCustom.AllKeys;
                for (int i = 0; i < this.CurrentCustom.Count; i++)
                {
                    if (allKeys[i].StartsWith("VSWPath_"))
                    {
                        html = html.Replace("{" + allKeys[i] + "}", this.CurrentCustom.GetValue(allKeys[i]).ToString());
                    }
                }
                allKeys = this.CurrentSite.Items.AllKeys;
                for (int j = 0; j < this.CurrentSite.Items.Count; j++)
                {
                    if (allKeys[j].StartsWith("VSWPath_"))
                    {
                        html = html.Replace("{" + allKeys[j] + "}", this.CurrentSite.Items.GetValue(allKeys[j]).ToString());
                    }
                }
            }
            string GetQuery = HttpRequest.Query;

            if (GetQuery.Trim() == string.Empty)
            {
                html = html.Replace("<link  name=\"prev_name\" rel=\"prev\" />", "");
                html = html.Replace("<link  name=\"next_name\" rel=\"next\" />", "");
                html = html.Replace("<link  name=\"prev_name\" rel=\"prev\" href=\"Views/Shared/\" />", "");
                html = html.Replace("<link  name=\"next_name\" rel=\"next\" href=\"Views/Shared/\" />", "");
            }
            if (GetQuery.Contains("page"))
            {

                int Checckpage = 0;
                if (!GetQuery.Contains("sort")||!GetQuery.Contains("keyword"))
                {
                    Checckpage = System.Convert.ToInt32(GetQuery.Substring(GetQuery.Length - 1));
                }              
                if (Checckpage < 2)
                {
                    html = html.Replace("<link  name=\"prev_name\" rel=\"prev\" href=\"Views/Shared/\" />", "");
                    //html = html.Replace("<link  name=\"next_name\" rel=\"next\" href=\"Views/Shared/\" />", "");
                }
            }
            if (Setting.Sys_CompressionHtml)
            {
                html = html.Replace("\n", "");
                html = html.Replace("\r", " ");
                html = html.Replace("\t", " ");
                int num = 0;
                while (html.IndexOf("  ") > -1 && num < 20)
                {
                    num++;
                    html = html.Replace(" ", " ");
                }
                html = html.Replace("> <", "><").Trim();
                html = html.Replace("    ", "");
            }
            return html;
        }
        protected virtual IPageInterface PageNotFound()
        {
            if (this.CurrentPage == null)
            {
                this.CurrentPage = this.PageService.VSW_Core_GetByID(this.CurrentSite.PageID);
            }
            return this.CurrentPage;
        }
        protected virtual ISiteInterface SiteNotFound()
        {
            if (this.CurrentSite == null)
            {
                this.CurrentSite = this.SiteService.VSW_Core_GetDefault();
            }
            return this.CurrentSite;
        }
        private bool bool_1;

        private List<Controller> list_0 = new List<Controller>();
        private sealed class DynamicAction
        {
            public bool Exists(string key)
            {
                return key.ToLower() == this.controller.IndexAction.ToLower();
            }
            public Controller controller;
        }

        private sealed class StaticAction
        {
            public bool Exists(string key)
            {
                return key.ToLower() == "action" + this.FuncNamePOST.ToLower();
            }
            public DynamicAction _dynamicAction;
            public string FuncNamePOST;
        }
    }
}
