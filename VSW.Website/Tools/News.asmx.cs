using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Website.Tools
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class News : System.Web.Services.WebService
    {
        public HeaderUserAuth UserInfo = null;
        public HeaderError ErrorInfo = null;

        private bool IsValid()
        {
            if (UserInfo == null) 
                return false;

            CPUserEntity _User = CPUserService.Instance.GetLoginMd5(UserInfo.LoginName, UserInfo.Password);

            //Kiem tra quyen
            if (_User == null) 
                return false;

            return _User.IsAdministrator;
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void Clear_Cache()
        {
            if (!this.IsValid())
                return;

            VSW.Core.Web.Cache.Clear();
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public bool Exists(string Code, int MenuID)
        {
            if (!this.IsValid())
                return true;

            WebMenuEntity _Menu = WebMenuService.Instance.GetByID(MenuID);

            if (_Menu == null)
                return true;

            string listID = WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, _Menu.LangID);

            return ModNewsService.Instance.CreateQuery()
                    .Count()
                    .Where(o => o.Code == Code)
                    .WhereIn(o => o.MenuID, listID)
                    .ToValue().ToBool();
        }

        private int getMaxOrder()
        {
            return ModNewsService.Instance.CreateQuery()
                .Max(o => o.Order)
                .ToValue().ToInt(1) + 1;
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void Update(WSNews wsNews)
        {
            if (!this.IsValid())
                return;

            ModNewsService.Instance.Save(new ModNewsEntity()
            {
                MenuID = wsNews.MenuID,
                Name = wsNews.Name,
                Code = VSW.Lib.Global.Data.GetCode(wsNews.Name),
                Summary = wsNews.Summary,
                Content = wsNews.Content,
                File = wsNews.File,
                Custom = wsNews.Custom,
                State = 0,
                Published = (wsNews.Publish.Year > 2000 ? wsNews.Publish : DateTime.Now),
                Order = getMaxOrder(),
                Activity = true
            });

        }
    }

    public class WSNews
    {
        public int MenuID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Custom { get; set; }
        public string File { get; set; }
        public string Source { get; set; }
        public string Tag { get; set; }
        public DateTime Publish { get; set; }
    }
}
