using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Thương hiệu",
        Description = "Quản lý - Nhãn hiệu",
        Code = "ModBrand",
        Access = 31,
        Order = 4,
        Activity = true,
        CssClass = "fa-list-alt")]
    public class ModBrandController : CPController
    {
        public ModBrandController()
        {
            //khoi tao Service
            DataService = ModBrandService.Instance;
            DataEntity = new ModBrandEntity();
            CheckPermissions = true;
        }

        public void ActionIndex(ModBrandModel model)
        {
            //sap xep tu dong
            var orderBy = AutoSort(model.Sort);

            //tao danh sach
            var dbQuery = ModBrandService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => (o.Name.Contains(model.SearchText) || o.Code.Contains(model.SearchText)))
                                .Where(model.State > 0, o => (o.State & model.State) == model.State)
                                .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Brand", model.MenuID, model.LangID))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModBrandModel model)
        {
            if (model.RecordID > 0)
            {
                _item = ModBrandService.Instance.GetByID(model.RecordID);

                //khoi tao gia tri mac dinh khi update
                if (_item.Updated <= DateTime.MinValue) _item.Updated = DateTime.Now;
            }

            else
            {
                _item = new ModBrandEntity
                {
                    MenuID = model.MenuID,
                    Published = DateTime.Now,
                    Updated = DateTime.Now,
                    Order = GetMaxOrder(),
                    Activity = CPViewPage.UserPermissions.Approve
                };
            }

            ViewBag.Data = _item;
            ViewBag.Model = model;
        }
        public void ActionSave(ModBrandModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }
        public void ActionApply(ModBrandModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, _item.ID);
        }
    
        public void ActionSaveNew(ModBrandModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, _item.ID);
        }
  
        public override void ActionDelete(int[] arrID)
        {
            //xoa cleanurl
            ModCleanURLService.Instance.Delete("[Value] IN (" + Core.Global.Array.ToString(arrID) + ") AND [Type]='Brand'");

            //xoa Brand
            DataService.Delete("[ID] IN (" + Core.Global.Array.ToString(arrID) + ")");

            //thong bao
            CPViewPage.SetMessage("Đã xóa thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModBrandEntity _item;

        private bool ValidSave(ModBrandModel model)
        {
            TryUpdateModel(_item);

            //chong hack
            _item.ID = model.RecordID;

            ViewBag.Data = _item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            //kiem tra ten
            if (_item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tiêu đề.");

            if (ModCleanURLService.Instance.CheckCode(!string.IsNullOrEmpty(_item.Code) ? _item.Code : Data.GetCode(_item.Name), "Brand", _item.ID, model.LangID))
                CPViewPage.Message.ListMessage.Add("Mã đã tồn tại. Vui lòng chọn mã khác.");

            //kiem tra chuyen muc
            if (_item.MenuID < 1)
                CPViewPage.Message.ListMessage.Add("Chọn chuyên mục.");

            if (CPViewPage.Message.ListMessage.Count != 0) return false;

            _item.Code = Data.GetCode(_item.Name);

            //cap nhat state
            _item.State = GetState(model.ArrState);

            try
            {
                //save
                ModBrandService.Instance.Save(_item);

                //update url
                ModCleanURLService.Instance.InsertOrUpdate("k_N"+_item.Code, "Brand", _item.ID, _item.MenuID, model.LangID);
            }
            catch (Exception ex)
            {
                Error.Write(ex);
                CPViewPage.Message.ListMessage.Add(ex.Message);
                return false;
            }

            return true;
        }

        private static int GetMaxOrder()
        {
            return ModBrandService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion private func
    }

    public class ModBrandModel : DefaultModel
    {
        public int LangID { get; set; } = 1;

        public int MenuID { get; set; }
        public int State { get; set; }
        public string SearchText { get; set; }



        public List<int> ArrState { get; set; }
    }
}