<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    //if (ViewPage.ViewBag.Data != null || (ViewPage.Request.RawUrl != "/" && ViewPage.CurrentModule.Code != "MBrand" && ViewPage.CurrentModule.Code != "MUniform"))
    //    return;
    var listItem = ViewBag.Data as List<ModAdvEntity>;
%>

<!-- end sec bread -->
<section class="sec-banner-page banner_page--sp">
    <div class="container">
        <div class="wp-banenr-page">
            <div id="banner-page" class="owl-carousel owl-theme">
                <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                    { %>

                <div class="item">
                    <div class="wp-img-slider-main">
                        <a href="#" title="<%=listItem[i].Name %>">
                            <img src="<%=Utils.GetResizeFile(listItem[i].File, 4, 500, 500) %>"></a>
                    </div>
                </div>              
                <%} %>               
            </div>
        </div>
    </div>
</section>
