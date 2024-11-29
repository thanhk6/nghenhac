<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var listItem = ViewBag.Data as List<SysPageEntity>;
    if (listItem == null) return;
%>

<div class="tile-hea01 all">
    <h2><a class="red" href="<%=ViewPage.GetURL(ViewPage.CurrentPage.Code) %>"><%=ViewPage.CurrentPage.Name %></a></h2>
</div>
<div class="all mb15">
    <div class="lf-cate fl" style="width: 100%;">
        <ul class="list_item_rss all">
             <%for (int i = 0; listItem != null && i < listItem.Count; i++)
          {
        %>
            <li><a href="<%=ViewPage.GetURL("rss/"+listItem[i].Code) %>"><%=listItem[i].Name %> </a>               
                <a class="fl icon_responsive ico_respone_rss" href="<%=ViewPage.GetURL("rss/"+listItem[i].Code) %>">&nbsp;</a></li>
            <%} %>
        </ul>
    </div>
</div>
<div style="clear:both"></div>
