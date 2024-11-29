using System;
using System.Drawing;
using VSW.Core.Global;
using VSW.Core.MVC;
using VSW.Lib.Global;
using VSW.Lib.Global.ListItem;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using Setting = VSW.Core.Web.Setting;
namespace VSW.Lib.CPControllers
{
    public class AjaxController : CPController
    {
        public void ActionIndex()
        {

        }
        public void ActionGetChild(GetChildModel model)
        {
            var json = new Json();

            var listItem = WebMenuService.Instance.GetByParentID_Cache(model.ParentID);

            json.Instance.Node1 = @"<option value=""0"" selected=""selected"">- chọn quận / huyện -</option>";
            for (var i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node1 += @"<option value=""" + listItem[i].ID + @""" " + (listItem[i].ID == model.SelectedID ? @"selected=""selected""" : @"") + @">" + listItem[i].Name + @"</option>";
            }

            ViewBag.Model = model;

            json.Create();
        }
        public void ActionSiteGetPage(SiteGetPageModel model)
        {
            var json = new Json();

            var listPage = List.GetList(SysPageService.Instance, model.LangID);

            for (var i = 0; listPage != null && i < listPage.Count; i++)
            {
                json.Instance.Node2 += "<option " + (model.PageID.ToString() == listPage[i].Value ? "selected" : "") + " value='" + listPage[i].Value + "'>&nbsp; " + listPage[i].Name + "</option>";
            }

            json.Create();
        }
        public void ActionTemplateGetCustom(int templateID)
        {
            var json = new Json();

            json.Instance.Node1 = SysTemplateService.Instance.GetByID(templateID).Custom;

            json.Create();
        }
        public void ActionPageGetCustom(int pageID)
        {
            var json = new Json();

            json.Instance.Node1 = SysPageService.Instance.GetByID(pageID).Custom;

            json.Create();
        }
        public void ActionPageGetControl(PageGetControlModel model)
        {
            var json = new Json();
            try
            {
                if (!string.IsNullOrEmpty(model.ModuleCode))
                {
                    SysPageEntity currentPage = null;
                    var currentModule = SysModuleService.Instance.VSW_Core_GetByCode(model.ModuleCode);

                    if (model.PageID > 0)
                        currentPage = SysPageService.Instance.GetByID(model.PageID);

                    if (currentModule != null)
                    {
                        var currentObject = new Class(currentModule.ModuleType);

                        var filePath = (File.Exists(CPViewPage.Server.MapPath("~/Views/Design/" + currentModule.Code + ".ascx")) ?
                            "~/Views/Design/" + currentModule.Code + ".ascx" : "~/" + Setting.Sys_CPDir + "/Design/EditModule.ascx");
                        var sHtml = Core.Web.Utils.GetHtmlControl(CPViewPage, filePath,
                                            "CurrentObject", currentObject,
                                            "CurrentPage", currentPage,
                                            "CurrentModule", currentModule,
                                            "LangID", model.LangID);

                        if (currentObject.ExistsField("MenuID"))
                        {
                            var fieldInfo = currentObject.GetFieldInfo("MenuID");
                            var attributes = fieldInfo.GetCustomAttributes(typeof(PropertyInfo), true);
                            if (attributes.GetLength(0) > 0)
                            {
                                var propertyInfo = (PropertyInfo)attributes[0];
                                var listMenu = List.GetListByText(propertyInfo.Value.ToString());

                                var menuType = List.FindByName(listMenu, "Type").Value;

                                listMenu = List.GetList(WebMenuService.Instance, model.LangID, menuType);
                                listMenu.Insert(0, new Item(string.Empty, string.Empty));

                                for (var j = 1; j < listMenu.Count; j++)
                                {
                                    if (string.IsNullOrEmpty(listMenu[j].Name)) continue;

                                    json.Instance.Node2 += "<option " + (currentPage != null && currentPage.MenuID.ToString() == listMenu[j].Value ? "selected" : "") + " value='" + listMenu[j].Value + "'>&nbsp; " + listMenu[j].Name + "</option>";
                                }
                            }
                        }
                        if (currentObject.ExistsField("BrandID"))
                        {


                            var fieldInfo = currentObject.GetFieldInfo("BrandID");
                            var attributes = fieldInfo.GetCustomAttributes(typeof(PropertyInfo), true);
                            if (attributes.GetLength(0) > 0)
                            {
                                var propertyInfo = (PropertyInfo)attributes[0];
                                var listMenu = List.GetListByText(propertyInfo.Value.ToString());

                                var menuType = List.FindByName(listMenu, "Type").Value;

                                listMenu = List.GetList(WebMenuService.Instance, model.LangID, menuType);
                                listMenu.Insert(0, new Item(string.Empty, string.Empty));

                                for (var j = 1; j < listMenu.Count; j++)
                                {
                                    if (string.IsNullOrEmpty(listMenu[j].Name)) continue;

                                    json.Instance.Node3 += "<option " + (currentPage != null && currentPage.BrandID.ToString() == listMenu[j].Value ? "selected" : "") + " value='" + listMenu[j].Value + "'>&nbsp; " + listMenu[j].Name + "</option>";
                                }
                            }

                        }

                        json.Instance.Node1 = sHtml.Replace("{CPPath}", CPViewPage.CPPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.Write(ex);
            }

            json.Create();
        }





        #region Product
        public void ActionProductUpload(UploadModel model)
        {
            var json = new Json();

            var item = ModMp3Service.Instance.GetByID(model.ProductID);
            if (item == null)
            {
                json.Instance.Node2 += "Sản phẩm không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            var listPosted = CPViewPage.Request.Files;
            for (int i = 0; listPosted != null && i < listPosted.Count; i++)
            {
                string fileName = listPosted[i].FileName;
                if (fileName.Length < 1)
                    continue;

                try
                {
                    new Bitmap(listPosted[i].InputStream);

                    string path = CPViewPage.Server.MapPath("~/Data/upload/images/Product/");

                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);

                    string dbFile = "~/Data/upload/images/Product/" + Data.GetCode(System.IO.Path.GetFileNameWithoutExtension(fileName)) + System.IO.Path.GetExtension(fileName);
                    string file = CPViewPage.Server.MapPath(dbFile);

                    listPosted[i].SaveAs(file);

                    int order = ModProductFileService.Instance.CreateQuery()
                                                .Select(o => o.ID)
                                                .Where(o => o.ProductID == item.ID)
                                                .Count()
                                                .ToValue()
                                                .ToInt(0);

                    ModProductFileService.Instance.Save(new ModProductFileEntity
                    {
                        ID = 0,
                        ProductID = item.ID,
                        File = dbFile,
                        Order = order + 1
                    }); ;
                }
                catch (Exception ex)
                {
                    json.Instance.Node2 += ex.Message + "- File tải lên không phải ảnh. Chọn file khác.";
                }
            }

            var listItem = item.GetFile();
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node1 += @"<li data-id=""" + listItem[i].ID + @""">
                                            <img src=""" + Utils.GetResizeFile(listItem[i].File, 4, 100, 100) + @""" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Kéo thả để thay đổi vị trí ảnh"" />
                                            <a href=""javascript:void(0)"" onclick=""remove('" + listItem[i].ID + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-ban""></i></a>
                                      <a href=""javascript:void(0)"" onclick=""upFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển lên trên""><i class=""fa fa-arrow-up""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""downFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển xuống dưới""><i class=""fa fa-arrow-down""></i></a>


</li>";
            }

            ViewBag.Model = model;

            json.Create();
        }
        public void ActionProductUpdate(UpdateModel model)
        {
            var json = new Json();

            var item = ModMp3Service.Instance.GetByID(model.ProductID);
            if (item == null)
            {
                json.Instance.Node2 += "Sản phẩm không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            if (string.IsNullOrEmpty(model.Value))
            {
                json.Instance.Node2 += "Không tồn tại thông tin sắp xếp. F5 và thử lại.";
                json.Create();
            }

            string[] ArrOrder = model.Value.Split(',');

            var listItem = item.GetFile();
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                for (int j = 0; ArrOrder != null && j < ArrOrder.Length; j++)
                {
                    string[] ArrIndex = ArrOrder[j].Split('|');
                    if (listItem[i].ID.ToString() == ArrIndex[0])
                    {
                        listItem[i].Order = VSW.Core.Global.Convert.ToInt(ArrIndex[1]);
                        ModProductFileService.Instance.Save(listItem[i], o => o.Order);
                        break;
                    }
                }
            }

            ViewBag.Model = model;

            json.Create();
        }

        public void ActionProductRemove(RemoveModel model)
        {
            var json = new Json();

            var item = ModMp3Service.Instance.GetByID(model.ProductID);
            if (item == null)
            {
                json.Instance.Node2 += "Sản phẩm không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            var productFile = ModProductFileService.Instance.GetByID(model.FileID);
            if (productFile == null)
            {
                json.Instance.Node2 += "File không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            ModProductFileService.Instance.Delete(o => o.ID == model.FileID);

            var listItem = item.GetFile();
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node1 += @"<li data-id=""" + listItem[i].ID + @""">
                                            <img src=""" + Utils.GetResizeFile(listItem[i].File, 4, 100, 100) + @""" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Kéo thả để thay đổi vị trí ảnh"" />
                                            <a href=""javascript:void(0)"" onclick=""remove('" + listItem[i].ID + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-ban""></i></a>
                                        </li>";
            }

            ViewBag.Model = model;

            json.Create();
        }
        #endregion

        #region Uniform
     

      

     




        #endregion

        #region File

        public void ActionGetProperties(GetPropertiesModel model)
        {
            var json = new Json();

            var menu = WebMenuService.Instance.GetByID(model.MenuID);

            if (menu == null) json.Create();

            var property = WebPropertyService.Instance.GetByID(menu.PropertyID);
            if (property == null) json.Create();

            var listProperty = WebPropertyService.Instance.CreateQuery()
                                        .Select(o => new { o.ID, o.Name, o.Code, o.Multiple })
                                        .Where(o => o.Activity == true && o.ParentID == property.ID)
                                        .OrderByAsc(o => o.Order)
                                        .ToList_Cache();

            for (var i = 0; listProperty != null && i < listProperty.Count; i++)
            {
                var listPropertyValue = WebPropertyService.Instance.CreateQuery()
                                                    .Select(o => new { o.ID, o.Name })
                                                    .Where(o => o.ParentID == listProperty[i].ID)
                                                    .OrderByAsc(o => o.Order)
                                                    .ToList_Cache();
                if (listProperty[i].Multiple)
                {
                    json.Instance.Node1 += @"<div class=""form-group row"">
                                                <label class=""col-md-3 col-form-label text-right"">" + listProperty[i].Name + @":</label>
                                                <div class=""col-md-9"">
                                                    <div class=""checkbox-list"">";

                    for (var j = 0; listPropertyValue != null && j < listPropertyValue.Count; j++)
                    {
                        var exists = model.ProductID > 0 && ModPropertyService.Instance.GetByID(model.ProductID, model.MenuID, listProperty[i].ID, listPropertyValue[j].ID) != null;
                        json.Instance.Node1 += @"        <label class=""itemCheckBox itemCheckBox-sm"">
                                                            <input type=""checkbox"" " + (exists ? "checked" : "") + @" name=""Property_" + listProperty[i].ID + @"_" + listPropertyValue[j].ID + @""" id=""Property_" + listProperty[i].ID + @"_" + listPropertyValue[j].ID + @""" value=""" + listPropertyValue[j].ID + @""" />
                                                            <i class=""check-box""></i>
                                                            <span>" + listPropertyValue[j].Name + @"</span>
                                                        </label>";
                    }
                    json.Instance.Node1 += @"        </div>
                                                </div>                                            </div>";
                }
                else
                {
                    json.Instance.Node1 += @" <div class=""form-group row"">
                                                <label class=""col-md-3 col-form-label text-right"">" + listProperty[i].Name + @":</label>
                                                <div class=""col-md-9"">
                                                    <select class=""form-control"" name=""Property_" + listProperty[i].ID + @""" id=""Property_" + listProperty[i].ID + @""">
                                                        <option value=""0"">Root</option>";

                    for (var j = 0; listPropertyValue != null && j < listPropertyValue.Count; j++)
                    {
                        var exists = model.ProductID > 0 && ModPropertyService.Instance.GetByID(model.ProductID, model.MenuID, listProperty[i].ID, listPropertyValue[j].ID) != null;
                        json.Instance.Node1 += @"        <option value=""" + listPropertyValue[j].ID + @""" " + (exists ? "selected" : "") + @">" + listPropertyValue[j].Name + @"</option>";
                    }

                    json.Instance.Node1 += @"        </select>
                                                </div>
                                            </div>";
                }
            }
            json.Create();
        }
        public void ActionUpload(UploadModel model)
        {
            var json = new Json();

            var item = ModMp3Service.Instance.GetByID(model.ProductID);
            if (item == null)
            {
                json.Instance.Node2 += "Sản phẩm không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            var listPosted = CPViewPage.Request.Files;
            for (int i = 0; listPosted != null && i < listPosted.Count; i++)
            {
                string fileName = listPosted[i].FileName;
                if (fileName.Length < 1)
                    continue;

                try
                {
                    new Bitmap(listPosted[i].InputStream);

                    string path = CPViewPage.Server.MapPath("~/Data/upload/images/Product/");

                    if (!System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(path);

                    string dbFile = "~/Data/upload/images/Product/" + Data.GetCode(System.IO.Path.GetFileNameWithoutExtension(fileName)) + System.IO.Path.GetExtension(fileName);
                    string file = CPViewPage.Server.MapPath(dbFile);

                    listPosted[i].SaveAs(file);

                    int order = ModProductFileService.Instance.CreateQuery()
                                                .Select(o => o.ID)
                                                .Where(o => o.ProductID == item.ID)
                                                .Count()
                                                .ToValue()
                                                .ToInt(0);

                    ModProductFileService.Instance.Save(new ModProductFileEntity
                    {
                        ID = 0,
                        ProductID = item.ID,
                        File = dbFile,
                        Order = order + 1
                    }); ;
                }
                catch (Exception ex)
                {
                    json.Instance.Node2 += ex.Message + "- File tải lên không phải ảnh. Chọn file khác.";
                }
            }

            var listItem = item.GetFile();
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node1 += @"<li data-id=""" + listItem[i].ID + @""">
                                            <img src=""" + Utils.GetResizeFile(listItem[i].File, 4, 100, 100) + @""" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Kéo thả để thay đổi vị trí ảnh"" />
                                            <a href=""javascript:void(0)"" onclick=""remove('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-ban""></i></a>
                                        </li>";
            }

            ViewBag.Model = model;

            json.Create();
        }

        public void ActionUpdate(UpdateModel model)
        {
            var json = new Json();

            var item = ModMp3Service.Instance.GetByID(model.ProductID);
            if (item == null)
            {
                json.Instance.Node2 += "Sản phẩm không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            if (string.IsNullOrEmpty(model.Value))
            {
                json.Instance.Node2 += "Không tồn tại thông tin sắp xếp. F5 và thử lại.";
                json.Create();
            }

            string[] ArrOrder = model.Value.Split(',');

            var listItem = item.GetFile();
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                for (int j = 0; ArrOrder != null && j < ArrOrder.Length; j++)
                {
                    string[] ArrIndex = ArrOrder[j].Split('|');
                    if (listItem[i].ID.ToString() == ArrIndex[0])
                    {
                        listItem[i].Order = VSW.Core.Global.Convert.ToInt(ArrIndex[1]);
                        ModProductFileService.Instance.Save(listItem[i], o => o.Order);
                        break;
                    }
                }
            }

            ViewBag.Model = model;

            json.Create();
        }

        public void ActionDeleteFile(RemoveModel model)
        {
            var json = new Json();

            var item = ModMp3Service.Instance.GetByID(model.ProductID);
            if (item == null)
            {
                json.Instance.Node1 += "Sản phẩm không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            var productFile = ModProductFileService.Instance.GetByID(model.FileID);
            if (productFile == null)
            {
                json.Instance.Node1 += "File không còn tồn tại. F5 và thử lại.";
                json.Create();
            }

            ModProductFileService.Instance.Delete(o => o.ID == model.FileID);

            var listItem = item.GetFile();
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node2 += @"<li data-id=""" + listItem[i].ID + @""">
                                            <img src=""" + Utils.GetResizeFile(listItem[i].File, 4, 100, 100) + @""" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Kéo thả để thay đổi vị trí ảnh"" />
                                            <a href=""javascript:void(0)"" onclick=""DeleteFile('" + listItem[i].ID + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-ban""></i></a>
                                         <a href=""javascript:void(0)"" onclick=""upFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển lên trên""><i class=""fa fa-arrow-up""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""downFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển xuống dưới""><i class=""fa fa-arrow-down""></i></a>

</li>";
            }
            ViewBag.Model = model;

            json.Create();
        }

        public void ActionUpFile(RemoveModel model)
        {
            var json = new Json();

            if (model.ProductID < 1)
            {
                json.Instance.Node1 += "Sản phẩm không tồn tại.";
                ViewBag.Model = model;

                json.Create();
            }

            var item = ModProductFileService.Instance.CreateQuery()
                                        .Where(o => o.ProductID == model.ProductID && o.File == model.File)
                                        .ToSingle();
            if (item == null)
            {
                json.Instance.Node1 += "Ảnh không tồn tại.";
                ViewBag.Model = model;

                json.Create();
            }


            var listItem = ModProductFileService.Instance.CreateQuery()
                                            .Where(o => o.Activity == true && o.ProductID == model.ProductID)
                                            .OrderByAsc(o => o.Order)
                                            .ToList_Cache();

            var index = listItem.FindIndex(o => o.ID == item.ID);

            var indexPrev = index == 0 ? listItem.Count - 1 : (index - 1);

            var itemPrev = listItem[indexPrev];

            var order = item.Order;

            item.Order = itemPrev.Order;
            ModProductFileService.Instance.Save(item, o => o.Order);

            itemPrev.Order = order;
            ModProductFileService.Instance.Save(itemPrev, o => o.Order);

            listItem = ModProductFileService.Instance.CreateQuery()
                                            .Where(o => o.Activity == true && o.ProductID == model.ProductID)
                                            .OrderByAsc(o => o.Order)
                                            .ToList_Cache();

            for (var i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node2 += @"<li>
                                            <img src=""" + listItem[i].File + @""" />

                                            <a href=""javascript:void(0)"" onclick=""DeleteFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-ban""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""upFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển lên trên""><i class=""fa fa-arrow-up""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""downFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển xuống dưới""><i class=""fa fa-arrow-down""></i></a>
                                        </li>";
            }

            ViewBag.Model = model;

            json.Create();
        }

        public void ActionDownFile(RemoveModel model)
        {
            var json = new Json();

            if (model.ProductID < 1)
            {
                json.Instance.Node1 += "Sản phẩm không tồn tại.";
                ViewBag.Model = model;

                json.Create();
            }

            var item = ModProductFileService.Instance.CreateQuery()
                                        .Where(o => o.ProductID == model.ProductID && o.File == model.File)
                                        .ToSingle();
            if (item == null)
            {
                json.Instance.Node1 += "Ảnh không tồn tại.";
                ViewBag.Model = model;

                json.Create();
            }

            var listItem = ModProductFileService.Instance.CreateQuery()
                                            .Where(o => o.Activity == true && o.ProductID == model.ProductID)
                                            .OrderByAsc(o => o.Order)
                                            .ToList_Cache();

            var index = listItem.FindIndex(o => o.ID == item.ID);
            var indexNext = index == listItem.Count - 1 ? 0 : (index + 1);

            var itemNext = listItem[indexNext];

            var order = item.Order;

            item.Order = itemNext.Order;
            ModProductFileService.Instance.Save(item, o => o.Order);

            itemNext.Order = order;
            ModProductFileService.Instance.Save(itemNext, o => o.Order);

            listItem = ModProductFileService.Instance.CreateQuery()
                                            .Where(o => o.Activity == true && o.ProductID == model.ProductID)
                                            .OrderByAsc(o => o.Order)
                                            .ToList_Cache();

            for (var i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node2 += @"<li>
                                            <img src=""" + listItem[i].File + @""" />

                                            <a href=""javascript:void(0)"" onclick=""deleteFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-ban""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""upFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển lên trên""><i class=""fa fa-arrow-up""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""downFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển xuống dưới""><i class=""fa fa-arrow-down""></i></a>
                                        </li>";
            }
            ViewBag.Model = model;
            json.Create();
        }
        public void ActionAddFile(FileModel model)
        {
            var json = new Json();

            if (string.IsNullOrEmpty(model.Name))
            {
                json.Instance.Node1 += "Nhập tên sản phẩm trước.";
                ViewBag.Model = model;

                json.Create();
            }

            if (model.MenuID < 1)
            {
                json.Instance.Node1 += "Chọn chuyên mục sản phẩm trước.";
                ViewBag.Model = model;

                json.Create();
            }

            ModMp3Entity product = null;
            if (model.ProductID > 0)
                product = ModMp3Service.Instance.GetByID(model.ProductID);

            if (product == null)
            {
                //luu san pham
                product = new ModMp3Entity()
                {
                    ID = 0,
                    MenuID = model.MenuID,
                    Name = model.Name,
                    Code = Data.GetCode(model.Name),
                    Published = DateTime.Now,
                    Updated = DateTime.Now,
                    Order = GetMaxProductOrder(),
                    Activity = false
                };

                ModMp3Service.Instance.Save(product);

                json.Instance.Node3 += product.ID;
            }

            if (ModProductFileService.Instance.Exists(product.ID, model.File))
            {
                json.Instance.Node1 += "Ảnh đã tồn tại.";
                ViewBag.Model = model;

                json.Create();
            }

            ModProductFileService.Instance.Save(new ModProductFileEntity()
            {
                ID = 0,
                ProductID = product.ID,
                File = model.File,
                Order = GetMaxProductFileOrder(product.ID),
                Activity = true
            });

            var listItem = ModProductFileService.Instance.CreateQuery()
                                            .Where(o => o.Activity == true && o.ProductID == product.ID)
                                            .OrderByAsc(o => o.Order)
                                            .ToList_Cache();

            for (var i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node2 += @"<li>
                                            <img src=""" + listItem[i].File + @""" />

                                            <a href=""javascript:void(0)"" onclick=""deleteFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-ban""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""upFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển lên trên""><i class=""fa fa-arrow-up""></i></a>
                                            <a href=""javascript:void(0)"" onclick=""downFile('" + listItem[i].File + @"')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Chuyển xuống dưới""><i class=""fa fa-arrow-down""></i></a>
                                        </li>";
            }
            ViewBag.Model = model;

            json.Create();
        }
        #endregion

        #region private

        private static int GetMaxProductOrder()
        {
            return ModMp3Service.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        private static int GetMaxProductFileOrder(int productID)
        {
            return ModProductFileService.Instance.CreateQuery()
                    .Where(o => o.ProductID == productID)
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }
        #endregion private

        public class GetChildModel
        {
            public int ParentID { get; set; }
            public int SelectedID { get; set; }
        }
        public class PageGetControlModel
        {
            public int LangID { get; set; }
            public int PageID { get; set; }
            public string ModuleCode { get; set; }
        }

        public class SiteGetPageModel
        {
            public int LangID { get; set; }
            public int PageID { get; set; }
        }

        #region File

        public class UploadModel
        {
            public int ProductID { get; set; }
        }

        public class UpdateModel
        {
            public int ProductID { get; set; }
            public string Value { get; set; }
        }

        public class RemoveModel
        {
            public int ProductID { get; set; }
            public int FileID { get; set; }
            public string File { get; set; }
        }

        #endregion

        public class GetPropertiesModel
        {
            public int MenuID { get; set; }
            public int LangID { get; set; }
            public int ProductID { get; set; }
        }
        public class FileModel
        {
            public string Name { get; set; }
            public int MenuID { get; set; }

            public int ProductID { get; set; }
            public string File { get; set; }
        }

    }
}