<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var listItem = ViewBag.Data as List<ModProductEntity>;
%>
<section class="sec-02">
    <div class="container">
        <div class="wp-sec-km bg-white">
            <div class="title-img-pro title-clock clock_banner">
                <p>Flash sale</p>
                <div id="clockdiv">
                    <div>
                        <span class="days"></span>
                        <div class="smalltext">Ngày</div>
                    </div>
                    :
                                <div>
                                    <span class="hours"></span>
                                    <div class="smalltext">Giờ</div>
                                </div>
                    :
                                <div>
                                    <span class="minutes"></span>
                                    <div class="smalltext">Phút</div>
                                </div>
                    :
                                <div>
                                    <span class="seconds"></span>
                                    <div class="smalltext">Giây</div>
                                </div>
                </div>
                <%-- <div class="link_all">
                                <a href="#">Xem tất cả<img src="images/icon/arrow1.png" alt=""></a>
                            </div>--%>
            </div>
            <div style="clear: both;"></div>

            <div class="wp-list-sp sp-km">
                <div id="sp-km" class="owl-carousel owl-theme">
                    <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                        { %>
                    <div class="item">
                        <div class="wp-item-sp wp-item-sp-page item-sp-b">
                            <div class="img img-cover">
                                <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>" class="h_1">
                                    <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Name %>" src="<%=Utils.GetResizeFile(listItem[i].File,4,245,245)%>">
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
                                <h4 class="h2_title"><a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>"><%=listItem[i].Name %></a></h4>
                                <%--<div class="rating">
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
                                <div class="int"><%=string.Format("{0:#,##0}", listItem[i].Price)%>đ 
                                <%if (listItem[i].SellOffPercent > 0)
                                    { %> <span class="SellOff">(-<%=listItem[i].SellOffPercent %>%)</span><%} %></div>
                                <%} %>



                                <%if (listItem[i].Price2 > 0)
                                    { %>
                                <div class="sale">
                                    <%=string.Format("{0:#,##0}", listItem[i].Price2) %>đ</div>
                                                                  
                                    <%} %>
                                




                                <%if (listItem[i].SellOff > 0)
                                    { %>
                                <div class="des">Tiết kiệm  <%=string.Format("{0:#,##0}", listItem[i].SellOff)%>đ</div>
                                <%} %>
                            </div>
                        </div>
                    </div>
                    <%} %>
                    <!-- end item -->
                </div>
            </div>
        </div>
    </div>
</section>

<script>
    function getTimeRemaining(endtime) {
        var t = Date.parse(endtime) - Date.parse(new Date());
        var seconds = Math.floor((t / 1000) % 60);
        var minutes = Math.floor((t / 1000 / 60) % 60);
        var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
        var days = Math.floor(t / (1000 * 60 * 60 * 24));
        return {
            'total': t,
            'days': days,
            'hours': hours,
            'minutes': minutes,
            'seconds': seconds
        };
    }
    function initializeClock(id, endtime) {
        var clock = document.getElementById(id);
        var daysSpan = clock.querySelector('.days');
        var hoursSpan = clock.querySelector('.hours');
        var minutesSpan = clock.querySelector('.minutes');
        var secondsSpan = clock.querySelector('.seconds');
        function updateClock() {
            var t = getTimeRemaining(endtime);
            daysSpan.innerHTML = t.days;
            hoursSpan.innerHTML = ('0' + t.hours).slice(-2);
            minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);
            secondsSpan.innerHTML = ('0' + t.seconds).slice(-2);

            if (t.total <= 0) {
                clearInterval(timeinterval);
            }
        }
        updateClock();
        var timeinterval = setInterval(updateClock, 1000);
    }
    var deadline = new Date(Date.parse(new Date()) + 15 * 24 * 60 * 60 * 1000);
    initializeClock('clockdiv', deadline);


    $(document).ready(function () {
        $(".icon-comment").click(function () {
            $(this).toggleClass("active");

            $(this).parent().toggleClass("active");
        })
    })
</script>
