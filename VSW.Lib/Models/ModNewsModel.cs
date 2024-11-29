using System;
using System.Collections.Generic;
using VSW.Core.Models;
using VSW.Lib.Global;

namespace VSW.Lib.Models
{
    public class ModNewsEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int BrandID { get; set; }

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

        #endregion Autogen by VSW

        private string _oSummary = string.Empty;
        public string Summary2
        {
            get
            {
                if (string.IsNullOrEmpty(_oSummary))
                {
                    if (!string.IsNullOrEmpty(Summary))
                        _oSummary = Summary;
                    else
                        _oSummary = Data.CutString(Data.RemoveAllTag(Content), 170);
                }

                return _oSummary;
            }
        }

        public void UpView()
        {
            View++;
            ModNewsService.Instance.Save(this, o => o.View);
        }

        private WebMenuEntity _oMenu;
        public WebMenuEntity GetMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            return _oMenu ?? (_oMenu = new WebMenuEntity());
        }

        private ModBrandEntity _oBrand;
        public ModBrandEntity GetBrand()
        {
            if (_oBrand == null && BrandID > 0)
                _oBrand = ModBrandService.Instance.GetByID(BrandID);

            return _oBrand ?? (_oBrand = new ModBrandEntity());
        }
    }

    public class ModNewsService : ServiceBase<ModNewsEntity>
    {
        #region Autogen by VSW

        public ModNewsService() : base("[Mod_News]")
        {
            DBExecuteMode = DBExecuteType.DataReader;
        }

        private static ModNewsService _instance;
        public static ModNewsService Instance => _instance ?? (_instance = new ModNewsService());

        #endregion Autogen by VSW

        public ModNewsEntity GetByID(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
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

        //public List<ModNewsEntity> GetByBrand(int brandID)
        //{
        //    List<ModNewsEntity> listItem = null;

        //    var keyCache = Core.Web.Cache.CreateKey("Mod_News", "GetByBrand." + brandID);
        //    var obj = Core.Web.Cache.GetValue(keyCache);
        //    if (obj != null) listItem = obj as List<ModNewsEntity>;
        //    else
        //    {
        //        listItem = base.CreateQuery()
        //                                .Where(o => o.Activity == true && o.BrandID == brandID)
        //                                .OrderByAsc(o => new { o.Order, o.ID })
        //                                .ToList_Cache();

        //        Core.Web.Cache.SetValue(keyCache, listItem);
        //    }

        //    return listItem;
        //}
    }
}