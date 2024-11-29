<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    //if (ViewPage.ViewBag.Data != null || (ViewPage.Request.RawUrl != "/" && ViewPage.CurrentModule.Code != "MBrand" && ViewPage.CurrentModule.Code != "MUniform"))
    //    return;
    var listItem = ViewBag.Data as List<ModAdvEntity>;
%>
<section class="sec-banner-page">
    <div class="container">
        <div class="wp-banenr-page">
            <div id="banner-page" class="owl-carousel owl-theme">
                <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                    {
                        string img = !string.IsNullOrEmpty(listItem[i].File) ? listItem[i].File.Replace("~/", "/") : "";
                %>
                <div class="item">
                    <div class="wp-img-slider-main">
                        <a href="<%=listItem[i].URL %>" title="">
                            <img src="<%=Utils.GetResizeFile(img,4,500,500)%>"></a>
                    </div>
                </div>
                <%} %>
            </div>
        </div>
    </div>
</section>
