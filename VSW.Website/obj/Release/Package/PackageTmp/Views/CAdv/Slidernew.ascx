<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    //if (ViewPage.ViewBag.Data != null || (ViewPage.Request.RawUrl != "/" && ViewPage.CurrentModule.Code != "MBrand" && ViewPage.CurrentModule.Code != "MUniform"))
    //    return;
    var listItem = ViewBag.Data as List<ModAdvEntity>;
%>
<section class="sec-01">
    <div class="container">
        <div class="banner_top">
            <div class="slider-top">
                <div class="wp-banner-main-tab">
                    <div id="banner-home-tab" class="owl-carousel owl-theme">
                        <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                            { %>
                        <div class="item">
                            <div class="wp-img-slider-main">
                                <div class="banner-hover">
                                    <a href="<%=listItem[i].URL %>" title=" <%=listItem[i].Name %>">
                                        <img src="<%=listItem[i].File.Replace("~/","") %>" alt="<%=listItem[i].Name %>"></a>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
