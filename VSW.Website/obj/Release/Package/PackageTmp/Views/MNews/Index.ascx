<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var listItem = ViewBag.Data as List<ModNewsEntity>;
    var model = ViewBag.Model as MNewsModel;
    var brand = ViewPage.ViewBag.Brand as ModBrandEntity;
    var listBrand = ModBrandService.Instance.GetByLang(ViewPage.CurrentLang.ID);
    var listPage = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage.ID);
    if (listPage.Count < 1)
        listPage = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage.ParentID);
    string file = ViewPage.CurrentPage.File;
    if (string.IsNullOrEmpty(file))
        file = ViewPage.CurrentPage.GetParent().File;
%>
<h1 class="logo-text"><%=ViewPage.CurrentPage.PageTitle %></h1>
<div class="beadcrumb-tuyen">
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <ol vocab="https://schema.org/" typeof="BreadcrumbList" class="breadcrumb">
                    <%= Utils.GetMapPage(ViewPage.CurrentPage) %>
                </ol>
            </div>
        </div>
    </div>
</div>
<main id="main-site" class="main_new_HHT">
    <div class="back_page_KN">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <a href="#" onclick="javascript:window.history.back()" ><i class="fa fa-angle-double-left" aria-hidden="true"></i>Quay lại</a>
                </div>
            </div>
        </div>
    </div>
    <div class="title_news_01">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h2><%=ViewPage.CurrentPage.Name %>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <!-- title_news -->
    <div class="list_news_01">
        <div class="container">
            <div class="row">
                <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                    { %>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="box_news_KN">
                        <div class="box_news_KN_img">
                            <figure>
                                <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>" class="h_1">
                                    <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Name %>" src="<%=listItem[i].File.Replace("~/","/") %>">
                                </a>
                            </figure>
                        </div>
                        <div class="box_news_KN_content">
                            <h3><a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>"><%=listItem[i].Name %></a></h3>
                            <p class="box_news_KN_content_detail">
                                <%=!string.IsNullOrEmpty(listItem[i].Summary)?listItem[i].Summary:listItem[i].Summary2 %>
                            </p>
                            <div class="box_news_KN_content_icon">
                                <p>
                                    <span class="box_news_KN_content_icon_date"><%=string.Format("{0:dd-MM-yy HH:mm}",listItem[i].Updated) %></span>
                                    <%--     <span class="box_news_KN_content_icon_cmt"><i class="fa fa-comment-o" aria-hidden="true"></i>500</span>--%>
                                    <span class="box_news_KN_content_icon_view"><i class="fa fa-eye" aria-hidden="true"></i><%=listItem[i].View %></span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <%} %>
                <!-- end box -->
                <!-- end box -->
                <div class="nav_list_news">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                                <nav aria-label="...">
                                    <ul class="pagination">
                                        <%= GetPagination(model.page, model.PageSize, model.TotalRecord)%>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</main>
