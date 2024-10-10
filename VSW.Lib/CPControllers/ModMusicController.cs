using Microsoft.Vbe.Interop;
using System;
using System.Collections.Generic;
using VSW.Lib.Global;
using VSW.Lib.Global.ListItem;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = " danh sách nhạc",
        Description = "Quản lý - danh sách nhạc",
        Code = "ModProduct",
        Access = 31,
        Order = 1,
        Activity = true,
        CssClass = "fa-cubes")]
    public class ModMusicController : CPController
    {
        public ModMusicController()
        {
            //khoi tao Service
            DataService = ModProductService.Instance;
            DataEntity = new ModProductEntity();

            CheckPermissions = true;
        }
        public void ActionIndex(ModProductModel model)
        {
            //sap xep tu dong
            var orderBy = AutoSort(model.Sort);
            //tao danh sach
            var dbQuery = ModProductService.Instance.CreateQuery()
                                    .Where(!string.IsNullOrEmpty(model.SearchText), o => (o.Name.Contains(model.SearchText) || o.Code.Contains(model.SearchText)))
                                    .Where(model.State > 0, o => (o.State & model.State) == model.State)
                                    .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Product", model.MenuID, model.LangID))
                                   
                                  .Take(model.PageSize)
                                    .OrderBy(orderBy)
            .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
            //ValidSave1();


        }
        public void ActionAdd(ModProductModel model)
        {
            if (model.RecordID > 0)
            {
                _item = ModProductService.Instance.GetByID(model.RecordID);

                //khoi tao gia tri mac dinh khi update
                _item.Updated = DateTime.Now;
            }
            else
            {
                //khoi tao gia tri mac dinh khi insert
                _item = new ModProductEntity
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
        public void ActionSave(ModProductModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }
        public void ActionApply(ModProductModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, _item.ID);
        }
        public void ActionSaveNew(ModProductModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, _item.ID);
        }
        public override void ActionDelete(int[] arrID)
        {
            //xoa cleanurl
            ModCleanURLService.Instance.Delete("[Value] IN (" + Core.Global.Array.ToString(arrID) + ") AND [Type]='Product'");
            //xoa Product
            DataService.Delete("[ID] IN (" + Core.Global.Array.ToString(arrID) + ")");
            //thong bao
            CPViewPage.SetMessage("Đã xóa thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModProductEntity _item;




        private bool ValidSave(ModProductModel model)
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

            if (ModCleanURLService.Instance.CheckCode(!string.IsNullOrEmpty(_item.Code) ? _item.Code : Data.GetCode(_item.Name), "Product", _item.ID, model.LangID))
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
                ModProductService.Instance.Save(_item);

                //update url
                ModCleanURLService.Instance.InsertOrUpdate(_item.Code, "Product", _item.ID, _item.MenuID, model.LangID);

                //cap nhat thuoc tinh
                ModPropertyService.Instance.Delete(o => o.ProductID == _item.ID);

                foreach (var key in CPViewPage.PageViewState.AllKeys)
                {
                    if (!key.StartsWith("Property_")) continue;

                    var arrProperty = key.Split('_');
                    if (arrProperty.Length < 2) continue;

                    int propertyID = 0, propertyValueID = 0;

                    switch (arrProperty.Length)
                    {
                        case 2:
                            propertyID = Core.Global.Convert.ToInt(key.Replace("Property_", ""));
                            propertyValueID = Core.Global.Convert.ToInt(CPViewPage.PageViewState[key]);
                            break;

                        case 3:
                            propertyID = Core.Global.Convert.ToInt(arrProperty[1]);
                            propertyValueID = Core.Global.Convert.ToInt(CPViewPage.PageViewState[key]);
                            break;
                    }

                    if (propertyID < 1 || propertyValueID < 1) continue;

                    var property = ModPropertyService.Instance.GetByID(_item.ID, _item.MenuID, propertyID, propertyValueID);
                    if (property != null) continue;

                    property = new ModPropertyEntity
                    {
                        ID = 0,
                        ProductID = _item.ID,
                        MenuID = _item.MenuID,
                        PropertyID = propertyID,
                        PropertyValueID = propertyValueID
                    };

                    ModPropertyService.Instance.Save(property);
                }

                Core.Web.Cache.Clear(ModPropertyService.Instance);
                Core.Web.Cache.Clear(WebPropertyService.Instance);
            }
            catch (Exception ex)
            {
                Error.Write(ex);
                CPViewPage.Message.ListMessage.Add(ex.Message);
                return false;
            }
            return true;
        }



        private bool ValidSave1()
        {
          
            var listItem = ViewBag.Data as List<ModProductEntity>;
            //chong hack

            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                if ( listItem[i].MenuID==1277)
                {

                    CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

                    //kiem tra quyen han


                    //kiem tra ten
                    if (listItem[i].Name.Trim() == string.Empty)
                        CPViewPage.Message.ListMessage.Add("Nhập tiêu đề.");


                    //kiem tra chuyen muc
                    if (listItem[i].MenuID < 1)
                        CPViewPage.Message.ListMessage.Add("Chọn chuyên mục.");

                    

                    try
                    {
                        //save
                        ModProductService.Instance.Save(listItem[i]);

                        //update url
                   

                        //cap nhat thuoc tinh
                        ModPropertyService.Instance.Delete(o => o.ProductID == listItem[i].ID);



                        var property = ModPropertyService.Instance.GetByID(listItem[i].ID, listItem[i].MenuID, 116,128);
                        if (property != null) continue;
                        property = new ModPropertyEntity
                        {
                            ID = 0,
                            ProductID = listItem[i].ID,
                            MenuID = listItem[i].MenuID,
                            PropertyID = 116,
                            PropertyValueID = 128
                        };
                        ModPropertyService.Instance.Save(property);

                        Core.Web.Cache.Clear(ModPropertyService.Instance);
                        Core.Web.Cache.Clear(WebPropertyService.Instance);
                    }
                    catch (Exception ex)
                    {
                        Error.Write(ex);
                        CPViewPage.Message.ListMessage.Add(ex.Message);
                        return false;
                    }
                }
            }
            return true;
        }





        private static int GetMaxOrder()
        {
            return ModProductService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion private func
        //public void ActionExport(ModProductModel model)
        //{
        //    //tao danh sach
        //    var listItem = ModProductService.Instance.CreateQuery()
        //                            //.Where(!string.IsNullOrEmpty(model.SearchText), o => (o.Name.Contains(model.SearchText) || o.Code.Contains(model.SearchText)))
        //                            .Where(model.State > 0, o => (o.State & model.State) == model.State)
        //                             .Where(model.MenuID>0, o => (o.GetMenu().Code.StartsWith("bon-cau")))
        //                            .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Product", model.MenuID, model.LangID))

        //                            .Where(model.BrandID > 0, o => o.BrandID == model.BrandID)
        //                            .ToList_Cache();
        //    //title	description	link	image_link	condition	availability	price	sale_price	sale_price_effective_date	brand
        //    var listExcel = new List<List<object>>();
        //    for (int i = 0; listItem != null && i < listItem.Count; i++)
        //    {
        //        string file = !string.IsNullOrEmpty(listItem[i].File) ? Core.Web.HttpRequest.Domain + listItem[i].File.Replace("~/", "/") : "";
        //        var listRow = new List<object>
        //        {
        //            listItem[i].ID,
        //            listItem[i].Name,
        //            listItem[i].GetBrand().Name,
        //          //  listItem[i].Summary,
        //            Core.Web.HttpRequest.Domain + "/" + listItem[i].Code + Core.Web.Setting.Sys_PageExt,
        //            file,
        //           // "",
        //           // "",
        //            listItem[i].Price,
        //            listItem[i].Price2,
        //           // string.Format("{0:dd/MM/yyyy}", listItem[i].Updated),
        //          //B listItem[i].GetBrand().Name
        //        };
        //        listExcel.Add(listRow);
        //    }
        //    string exportFile = CPViewPage.Server.MapPath("~/Data/upload/files/EXPORT/catalog_" + string.Format("{0:ddMMyyyy}", DateTime.Now) + "_" + DateTime.Now.Ticks + ".xls");
        //    Excel.Export(listExcel, 1, CPViewPage.Server.MapPath("~/CP/Template/catalog.xls"), exportFile);
        //    CPViewPage.Response.Clear();
        //    CPViewPage.Response.ContentType = "application/excel";
        //    CPViewPage.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(exportFile));
        //    CPViewPage.Response.WriteFile(exportFile);
        //    CPViewPage.Response.End();
        //}
    }
    public class ModProductModel : DefaultModel
    {
        public int LangID { get; set; } = 1;
        public string SearchText { get; set; }
        public int MenuID { get; set; }
        public int BrandID { get; set; }
        public int State { get; set; }
        public int[] ArrState { get; set; }
    }
}



