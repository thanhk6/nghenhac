<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    //if (ViewPage.ViewBag.Data != null || (ViewPage.Request.RawUrl != "/" && ViewPage.CurrentModule.Code != "MBrand" && ViewPage.CurrentModule.Code != "MUniform"))
    //    return;

    var listItem = ViewBag.Data as List<ModAdvEntity>;
%>
<div class="wp-ads1 hidden-sm hidden-xs">
    <div class="container">
        <div class="banner-bottom-slide">
            <ul>
                <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                    { %>
                <li><a href="#" title="" target="_blank" class="img-cover">
                    <img src="<%=listItem[i].File.Replace("~/", "/") %>" alt=""></a></li>
                <%} %>
            </ul>
        </div>
    </div>
</div>
