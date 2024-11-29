using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Đơn hàng",
        Description = "Quản lý  - Đơn hàng",
        Code = "ModOrder",
        Access = 31,
        Order = 2,
        Activity = true,
        CssClass = "fa-shopping-cart")]
    public class ModOrderController : CPController
    {
        public ModOrderController()
        {
            //khoi tao Service
            DataService = ModOrderService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModOrderModel model)
        {
            //sap xep tu dong
            var orderBy = AutoSort(model.Sort);

            //tao danh sach
            var dbQuery = ModOrderService.Instance.CreateQuery()
                                    .Where(!string.IsNullOrEmpty(model.SearchText), o => (o.Name.Contains(model.SearchText) || o.Code.Contains(model.SearchText) || o.Email.Contains(model.SearchText) || o.Phone.Contains(model.SearchText) || o.Address.Contains(model.SearchText)))
                                    .Take(model.PageSize)
                                    .OrderBy(orderBy)
                                    .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList_Cache();


            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModOrderModel model)
        {
            if (model.RecordID > 0)
            {
                _item = ModOrderService.Instance.GetByID(model.RecordID);

                //khoi tao gia tri mac dinh khi update
            }
            else
            {
                _item = new ModOrderEntity();

                //khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = _item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModOrderModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModOrderModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, _item.ID);
        }

        #region private func

        private ModOrderEntity _item;

        private bool ValidSave(ModOrderModel model)
        {
            TryUpdateModel(_item);

            ViewBag.Data = _item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            if (CPViewPage.Message.ListMessage.Count != 0) return false;

            //save
            ModOrderService.Instance.Save(_item);

            return true;
        }

        #endregion private func
    }

    public class ModOrderModel : DefaultModel
    {
        private int _langID = 1;
        public int LangID
        {
            get { return _langID; }
            set { _langID = value; }
        }

        public int WebUserID { get; set; }

        public string SearchText { get; set; }
    }
}