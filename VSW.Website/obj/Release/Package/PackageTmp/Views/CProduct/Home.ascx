<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var listItem = ViewBag.Data as List<ModProductEntity>;
%>






<section class="main-page-danhmuc content_page-danhmuc">
   
    <div class="container">
        <div class="row row_width--b">
          <div class="title-img-pro title-clock">
     <p>Sản phẩm bán chạy:</p>
    
 </div>
 <div style="clear: both;"></div>

            <div class="col-md-12 col-sm-12 col-xs-12">
                
                <div class="wp-main-danhmuc-sp">
                    <div class="wp-list-sp-danhmuc">
                        <div class="row product-fs">
                            <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                                { %>
                            <div class="col-md-3 col-sm-3 col-xs-6">
                                <div class="wp-item-sp wp-item-sp-page item-sp-b">
                                    <div class="img img-cover">
                                        <a href="<%=ViewPage.GetURL(listItem[i].BrandID,listItem[i].Code) %>" title="<%=listItem[i].Name %>" class="h_1">
                                            <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Name %>" src="<%=Utils.GetResizeFile(listItem[i].File,4,245,245) %>">
                                        </a>
                                        <div class="sale">
                                            <%if ((listItem[i].State & 1) == 1)
                                                { %>
                                            <span class="icon-54">Sale</span>
                                            <%} %>
                                            <%if ((listItem[i].State & 4) == 4)
                                                { %>
                                            <span class="icon-55">Mới</span>
                                            <%} %>
                                            <%if ((listItem[i].State & 2) == 2)
                                                {%>
                                            <span class="icon-56">Hot</span>
                                            <%} %>
                                        </div>
                                    </div>
                                    <div class="text">
                                        <h3 class="h2_title"><a href="<%=ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code)%>"><%=listItem[i].Name %></a></h3>
                                        <%-- <div class="rating">
                                            <i class="fa fa-star"></i>
                                            <i class="fa fa-star"></i>
                                            <i class="fa fa-star"></i>
                                            <i class="fa fa-star"></i>
                                            <i class="fa fa-star"></i>
                                            <span class="int">35</span>
                                        </div>--%>
                                    </div>
                                    <div class="price">
                                        <%if (listItem[i].Price > 0)
                                            { %>
                                        <div class="int">
                                            <%=string.Format("{0:#,##0}", listItem[i].Price)%>đ <%if (listItem[i].SellOffPercent > 0)
                                                                                                    { %><span class="SellOff">(-<%=listItem[i].SellOffPercent %>%)</span><%} %>
                                        </div>
                                        <%}
                                            else
                                            { %>
                                        <div class="int">Liên hệ để nhận báo giá</div>
                                        <%} %>

                                        <%if (listItem[i].Price2 > 0)
                                            { %>
                                        <div class="sale"><%=string.Format("{0:#,##0}", listItem[i].Price2) %>đ</div>
                                        <%} %>
                                        <%if (listItem[i].SellOff > 0)
                                            { %>
                                        <div class="des">Tiết kiệm <%=string.Format("{0:#,##0}", listItem[i].SellOff)%> đ</div>

                                        <%} %>
                                    </div>
                                </div>
                            </div>
                            <%} %>
                            <!-- end item sp -->
                        </div>

                        
                    </div>
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
    </div>
</section>
