<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModCommentModel;
    var item = ViewBag.Data as ModCommentEntity;
%>

<form id="vswForm" name="vswForm" method="post">
    <input type="hidden" id="_vsw_action" name="_vsw_action" />

    <div class="page-content-wrapper">
        <h3 class="page-title">Ý kiến khách hàng <small><%= model.RecordID > 0 ? "Chỉnh sửa": "Thêm mới"%></small></h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">Ý kiến khách hàng</a>
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
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Thông tin chung</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Tên khách:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control title" name="Name" value="<%=item.Name %>" />
                                                        <span class="help-block text-primary">Ký tự đối ta: 200</span>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <%if (!string.IsNullOrEmpty(item.File)){ %>
                                                    <p class="preview "><%= Utils.GetMedia(item.File, 80, 80)%></p>
                                                    <%}else{ %>
                                                    <p class="preview"><img src="" width="80" height="80" /></p>
                                                    <%} %>

                                                    <label class="col-md-3 col-form-label text-right">Hình minh họa:</label>
                                                    <div class="col-md-9">
                                                        <div class="form-inline">
                                                            <input type="text" class="form-control" name="File" id="File" value="<%=item.File %>" />
                                                            <button type="button" class="btn btn-primary" onclick="ShowFileForm('File'); return false">Chọn ảnh</button>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="form-group row">
                                                    <label class="col-md-12 col-form-label text-right">NỘI DUNG BÌNH LUẬN</label>
                                                    <div class="col-md-12">
                                                        <textarea class="form-control ckeditor" name="Content" id="Content" rows="" cols=""><%=item.Content %></textarea>
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
    </div>

    <script type="text/javascript">
        //setTimeout(function () { vsw_exec_cmd('[autosave][<%=model.RecordID%>]') }, 5000);
    </script>

</form>
