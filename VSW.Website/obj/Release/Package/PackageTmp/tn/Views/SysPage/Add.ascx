<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as SysPageModel;
    var item = ViewBag.Data as SysPageEntity;
    var listTemplate = SysTemplateService.Instance.CreateQuery()
                                        .Where(o => o.LangID == model.LangID && o.Device == 0)
                                        .OrderByAsc(o => o.Order)
                                        .ToList();
    //var listTemplateMobile = SysTemplateService.Instance.CreateQuery()
    //                                    .Where(o => o.LangID == model.LangID && o.Device == 1)
    //                                    .OrderByAsc(o => o.Order)
    //                                    .ToList();

    //var listTemplateTablet = SysTemplateService.Instance.CreateQuery()
    //                                    .Where(o => o.LangID == model.LangID && o.Device == 2)
    //                                    .OrderByAsc(o => o.Order)
    //                                    .ToList();
    var listModule = VSW.Lib.Web.Application.Modules.Where(o => o.IsControl == false && o.Activity == true).OrderBy(o => o.Order).ToList();

    var listParent = VSW.Lib.Global.ListItem.List.GetList(SysPageService.Instance, model.LangID);

    if (model.RecordID > 0)
    {
        //loai bo danh muc con cua danh muc hien tai

        listParent = VSW.Lib.Global.ListItem.List.GetListForEdit(listParent, model.RecordID);
    }

    var parent = item.ID > 0 ? SysPageService.Instance.GetByID_Cache(item.ParentID) : null;

    var listBrand = ModBrandService.Instance.GetByLang(model.LangID);
%>
<form id="vswForm" name="vswForm" method="post">
    <input type="hidden" id="_vsw_action" name="_vsw_action" />
    <div class="page-content-wrapper">
        <h3 class="page-title">Trang <small><%= model.RecordID > 0 ? "Chỉnh sửa": "Thêm mới"%></small></h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">Trang</a>
                </li>
            </ul>
            <div class="page-toolbar">
                <div class="btn-group">
                    <a href="/" class="btn green" target="_blank"><i class="icon-screen-desktop"></i>Xem Website</a>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <%= ShowMessage()%>

                <div class="form-horizontal form-row-seperated">
                    <div class="portlet">
                        <div class="portlet-title">
                            <div class="caption"></div>
                            <div class="actions btn-set">
                                <%= GetDefaultAddCommand()%>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-8">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Thông tin chung </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Tên trang:</label>
                                                    <div class="col-md-9">
                                                        <div class="form-inline">
                                                            <input type="text" class="form-control title" name="Name" id="Name" value="<%=item.Name %>" />
                                                            <span class="help-block text-primary">Ký tự còn lại: 200</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">URL trình duyệt:</label>
                                                    <div class="col-md-9">
                                                        <div class="form-inline">
                                                            <input type="text" class="form-control" name="Code" value="<%=item.Code %>" placeholder="Nếu không nhập sẽ tự sinh theo Tiêu đề" />
                                                            <span class="help-block text-primary"><a href="<%=VSW.Core.Web.HttpRequest.Domain %>/<%=item.Code %><%=Setting.Sys_PageExt %>" target="_blank">Xem trên web</a></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%if (model.ParentID > 0)
                                                    {%>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Mãu giao diện:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control" name="TemplateID">
                                                            <option value="0"></option>
                                                            <%for (var i = 0; listTemplate != null && i < listTemplate.Count; i++)
                                                                { %>
                                                            <option <%if (item.TemplateID == listTemplate[i].ID)
                                                                {%>selected<%} %>
                                                                value="<%= listTemplate[i].ID%>">&nbsp; <%= listTemplate[i].Name%></option>
                                                            <%} %>
                                                        </select>
                                                    </div>
                                                </div>
                                                <%--<div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Mãu giao diện Mobile:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control" name="TemplateMobileID">
                                                            <option value="0"></option>
                                                             <%for (var i = 0; listTemplateMobile != null && i < listTemplateMobile.Count; i++){ %>
                                                            <option <%if (item.TemplateMobileID == listTemplateMobile[i].ID){%>selected<%} %> value="<%= listTemplateMobile[i].ID%>">&nbsp; <%= listTemplateMobile[i].Name%></option>
                                                            <%} %>
                                                        </select>
                                                    </div>
                                                </div>--%>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Chức năng:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control" name="ModuleCode" id="ModuleCode" onchange="page_control_change(this.value)">
                                                            <option value="0"></option>

                                                            <%for (var i = 0; i < listModule.Count; i++)
                                                                { %>
                                                            <option <%if (item.ModuleCode == listModule[i].Code)
                                                                {%>selected<%}

                                                                %>
                                                                value="<%= listModule[i].Code%>">&nbsp; <%= listModule[i].Name%>
                                                            </option>

                                                            <%}%>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div id="control_param"></div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Chuyên mục:</label>
                                                    <div class="col-md-9">
                                                        <div id="list_menu"></div>
                                                    </div>
                                                </div>
                                                <%--  <%if(item.GetParent().ModuleCode == "MBrand") {%>--%>

<%--                                                <div class="form-group row brand-block" style="display: none;">
                                                    <label class="col-md-3 col-form-label text-right">Liên kết Thương hiệu:</label>
                                                    <div class="col-md-9">
                                                        <div id="list_brand"></div>
                                                    </div>
                                                </div>--%>


                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Thương hiệu:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control" name="BrandID" id="BrandID">
                                                            <option value="0">Root</option>
                                                            <%= Utils.ShowDdlMenuByType2("Brand", model.LangID, item.BrandID)%>
                                                        </select>
                                                    </div>
                                                </div>



                                                <%--    <%} %>--%>

                                                <%} %>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Trang cha:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control" name="ParentID">
                                                            <option value="0">Root</option>
                                                            <%for (var i = 0; listParent != null && i < listParent.Count; i++)
                                                                { %>
                                                            <option <%if (item.ParentID.ToString() == listParent[i].Value)
                                                                {%>selected<%} %>
                                                                value="<%= listParent[i].Value%>">&nbsp; <%= listParent[i].Name%></option>
                                                            <%} %>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Mã thiết kế:</label>
                                                    <div class="col-md-9">
                                                        <textarea class="form-control" rows="5" name="Custom" id="Custom" placeholder=""><%=item.Custom%></textarea>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Mô tả:</label>
                                                    <div class="col-md-9">
                                                        <textarea class="form-control" rows="5" name="Summary" id="Summary" placeholder=""><%=item.Summary%></textarea>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-12 col-form-label text-right">NỘI DUNG</label>
                                                    <div class="col-md-12">
                                                        <textarea class="form-control ckeditor" name="Content" id="Content" rows="" cols=""><%=item.Content %></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-4">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Thuộc tính</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">

                                                <div class="form-group">
                                                    <%if (!string.IsNullOrEmpty(item.File))
                                                        { %>
                                                    <p class="preview "><%= Utils.GetMedia(item.File, 80, 80)%></p>
                                                    <%}
                                                        else
                                                        { %>
                                                    <p class="preview">
                                                        <img src="" width="80" height="80" />
                                                    </p>
                                                    <%} %>

                                                    <label class="portlet-title-sub">Ảnh đại diện:</label>
                                                    <div class="form-inline">
                                                        <input type="text" class="form-control" name="File" id="File" value="<%=item.File %>" />
                                                        <button type="button" class="btn btn-primary" onclick="ShowFileForm('File'); return false">Chọn ảnh</button>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <%if (!string.IsNullOrEmpty(item.FileBrand))
                                                        { %>
                                                    <p class="preview "><img src="<%=item.FileBrand.Replace("~/","/") %>"/></p>
                                                    <%}
                                                        else
                                                        { %>
                                                    <p class="preview">
                                                        <img src="" width="80" height="80" />
                                                    </p>
                                                    <%} %>

                                                    <label class="portlet-title-sub">Ảnh cho page thương hiệu:</label>
                                                    <div class="form-inline">
                                                        <input type="text" class="form-control" name="FileBrand" id="FileBrand" value="<%=item.FileBrand %>" />
                                                        <button type="button" class="btn btn-primary" onclick="ShowFileForm('FileBrand'); return false">Chọn ảnh</button>
                                                    </div>
                                                </div>


                                                <%--  
                                                <div class="form-group">
                                                    <%if (!string.IsNullOrEmpty(item.Icon)){ %>
                                                    <p class="preview "><%= Utils.GetMedia(item.Icon, 80, 80)%></p>
                                                    <%}else{ %>
                                                    <p class="preview">
                                                        <img src="" width="80" height="80" /></p>
                                                    <%} %>

                                                    <label class="portlet-title-sub">icon</label>
                                                    <div class="form-inline">
                                                        <input type="text" class="form-control" name="Icon" id="Icon" value="<%=item.Icon %>" />
                                                        <button type="button" class="btn btn-primary" onclick="ShowFileForm('Icon'); return false">Chọn icon</button>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <%if (!string.IsNullOrEmpty(item.ImgBaner)){ %>
                                                    <p class="preview "><%= Utils.GetMedia(item.ImgBaner, 80, 80)%></p>
                                                    <%}else{ %>
                                                    <p class="preview">
                                                        <img src="" width="80" height="80" /></p>
                                                    <%} %>

                                                    <label class="portlet-title-sub">ảnh baner</label>
                                                    <div class="form-inline">
                                                        <input type="text" class="form-control" name="ImgBaner" id="ImgBaner" value="<%=item.ImgBaner %>" />
                                                        <button type="button" class="btn btn-primary" onclick="ShowFileForm('ImgBaner'); return false">Chọn ảnh</button>
                                                    </div>
                                                </div>--%>
                                                <%--<div class="form-group">
                                                    <label class="portlet-title-sub">Vị trí</label>
                                                    <div class="checkbox-list">
                                                        <%= Utils.ShowCheckBoxByConfigkey("Mod.SysPageState","ArrState", item.State)%>
                                                    </div>
                                                </div>--%>


                                                <%if (CPViewPage.UserPermissions.Approve)
                                                    {%>
                                                <div class="form-group">
                                                    <label class="portlet-title-sub">Duyệt</label>
                                                    <div class="radio-list">
                                                        <label class="radioPure radio-inline">
                                                            <input type="radio" name="Activity" <%= item.Activity ? "checked": "" %> value="1" />
                                                            <span class="outer"><span class="inner"></span></span><i>Có</i>
                                                        </label>
                                                        <label class="radioPure radio-inline">
                                                            <input type="radio" name="Activity" <%= !item.Activity ? "checked": "" %> value="0" />
                                                            <span class="outer"><span class="inner"></span></span><i>Không</i>
                                                        </label>
                                                    </div>
                                                </div>
                                                <%} %>
                                                <div class="form-group">
                                                    <label class="portlet-title-sub">Hiên trang chủ</label>
                                                    <div class="radio-list">
                                                        <label class="radioPure radio-inline">
                                                            <input type="radio" name="ChooseMenu" <%= item.ChooseMenu ? "checked": "" %> value="1" />
                                                            <span class="outer"><span class="inner"></span></span><i>Có</i>
                                                        </label>
                                                        <label class="radioPure radio-inline">
                                                            <input type="radio" name="ChooseMenu" <%= !item.ChooseMenu ? "checked": "" %> value="0" />
                                                            <span class="outer"><span class="inner"></span></span><i>Không</i>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">SEO</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-form-label">PageTitle:</label>
                                                    <input type="text" class="form-control title" name="PageTitle" value="<%=item.PageTitle %>" />
                                                    <span class="help-block text-primary">Ký tự còn lại: 200</span>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-form-label">PageHeading (H1):</label>
                                                    <input type="text" class="form-control title" name="PageHeading" value="<%=item.PageHeading %>" />
                                                    <span class="help-block text-primary">Ký tự còn lại: 200</span>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-form-label">Description:</label>
                                                    <textarea class="form-control description" rows="5" name="PageDescription" placeholder="Nhập nội dung tóm tắt. Tối đa 400 ký tự"><%=item.PageDescription%></textarea>
                                                    <span class="help-block text-primary">Ký tự còn lại: 400</span>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-form-label">Keywords:</label>
                                                    <input type="text" class="form-control" name="PageKeywords" value="<%=item.PageKeywords %>" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-form-label">PageCanonical:</label>
                                                    <input type="text" class="form-control title" name="PageCanonical" value="<%=item.PageCanonical %>" />
                                                    <span class="help-block text-primary">Ký tự còn lại: 200</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function menu_change(name) {
            var txtPageName = document.getElementById('Name');
            if (txtPageName.value === '') {
                var i = name.indexOf('---- ');
                if (i > -1)
                    txtPageName.value = name.substr(i + 5);
            }
        }
        function page_control_change(controlID) {
            var ranNum = Math.floor(Math.random() * 999999);

            var dataString = 'LangID=<%= model.LangID%>&PageID=<%=item.ID %>&ModuleCode=' + controlID + '&rnd=' + ranNum;
            $.ajax({
                url: '/{CPPath}/Ajax/PageGetControl.aspx',
                type: 'get',
                data: dataString,
                dataType: 'json',
                success: function (data) {
                    var listParam = data.Node1;
                    var listMenu = data.Node2;
                    var listBrand = data.Node3;
                    listMenu = '<select class="form-control select2" name="MenuID" onchange="menu_change(this.options[this.selectedIndex].text)"><option value="0">Root</option>' + listMenu + '</select>';
                    listBrand = '<select class="form-control select2" name="BrandID"><option value="0">Root</option>' + listBrand + '</select>';
                    $('#control_param').html(listParam);
                    $('#list_menu').html(listMenu);

                    $('#list_brand').html(listBrand);

                    if (controlID != 'MProduct')
                        $('.brand-block').hide();
                    else
                        $('.brand-block').show();
                    $('.select2').select2({
                        theme: 'bootstrap'
                    }).on('select2:opening', function () {
                        $(this).data('select2').$dropdown.find(':input.select2-search__field').attr('placeholder', 'Nhập từ khóa để tìm kiếm')
                    })
                    //window.setTimeout('CKEditorInstance()', 100);
                },
                error: function () { }
            });
        }

        if ('<%=item.ModuleCode%>' !== '') page_control_change('<%= item.ModuleCode %>');

        else if ($('#ModuleCode').val() !== '') page_control_change($('#ModuleCode').val());
    </script>
</form>
