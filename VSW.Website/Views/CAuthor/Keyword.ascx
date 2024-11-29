<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<ModBrandEntity>;
%>
<section id="footer-top">
    <p>
        {RS:Web_Slogan}
        <%for (var i = 0; listItem != null && i < listItem.Count; i++){%>
        <a href="<%=ViewPage.GetURL(listItem[i].Code) %>"><%=listItem[i].Name %></a><%=i < listItem.Count - 1 ? ", " : string.Empty %>
        <%} %>
    </p>
</section>