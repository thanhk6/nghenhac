<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var listItem = ViewBag.Data as List<ModProductEntity>;
%>

<section class="sec-02">
    <div class="container">
        <div class="wp-sec-km bg-white">
            <div class="title-img-pro title-clock">
                <h2>{RS:Tieu_de_gia_soc_home}</h2>
                <div id="clockdiv">
                    <div>
                        <span class="days" id="day"></span>
                        <div class="smalltext">Ngày</div>
                    </div>
                    <span style="padding: 5px;">:</span>
                    <div>
                        <span class="hours" id="hour"></span>
                        <div class="smalltext">Giờ</div>
                    </div>
                    <span style="padding: 5px;">:</span>
                    <div>
                        <span class="minutes" id="minute"></span>
                        <div class="smalltext">Phút</div>
                    </div>
                    <span style="padding: 5px;">:</span>
                    <div>
                        <span class="seconds" id="second"></span>
                        <div class="smalltext">Giây</div>
                    </div>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div class="wp-list-sp sp-km">
                <div id="sp-km" class="owl-carousel owl-theme">
                    <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                        {
                            string img = string.IsNullOrEmpty(listItem[i].File) ? listItem[i].File.Replace("~/", "/") : "";
                             long priegit = 0;
                            var listgit = listItem[i].GetGift();
                            if (listgit != null && listgit.Count > 0)
                            {
                                for (int l = 0; listgit != null && l < listgit.Count; l++)
                                {
                                    priegit += listgit[l].Price;
                                }
                            }
                    %>

                    <div class="item">
                        <div class="wp-item-sp">
                            <div class="hpl18-item-li hpl18-item-li-0">
                                <p class="hpl18-item-image">
                                    <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="" class="h_1">
                                        <img class="lazy" title="<%=listItem[i].Name %>" alt=""data-lazyload="<%=Utils.GetUrlFile(listItem[i].File) %>" >
                                    </a>
                                    <span class="icon-55"></span>
                                    <%if (listItem[i].SellOffPercent > 0)
                                        { %>
                                    <span class="hpl18-item-discount">-<%=listItem[i].SellOffPercent %>%</span>
                                    <%}%>
                                </p>
                                <h3 class="hpl18-item-brand"><a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>"><span><%=listItem[i].Name %></span></a> </h3>
                                <%if (listItem[i].Price > 1)
                                    {%>
                                <div class="hpl18-item-pbuy"><%=string.Format("{0:#,##0} ", listItem[i].Price+priegit) %><span>đ</span> </div>
                                <%}
                                else
                                {
                                %>
                                <div class="hpl18-item-pbuy">Liên hệ<span>đ</span> </div>
                                <%} %>
                                <%if (listItem[i].Price2 > 1)
                                    { %>
                                <div class="hpl18-item-pmarket"><%=string.Format("{0:#,##0}", listItem[i].Price2)%><span>đ</span> </div>
                                <%}
                                    %>
                                <!-- <%if (priegit > 0)
                                       { %>
                                <div class="hpl18-item-soffer">* Giảm giá lên tới <span><%=string.Format("{0:#,##0}", priegit)%> </span></div>
                              <%} %> -->                                                           
                            </div>
                        </div>
                    </div>
                    <%} %>
                </div>
                <%--<div class="Xemthem-product-home">
                    <a href="san-pham.html">Xem thêm sản phẩm <i class="fa fa-angle-double-right" aria-hidden="true"></i></a>
                </div>--%>
            </div>
        </div>
    </div>
</section>
