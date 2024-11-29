using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using VSW.Core.Models;
namespace VSW.Lib.Models
{
    public class ModMp3Entity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }
        [DataInfo]
        public int State { get; set; }
        [DataInfo]
        public int AuthorID { get; set; }
        [DataInfo]
        public override string Name { get; set; }
        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string FileMp3 { get; set; }
        [DataInfo]
        public long View { get; set; } = 1;

        [DataInfo]
        public string Summary { get; set; }

        [DataInfo]
        public string Content { get; set; }

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
        public string Specifications { get; set; }
        [DataInfo]
        public string KeyWordSearch { get; set; }
        [DataInfo]
        public string Alt { get; set; }
        
        #endregion Autogen by VSW
        public ModMp3Entity()
        {
            Items.SetValue("IsName", true);

            Items.SetValue("IsSummary", true);
        }
        //public bool HasGift => GetGift().Count > 0;
        public void UpView()
        {
            View++;
            ModMp3Service.Instance.Save(this, o => o.View);
        }


        private string _oSummary;
        public string Summary2
        {
            get
            {
                if (string.IsNullOrEmpty(_oSummary) && !string.IsNullOrEmpty(Summary))
                {
                    _oSummary = Summary.Replace("\n", "<br />");
                }

                return _oSummary;
            }
      
        }      
        private WebMenuEntity _oMenu;
        public WebMenuEntity GetMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            return _oMenu ?? (_oMenu = new WebMenuEntity());
        }
        //private ModBrandEntity _oBrand;
        //public ModBrandEntity GetBrand()
        //{
        //    if (_oBrand == null && BrandID > 0)

        //        _oBrand = ModBrandService.Instance.GetByID(BrandID);
        //    return _oBrand ?? (_oBrand = new ModBrandEntity());
        //}
        

       

        private List<ModProductFileEntity> _oGetFile;
        public List<ModProductFileEntity> GetFile()
        {
            if (_oGetFile == null && MenuID > 0)
                _oGetFile = ModProductFileService.Instance.CreateQuery()
                                                .Where(o => o.ProductID == ID)
                                                .OrderByAsc(o => o.Order)
                                               .ToList_Cache();
            return _oGetFile;
        }

        
    }
    public class ModMp3Service : ServiceBase<ModMp3Entity>
    {
        #region Autogen by VSW

        public ModMp3Service() : base("[Mod_Mp3]")
        {
            //DBExecuteMode = DBExecuteType.DataReader;
            //DBType = DBType.SQL2012;
        }

        private static ModMp3Service _instance;

        public static ModMp3Service Instance => _instance ?? (_instance = new ModMp3Service());

        #endregion Autogen by VSW
        public ModMp3Entity GetByID(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
        public ModMp3Entity GetByID_Cache(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle_Cache();
        }
        public ModMp3Entity Getcode(string code)
        {
            return CreateQuery().Where(o => o.Code == code).ToSingle();
        }

      public DateTime GetDateTime(DateTime a)
        {

            return a; 
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