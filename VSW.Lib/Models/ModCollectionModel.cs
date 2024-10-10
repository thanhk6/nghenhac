using System;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModCollectionEntity : EntityBase
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

        public void UpView()
        {
            View++;
            ModCollectionService.Instance.Save(this, o => o.View);
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

    public class ModCollectionService : ServiceBase<ModCollectionEntity>
    {
        #region Autogen by VSW

        public ModCollectionService() : base("[Mod_Collection]")
        {
            DBExecuteMode = DBExecuteType.DataReader;
        }

        private static ModCollectionService _instance;
        public static ModCollectionService Instance => _instance ?? (_instance = new ModCollectionService());

        #endregion Autogen by VSW

        public ModCollectionEntity GetByID(int id)
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
    }
}