<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var page = ViewBag.Page as SysPageEntity;
    if (page == null) return;

    var listItem = ViewBag.Data as List<ModNewsEntity>;
%>

<section class="newBottom">
    <h3 class="title-bx-main"><%=page.Name %></h3>
    <ul class="tt home-new">
        <%for (var i = 0; listItem != null && i < listItem.Count; i++){%>
        <li>
            <a href="<%=ViewPage.GetURL(listItem[i].Code) %>" class="ttimg" title="<%=listItem[i].Name %>">
                <img src="<%=Utils.GetResizeFile(listItem[i].File, 4, 325, 325) %>" alt="<%=listItem[i].Name %>" />
            </a>
            <h3 class="eclip-2"><a href="<%=ViewPage.GetURL(listItem[i].Code) %>" title="<%=listItem[i].Name %>"><%=listItem[i].Name %></a></h3>
            <div><span><%=string.Format("{0:f}", listItem[i].Published) %></span> | <span><%=string.Format("{0:#,##0}", listItem[i].View) %> lượt xem</span></div>
        </li>
        <%} %>
    </ul>
</section>