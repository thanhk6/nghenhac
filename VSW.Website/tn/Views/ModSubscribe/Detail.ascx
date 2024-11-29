<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModSubscribeModel;
    var item = ViewBag.data as ModSubscribeEntity;
%>
<form id="vswForm" name="vswForm" method="post">
    <input type="hidden" id="_vsw_action" name="_vsw_action" />
    <input type="hidden" id="RecordID" value="<%=model.RecordID %>" />
    <div class="page-content-wrapper">
        <h3 class="page-title">Sản phẩm <small><%= model.RecordID > 0 ? "Chỉnh sửa": "Thêm mới"%></small></h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">liên hệ</a>
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


                                <%--  <%= GetSortAddCommand()%>--%>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-9">
                                    <div class="portlet box blue-steel">
                                        <div class="portlet-title">
                                            <div class="caption">Khách hàng</div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Họ và tên:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Name" value="<%=item.Name %>" readonly="readonly" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Điện thoại:</label>
                                                    <div class="col-md-9">
                                                        <input type="text" class="form-control" name="Phone" value="<%=item.Phone %>" readonly="readonly" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Email</label>
                                                    <div class="col-md-9">
                                                        <label class="form-control"><%=item.Email %></label>
                                                    </div>

                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">tên công ty</label>
                                                    <div class="col-md-9">
                                                        <label class="form-control"><%=item.Company %></label>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right"> file danh sách sản phẩm đính kèm</label>
                                                    <div class="col-md-9">
                                                        <a href="<%=item.CvFile.Replace("~/","/") %>" target="_blank">Tải về</a>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-md-3 col-form-label text-right">Ghi chú:</label>
                                                    <div class="col-md-9">
                                                        <textarea class="form-control" rows="5" name="Content" readonly="readonly"><%=item.Content%></textarea>
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
    <%--<script type="text/javascript">
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
        else GetProperties($('#MenuID').val());
    </script>--%>
</form>
