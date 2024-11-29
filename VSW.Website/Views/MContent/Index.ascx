<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    string file = ViewPage.CurrentPage.File;
    if (string.IsNullOrEmpty(file))
    {
        var page = SysPageService.Instance.CreateQuery()
                            .Select(o => o.File)
                            .Where(o => o.ModuleCode == "MNews" && o.LangID == ViewPage.CurrentLang.ID)
                            .Take(1)
                            .ToSingle_Cache();
        if (page != null)
            file = page.File;
    }
%>
<%--<section class="banner-news-blog">
    <%if(!string.IsNullOrEmpty(file)) {%>
    <img src="<%=file.Replace("~/", "/") %>" alt="<%=ViewPage.CurrentPage.Name %>" />
    <%} %>
    <div class="title-banner-newsblog">
        <h1><%=ViewPage.CurrentPage.Name %></h1>
    </div>
</section>--%>
    <section class="news-detail" style=" margin-top: 30px; ">
        <div class="container">
            <div class="row">
                <%--<h1 class="mrcontent"><%=ViewPage.CurrentPage.Name %></h1>--%>
                <%-- <p class="time-news"><%=string.Format("{0:f}", ViewPage.CurrentPage.Updated) %></p>--%>

                <div class="content-news">
                    <%=Utils.GetHtmlForSeo(ViewPage.CurrentPage.Content)%>
                </div>
            </div>
        </div>
    </section>