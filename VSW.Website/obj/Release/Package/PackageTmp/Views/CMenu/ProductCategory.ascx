<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    string url = ViewPage.SearchURL;
    //var listBrand = ModBrandService.Instance.GetByLang(ViewPage.CurrentLang.ID);
    var listMenu = ViewPage.CurrentPage.ID;
    var listBrand = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage, true);
    var listChildPage = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage, false);
%>
<h1 class="logo-text"><%=ViewPage.CurrentPage.PageTitle %></h1>
<section class="content_page-danhmuc">
    <div class="container text_content">
        <p class="h1_title"><%=ViewPage.CurrentPage.Name %></p>
        <%--  <div class="link_all"><a href="<%=ViewPage.CurrentPage.Code%>.html">Tất cả <%=ViewPage.CurrentPage.Name %> tại cửa hàng ›</a></div>--%>
        <div class="des">Những sản phẩm <%=ViewPage.CurrentPage.Name %> chất lượng tốt nhất.</div>
    </div>
    <%if (listChildPage.Count > 0)
        { %>
    <div class="container list_danhmuc">
        <div class="title_section--b">
            <p class="h2_title">Chủng loại <%=ViewPage.CurrentPage.Name.ToLower() %></p>
        </div>
        <div class="row">
            <%for (var i = 0; listChildPage != null && i < listChildPage.Count; i++)
                { %>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                <div class="wp-item-sp item_dp--news">
                    <div class="hpl18-item-li-0">
                        <div class="hpl18-item-image__b img-cover">
                            <a href="<%=ViewPage.GetPageURL(listChildPage[i]) %>" title="<%=listChildPage[i].Name %>" class="h_1">
                                <img class="lazy" title="<%=listChildPage[i].Name %>" alt="<%=listChildPage[i].Name %>" src="<%=listChildPage[i].File.Replace("~/", "/") %>">
                            </a>
                        </div>
                        <p class="hpl18-item-brand"><a href="<%=ViewPage.GetPageURL(listChildPage[i]) %>" title="<%=listChildPage[i].Name%>"><%=listChildPage[i].Name %></a> </p>
                    </div>
                </div>
            </div>
            <%}%>
            <!-- end item -->
        </div>
    </div>
    <%} %>
    <%if (listBrand.Count > 0)
        {%>
    <div class="container list_danhmuc">
        <div class="title_section--b">
            <p class="h2_title">Thương hiệu <%= ViewPage.CurrentPage.Name.ToLower() %></p>
        </div>
        <div class="row">
            <%for (var j = 0; listBrand != null && j < listBrand.Count; j++)
                {%>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                <div class="wp-item-sp item_dp--news">
                    <div class="hpl18-item-li-0">
                        <div class="hpl18-item-image__b img-cover">
                            <a href="<%=ViewPage.GetPageURL(listBrand[j]) %>" title="<%=listBrand[j].Name %>" class="h_1">
                                <img class="lazy" title="<%=listBrand[j].Name %>" alt="<%=listBrand[j].Name %>" src="<%=listBrand[j].File.Replace("~/","/") %>">
                            </a>
                        </div>
                        <p class="hpl18-item-brand"><a href="<%=ViewPage.GetPageURL(listBrand[j]) %>" title="<%=listBrand[j].Name%>"><%=listBrand[j].Name %></a> </p>
                    </div>
                </div>
            </div>
            <%} %>
            <!-- end item -->

        </div>
    </div>
    <%} %>

    <div class="container list_danhmuc">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="text_post_content effect7">
                <p>
                    <%=ViewPage.CurrentPage.Content %>
                </p>
            </div>


            <%if (!string.IsNullOrEmpty(ViewPage.CurrentPage.Content))
                {%>
            <div class="text_post_content">

                <p class="show-more" style="display: block;">
                    <a class="readmore" id="show-more-page"><i class="fa fa-search"></i></a>
                </p>
            </div>
            <%} %>
        </div>
    </div>
</section>





