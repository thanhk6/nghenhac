using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Tin tức",
        Description = "Quản lý - Tin tức",
        Code = "ModNews",
        Access = 31,
        Order = 4,
        Activity = true,
        CssClass = "fa-list-alt")]
    public class ModNewsController : CPController
    {
        public ModNewsController()
        {
            //khoi tao Service
            DataService = ModNewsService.Instance;
            DataEntity = new ModNewsEntity();
            CheckPermissions = true;
        }
        public void ActionIndex(ModNewsModel model)
        {
            //sap xep tu dong
            var orderBy = AutoSort(model.Sort);
            //tao danh sach
            var dbQuery = ModNewsService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => (o.Name.Contains(model.SearchText) || o.Code.Contains(model.SearchText)))
                                .Where(model.State > 0, o => (o.State & model.State) == model.State)
                                .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("News", model.MenuID, model.LangID))
                                .Where(model.BrandID > 0, o => o.BrandID == model.BrandID)
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);
            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModNewsModel model)
        {
            if (model.RecordID > 0)
            {
                _item = ModNewsService.Instance.GetByID(model.RecordID);

                //khoi tao gia tri mac dinh khi update
                _item.Updated = DateTime.Now;
            }
            else
            {
                _item = new ModNewsEntity
                {
                    MenuID = model.MenuID,
                    BrandID = model.BrandID,
                    Published = DateTime.Now,
                    Updated = DateTime.Now,
                    Order = GetMaxOrder(),
                    Activity = CPViewPage.UserPermissions.Approve
                };

               // khoi tao gia tri mac dinh khi insert
                var json = Cookies.GetValue(DataService.ToString(), true);
                if (!string.IsNullOrEmpty(json))
                    _item = new JavaScriptSerializer().Deserialize<ModNewsEntity>(json);
            }
            ViewBag.Data = _item;
            ViewBag.Model = model;
        }
        public void ActionSave(ModNewsModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }
        public void ActionApply(ModNewsModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, _item.ID);
        }
        public void ActionSaveNew(ModNewsModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, _item.ID);
        }

        public override void ActionDelete(int[] arrID)
        {
            //xoa cleanurl
            ModCleanURLService.Instance.Delete("[Value] IN (" + Core.Global.Array.ToString(arrID) + ") AND [Type]='News'");

            //xoa News
            DataService.Delete("[ID] IN (" + Core.Global.Array.ToString(arrID) + ")");

            //thong bao
            CPViewPage.SetMessage("Đã xóa thành công.");
            CPViewPage.RefreshPage();
        }
        #region private func

        private ModNewsEntity _item;

        private bool ValidSave(ModNewsModel model)
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

            if (ModCleanURLService.Instance.CheckCode(!string.IsNullOrEmpty(_item.Code) ? _item.Code : Data.GetCode(_item.Name), "News", _item.ID, model.LangID))
                CPViewPage.Message.ListMessage.Add("Mã đã tồn tại. Vui lòng chọn mã khác.");

            //kiem tra chuyen muc
            if (_item.MenuID < 1)
                CPViewPage.Message.ListMessage.Add("Chọn chuyên mục.");

            if (CPViewPage.Message.ListMessage.Count != 0) return false;

            if (string.IsNullOrEmpty(_item.Code)) _item.Code = Data.GetCode(_item.Name);

            //cap nhat state
            _item.State = GetState(model.ArrState);

            try
            {
                //save
                ModNewsService.Instance.Save(_item);

                //update url
                ModCleanURLService.Instance.InsertOrUpdate(_item.Code, "News", _item.ID, _item.MenuID, model.LangID);
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
            return ModNewsService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion private func
    }
    public class ModNewsModel : DefaultModel
    {
        public int LangID { get; set; } = 1;

        public int MenuID { get; set; }
        public int BrandID { get; set; }
        public int State { get; set; }
        public string SearchText { get; set; }

        public int[] ArrState { get; set; }
    }
}