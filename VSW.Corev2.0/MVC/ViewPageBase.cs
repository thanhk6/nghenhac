using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using VSW.Core.Global;
using VSW.Core.Interface;
using VSW.Core.Models;
using VSW.Core.Web;
namespace VSW.Core.MVC
{
    public class ViewPageBase : Page
    {
        public string ActionForm { get; protected set; }

        public string ApplicationPath { get; private set; }
        public Controller Controller { get; protected set; }
        public ILangInterface CurrentLang { get; protected set; }
        public IModuleInterface CurrentModule { get; protected set; }
        public ViewState CurrentParams { get; protected set; }
        public VQS CurrentVQS { get; protected set; }
        public VQS CurrentVQSMVC { get; protected set; }
        internal string FuncNamePOST { get; set; }
        internal string FuncParamPOST { get; set; }
        public new bool IsPostBack
        {
            get
            {
                return base.Request.HttpMethod == "POST";
            }
        }
        public ViewState PageViewState { get; protected set; }
        public IResourceServiceInterface ResourceService { get; protected set; }
        public dynamic ViewBag { get; protected set; }
        public ViewControl ViewControl { get; protected set; }
        public Dictionary<string, object> ViewData { get; protected set; }
        public ViewPageBase()
        {
            this.ViewData = new Dictionary<string, object>();
            this.ViewBag = new DynamicObject(this.ViewData);
        }
        internal void nguyenthanh_0()
        {
            if (!this.check_License())
            {
                base.Response.Write("Error license!");
                base.Response.End();
                return;
            }

            this.PageViewState = new ViewState();
            this.CurrentParams = new ViewState();
            if (this.Context.Items["VSW_PARAMS"] != null)
            {
                string text = this.Context.Items["VSW_PARAMS"].ToString();
                if (text.StartsWith("/"))
                {
                    text = text.Substring(1);
                }
                string[] array = text.Split(new char[]
                {
                    '/'
                });
                for (int i = 0; i < array.Length - 1; i += 2)
                {
                    this.CurrentParams.SetValue(array[i], array[i + 1]);
                    this.PageViewState.SetValue(array[i], array[i + 1]);
                }
                this.Context.Items["VSW_PARAMS"] = null;
            }
            this.PageViewState.UpdateQueryString();
            this.PageViewState.UpdateForm();
            string url = string.Empty;
            if (this.Context.Items["VSW_VQS"] != null)
            {
                url = this.Context.Items["VSW_VQS"].ToString();
                this.Context.Items["VSW_VQS"] = null;
            }
            this.CurrentVQS = new VQS(url);
            this.ApplicationPath = HttpRequest.ApplicationPath;
        }
        internal void nguyenthanh_1()
        {
            if (this.IsPostBack)
            {
                if (this.PageViewState.Exists("_vsw_action"))
                {
                    this.FuncNamePOST = this.PageViewState.GetValue("_vsw_action").ToString();
                }
                else
                {
                    string[] allKeys = this.PageViewState.AllKeys;
                    int num = 0;
                    while (allKeys != null && num < allKeys.Length)
                    {
                        if (allKeys[num].StartsWith("_vsw_action"))
                        {
                            this.FuncNamePOST = allKeys[num].Replace("_vsw_action", string.Empty);
                            break;
                        }
                        num++;
                    }
                }
                if (!string.IsNullOrEmpty(this.FuncNamePOST))
                {
                    this.FuncNamePOST = this.FuncNamePOST.Replace("][", "|").Replace("[", string.Empty).Replace("]", string.Empty);
                    int num2 = this.FuncNamePOST.IndexOf('|');
                    if (num2 > -1)
                    {
                        string funcNamePOST = this.FuncNamePOST.Substring(0, num2);
                        this.FuncParamPOST = this.FuncNamePOST.Substring(num2 + 1);
                        this.FuncNamePOST = funcNamePOST;
                    }
                }
            }
        }
        internal void nguyenthanh_2(bool bool_1)
        {
            Predicate<string> predicate = null;
            if (!string.IsNullOrEmpty(this.FuncNamePOST) && this.Controller != null && (!bool_1 || this.FuncNamePOST.EndsWith("_UP")) && (bool_1 || !this.FuncNamePOST.EndsWith("_UP")))
            {
                Class @class = new Class(this.Controller);
                if (predicate == null)
                {
                    predicate = new Predicate<string>(this.Exists);
                }
                string text = @class.GetMethodsName().Find(predicate);
                if (text != null)
                {
                    this.IsSuccess = ViewPageRender.RenderPage(this, this.Controller, @class, text, this.FuncParamPOST);
                }
            }
        }
        internal void nguyenthanh_3(Class @class, string action)
        {
            Predicate<string> predicate = null;
            ViewPageBase.ViewPageAction viewPageAction = new ViewPageBase.ViewPageAction
            {
                Action = null
            };
            if (this.CurrentVQSMVC.Count == 0)
            {
                viewPageAction.Action = this.Controller.IndexAction;
            }
            else if (this.CurrentVQSMVC.Count > 0)
            {
                viewPageAction.Action = "Action" + this.CurrentVQSMVC.GetString(0);
                if (predicate == null)
                {
                    predicate = new Predicate<string>(viewPageAction.Exists);
                }
                string text = @class.GetMethodsName().Find(predicate);
                if (text != null)
                {
                    viewPageAction.Action = text;
                }
                else
                {
                    viewPageAction.Action = action;
                }
            }
            if (!string.IsNullOrEmpty(viewPageAction.Action))
            {
                this.IsSuccess = ViewPageRender.RenderPage(this, this.Controller, @class, viewPageAction.Action);
            }
        }
        internal void nguyenthanh_4(string directory)
        {
            if (!this.IsSuccess)
            {
                if (this.Controller != null)
                {
                    this.Controller.ViewLayout = null;
                }
                return;
            }
            string text = string.Concat(new string[]
            {
                "~/",
                directory,
                "Views/",
                this.CurrentModule.Code,
                "/",
                this.Controller.ViewLayout,
                ".ascx"
            });
            if (!File.Exists(base.Server.MapPath(text)))
            {
                throw new Exception("The file '" + text + "' not found");
            }
            this.ViewControl = (base.LoadControl(text) as ViewControl);
            this.Controller.ViewControl = this.ViewControl;
            this.ViewControl.Controller = this.Controller;
            this.ViewControl.ViewData = this.Controller.ViewData;
            this.ViewControl.ViewBag = this.Controller.ViewBag;
        }
        private bool Exists(string action)
        {
            return action.ToLower() == "action" + this.FuncNamePOST.ToLower();
        }
        protected virtual string OnPreRender(string html)
        {
            return html;
        }
        protected virtual string OnRender(string html)
        {
            return html;
        }
        protected sealed override void Render(HtmlTextWriter writer)
        {

            StringBuilder stringBuilder = new StringBuilder();

            HtmlTextWriter writer2 = new HtmlTextWriter(new StringWriter(stringBuilder));

            base.Render(writer2);
            string text = stringBuilder.ToString();
            text = this.OnPreRender(text);           
            int num = text.IndexOf("<form");
            if (num > -1)
            {
                int num2 = text.IndexOf("</div>", num);
                if (num2 > -1)
                {
                    text = text.Remove(num, num2 - num + 6);
                }
            }
            num = text.LastIndexOf("</form>");

            if (num > -1)
            {
                text = text.Remove(num, 7);
            }           
            text = text.Replace(" id=\"ctl00_Head\"", string.Empty).Replace(" id=\"ctl00_description\"", string.Empty).Replace(" id=\"ctl00_keywords\"", string.Empty);
            text = text.Replace(" id=\"ctl00_hreflang_name\"", string.Empty).Replace(" name=\"hreflang_name\"", string.Empty).Replace(" id=\"ctl00_canonical_name\"", string.Empty).Replace(" name=\"canonical_name\"", string.Empty);        
            text = text.Replace("id=\"ctl00_prev_name\"", string.Empty).Replace("\name=prev_name\"",string.Empty);
            text = text.Replace("id=\"ctl00_next_name\"", string.Empty).Replace("\name=next_name\"", string.Empty);
            text = text.Replace(" id=\"ctl00_schema_name\"", string.Empty).Replace(" name=\"schema_name\"", string.Empty);
            text = text.Replace(" id=\"ctl00_schema_description\"", string.Empty).Replace(" name=\"schema_description\"", string.Empty);
            text = text.Replace(" id=\"ctl00_schema_image\"", string.Empty).Replace(" name=\"schema_image\"", string.Empty);
            text = text.Replace(" id=\"ctl00_og_title\"", string.Empty).Replace(" name=\"og_title\"", string.Empty);
            text = text.Replace(" id=\"ctl00_og_url\"", string.Empty).Replace(" name=\"og_url\"", string.Empty);
            text = text.Replace(" id=\"ctl00_og_image\"", string.Empty).Replace(" name=\"og_image\"", string.Empty);
            text = text.Replace(" id=\"ctl00_og_image_url\"", string.Empty).Replace(" name=\"og_image_url\"", string.Empty);
            text = text.Replace(" id=\"ctl00_og_description\"", string.Empty).Replace(" name=\"og_description\"", string.Empty);
            if (this.ResourceService != null && this.CurrentLang != null)
            {
                MatchCollection matchCollection = new Regex("\\{RS:[\\w]+\\}").Matches(text);
                for (int i = 0; i < matchCollection.Count; i++)
                {
                    string text2 = matchCollection[i].Value.Replace("{RS:", string.Empty).Replace("}", string.Empty);
                    text = text.Replace("{RS:" + text2 + "}", this.ResourceService.VSW_Core_GetByCode(text2, string.Empty));
                }
            }

            text = this.OnRender(text);



            writer.Write(string.Concat(new object[]
            {
                text,
                "\r\n<!-- Copyright © by ",
                Setting.Copyright,
                " -->"
            }));
        }
        public void SetLink(string name, string content)
        {
            HtmlLink htmlLink = (HtmlLink)base.Header.FindControl(name);
            if (htmlLink != null)
            {
                htmlLink.Href = content;
            }
        }

        public void SetLink(string name, string content, string tagA, string valA)
        {
            HtmlLink htmlLink = (HtmlLink)base.Header.FindControl(name);
            if (htmlLink != null)
            {
                htmlLink.Href = content;
                htmlLink.Attributes[tagA] = valA;
            }
        }
        public void SetMeta(EntityBase entityBase)
        {
            if (entityBase != null)
            {
                string text = entityBase.Items.GetValue("Title").ToString();
                string text2 = entityBase.Items.GetValue("Keywords").ToString();
                string text3 = entityBase.Items.GetValue("Description").ToString();

                if (text == string.Empty)
                {
                    text = entityBase.Name;
                }
                if (text2 == string.Empty)
                {
                    text2 = entityBase.Name;
                }
                if (text != string.Empty)
                {
                    this.SetTitle(text);
                }
                if (text2 != string.Empty)
                {
                    this.SetMetaKeywords(text2);
                }
                if (text3 != string.Empty)
                {
                    this.SetMetaDescription(text3);
                }
            }
        }


        public void SetMeta(string name, string content)
        {
            HtmlMeta htmlMeta = (HtmlMeta)base.Header.FindControl(name);
            if (htmlMeta != null)
            {
                htmlMeta.Content = content;
            }
        }

        public void SetMetaDescription(string content)
        {
            this.SetMeta("description", content);
        }
        public void SetMetaKeywords(string content)
        {
            this.SetMeta("keywords", content);
        }
        public void SetTitle(string title)
        {
            base.Title = title;
        }
        public bool check_License()
        {
            bool result = false;
            try
            {
                if (this.Context == null)
                {
                    result = false;
                    return result;
                }
                string text = this.Context.Server.MapPath("~/License.lic");
                if (!File.Exists(text))
                {
                    result = false;
                    return result;
                }
                string text2 = this.Context.Request.Url.Host.ToLower();
                string text3 = "";
                XmlDataDocument xmlDataDocument = new XmlDataDocument();
                xmlDataDocument.Load(text);
                XmlNodeList elementsByTagName = xmlDataDocument.GetElementsByTagName("Domain");
                for (int i = 0; i <= elementsByTagName.Count - 1; i++)
                {
                    string innerText = xmlDataDocument.GetElementsByTagName("Domain")[i].InnerText;
                    if (innerText.ToLower() == text2)
                    {
                        text3 = xmlDataDocument.GetElementsByTagName("LicenseKey")[i].InnerText;
                        break;
                    }
                }
                if (text2 == string.Empty)
                {
                    result = false;
                    return result;
                }
                if (text2.StartsWith("www."))
                {
                    text2 = text2.Substring(4);
                }
                if (text3 == string.Empty)
                {
                    result = false;
                    return result;
                }
                if (CryptoString.MD5Hash(text2 + Setting.Key).Trim().ToLower() == text3.ToLower())
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }


        private bool IsSuccess;
        private sealed class ViewPageAction
        {
            public bool Exists(string key)
            {
                return key.ToLower() == this.Action.ToLower();
            }
            public string Action;
        }
    }
}
