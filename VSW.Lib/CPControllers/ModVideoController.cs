using System;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using Array = VSW.Core.Global.Array;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Video",
        Description = "Quản lý  - Video",
        Code = "ModVideo",
        Access = 31,
        Order = 4,
        ShowInMenu = true,
        CssClass = "icon-16-article")]
    public class ModVideoController : CPController
    {
        public ModVideoController()
        {
            //khoi tao Service
            DataService = ModVideoService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModVideoModel model)
        {
            //sap xep tu dong
            var orderBy = AutoSort(model.Sort);

            //tao danh sach
            var dbQuery = ModVideoService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => (o.Name.Contains(model.SearchText) || o.Code.Contains(model.SearchText)))
                                .Where(model.State > 0, o => (o.State & model.State) == model.State)
                                .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Video", model.MenuID, model.LangID))
                                .Where(model.WebUserID > 0, o => o.WebUserID == model.WebUserID)
                                .Where(model.CityID > 0, o => o.CityID == model.CityID)
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModVideoModel model)
        {
            if (model.RecordID > 0)
            {
                _item = ModVideoService.Instance.GetByID(model.RecordID);

                //khoi tao gia tri mac dinh khi update
                if (_item.Updated <= DateTime.MinValue) _item.Updated = DateTime.Now;
            }
            else
            {
                _item = new ModVideoEntity
                {
                    WebUserID = model.WebUserID,
                    MenuID = model.MenuID,
                    CityID = model.CityID,
                    Published = DateTime.Now,
                    Updated = DateTime.Now,
                    Order = GetMaxOrder(),
                    Activity = CPViewPage.UserPermissions.Approve
                };

                //khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = _item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModVideoModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModVideoModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, _item.ID);
        }

        public void ActionSaveNew(ModVideoModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, _item.ID);
        }

        public override void ActionDelete(int[] arrID)
        {
            //xoa clear url
            ModCleanURLService.Instance.Delete("[Value] IN (" + Array.ToString(arrID) + ") AND [Type]='Video'");

            //xoa Video
            DataService.Delete("[ID] IN (" + Array.ToString(arrID) + ")");

            //thong bao
            CPViewPage.SetMessage("Đã xóa thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModVideoEntity _item;

        private bool ValidSave(ModVideoModel model)
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

            if (ModCleanURLService.Instance.CheckCode(_item.Code, "Video", _item.ID, model.LangID))
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
                ModVideoService.Instance.Save(_item);

                //update url
                ModCleanURLService.Instance.InsertOrUpdate(_item.Code, "Video", _item.ID, _item.MenuID, model.LangID);
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
            return ModVideoService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion private func
    }

    public class ModVideoModel : DefaultModel
    {
        private int _langID = 1;

        public int LangID
        {
            get { return _langID; }
            set { _langID = value; }
        }

        public int WebUserID { get; set; }

        public int MenuID { get; set; }
        public int CityID { get; set; }
        public int State { get; set; }
        public string SearchText { get; set; }
        public int[] ArrState { get; set; }
    }
}