<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var item = ViewBag.Data as ModAdvEntity;
    if (item == null || string.IsNullOrEmpty(item.File)) return;
%>

<div class="col-xs-3 hidden-lg hidden-md hidden-sm">
    <div class="menu-mobile-beptot">
        <div id="trigger-mobile">
            <i class="fa fa-bars"></i>
        </div>
    </div>
</div>
<div class="col-md-2 col-xs-6 col-sm-12">
       <%if (ViewPage.Request.RawUrl == "/"){%><h1 class="logo-text"><%=ViewPage.CurrentPage.PageTitle %></h1><%} %>
    <div class="logo-bep68">
        <a href="<%=item.URL %>">
            <img src="<%=item.File.Replace("~/","/") %>" alt=""></a>
    </div>
</div>

