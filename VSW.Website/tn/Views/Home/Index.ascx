<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<% 
    var listModule = VSW.Lib.Web.Application.CPModules.Where(o => o.Activity == true).OrderBy(o => o.Order).ToList();

    //logs
    var listUserLog = CPUserLogService.Instance.CreateQuery().Take(10).OrderByDesc(o => o.ID).ToList_Cache();

    //bai viet


    //san pham
    var listProduct = ModMp3Service.Instance.CreateQuery().Take(10).OrderByDesc(o => o.Order).ToList_Cache();
    var listProductView = ModMp3Service.Instance.CreateQuery().Take(10).OrderByDesc(o => o.View).ToList_Cache();

    //quang cao/lien ket
    int TotalProduct = ModMp3Service.Instance.CreateQuery().Select(o => o.ID).Count().ToValue_Cache().ToInt(0);

    int TotalAdv = ModAdvService.Instance.CreateQuery().Select(o => o.ID).Count().ToValue_Cache().ToInt(0);
%>

<%= ShowMessage()%>

<div class="page-content-wrapper">
    <div class="page-bar justify-content-between">
        <ul class="breadcrumb">
            <li class="breadcrumb-item">
                <i class="fa fa-home"></i>
                <a href="/{CPPath}/">Home</a>
            </li>
        </ul>
        <div class="page-toolbar">
            <div class="btn-group">
                <a href="/" class="btn green" target="_blank"><i class="icon-screen-desktop"></i>Xem Website</a>
            </div>
        </div>
    </div>

    <div class="row">
        <%if (listModule.Find(o => o.Code == "ModProduct") != null)
            {%>
        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
            <div class="dashboard-stat blue-madison">
                <div class="visual">
                    <i class="fa fa-cubes"></i>
                </div>
                <div class="details">
                    <div class="number"><%=TotalProduct %></div>
                    <div class="desc">Danh sách nhạc</div>
                </div>
                <a href="/{CPPath}/ModProduct/Index.aspx" class="more">Xem thêm<i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <%} %>


        <%if (listModule.Find(o => o.Code == "ModAdv") != null)
            {%>
        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
            <div class="dashboard-stat green-haze">
                <div class="visual">
                    <i class="fa fa-money"></i>
                </div>
                <div class="details">
                    <div class="number"><%=TotalAdv %></div>
                    <div class="desc">Quảng cáo - Liên kết</div>
                </div>
                <a href="/{CPPath}/ModAdv/Index.aspx" class="more">Xem thêm<i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <%} %>
    </div>
    <div class="clear"></div>

    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
            <div class="dashboard-stat blue-madison">
                <div class="visual">
                    <i class="fa fa-bars"></i>
                </div>
                <div class="details">
                    <div class="number"><a href="/{CPPath}/SysMenu/Index.aspx">Chuyên mục</a></div>
                </div>
                <a href="/{CPPath}/SysMenu/Index.aspx" class="more">Xem thêm<i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
            <div class="dashboard-stat red-intense">
                <div class="visual">
                    <i class="fa fa-columns"></i>
                </div>
                <div class="details">
                    <div class="number"><a href="/{CPPath}/SysPage/Index.aspx">Trang</a></div>
                </div>
                <a href="/{CPPath}/SysPage/Index.aspx" class="more">Xem thêm<i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
            <div class="dashboard-stat purple-plum">
                <div class="visual">
                    <i class="fa fa-user"></i>
                </div>
                <div class="details">
                    <div class="number"><a href="/{CPPath}/SysUser/Index.aspx">Người sử dụng</a></div>
                </div>
                <a href="/{CPPath}/SysUser/Index.aspx" class="more">Xem thêm<i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
            <div class="dashboard-stat green-haze">
                <div class="visual">
                    <i class="fa fa-globe"></i>
                </div>
                <div class="details">
                    <div class="number"><a href="/{CPPath}/SysResource/Index.aspx">Tài nguyên</a></div>
                </div>
                <a href="/{CPPath}/SysResource/Index.aspx" class="more">Xem thêm<i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
    </div>
    <div class="clear"></div>

  
    <div class="clear"></div>

    <div class="row">
        <%--box bài viết--%>
        
        <%--end box bài viết--%>

        <%--box đăng nhập--%>
        <div class="col-md-6 col-sm-12">
            <div class="portlet box green-haze box-logs">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-sign-in"></i>Đăng nhập gần nhất
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-responsive">
                        <table class="table table-hover ">
                            <thead>
                                <tr>
                                    <th>Tài khoản</th>
                                    <th>Ghi chú</th>
                                    <th>IP</th>
                                    <th>Ngày</th>
                                    <th>ID</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%for (var i = 0; listUserLog != null && i < listUserLog.Count; i++)
                                    { %>
                                <tr>
                                    <td><%= listUserLog[i].GetUser().LoginName%></td>
                                    <td><%= listUserLog[i].Note%></td>
                                    <td><%= listUserLog[i].IP%></td>
                                    <td><%= string.Format("{0:dd-MM-yyyy HH:mm}", listUserLog[i].Created)%></td>
                                    <td><%= listUserLog[i].ID%></td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <%--end box đăng nhập--%>
    </div>
    <div class="clear"></div>
</div>
