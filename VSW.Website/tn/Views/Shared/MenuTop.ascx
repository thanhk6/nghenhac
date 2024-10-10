<%@ Control Language="C#" AutoEventWireup="true" %>

<% 
    var listModule = VSW.Lib.Web.Application.CPModules.Where(o => o.Activity == true).OrderBy(o => o.Order).ToList();
%>

<div class="hor-menu nav-desktop">
    <ul class="navmenu">
        <li class="item active">
            <a href="/{CPPath}/">Home</a>
        </li>
        <li class="item">
            <a class="a-open-down"></a>
            <a href="javascript:;">{RS:MenuTop_Management} <i class="fa fa-angle-down"></i></a>
            <ul class="sub-menu">
                <%for (var i = 0; i < listModule.Count; i++){%>
                <li><a href="/{CPPath}/<%=listModule[i].Code%>/Index.aspx"><i class="fa <%=listModule[i].CssClass %>"></i><%=listModule[i].Name%></a></li>
                <%} %>
            </ul>
        </li>
        <li class="item">
            <a class="a-open-down"></a>
            <a href="javascript:void(0)">{RS:MenuTop_Design} <i class="fa fa-angle-down"></i></a>
            <ul class="sub-menu">
                <li><a href="/{CPPath}/SysPage/Index.aspx"><i class="fa fa-th"></i>{RS:MenuTop_Page}</a></li>
                <li><a href="/{CPPath}/SysTemplate/Index.aspx"><i class="fa fa-columns"></i>{RS:MenuTop_Template}</a></li>
                <li><a href="/{CPPath}/SysSite/Index.aspx"><i class="fa fa-sitemap"></i>Site</a></li>
            </ul>
        </li>
        <li class="item">
            <a class="a-open-down"></a>
            <a href="javascript:;">{RS:MenuTop_System} <i class="fa fa-angle-down"></i></a>
            <ul class="sub-menu">
                <li><a href="/{CPPath}/SysMenu/Index.aspx"><i class="fa fa-bars"></i>Chuyên mục</a></li>
                <li><a href="/{CPPath}/SysRedirection/Index.aspx"><i class="fa fa-flash"></i>Redicrect 301</a></li>
                <li><a href="/{CPPath}/SysProperty/Index.aspx"><i class="fa fa-filter"></i>Thuộc tính</a></li>
                <li><a href="/{CPPath}/SysRole/Index.aspx"><i class="fa fa-users"></i>Nhóm người sử dụng</a></li>
                <li><a href="/{CPPath}/SysUser/Index.aspx"><i class="fa fa-user"></i>Người sử dụng</a></li>
                <li><a href="/{CPPath}/SysResource/Index.aspx"><i class="fa fa-globe"></i>Tài nguyên</a></li>
                <li><a href="/{CPPath}/SysUserLog/Index.aspx"><i class="fa fa-calendar"></i>Nhật ký đăng nhập</a></li>
                <%--<li><a href="/{CPPath}/ModSearch/Index.aspx"><i class="fa fa-tasks"></i>Đánh chỉ mục</a></li>--%>
            </ul>
        </li>
    </ul>
</div>