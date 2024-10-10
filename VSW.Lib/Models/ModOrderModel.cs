using System;
using System.Collections.Generic;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModOrderEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int StatusID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public long Total { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public string Phone { get; set; }

        [DataInfo]
        public string Address { get; set; }

        [DataInfo]
        public string Title { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }
        [DataInfo]
        public string Payment { get; set; }
        [DataInfo]
        public string  Gender { get; set; }
        [DataInfo]
        public int CityID { get; set; }
        [DataInfo]
        public int DistrictID { get; set; }
        [DataInfo]
        public int WardID { get; set; }
        [DataInfo]
        public bool Invoice { get; set; }

        [DataInfo]
        public string CompanyName { get; set; }
        [DataInfo]
        public string CompanyAddress { get; set; }
        [DataInfo]
        public string CompanyTax { get; set; }
        [DataInfo]
        public bool Formality { get; set; }
        #endregion Autogen by VSW

        private WebMenuEntity _oStatus;
        public WebMenuEntity GetStatus()
        {
            if (_oStatus == null && StatusID > 0)
                _oStatus = WebMenuService.Instance.GetByID_Cache(StatusID);

            return _oStatus ?? (_oStatus = new WebMenuEntity());
        }
        private List<ModOrderDetailEntity> _oGetOrderDetail;
        public List<ModOrderDetailEntity> GetOrderDetail()
        {
            if (_oGetOrderDetail == null)
            {
                _oGetOrderDetail = ModOrderDetailService.Instance.CreateQuery()
                                                    .Where(o => o.OrderID == ID)
                                                    .ToList_Cache();
            }

            return _oGetOrderDetail ?? (_oGetOrderDetail = new List<ModOrderDetailEntity>());
        }


        private WebMenuEntity _oGetCity;
        public WebMenuEntity GetCity()
        {
            if (_oGetCity == null && CityID > 0)
                _oGetCity = WebMenuService.Instance.GetByID_Cache(CityID);

            return _oGetCity ?? (_oGetCity = new WebMenuEntity());
        }
        private WebMenuEntity _oGetDistrict;
        public WebMenuEntity GetDistrict()
        {
            if (_oGetDistrict == null && DistrictID > 0)
                _oGetDistrict = WebMenuService.Instance.GetByID_Cache(DistrictID);

            return _oGetDistrict ?? (_oGetDistrict = new WebMenuEntity());
        }
        private WebMenuEntity _oGetWard;
        public WebMenuEntity GetWard()
        {
            if (_oGetWard == null && WardID > 0)
                _oGetWard = WebMenuService.Instance.GetByID_Cache(WardID);

            return _oGetWard ?? (_oGetWard = new WebMenuEntity());
        }
    }
    public class ModOrderService : ServiceBase<ModOrderEntity>
    {
        #region Autogen by VSW

        private ModOrderService() : base("[Mod_Order]")
        {
        }

        private static ModOrderService _instance;
        public static ModOrderService Instance => _instance ?? (_instance = new ModOrderService());

        #endregion Autogen by VSW

        public ModOrderEntity GetByID(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
    }
}