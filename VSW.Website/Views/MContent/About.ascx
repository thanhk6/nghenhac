<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage.ID);
    string file = ViewPage.CurrentPage.File;
%>

<section id="banner-abouts">
    <%if (!string.IsNullOrEmpty(file)) {%>
    <img src="<%=file.Replace("~/", "/") %>" alt="<%=ViewPage.CurrentPage.Name %>" />
    <%} %>

    <div class="content-banner content-banner-intro">
        

        {RS:GioiThieuChung}


    </div>
    <a href="#contact" id="subnav" rel="nofollow"><img src="/Content/desktop/images/scroll1.png" alt="scroll" /></a>
</section>

<%if(listItem.Count > 0) {%>
<section class="row-page-intro">
    <h1>Tầm nhìn chiến lược</h1>
    <%for (var i = 0; i < listItem.Count; i++){
            if (string.IsNullOrEmpty(listItem[i].File)) continue;
    %>
    <div id="<%=listItem[i].Code %>" class="page-intro-box">
        <div class="page-intro-box-text wow <%= i % 2 == 0 ? "fl bounceInLeft" : "fr bounceInRight" %>" data-wow-duration="0.5s">
            <h2><%=listItem[i].Name %></h2>
            <%=Utils.GetHtmlForSeo(listItem[i].Content) %>
        </div>
        <div class="page-intro-box-img wow <%= i % 2 == 0 ? "fr bounceInRight" : "fl bounceInLeft" %>" data-wow-duration="0.5s" data-wow-delay="1s">
            <figure>
                <img src="<%=listItem[i].File.Replace("~/", "/") %>" alt="<%=listItem[i].Name %>" />
            </figure>
        </div>
    </div>
    <%} %>
</section>
<%} %>