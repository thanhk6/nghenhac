<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var listItem = ViewBag.Data as List<ModProductEntity>;
    var model = ViewBag.Model as MSearchModel;
%>
<div style="margin-top: 10px;"></div>

<%if (listItem != null && listItem.Count > 0)
    { %>
<section class="main-page-danhmuc">
    <div class="container">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="wp-main-danhmuc-sp">
                    <div class="wp-list-sp-danhmuc">
                        <div class="row product-fs">
                            <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                                { %>
                            <div class="col-md-3 col-sm-4 col-xs-6">
                                <div class="wp-item-sp wp-item-sp-page item-sp-b">
                                    <div class="img img-cover">
                                        <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>" class="h_1">
                                            <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Alt!=null?listItem[i].Alt:listItem[i].Name %>" src="<%=listItem[i].File.Replace("~/","/") %>">
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
                                        <h2 class="h2_title"><a href="<%=ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code)%>"><%=listItem[i].Name %></a></h2>
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
                                        <div class="int"><%=string.Format("{0:#,##0}", listItem[i].Price)%>đ</div>
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
                        <div class="phantrang text-center">
                            <ul class="pagination">
                                <%= GetPagination(model.page, model.PageSize, model.TotalRecord)%>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<%}%>
<% else
    {%>
<section class="content_page-ketqua_search">
    <div class="container ketqua-timkiem">
        <div class="des_tt-timkiem">Website tìm được 0 kết quả cho từ khóa  “<%=model.keyword %>”  của bạn. </div>
        <div class="text-center img">
            <img src="/Content/Desktopnew/images/img-ketqua-timkiem.png" alt="">
        </div>
        <div class="des_timkiem">
            <div class="left">
                <b>Mẹo tìm kiếm</b>
                <ul>
                    <li>Tìm kiếm theo mã số của sản phẩm để có được kết quả chính xác nhất</li>
                    <li>Viết đúng chính tả cho từ khóa tìm kiếm</li>
                </ul>
            </div>
            <div class="right">
                <div class="b">
                    <b>Bạn cần thông tin nhanh và chính xác nhất?
                                            <br>
                        Hãy liên hệ trực tiếp với chúng tôi</b>
                </div>
                <ul class="ul-b">
                    <li><a href="tel:{RS:Top_Hotline}">
                        <img src="/Content/Desktopnew/images/icon-phone1.png" alt=""></a></li>

                    <li><a href="{RS:link_zalo}">
                        <img src="/Content/Desktopnew/images/icon-zalo1.png" alt=""></a></li>

                    <li><a href="#">
                        <img src="/Content/Desktopnew/images/icon-mail1.png" alt=""></a></li>

                </ul>
            </div>
        </div>
    </div>
</section>
<%}%>
