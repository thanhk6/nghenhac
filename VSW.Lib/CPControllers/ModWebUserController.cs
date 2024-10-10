using System;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Thành viên",
        Description = "Quản lý - Thành viên",
        Code = "ModWebUser",
        Access = 29,
        Order = 8,
         Activity = true,
        CssClass = "icon-16-component")]
    public class ModWebUserController : CPController
    {
        public ModWebUserController()
        {
            //khoi tao Service
            DataService = ModWebUserService.Instance;
            //CheckPermissions = false;
        }
        public void ActionIndex(ModWebUserModel model)
        {
            //sap xep tu dong
            string orderBy = AutoSort(model.Sort);
            //tao danh sach
            var dbQuery = ModWebUserService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);
            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }
        public void ActionAdd(ModWebUserModel model)
        {
            _item = model.RecordID > 0 ? ModWebUserService.Instance.GetByID(model.RecordID) : new ModWebUserEntity();
            ViewBag.Data = _item;
            ViewBag.Model = model;
        }
        public void ActionSave(ModWebUserModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }
        public void ActionApply(ModWebUserModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, _item.ID);
        }

        public void ActionSaveNew(ModWebUserModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, _item.ID);
        }
        #region private func
        private ModWebUserEntity _item;
        private bool ValidSave(ModWebUserModel model)
        {
            TryUpdateModel(_item);

            ViewBag.Data = _item;
            ViewBag.Model = model;
            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
            //kiem tra ten dang nhap
            if (!Utils.IsEmailAddress(_item.Email))
                CPViewPage.Message.ListMessage.Add("Nhập email đăng nhập.");
            else if (model.RecordID < 1 && ModWebUserService.Instance.GetByEmail(_item.Email.Trim()) != null)
                CPViewPage.Message.ListMessage.Add("Email đã tồn tại. Chọn email khác.");
            else if (model.RecordID > 0 && ModWebUserService.Instance.CheckEmail(_item.Email.Trim(), _item.ID))
                CPViewPage.Message.ListMessage.Add("Email đã tồn tại. Chọn email khác.");
            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                if (model.Password2 != string.Empty)
                    _item.Password = Security.Md5(model.Password2);
                try
                {
                    //save
                    ModWebUserService.Instance.Save(_item);
                }
                catch (Exception ex)
                {
                    Error.Write(ex);
                    CPViewPage.Message.ListMessage.Add(ex.Message);
                    return false;
                }
                return true;
            }

            return false;
        }

        #endregion private func
    }
    public class ModWebUserModel : DefaultModel
    {
        public int LangID { get; set; } = 1;

        public string SearchText { get; set; }
        public string Password2 { get; set; }
    }
}