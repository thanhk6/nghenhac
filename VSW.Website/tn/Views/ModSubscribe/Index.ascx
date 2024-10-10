<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModSubscribeModel;
    var listItem = ViewBag.Data as List<ModSubscribeEntity>;
%>

<form id="vswForm" name="vswForm" method="post">

    <input type="hidden" id="_vsw_action" name="_vsw_action" />
    <input type="hidden" id="boxchecked" name="boxchecked" value="0" />

    <div class="page-content-wrapper">
        <h3 class="page-title">Đăng ký tư vấn</h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">Đăng ký tư vấn</a>
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
                <div class="portlet portlet">
                    <div class="portlet-title">
                        <div class="actions btn-set">
                            <%=GetTinyListCommand()%>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="dataTables_wrapper">                            
                            <div class="table-scrollable">
                                <table class="table table-striped table-hover table-bordered dataTable">
                                    <thead>
                                        <tr>
                                            <th class="sorting text-center w1p">#</th>
                                            <th class="sorting_disabled text-center w1p">
                                                <label class="itemCheckBox itemCheckBox-sm">
                                                    <input type="checkbox" name="toggle" onclick="checkAll(<%= model.PageSize %>)">
                                                    <i class="check-box"></i>
                                                </label>
                                            </th>
                                            <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortLink("tên","Name")%></th>
                                           
                                             <th class="sorting"><%= GetSortLink("sô điên thoại", "Phone")%></th>
                                             <th class="sorting"><%= GetSortLink("Email", "Email")%></th>
                                                     <th class="sorting"><%= GetSortLink("tên công ty", " Company")%></th>
                                            <th class="sorting"><%= GetSortLink("giới tinh", "Gender")%></th>
                                            <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortLink("Ngày gửi", "Created")%></th>
                                            <th class="sorting text-center w1p"><%= GetSortLink("ID", "ID")%></th>
                                            <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortLink("IP", "IP")%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%for (var i = 0; listItem != null && i < listItem.Count; i++){ %>
                                        <tr>
                                            <td class="text-center"><%= i + 1%></td>
                                            <td class="text-center">
                                                <%= GetCheckbox(listItem[i].ID, i)%>
                                            </td>
                                          <td>
                                                <a href="javascript:VSWRedirect('Detail', <%= listItem[i].ID %>)"><%= listItem[i].Name%></a>
                                                <%--<p class="smallsub hidden-sm hidden-col">(<span>Mã</span>: <%= listItem[i].Code%>)</p>--%>
                                            </td>
                                            
                                            <td class="text-center"><%= listItem[i].Phone%></td>
                                            <td class="text-center"><%= listItem[i].Email%></td>
                                            <td class="text-center"><%= listItem[i].Company%></td>
                                            <td class="text-center"><%= listItem[i].Gender%></td>
                                            <td class="text-center hidden-sm hidden-col"><%= string.Format("{0:dd-MM-yyyy HH:mm}", listItem[i].Created) %></td>
                                            <td class="text-center"><%= listItem[i].ID%></td>
                                             <td class="text-center hidden-sm hidden-col"><%= listItem[i].IP%></td>
                                        </tr>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 justify-content-center d-flex ">
                                    <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord)%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        var VSWController = "ModSubscribe";

        var VSWArrVar = [
            "limit", "PageSize"
        ];

        var VSWArrQT = [
            "<%= model.PageIndex + 1 %>", "PageIndex",
            "<%= model.Sort %>", "Sort"
        ];

        var VSWArrDefault = [
            "1", "PageIndex",
            "20", "PageSize"
        ];
    </script>

</form>