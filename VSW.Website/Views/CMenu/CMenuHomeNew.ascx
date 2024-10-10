<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    
    var listItem = ViewBag.Data as List<SysPageEntity>;
    var page = ViewBag.Page as SysPageEntity;
    var listNews = ModNewsService.Instance.CreateQuery().ToList();
%>
<%--<%for (var i = 0; listItem != null && i < listItem.Count; i++)
    {
        var lisMenucon = SysPageService.Instance.GetByParent_Cache(listItem[i].ID);
        //var lisMenuParent = SysPageService.Instance.GetByGrandParent_Cache(listItem[i].ID);
%>--%>


<section class="sec-03 mgb-35">
    <div class="container">
        <div class="title_section--b">
           <%-- <h2 class="h2_title"><%=listItem[i].Name %></h2>--%>


            <h2  class="h2_title" >DANH MỤC SẢN PHẨM HOMELED </h2>

        </div>
        <div class="row row-b row_b35">
            <%for (var j = 0; listItem != null && j < listItem.Count; j++)
                {
                    //var lisMenucon2 = SysPageService.Instance.GetByParent_Cache(lisMenucon[j].ID);
            %>
          
            <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                <div class="wp-item-sp item_dp--news">
                    <div class="hpl18-item-li-0">
                        <div class="hpl18-item-image__b img-cover">
                            <a href="<%=ViewPage.GetPageURL(listItem[j]) %>" title="<%=listItem[j].Name %>" class="h_1">
                                <img class="lazy" title="<%=listItem[j].Name %>" alt="<%=listItem[j].Name %>" src="<%=Utils.GetResizeFile(listItem[j].File,4,245,245) %>">
                            </a>
                        </div>
                        <h3 class="hpl18-item-brand"><a href="<%=ViewPage.GetPageURL(listItem[j])%>" title="<%=listItem[j].Name %>"><%=listItem[j].Name %></a> </h3>
                    </div>
                </div>
            </div>
           
           
            <%}
            %>
        </div>
    </div>
</section>

