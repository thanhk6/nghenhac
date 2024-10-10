using System;
using VSW.Core.Interface;
using VSW.Lib.Global;
using VSW.Lib.Models;
namespace VSW.Lib.MVC
{
    public class ViewPage : Core.MVC.ViewPage
    {
        public ViewPage()
        {
            LangService = SysLangService.Instance;
            ModuleService = SysModuleService.Instance;
            SiteService = SysSiteService.Instance;
            TemplateService = SysTemplateService.Instance;
            PageService = SysPageService.Instance;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ResourceService = new IniSqlResourceService(CurrentLang);

            //lượt truy cập
            //Lib.Global.Utils.UpdateOnline();
        }
        protected override IPageInterface PageNotFound()
        {
            Error404();
            return null;
        }
        public void Error404()
        {
            Core.Web.HttpRequest.Error404();
        }
        public new SysSiteEntity CurrentSite => base.CurrentSite as SysSiteEntity;
        public new SysTemplateEntity CurrentTemplate => base.CurrentTemplate as SysTemplateEntity;
        public new SysPageEntity CurrentPage => base.CurrentPage as SysPageEntity;
        public new SysLangEntity CurrentLang => base.CurrentLang as SysLangEntity;
        public ModCleanURLEntity CurrentCleanUrl => ViewBag.CleanURL as ModCleanURLEntity;
        private string _currentUrl;
        public string CurrentURL => _currentUrl ?? (_currentUrl = GetPageURL(CurrentPage));
        //Module Base
        private string _oSearchURL;
        public string SearchURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oSearchURL))
                    return _oSearchURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MSearch" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oSearchURL = GetURL(item.Code);

                return _oSearchURL;
            }
        }

        private string _oViewCartURL;
        public string ViewCartURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oViewCartURL))
                    return _oViewCartURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MViewCart" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oViewCartURL = GetURL(item.Code);

                return _oViewCartURL;
            }
        }
        private string _oCheckoutURL;
        public string CheckoutURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oCheckoutURL))
                    return _oCheckoutURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MCheckout" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oCheckoutURL = GetURL(item.Code);

                return _oCheckoutURL;
            }
        }
        private string _oLoginURL;
        public string LoginURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oLoginURL))
                    return _oLoginURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MLogin" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oLoginURL = GetURL(item.Code);

                return _oLoginURL;
            }
        }
        private string _oRegisterURL;
        public string RegisterURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oRegisterURL))
                    return _oRegisterURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MRegister" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oRegisterURL = GetURL(item.Code);

                return _oRegisterURL;
            }
        }
        private string _oForgotURL;
        public string ForgotURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oForgotURL))
                    return _oForgotURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MForgot" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oForgotURL = GetURL(item.Code);

                return _oForgotURL;
            }
        }
        private string _oLogoutURL;
        public string LogoutURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oLogoutURL))
                    return _oLogoutURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MUser" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oLogoutURL = GetURL(item.Code + "/Logout");

                return _oLogoutURL;
            }
        }

        //Module Ex
        private string _oAccountURL;
        public string AccountURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oAccountURL))
                    return _oAccountURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MAccount" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oAccountURL = GetURL(item.Code);

                return _oAccountURL;
            }
        }

        private string _oOrderURL;
        public string OrderURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oOrderURL))
                    return _oOrderURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MOrder" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oOrderURL = GetURL(item.Code);

                return _oOrderURL;
            }
        }

        private string _oWishlistURL;
        public string WishlistURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_oWishlistURL))
                    return _oWishlistURL;

                var item = SysPageService.Instance.CreateQuery()
                                    .Select(o => o.Code)
                                    .Where(o => o.Activity == true && o.ModuleCode == "MWishlist" && o.LangID == CurrentLang.ID)
                                    .ToSingle_Cache();

                if (item != null)
                    _oWishlistURL = GetURL(item.Code);

                return _oWishlistURL;
            }
        }

        public Message Message { get; } = new Message();

        public string SortMode
        {
            get
            {
                var sort = Core.Web.HttpQueryString.GetValue("sort").ToString().ToLower().Trim();

                if (sort == "new_asc" || sort == "price_asc" || sort == "price_desc" || sort == "view_desc")
                    return sort;

                return "new_asc";
            }
        }

        public string GetURL(string key, string value)
        {
            var url = string.Empty;
            for (var i = 0; i < PageViewState.Count; i++)
            {
                var tempKey = PageViewState.AllKeys[i];
                var tempValue = PageViewState[tempKey].ToString();

                if (string.Equals(tempKey, key, StringComparison.OrdinalIgnoreCase) || string.Equals(tempKey, "vsw", StringComparison.OrdinalIgnoreCase) || tempKey.IndexOf("web.", StringComparison.OrdinalIgnoreCase) >= 0)
                    continue;

                if (url.Length == 0)
                    url = "?" + tempKey + "=" + Server.UrlEncode(tempValue);
                else
                    url += "&" + tempKey + "=" + Server.UrlEncode(tempValue);
            }

            url += (url == string.Empty ? "?" : "&") + key + "=" + value;

            return url;
        }



        public string GetURL(int menuID, string code)
        {
            return GetURL(code);
        }
      
        public string GetPageURL(SysPageEntity page)
        {
            var typeValue = page.Items.GetValue("Type").ToString();

            if (typeValue.Length == 0)
               return GetURL(page.Code);

            if (!typeValue.Equals("http", StringComparison.OrdinalIgnoreCase)) return "#";

            var target = page.Items.GetValue("Target").ToString();

            var url = page.Items.GetValue("URL").ToString();

            if (url.Length == 0)
                url = page.Code;
            return url.Replace("{URLBase}/", URLBase).Replace("{PageExt}", PageExt) + (target == string.Empty ? string.Empty : "\" target=\"" + target);
        }





        public bool IsPageActived(SysPageEntity pageToCheck)
        {
            if (CurrentPage.ID == pageToCheck.ID)
                return true;

            var page = (SysPageEntity)CurrentPage.Clone();
            while (true)
            {
                page = SysPageService.Instance.GetByID_Cache(page.ParentID);

                if (page == null || page.ParentID == 0)
                    return false;

                if (page.ID == pageToCheck.ID)
                    return true;
            }
        }

        public bool IsPageActived(SysPageEntity page, int index)
        {
            return CurrentPage.ID == page.ID || CurrentVQS.Equals(index, page.Code);
        }

        public void Back(int step)
        {
            JavaScript.Back(step, Page);
        }

        public void Navigate(string url)
        {
            JavaScript.Navigate(url, Page);
        }

        public void Close()
        {
            JavaScript.Close(Page);
        }

        public void Script(string key, string script)
        {
            JavaScript.Script(key, script, Page);
        }

        public void RefreshPage()
        {
            Response.Redirect(Request.RawUrl);
        }

        #region sweet alert

        public void Alert(string title, string content, string type)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content)) return;

            var html = @"<script type=""text/javascript"">sw_alert('" + title + @"', '" + content + @"', '" + type + @"');</script>";

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "AlertScript", html);
        }

        public void Alert(string title, string content)
        {
            Alert(title, content, "info");
        }

        public void Alert(string content)
        {
            Alert("Thông báo !", content, "info");
        }

        public void Redirect(string title, string content, string action)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(action)) return;

            var html = @"<script type=""text/javascript"">sw_redirect('" + title + @"', '" + content + @"', '" + action + @"');</script>";

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "AlertScript", html);
        }

        public void Redirect(string content, string action)
        {
            Redirect("Thông báo !", content, action);
        }

        public void Redirect(string content)
        {
            Redirect("Thông báo !", content, "/");
        }

        public void Confirm(string title, string content, string action)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(action)) return;

            var html = @"<script type=""text/javascript"">sw_confirm('" + title + @"', '" + content + @"', '" + action + @"');</script>";

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ConfirmScript", html);
        }

        public void Confirm(string content, string action)
        {
            Confirm("Thông báo !", content, action);
        }

        public void Confirm(string content)
        {
            Confirm("Thông báo !", content, "/");
        }

        #endregion zebradialog
    }
}