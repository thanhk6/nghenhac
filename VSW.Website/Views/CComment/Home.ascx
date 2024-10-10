<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    //var page = ViewBag.Page as SysPageEntity;
    //if (page == null) return;
    
    var listItem = ViewBag.Data as List<ModCommentEntity>;
    string title = ViewBag.Title;
%>

<h2><%=title %></h2>
<ul class="customer" id="list-customer">
    <%for (int i = 0; listItem != null && i < listItem.Count; i++){%>
    <li>
        <img src="<%=Utils.GetResizeFile(listItem[i].File, 2, 100, 0) %>" width="100" alt="<%=listItem[i].Name%>" />
        <h3><%=listItem[i].Name%></h3>
        <div class="description"><%=listItem[i].Summary%></div>
        <div class="content"><%=listItem[i].Content%></div>
    </li>
    <%} %>    
</ul>