<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var page = ViewBag.Page as SysPageEntity;
    var page2 = ViewBag.Page2 as SysPageEntity;

    if (page == null || page2 == null) return;

    var listItem = ViewBag.Data as List<ModNewsEntity>;
    if (listItem == null) return;

    var item = listItem[0];
%>

<div class="adv-newTop">
    <div class="flexbox advtit">
        <a href="<%=ViewPage.GetPageURL(page) %>" title="<%=page.Name %>"><%=page.Name %></a>
        <a href="/thuong-hieu-elica" title="Thương hiệu Elica">
            <span class="dot"><span class="ping"></span></span>
            <img style="width: 80px;" src="https://germankitchen.vn/Content/img/i/w/elica.png" alt="Elica">
        </a>
    </div>
    <ul class="newTopRight">
        <li>
            <a href="<%=ViewPage.GetURL(item.Code) %>">
                <div class="thumb-img">
                    <div class="hm-responsive">
                        <img src="<%=Utils.GetResizeFile(item.File, 4, 325, 325) %>" alt="<%=item.Name %>" />
                    </div>
                </div>
                <div class="title-newTop">
                    <h3 class="eclip-2"><%=item.Name %></h3>
                </div>
            </a>
        </li>
        <%for (var i = 1; listItem != null && i < listItem.Count; i++){%>
        <li>
            <a href="<%=ViewPage.GetURL(listItem[i].Code) %>" title="<%=listItem[i].Name %>"><%=listItem[i].Name %></a>
        </li>
        <%} %>
    </ul>
</div>