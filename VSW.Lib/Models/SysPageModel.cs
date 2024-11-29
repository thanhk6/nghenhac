using System;
using System.Collections.Generic;
using VSW.Core.Interface;
using VSW.Core.Models;
using VSW.Core.MVC;
using VSW.Core.Web;

namespace VSW.Lib.Models
{
    public class SysPageEntity : EntityBase, IPageInterface
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int TemplateID { get; set; }

        [DataInfo]
        public int TemplateMobileID { get; set; }

        [DataInfo]
        public int TemplateTabletID { get; set; }

        [DataInfo]
        public string ModuleCode { get; set; }

        [DataInfo]
        public int LangID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int BrandID { get; set; }

        [DataInfo]
        public int ParentID { get; set; }

        [DataInfo]
        public int State { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string File { get; set; }
        [DataInfo]
        public string FileBrand { get; set; }
        //[DataInfo]
        //public string Url { get; set; }

        [DataInfo]
        public string Summary { get; set; }

        [DataInfo]
        public string TopContent { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public string LinkTitle { get; set; }

        [DataInfo]
        public string PageTitle { get; set; }

        [DataInfo]
        public string PageHeading { get; set; }

        [DataInfo]
        public string PageDescription { get; set; }

        [DataInfo]
        public string PageKeywords { get; set; }

        [DataInfo]
        public string PageCanonical { get; set; }

        [DataInfo]
        public string Custom { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }

        [DataInfo]
        public DateTime Updated { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }
        [DataInfo]
        public string Icon { get; set; }
        [DataInfo]
        public bool ShowMenuTop { get; set; }
        [DataInfo]
        public bool ChooseMenu { get; set; }

        #endregion Autogen by VSW

        #region SEO

        public bool PageState { get; set; } = true;

        public string PageURL { get; set; } = string.Empty;

        private string _pageFile = string.Empty;
        public string PageFile
        {
            get => _pageFile.IndexOf("base64", StringComparison.OrdinalIgnoreCase) > -1 ? string.Empty : _pageFile;
            set => _pageFile = value;
        }

        #endregion SEO

        public bool HasEnd { get; set; }

        public bool Root => ParentID == 0;

        public bool End => Items.GetValue("End").ToBool();
        public long Count
        {
            get
            {
                return ModMp3Service.Instance.CreateQuery()
                                        .Select(o => o.ID)
                                        .Where(o => o.Activity == true)                                      
                                        .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, LangID))
                                        .Count()
                                        .ToValue_Cache()
                                        .ToLong(0);
            }
        }
        public long Count1
        {
            get
            {
                return ModMp3Service.Instance.CreateQuery()
                                        .Select(o => o.ID)
                                        .Where(o => o.Activity == true)
                                      // .Where(BrandID>0, o=>o.BrandID==BrandID)
                                     // .Where( o => o.MenuID == MenuID)                                 
                                     
                                       .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, LangID))
                                        .Count()
                                        .ToValue_Cache()
                                        .ToLong(0);
            }
        }
        private SysPageEntity _oParent;

        public SysPageEntity GetParent()
        {
            if (_oParent == null && ParentID > 0)
                _oParent = SysPageService.Instance.GetByID_Cache(ParentID);

            return _oParent ?? (_oParent = new SysPageEntity());
        }
        private WebMenuEntity _oMenu;
        public WebMenuEntity GetMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            return _oMenu ?? (_oMenu = new WebMenuEntity());
        }
        private ModAuthorEntity _oBrand;

        public ModAuthorEntity GetBrand()
        {
            if (_oBrand == null && BrandID > 0)
                _oBrand = ModAuthorService.Instance.GetByID(BrandID);

            return _oBrand ?? (_oBrand = new ModAuthorEntity());
        }
    }
    public class SysPageService : ServiceBase<SysPageEntity>, IPageServiceInterface
    {
        #region Autogen by VSW

        public SysPageService() : base("[Sys_Page]")
        {
        }

        private static SysPageService _instance;

        public static SysPageService Instance => _instance ?? (_instance = new SysPageService());

        #endregion Autogen by VSW

        public SysPageEntity GetByID(int id)
        {
            return CreateQuery().Where(o => o.ID == id).ToSingle();
        }

        public SysPageEntity GetByCode(string code)
        {
            return CreateQuery().Where(o => o.Code == code).ToSingle();
        }    
        public SysPageEntity GetByID_Cache(int id)
        {
            if (GetAll_Cache() == null) return null;

            return GetAll_Cache().Find(o => o.ID == id);
        }
        public List<SysPageEntity> GetByParent_Cache(SysPageEntity page, bool brand)
        {
            if (GetAll_Cache() == null) return null;

            List<SysPageEntity> list;
            if (brand)
            {
                list = GetAll_Cache().FindAll(o => o.ParentID == page.ID && o.BrandID > 0);
                if (list.Count < 1)
                    list = GetAll_Cache().FindAll(o => o.ParentID == page.ParentID && o.BrandID > 0);
            }
            else
            {
                list = GetAll_Cache().FindAll(o => o.ParentID == page.ID && o.BrandID < 1);

                //if (list.Count < 1)
                //    list = GetAll_Cache().FindAll(o => o.ParentID == page.ParentID && o.BrandID < 1);
            }

            list.Sort((o1, o2) => o1.Order.CompareTo(o2.Order));

            return list;
        }
        public List<SysPageEntity> GetByParent_Cache(int parentID)
        {
            if (GetAll_Cache() == null) return null;

            var list = GetAll_Cache().FindAll(o => o.ParentID == parentID);

            list.Sort((o1, o2) => o1.Order.CompareTo(o2.Order));

            return list;
        }
        public List<SysPageEntity> GetByParent_Cachemobile(int parentID)
        {
            if (GetAll_Cache() == null) return null;
            var list = GetAll_Cachemmobil().FindAll(o => o.ParentID == parentID);
            list.Sort((o1, o2) => o1.Order.CompareTo(o2.Order));
            return list;
        }
        public List<SysPageEntity> GetByGrandParent_Cache(int parentID)
        {
            if (GetAll_Cache() == null) return null;

            var list = GetAll_Cache().FindAll(o => o.ParentID == parentID);

            var listGrand = new List<SysPageEntity>();
            foreach (var t in list)
            {
                var temp = GetAll_Cache().FindAll(o => o.ParentID == t.ID);
                listGrand.AddRange(temp);
            }

            if (listGrand.Count > 0)
                listGrand.Sort((o1, o2) => o1.Order.CompareTo(o2.Order));
            return listGrand;
        }
        public List<SysPageEntity> GetByParent_Cache(int parentID, string module)
        {
            if (GetAll_Cache() == null) return null;

            var list = GetAll_Cache().FindAll(o => o.ParentID == parentID && o.ModuleCode == module);

            if (list.Count == 0) return null;

            list.Sort((o1, o2) => o1.Order.CompareTo(o2.Order));

            return list;
        }
        public string GetMapCode_Cache(SysPageEntity page)
        {
            var keyCache = Cache.CreateKey(page.ID.ToString());

            string mapCode;
            var obj = Cache.GetValue(keyCache);
            if (obj != null)
            {
                mapCode = obj.ToString();
            }
            else
            {
                var tempPage = page;

                mapCode = tempPage.Code;
                while (tempPage.ParentID > 0)
                {
                    var parentId = tempPage.ParentID;

                    tempPage = CreateQuery()
                           .Where(o => o.ID == parentId)
                           .ToSingle_Cache();

                    if (tempPage == null || tempPage.Root)
                        break;

                    mapCode = tempPage.Code + "/" + mapCode;
                }

                Cache.SetValue(keyCache, mapCode);
            }
            return mapCode;
        }
        private List<SysPageEntity> GetAll_Cache()
        {
            return CreateQuery().Where(o => o.Activity == true).ToList_Cache();
        }

        private List<SysPageEntity> GetAll_Cachemmobil()
        {
            return CreateQuery().ToList_Cache();
        }
        #region IPageServiceInterface Members

        public IPageInterface VSW_Core_GetByID(int id)
        {
            return GetAll_Cache().Find(o => o.ID == id);
        }
        public IPageInterface VSW_Core_CurrentPage(ViewPage viewPage)
        {
            var code = viewPage.CurrentVQS.GetString(0);

            if (code == string.Empty || code.Length > 260) return null;

            if (string.Equals(code.ToLower(), "ajax", StringComparison.OrdinalIgnoreCase) || code.StartsWith("sitemap", StringComparison.OrdinalIgnoreCase) || string.Equals(code, "rss", StringComparison.OrdinalIgnoreCase))
            {
                if (code.ToLower().StartsWith("sitemap") && viewPage.CurrentVQS.Count > 1)
                    HttpRequest.Redirect301(viewPage.GetURL(code));

                var template = SysTemplateService.Instance.CreateQuery()
                                            .Select(o => o.ID)
                                            .ToSingle_Cache();
                if (template == null) return null;
                viewPage.CurrentVQSMVC.Trunc(viewPage.CurrentVQS, 1);
                if (code.StartsWith("sitemap", StringComparison.OrdinalIgnoreCase))
                {
                    return new SysPageEntity
                    {
                        TemplateID = template.ID,
                        TemplateMobileID = template.ID,
                        Name = "Sitemap",
                        Code = "sitemap",
                        ModuleCode = "MSitemap",
                        Activity = true
                    };
                }

                switch (code.ToLower())
                {
                    case "ajax":
                        return new SysPageEntity
                        {
                            TemplateID = template.ID,
                            TemplateMobileID = template.ID,
                            Name = "Ajax",
                            Code = "ajax",
                            ModuleCode = "MAjax",
                            Activity = true
                        };

                    case "rss":
                        return new SysPageEntity
                        {
                            TemplateID = template.ID,
                            TemplateMobileID = template.ID,
                            Name = "Đọc tin RSS",
                            Code = "rss",
                            ModuleCode = "MRss",
                            Activity = true
                        };
                }
            }
            //if (code.IndexOf("--") > -1)
            //{
            //    string[] ArrCode = System.Text.RegularExpressions.Regex.Split(code, "--");
            //    code = ArrCode[0].Trim();

            //    if (ArrCode.Length > 1)
            //    {
            //        var brand = ModBrandService.Instance.GetByCode(ArrCode[1].Trim(), viewPage.CurrentLang.ID);
            //        viewPage.ViewBag.Brand = brand;
            //    }
            //}
            var cleanUrl = ModCleanURLService.Instance.GetByCode(code, viewPage.CurrentLang.ID);
            if (cleanUrl == null)
            {
                cleanUrl = ModCleanURLService.Instance.CreateQuery().Where(o => o.Code.StartsWith(code) && o.LangID == viewPage.CurrentLang.ID).ToSingle();
                if (cleanUrl != null)
                    HttpRequest.Redirect301(viewPage.GetURL(cleanUrl.Code));

                return null;
            }
            if (viewPage.CurrentVQS.Count > 1 && cleanUrl.MenuID > 0)
                HttpRequest.Redirect301(viewPage.GetURL(viewPage.CurrentVQS.EndCode));

            viewPage.ViewBag.CleanURL = cleanUrl;

            if (cleanUrl.Type == "Page")
            {
                viewPage.CurrentVQSMVC.Trunc(viewPage.CurrentVQS, 1);
                return GetByID_Cache(cleanUrl.Value);
            }
            //viewPage.CurrentVQSMVC.Trunc(viewPage.CurrentVQS, 0);
            viewPage.CurrentVQSMVC.Trunc(viewPage.CurrentVQS, viewPage.CurrentVQS.Count - 1);
            SysPageEntity page = null;
            var menuId = cleanUrl.MenuID;
            while (menuId > 0)
            {
                var id = menuId;
                page = CreateQuery().Where(o => o.MenuID == id && o.Activity == true).ToSingle_Cache();

                if (page != null) break;
                var menu = WebMenuService.Instance.GetByID_Cache(menuId);
                if (menu == null || menu.ParentID == 0) break;
                menuId = menu.ParentID;
            }
            return page;
        }
        public void VSW_Core_CPSave(IPageInterface item)
        {
            Save(item as SysPageEntity);
        }

        #endregion IPageServiceInterface Members
    }
}