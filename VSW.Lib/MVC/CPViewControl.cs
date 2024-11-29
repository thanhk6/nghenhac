using System.Collections.Generic;
using VSW.Core.Models;
using VSW.Lib.Global;
using VSW.Lib.Models;

namespace VSW.Lib.MVC
{
    public class CPViewControl : Core.MVC.ViewControl
    {
        private readonly string[] _arrColor = { "red", "blue", "green", "yellow", "orange", "purple" };
        private readonly string[] _arrCommand = "export,new,edit,publish,unpublish,delete,copy,config,storage,apply,save,save-new,upload,cancel".Split(',');
        private readonly string[] _arrClass = "fa-download,fa-plus-circle,fa-pencil-square-o,fa-check-circle,fa-ban,fa-times,fa-files-o,fa-refresh,fa-code,fa-check,fa-save,fa-plus,fa-plus,fa-times".Split(',');

        public CPViewPage CPViewPage => Page as CPViewPage;

        protected string GetName(EntityBase entityBase)
        {
            return entityBase == null ? string.Empty : entityBase.Name;
        }

        protected string GetOrder(int id, int order)
        {
            return @"<input type=""number"" class=""form-control text-area-order input-sm"" id=""order[" + id + @"]"" value=""" + order + @""" size=""10"" />";
        }

        protected string GetCheckbox(int id, int index)
        {
            return @"<label class=""itemCheckBox itemCheckBox-sm"">
                        <input type=""checkbox"" id=""cb" + index + @""" name=""cid"" value=""" + id + @""" onclick=""isChecked(this.checked)"" />
                        <i class=""check-box""></i>
                    </label>";
        }

        protected string GetDefault(int id, bool defaut)
        {
            return @"<a href=""javascript:void(0)"" onclick=""vsw_exec_cmd('[defaultgx][" + id + @"]'); return false"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Click để duyệt và hủy duyệt"">
                        <span class=""fa " + (defaut ? "fa-check-circle publish" : "fa-dot-circle-o unpublish") + @"""></span>
                    </a>";
        }
        protected string GetShowMenuTop(int id, bool showMenuTop)
        {
            return @"<a href=""javascript:void(0)"" onclick=""vsw_exec_cmd('[showmenutop][" + id + "," + !showMenuTop + @"]'); return false"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Click để h.thị và không h.thị"">
                        <span class=""fa " + (showMenuTop ? "fa-check-circle publish" : "fa-dot-circle-o unpublish") + @"""></span>
                    </a>";
        }
        protected string GetPublish(int id, bool activity)
        {
            return @"<a href=""javascript:void(0)"" onclick=""vsw_exec_cmd('[publishgx][" + id + "," + !activity + @"]'); return false"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Click để duyệt và hủy duyệt"">
                        <span class=""fa " + (activity ? "fa-check-circle publish" : "fa-dot-circle-o unpublish") + @"""></span>
                    </a>";
        }

        protected string GetMultiple(bool multiple)
        {
            return @"<a href=""javascript:void(0)"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""" + (multiple ? "Chọn nhiều" : "Chọn 1") + @""">
                        <span class=""fa " + (multiple ? "fa-check-circle publish" : "fa-dot-circle-o unpublish") + @"""></span>
                    </a>";
        }

        protected string GetSortUnLink(string name, string key)
        {
            return $@"<a href=""javascript:void(0)"">{name} {GetImgSortTypeDesc(key)}</a>";
        }

        protected string GetSortLink(string name, string key)
        {
            return $@"<a href=""javascript:VSWRedirect('Index', '{key}-{GetSortTypeDesc(key)}', 'Sort')"">{name} {GetImgSortTypeDesc(key)}</a>";
        }


        protected string GetTinyAddCommand()
        {
            return GetListCommand("cancel|Đóng");
        }

        protected string GetSortAddCommand()
        {
            return GetListCommand("apply|Lưu,save|Lưu  &amp; đóng,cancel|Đóng");
        }

        protected string GetDefaultAddCommand()
        {
            return GetListCommand("apply|Lưu,save|Lưu  &amp; đóng,save-new|Lưu &amp; thêm,cancel|Đóng");
        }

        protected string GetDefaultAddCommand(string extension)
        {
            return GetListCommand(extension + "apply|Lưu,save|Lưu  &amp; đóng,save-new|Lưu &amp; thêm,cancel|Đóng");
        }

        protected string GetTinyListCommand()
        {
            return GetListCommand("delete|Xóa,config|Xóa cache,storage|Xóa Html");
        }

        protected string GetSortListCommand()
        {
            return GetListCommand("new|Thêm,edit|Sửa,delete|Xóa,copy|Sao chép,config|Xóa cache,storage|Xóa Html");
        }

        protected string GetDefaultListCommand()
        {
            return GetListCommand("new|Thêm,edit|Sửa,publish|Duyệt,unpublish|Bỏ duyệt,delete|Xóa,copy|Sao chép,config|Xóa cache,storage|Xóa Html");
        }

        protected string GetDefaultListCommand(string extension)
        {
            return GetListCommand(extension + ",new|Thêm,edit|Sửa,publish|Duyệt,unpublish|Bỏ duyệt,delete|Xóa,copy|Sao chép,config|Xóa cache,storage|Xóa Html");
        }

        protected string GetListCommand(string commands)
        {
            var arrCommand = commands.Split(',');

            var html = @"<button type=""button"" class=""btn default"" onclick=""javascript:window.history.back()"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Trở lại trang trước""><i class=""fa fa-angle-left""></i>Back</button>";

            for (var i = 0; i < arrCommand.Length; i++)
            {
                var key = arrCommand[i].Split('|')[0];
                var name = arrCommand[i].Split('|')[1];
                var color = _arrColor[i % _arrColor.Length];

                var classValue = _arrClass[System.Array.IndexOf(_arrCommand, key)];

                switch (key)
                {
                    case "new":
                        html += @"<button type=""button"" class=""btn " + color + @""" onclick=""VSWRedirect('Add')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Thêm mới""><i class=""fa fa-plus""></i>" + name + @"</button>";
                        break;

                    case "delete":
                        html += @"<button type=""button"" class=""btn " + color + @""" onclick=""if(document.vswForm.boxchecked.value>0){sw_confirm('Thông báo !', 'Bạn chắc là mình muốn xóa chứ !', 'javascript:vsw_exec_cmd(\'delete\')')}"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""Xóa""><i class=""fa fa-trash-o""></i>" + name + @"</button>";
                        break;

                    case "publish":
                    case "unpublish":
                    case "edit":
                    case "copy":
                        html += $@"<button type=""button"" class=""btn {color}"" onclick=""if(document.vswForm.boxchecked.value>0){{vsw_exec_cmd('{key}')}}"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""{name}""><i class=""fa {classValue}""></i>{name}</button>";
                        break;

                    default:
                        html += $@"<button type=""button"" class=""btn {color}"" onclick=""vsw_exec_cmd('{key}')"" data-toggle=""tooltip"" data-placement=""bottom"" data-original-title=""{name}""><i class=""fa {classValue}""></i>{name}</button>";
                        break;
                }
            }

            return html;
        }

        protected string GetPagination(int pageIndex, int pageSize, int totalRecord)
        {
            var pager = new Pager
            {
                IsCpLayout = true,
                ActionName = "Index",
                ParamName = "PageIndex",
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecord = totalRecord
            };

            pager.Update();

            var html = @"<div class=""dataTables_length"">
                            <label>Hiển thị</label>
                            " + ShowDDLLimit(pager.PageSize) + @"
                        </div>";

            html += @"  <div class=""dataTables_paginate"">
                            <ul class=""pagination pagination-sm"">
                                " + pager.Html + @"
                            </ul>
                        </div>";

            return html;
        }

        protected string ShowDDLLimit(int pageSize)
        {
            return ShowDDLLimit(pageSize, "Index");
        }

        protected string ShowDDLLimit(int pageSize, string key)
        {
            int[] arrSize = { 5, 10, 15, 20, 30, 50, 100 };

            var html = @"<select class=""form-control input-inline input-sm"" name=""limit"" id=""limit"" onchange=""VSWRedirect('" + key + @"')"" size=""1"">";

            foreach (var t in arrSize)
            {
                html += @"<option value=""" + t + @""" " + (t == pageSize ? "selected" : string.Empty) + @">" + t + @"</option>";
            }

            html += @" </select>";

            return html;
        }

        protected string ShowDDLLang(int langID)
        {
            return ShowDDLLang(langID, "Index");
        }

        protected string ShowDDLLang(int langID, string key)
        {
            var list = SysLangService.Instance.CreateQuery().ToList_Cache();

            var html = @"   <label>Ngôn ngữ</label>
                            <select class=""form-control input-inline input-sm"" name=""filter_lang"" id=""filter_lang"" onchange=""VSWRedirect('" + key + @"','0','parent_id')"" size=""1"">";

            for (var i = 0; list != null && i < list.Count; i++)
            {
                html += @"      <option value=""" + list[i].ID + @""" " + (list[i].ID == langID ? "selected" : string.Empty) + @">" + list[i].Name + @"</option>";
            }

            html += @"      </select>";

            return html;
        }

        protected string ShowMap(List<EntityBase> listMap)
        {
            var html = @"<li class=""breadcrumb-item"">
                            <i class=""fa fa-home""></i>
                            <a href = ""javascript:VSWRedirect('Index', '0', 'ParentID')"">Root</a>
                        </li>";

            for (var i = 0; listMap != null && i < listMap.Count; i++)
            {
                html += @"<li class=""breadcrumb-item"">
                            <a href=""javascript:VSWRedirect('Index', '" + listMap[i].ID + @"', 'ParentID')"">" + listMap[i].Name + @"</a>
                        </li>";
            }

            return html;
        }

        protected string ShowMessage()
        {
            var html = string.Empty;

            var result = html;
            if (Cookies.GetValue("message") != string.Empty)
            {
                html += @"<div class=""note note-info"">
                            <p>" + Data.Base64Decode(Cookies.GetValue("message")) + @"</p>
                        </div>";

                Cookies.Remove("message");
            }
            else
            {
                var message = CPViewPage.Message;

                if (message == null || message.ListMessage.Count <= 0) return html;

                var classValue = message.MessageTypeName == "error" ? "note-danger" : "note-info";

                foreach (var m in message.ListMessage)
                    result = result + "<p>" + m + "</p>";

                html += @"  <div class=""note " + classValue + @""">
                                " + result + @"
                            </div>";
            }

            return html;
        }

        protected void CreatePathUpload(string pathChild)
        {
            Directory.Create(Server.MapPath("~/Data/upload/" + pathChild));
        }

        #region private

        private string SortType => CPViewPage.PageViewState.GetValue("Sort").ToString().Trim().Split('-')[0]
            .Replace("'", string.Empty)
            .Replace("-", string.Empty)
            .Replace(";", string.Empty);

        private bool SortDesc => string.Equals("desc", CPViewPage.PageViewState.GetValue("Sort").ToString().Trim().Split('-')[1], System.StringComparison.OrdinalIgnoreCase);

        private string GetSortTypeDesc(string type)
        {
            if (type != SortType)
                return "desc";

            return SortDesc ? "asc" : "desc";
        }

        private string GetImgSortTypeDesc(string type)
        {
            if (type != SortType)
                return string.Empty;

            return SortDesc ? @"<i class=""fa fa-angle-down""></i>" : @"<i class=""fa fa-angle-up""></i>";
        }

        #endregion private
    }
}