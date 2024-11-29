using System;
using System.Collections.Generic;
using VSW.Core.Models;
using VSW.Lib.Global;

namespace VSW.Lib.Models
{
    public class ModAuthorEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int State { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string Summary { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public long View { get; set; }

        [DataInfo]
        public string Custom { get; set; }

        [DataInfo]
        public string PageTitle { get; set; }

        [DataInfo]
        public string PageDescription { get; set; }

        [DataInfo]
        public string PageKeywords { get; set; }

        [DataInfo]
        public DateTime Published { get; set; }

        [DataInfo]
        public DateTime Updated { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }
        [DataInfo]
        public string LinkWeb { get; set; }
        #endregion Autogen by VSW

        private string _oIcon = string.Empty;
        public string Icon
        {
            get
            {
                if (string.IsNullOrEmpty(_oIcon))
                {
                    _oIcon = Code.Replace("-info", string.Empty);
                    _oIcon = "/Content/desktop/images/" + _oIcon + ".png";
                }

                return _oIcon;
            }
        }

        public void UpView()
        {
            View++;
            ModAuthorService.Instance.Save(this, o => o.View);
        }

        private WebMenuEntity _oMenu;
        public WebMenuEntity GetMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            return _oMenu ?? (_oMenu = new WebMenuEntity());
        }

        private List<ModMp3Entity> _oGetProduct;
        public List<ModMp3Entity> GetProduct()
        {
            if (_oGetProduct == null && MenuID > 0)
                _oGetProduct = ModMp3Service.Instance.CreateQuery()
                                               
                                                .OrderByDesc(o => new { o.Order, o.Activity })
                                                
                                                .ToList_Cache();

            return _oGetProduct;
        }



        private List<ModCollectionEntity> _oGetCollection;
        public List<ModCollectionEntity> GetCollection()
        {
            if (_oGetCollection == null && MenuID > 0)
                _oGetCollection = ModCollectionService.Instance.CreateQuery()
                                                .Where(o => o.BrandID == ID)
                                                .OrderByDesc(o => new { o.Order, o.Activity })
                                                .Take(10)
                                                .ToList_Cache();

            return _oGetCollection;
        }
    }

    public class ModAuthorService : ServiceBase<ModAuthorEntity>
    {
        #region Autogen by VSW

        public ModAuthorService() : base("[Mod_Brand]")
        {
            //DBExecuteMode = DBExecuteType.DataReader;


        }

        private static ModAuthorService _instance;
        public static ModAuthorService Instance => _instance ?? (_instance = new ModAuthorService());

        #endregion Autogen by VSW

        public ModAuthorEntity GetByID(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public ModAuthorEntity GetByCode(string code, int langID)
        {
            int MenuID = WebMenuService.Instance.CreateQuery()
                                .Select(o => o.ID)
                                .Where(o => o.Activity == true && o.Type == "Brand" && o.LangID == langID)
                                .ToValue_Cache()
                                .ToInt(0);

            return CreateQuery()
               .Where(o => o.Code == code)
               .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Brand", MenuID, langID))
               .ToSingle();
        }

        public bool Exists(string query)
        {
            return CreateQuery()
                           .Where(query)
                           .Count()
                           .ToValue()
                           .ToBool();
        }

        public List<ModAuthorEntity> GetByLang(int langID)
        {
            List<ModAuthorEntity> listItem = null;

            var keyCache = Core.Web.Cache.CreateKey("Mod_Brand", "GetByLang." + langID);
            var obj = Core.Web.Cache.GetValue(keyCache);
            if (obj != null) listItem = obj as List<ModAuthorEntity>;
            else
            {
                int MenuID = WebMenuService.Instance.CreateQuery()
                                .Select(o => o.ID)
                                .Where(o => o.Activity == true && o.Type == "Brand" && o.LangID == langID)
                                .ToValue_Cache()
                                .ToInt(0);

                listItem = base.CreateQuery()
                                        .Where(o => o.Activity == true)
                                        .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Brand", MenuID, langID))
                                        .OrderByAsc(o => new { o.Order, o.ID })
                                        .ToList_Cache();

                Core.Web.Cache.SetValue(keyCache, listItem);
            }

            return listItem;
        }
    }
}