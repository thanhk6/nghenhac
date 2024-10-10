using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using VSW.Core.Models;
namespace VSW.Lib.Models
{
    public class ModProductEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int BrandID { get; set; }

        [DataInfo]
        public int StyleID { get; set; }

        [DataInfo]
        public int State { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string FileDetail { get; set; }

        [DataInfo]
        public string Model { get; set; }

        [DataInfo]
        public string Color { get; set; }

        [DataInfo]
        public string Size { get; set; }

        [DataInfo]
        public long Price { get; set; }

        [DataInfo]
        public long Price2 { get; set; }

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
        public long PriceSale
        {
            get
            {
                if (Price2 == 0)
                {
                    return Price;
                }
                var brand = GetBrand();

                if (brand == null)
                {
                    return Price;
                };
                return Price;
                //else
                //{
                //    if (brand.Discount <= 0)
                //    {
                //        return Price;
                //    }
                //    return Price2 - (Price2 * brand.Discount / 100);
                //}
            }
        }
        #endregion Autogen by VSW
        public ModProductEntity()
        {
            Items.SetValue("IsName", true);

            Items.SetValue("IsSummary", true);
        }
        //public bool HasGift => GetGift().Count > 0;
        public void UpView()
        {
            View++;
            ModProductService.Instance.Save(this, o => o.View);
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
        private long _oSellOff;
        public long SellOff
        {
            get
            {
                if (_oSellOff == 0 && Price2 > Price)
                    _oSellOff = Price2 - Price;

                return _oSellOff;
            }
        }
        private long _oSellOffPercent;
        public long SellOffPercent
        {
            get
            {
                if (_oSellOffPercent == 0 && Price2 > 0 && SellOff > 0)

                    _oSellOffPercent = SellOff*100 / Price2;

                return _oSellOffPercent;
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
        private WebMenuEntity _oBrand;
        public WebMenuEntity GetBrand()
        {
            if (_oBrand == null && BrandID > 0)
                _oBrand = WebMenuService.Instance.GetByID_Cache(BrandID);

            return _oBrand ?? (_oBrand = new WebMenuEntity());
        }

        private WebMenuEntity _oStyle;
        public WebMenuEntity GetStyle()
        {
            if (_oStyle == null && StyleID > 0)

                _oStyle = WebMenuService.Instance.GetByID_Cache(StyleID);

            return _oStyle ?? (_oStyle = new WebMenuEntity());
        }

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


    public class ModProductService : ServiceBase<ModProductEntity>
    {
        #region Autogen by VSW

        public ModProductService() : base("[Mod_Product]")
        {
            //DBExecuteMode = DBExecuteType.DataReader;
            //DBType = DBType.SQL2012;
        }

        private static ModProductService _instance;

        public static ModProductService Instance => _instance ?? (_instance = new ModProductService());

        #endregion Autogen by VSW
        public ModProductEntity GetByID(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
        public ModProductEntity GetByID_Cache(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle_Cache();
        }
        public ModProductEntity Getcode(string code)
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