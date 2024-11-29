using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModOrderDetailEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int OrderID { get; set; }

        [DataInfo]
        public int ProductID { get; set; }

        [DataInfo]
        public int Quantity { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public long Price { get; set; }

        #endregion Autogen by VSW

        private ModOrderEntity _oOrder;

        public ModOrderEntity GetOrder()
        {
            if (_oOrder == null && OrderID > 0)
                _oOrder = ModOrderService.Instance.GetByID(OrderID);

            return _oOrder ?? (_oOrder = new ModOrderEntity());
        }

        private ModProductEntity _oProduct;

        public ModProductEntity GetProduct()
        {
            if (_oProduct == null && ProductID > 0)
                _oProduct = ModProductService.Instance.GetByID(ProductID);

            return _oProduct ?? (_oProduct = new ModProductEntity());
        }
    }

    public class ModOrderDetailService : ServiceBase<ModOrderDetailEntity>
    {
        #region Autogen by VSW

        private ModOrderDetailService() : base("[Mod_OrderDetail]")
        {
        }

        private static ModOrderDetailService _instance;
        public static ModOrderDetailService Instance => _instance ?? (_instance = new ModOrderDetailService());

        #endregion Autogen by VSW

        public ModOrderDetailEntity GetByID(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }
    }
}