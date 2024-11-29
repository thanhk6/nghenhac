<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModMp3Model;
    var item = ViewBag.Data as ModMp3Entity;
    var listFile = item.GetFile();
    // var listGift = item.GetGift();
    //var listBrand = ModAuthorService.Instance.GetByLang(model.LangID);
%>
<form id="vswForm" name="vswForm" method="post">
    <input type="hidden" id="_vsw_action" name="_vsw_action" />
    <input type="hidden" id="RecordID" value="<%=model.RecordID %>" />
    <div class="page-content-wrapper">
        <h3 class="page-title">mp3 <small><%= model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></small></h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">Mp3</a>
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
                                                    <label class="col-md-3 col-form-label text-right">Tên bài hát:</label>
                                                    <div class="col-md-9">
                                                        <div class="form-inline">
                                                            <input type="text" class="form-control title" name="Name" id="Name" value="<%=item.Name %>" />
                                                            <span class="help-block text-primary">Ký tự còn lại: 200</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">URL trình duyệt:</label>
                                                    <div class="col-md-9">
                                                        <div class="form-inline">
                                                            <input type="text" class="form-control" name="Code" value="<%=item.Code %>" placeholder="Nếu không nhập sẽ tự sinh theo Tiêu đề" />
                                                            <span class="help-block text-primary"><a href="<%=VSW.Core.Web.HttpRequest.Domain %>/<%=item.Code %><%=Setting.Sys_PageExt %>" target="_blank">Xem trên web</a></span>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Thể loại:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control select2" name="MenuID" id="MenuID">
                                                            <option value="0">Root</option>

                                                            <%= Utils.ShowDdlMenuByType("Mp3", model.LangID, item.MenuID)%>
                                                        </select>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Tác giả:</label>
                                                    <div class="col-md-9">
                                                        <select class="form-control select2" name="AuthorID" id="AuthorID">
                                                            <option value="0">Root</option>

                                                            <%= Utils.ShowDdlMenuByType("Author", model.LangID, item.AuthorID)%>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Thuộc tính lọc:</label>
                                                    <div class="col-md-9" id="list-property"></div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Mô Tả:</label>
                                                    <div class="col-md-9">
                                                        <textarea class="form-control description" rows="5" name="Summary" placeholder="Nhập nội dung tóm tắt. Tối đa 400 ký tự"><%=item.Summary%></textarea>
                                                        <span class="help-block text-primary">Ký tự đối ta:400</span>
                                                    </div>
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-4">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">THUỘC TÍNH</div>
                                        </div>

                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <%if (!string.IsNullOrEmpty(item.File))
                                                        { %>
                                                    <p class="preview ">
                                                        <img src=" <%=item.File.Replace("~/", "/")%>" />
                                                    </p>
                                                    <%}
                                                        else
                                                        { %>
                                                    <p class="preview">
                                                        <img src="" width="80" height="80" />
                                                    </p>
                                                    <%} %>

                                                    <label class="portlet-title-sub">Hình minh họa:</label>

                                                    <div class="form-inline">
                                                        <input type="text" class="form-control" name="File" id="File" value="<%=item.File%>" />
                                                        <button type="button" class="btn btn-primary" onclick="ShowFileForm('File'); return false">Chọn ảnh</button>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="portlet-title-sub">File mp3:</label>
                                                    <div class="form-inline">
                                                        <input type="text" class="form-control" name="FileMp3" id="FileMp3" value="<%=item.FileMp3%>" />
                                                        <button type="button" class="btn btn-primary" onclick="ShowFilemP3Form('FileMp3'); return false">Chọn ảnh</button>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="portlet-title-sub">Vị trí</label>
                                                    <div class="checkbox-list">
                                                        <%= Utils.ShowCheckBoxByConfigkey("Mod.Mp3State", "ArrState", item.State)%>
                                                    </div>
                                                </div>
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
                                                <div class="form-group">
                                                    <label class="col-form-label">Alt:</label>
                                                    <input type="text" class="form-control" name="Alt" value="<%=item.Alt %>" />
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
        //setTimeout(function () { vsw_exec_cmd('[autosave][<%=model.RecordID%>]') }, 30000);
        function GetProperties(MenuID) {
            var ranNum = Math.floor(Math.random() * 999999);
            var dataString = "MenuID=" + MenuID + "&LangID=<%=model.LangID %>&ProductID=<%=item.ID%>&rnd=" + ranNum;
            $.ajax({
                url: "/{CPPath}/Ajax/GetProperties.aspx",
                type: "get",
                data: dataString,
                dataType: 'json',
                success: function (data) {
                    var content = data.Node1;

                    $("#list-property").html(content);
                },
                error: function (status) { }
            });
        }
        if (<%=item.MenuID%> > 0) GetProperties('<%=item.MenuID%>');
        else
            GetProperties($('#MenuID').val());
    </script>
</form>
