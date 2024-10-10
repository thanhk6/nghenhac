
<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var listItem = ViewBag.Data as List<SysPageEntity>;
    var MenuID = ViewPage.CurrentPage.MenuID;
    var pagephukien = SysPageService.Instance.GetByGrandParent_Cache(7538);
%>
<section class="content_page-danhmuc">
    <div class="container text_content">
        <p class="h1_title">Thương hiệu <%=ViewPage.CurrentPage.Name.ToLower() %></p>
        <div class="des">Những sản phẩm thương hiệu <%=ViewPage.CurrentPage.Name.ToLower() %> chất lượng tốt nhất.</div>
    </div>
    <div class="container list_danhmuc">
        <div class="title_section--b">
            <h2 class="h2_title">Thương  hiệu <%=ViewPage.CurrentPage.Name.ToLower() %></h2>
        </div>
        <div class="row">
            <%for (var i = 0; listItem != null && i < listItem.Count; i++)

                {
                    var lispageBarnd = SysPageService.Instance.GetByParent_Cache(listItem[i].ID);
            %>
            <%for (var j = 0; lispageBarnd != null && j < lispageBarnd.Count; j++)
                {
                    string FileBrand = lispageBarnd[j].FileBrand != null ? lispageBarnd[j].FileBrand.Replace("~/", "/") : "";
                    var lispageBarnd1 = SysPageService.Instance.GetByParent_Cache(lispageBarnd[j].ID);
                    %>
          <%for (var k = 0; lispageBarnd1 != null && k < lispageBarnd1.Count; k++)
              {
                  var lispageBarnd2 = SysPageService.Instance.GetByParent_Cache(lispageBarnd1[k].ID);

                  if (lispageBarnd1[k].FileBrand == null || lispageBarnd1[k].FileBrand == string.Empty) continue;
                  string FileBrand1 = lispageBarnd1[k].FileBrand != null ? lispageBarnd1[k].FileBrand.Replace("~/", "/") : "";
                    %>
            <%if (lispageBarnd1[k].BrandID == ViewPage.CurrentPage.MenuID)
                {%>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                <div class="wp-item-sp item_dp--news">
                    <div class="hpl18-item-li-0">
                        <div class="hpl18-item-image__b img-cover">
                            <a href="<%=ViewPage.GetPageURL(lispageBarnd1[k]) %>" title="<%=lispageBarnd1[k].Name %>" class="h_1">
                                <img class="lazy" title="<%=lispageBarnd1[k].Name %>" alt="<%=lispageBarnd1[k].Name %>" src="<%=FileBrand1%>">
                            </a>
                        </div>
                        <h3 class="hpl18-item-brand"><a href="<%=ViewPage.GetPageURL(lispageBarnd1[k]) %>" title="<%=lispageBarnd1[k].Name %>"><%=lispageBarnd1[k].Name %></a> </h3>
                    </div>
                </div>
            </div>
            <%} %>
            <%for (var n=0;lispageBarnd2!=null&&n<lispageBarnd2.Count;n++) {
                    if (lispageBarnd2[n].FileBrand == null || lispageBarnd2[n].FileBrand == string.Empty) continue;
                    string FileBrand2 = lispageBarnd2[n].FileBrand != null ? lispageBarnd2[n].FileBrand.Replace("~/", "/") : "";
                    %>           
            <%if (lispageBarnd2[n].BrandID == ViewPage.CurrentPage.MenuID)
                {%>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                <div class="wp-item-sp item_dp--news">
                    <div class="hpl18-item-li-0">
                        <div class="hpl18-item-image__b img-cover">
                            <a href="<%=ViewPage.GetPageURL(lispageBarnd2[n]) %>" title="<%=lispageBarnd2[n].Name %>" class="h_1">
                                <img class="lazy" title="<%=lispageBarnd2[n].Name %>" alt="<%=lispageBarnd2[n].Name %>" src="<%=FileBrand1%>">
                            </a>
                        </div>
                        <h3 class="hpl18-item-brand"><a href="<%=ViewPage.GetPageURL(lispageBarnd2[n]) %>" title="<%=lispageBarnd2[n].Name %>"><%=lispageBarnd2[n].Name %></a> </h3>
                    </div>
                </div>
            </div>
            <%} %>
            <%} %>
            <%} %>
            <%} %>
          <%} %>

          <%--  lây trang phụ kiên--%>
            <%for (var m = 0; pagephukien != null && m < pagephukien.Count; m++)
                {
                    if (pagephukien[m].FileBrand == null || pagephukien[m].FileBrand == string.Empty) continue;
                    string FileBrand1 = pagephukien[m].FileBrand != null ? pagephukien[m].FileBrand.Replace("~/", "/") : "";
                    %>
            <%if (pagephukien[m].BrandID == ViewPage.CurrentPage.MenuID)
                {%>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                <div class="wp-item-sp item_dp--news">
                    <div class="hpl18-item-li-0">
                        <div class="hpl18-item-image__b img-cover">
                            <a href="<%=ViewPage.GetPageURL(pagephukien[m]) %>" title="<%=pagephukien[m].Name %>" class="h_1">
                                <img class="lazy" title="<%=pagephukien[m].Name %>" alt="<%=pagephukien[m].Name %>" src="<%=FileBrand1%>">
                            </a>
                        </div>
                        <h3 class="hpl18-item-brand"><a href="<%=ViewPage.GetPageURL(pagephukien[m]) %>" title="<%=pagephukien[m].Name %>"><%=pagephukien[m].Name %></a> </h3>
                    </div>
                </div>
            </div>
            <%} %>
            <%} %>
        </div>

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






