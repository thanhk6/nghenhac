﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModNewsModel;
    var item = ViewBag.Data as ModNewsEntity;

    var listBrand = ModBrandService.Instance.GetByLang(model.LangID);
%>

<form id="vswForm" name="vswForm" method="post">
    <input type="hidden" id="_vsw_action" name="_vsw_action" />
    <div class="page-content-wrapper">
        <h3 class="page-title">Bài viết <small><%= model.RecordID > 0 ? "Chỉnh sửa": "Thêm mới"%></small></h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">Bài viết</a>
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
                                            <div class="caption">Thông tin chung</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Tiêu đề:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control title" name="Name" value="<%=item.Name %>" />
                                                        <span class="help-block text-primary">Ký tự đối ta: 200</span>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Xem trên web site</label>
                                                    <div class="col-md-9">
                                                        <a href="/<%=item.Code%>.html" style="color:blue;" target="_blank">/<%=item.Code%>.html"</a>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">URL trình duyệt:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Code" value="<%=item.Code %>" placeholder="Nếu không nhập sẽ tự sinh theo Tiêu đề" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Mô tả:</label>
                                                    <div class="col-md-9">
                                                        <textarea class="form-control description" rows="5" name="Summary" placeholder="Nhập nội dung tóm tắt. Tối đa 400 ký tự"><%=item.Summary%></textarea>
                                                        <span class="help-block text-primary">Ký tự đối ta: 400</span>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Chuyên mục:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control" name="MenuID" id="MenuID">
                                                            <option value="0">Root</option>
                                                            <%= Utils.ShowDdlMenuByType("News", model.LangID, item.MenuID)%>
                                                        </select>
                                                    </div>
                                                </div>
                                              <%--  <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Nhãn hiệu:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control select2" name="BrandID" id="BrandID">
                                                            <option value="0">Root</option>
                                                            <%for (int i = 0; listBrand != null && i < listBrand.Count; i++){%>
                                                            <option value="<%=listBrand[i].ID %>" <%if(item.BrandID == listBrand[i].ID) {%>selected="selected"<%} %>><%=listBrand[i].Name %></option>
                                                            <%} %>
                                                        </select>
                                                    </div>
                                                </div>--%>
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
                                                    <%if (!string.IsNullOrEmpty(item.File)){ %>
                                                    <p class="preview "> <%= Utils.GetMedia(item.File, 80, 80)%> </p>
                                                    <%}
                                                        else { %>
                                                    <p class="preview"><img src="" width="80" height="80" /></p>
                                                    <%} %>

                                                    <label class="portlet-title-sub">Hình minh họa:</label>
                                                    <div class="form-inline">
                                                        <input type="text" class="form-control" name="File" id="File" value="<%=item.File %>" />
                                                        <button type="button" class="btn btn-primary" onclick="ShowFileForm('File'); return false">Chọn ảnh</button>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="portlet-title-sub">Vị trí</label>
                                                    <div class="checkbox-list">
                                                        <%= Utils.ShowCheckBoxByConfigkey("Mod.NewsState", "ArrState", item.State)%>
                                                    </div>
                                                </div>

                                                <%if (CPViewPage.UserPermissions.Approve){%>
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

                                            </div>
                                        </div>
                                    </div>

                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">SEO</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group ">
                                                    <label class="col-form-label">PageTitle:</label>
                                                    <input type="text" class="form-control title" name="PageTitle" value="<%=item.PageTitle %>" />
                                                    <span class="help-block text-primary">Ký tự đối ta: 200</span>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-form-label">Description:</label>
                                                    <textarea class="form-control description" rows="5" name="PageDescription" placeholder="Nhập nội dung tóm tắt. Tối đa 400 ký tự"><%=item.PageDescription%></textarea>
                                                    <span class="help-block text-primary">Ký tự đối ta: 400</span>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-form-label">Keywords:</label>
                                                    <input type="text" class="form-control" name="PageKeywords" value="<%=item.PageKeywords %>" />
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
        //setTimeout(function () { vsw_exec_cmd('[autosave][<%=model.RecordID%>]') }, 5000);
    </script>

</form>