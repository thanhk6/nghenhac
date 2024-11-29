<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 

    //if (ViewPage.ViewBag.Data != null || (ViewPage.Request.RawUrl != "/" && ViewPage.CurrentModule.Code != "MBrand" && ViewPage.CurrentModule.Code != "MUniform"))
    //    return;
    var item = ModAdvService.Instance.CreateQuery()
                                        .Where(o => o.Activity == true && o.MenuID == 334)
                                        .OrderByAsc(o => new { o.Order, o.ID })
                                        .Take(1)
                                        .ToSingle_Cache();


    var itemHotToDay = ModAdvService.Instance.CreateQuery()
                                      .Where(o => o.Activity == true && o.MenuID == 335)
                                      .OrderByAsc(o => new { o.Order, o.ID })
                                      .Take(1)
                                      .ToSingle_Cache();
%>
<style type="text/css">
    .title-clock {
        background: url(<%=itemHotToDay.File.Replace("~/","/")%>);
   }
</style>
<%--<div class="er-banner-top">
    <a class="banner-link" href="#" title="banner-top">
        <img src="<%=item.File.Replace("~/","/") %>" alt="<%=item.File.Replace("~/","/") %>"></a><span class="close_top_banner">✕</span>
</div>--%>
