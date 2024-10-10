<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModWebUserModel;
    var listItem = ViewBag.Data as List<ModWebUserEntity>;
%>

<form id="vswForm" name="vswForm" method="post">

    <input type="hidden" id="_vsw_action" name="_vsw_action" />
    <input type="hidden" id="boxchecked" name="boxchecked" value="0" />

    <div class="page-content-wrapper">
        <h3 class="page-title">Thành viên</h3>
        <div class="page-bar justify-content-between">
            <ul class="breadcrumb">
                <li class="breadcrumb-item">
                    <i class="fa fa-home"></i>
                    <a href="/{CPPath}/">Home</a>
                </li>
                <li class="breadcrumb-item">
                    <a href="/{CPPath}/<%=CPViewPage.CurrentModule.Code%>/Index.aspx">Thành viên</a>
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
                            <div class="row hidden-sm hidden-col">
                                <div class="col-12 col-sm-12 col-md-4 col-lg-4">
                                    <div class="dataTables_search">
                                        <input type="text" class="form-control input-inline input-sm" id="filter_search" value="<%= model.SearchText %>" placeholder="Nhập từ khóa cần tìm" onchange="VSWRedirect();return false;" />
                                        <button type="button" class="btn blue" onclick="VSWRedirect();return false;">Tìm kiếm</button>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 text-right pull-right">

                                    <div class="table-group-actions d-inline-block">
                                        <%= ShowDDLLang(model.LangID)%>
                                    </div>
                                </div>
                            </div>

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
                                            <%-- <th class="sorting title"><%= GetSortLink("Tên đăng nhập", "UserName")%></th>--%>
                                            <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortLink("Tên đăng nhập", "UserName")%></th>
                                            <%--<th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortLink("Ảnh", "File")%></th>--%>
                                            <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortLink("Email", "Email")%></th>
                                            <th class="sorting text-center w10p"><%= GetSortLink("Điện thoại", "Phone")%></th>
                                            <%--<th class="sorting text-center w10p"><%= GetSortLink("Tiền đã nạp", "Point")%></th>--%>
                                            <th class="sorting text-center w10p hidden-sm hidden-col"><%= GetSortLink("Ngày tạo", "Created")%></th>
                                            <th class="sorting text-center w1p"><%= GetSortLink("IP", "IP")%></th>
                                            <th class="sorting text-center w1p"><%= GetSortLink("ID", "ID")%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                                            { %>
                                        <tr>
                                            <td class="text-center"><%= i + 1%></td>
                                            <td class="text-center">
                                                <%= GetCheckbox(listItem[i].ID, i)%>
                                            </td>
                                             <td>
                                                <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)"><%= listItem[i].UserName%></a>
                                            </td>

                                         <%--   <td class="text-center hidden-sm hidden-col"><%= listItem[i].Name %></td>--%>
                                            <%--<td class="text-center hidden-sm hidden-col">
                                                <%= Utils.GetMedia(listItem[i].File, 40, 40)%>
                                            </td>--%>
                                            <td class="text-center hidden-sm hidden-col"><%= listItem[i].Email %></td>
                                            <td class="text-center"><%= listItem[i].Phone %></td>
                                            <%--<td class="text-center"><%= listItem[i].Point %> USD</td>--%>
                                            <td class="text-center hidden-sm hidden-col"><%= string.Format("{0:dd-MM-yyyy HH:mm}", listItem[i].Created) %></td>
                                            <td class="text-center hidden-sm hidden-col"><%= listItem[i].IP %></td>
                                            <td class="text-center"><%= listItem[i].ID%></td>
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

        var VSWController = "ModWebUser";

        var VSWArrVar = [
            "filter_lang", "LangID",
            "limit", "PageSize"
        ];

        var VSWArrVar_QS = [
            "filter_search", "SearchText"
        ];

        var VSWArrQT = [
            "<%= model.PageIndex + 1 %>", "PageIndex",
            "<%= model.Sort %>", "Sort"
        ];

        var VSWArrDefault = [
            "1", "PageIndex",
            "1", "LangID",
            "20", "PageSize"
        ];
    </script>

</form>
