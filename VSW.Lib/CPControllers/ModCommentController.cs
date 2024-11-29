using System;
using System.Collections.Generic;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Ý kiến khách hàng",
        Description = "Quản lý - Ý kiến khách hàng",
        Code = "ModComment",
        Access = 31,
        Order = 4,
        Activity = false,
        CssClass = "fa-bell")]

    public class ModCommentController : CPController
    {
        public ModCommentController()
        {
            //khoi tao Service
            DataService = ModCommentService.Instance;
            DataEntity = new ModCommentEntity();
            CheckPermissions = true;
        }

        public void ActionIndex(ModCommentModel model)
        {
            //sap xep tu dong
            var orderBy = AutoSort(model.Sort);

            //tao danh sach
            var dbQuery = ModCommentService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => (o.Name.Contains(model.SearchText) || o.Content.Contains(model.SearchText)))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModCommentModel model)
        {
            if (model.RecordID > 0)
            {
                _item = ModCommentService.Instance.GetByID(model.RecordID);

                //khoi tao gia tri mac dinh khi update
                if (_item.Updated <= DateTime.MinValue) _item.Updated = DateTime.Now;
            }
            else
            {
                _item = new ModCommentEntity
                {
                    Published = DateTime.Now,
                    Updated = DateTime.Now,
                    Order = GetMaxOrder(),
                    Activity = CPViewPage.UserPermissions.Approve
                };
            }

            ViewBag.Data = _item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModCommentModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModCommentModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, _item.ID);
        }

        public void ActionSaveNew(ModCommentModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, _item.ID);
        }

        #region private func

        private ModCommentEntity _item;

        private bool ValidSave(ModCommentModel model)
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
                CPViewPage.Message.ListMessage.Add("Nhập tên.");

            if (_item.Content.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập nội dung.");

            if (CPViewPage.Message.ListMessage.Count != 0) return false;

            try
            {
                //save
                ModCommentService.Instance.Save(_item);
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
            return ModCommentService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion private func
    }

    public class ModCommentModel : DefaultModel
    {
        public int LangID { get; set; } = 1;

        public string SearchText { get; set; }
    }
}