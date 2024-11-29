<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var listItem = ViewBag.WatchALot as List<ModNewsEntity>;
    var page = ViewBag.Page as SysPageEntity;
    if (page == null) return;
%>
<div class="name_page_KN">
    <div class="container">
        <h2>Tin tức</h2>
        <p>Những tin tức của chúng tôi</p>
    </div>
</div>
<section class="sec_page_news sec_slide_page_new">
    <div class="container">
        <div class="banner_top">

            <div class="slider-top">
                <div class="wp-banner-main-tab wp-banner-main-tab_KN">
                    <div id="banner-page-news" class="owl-carousel owl-theme">
                        <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                            { %>
                        <div class="item">
                            <div class="banner-page-news-left">
                                <img src="<%=listItem[i].File.Replace("~/","/") %>" alt="<%=listItem[i].Name %>">
                                <a href="javascript:void(0)" title="<%=listItem[i].Name %>">ĐỌC NHIỀU</a>
                            </div>
                            <div class="banner-page-news-right">
                                <div class="banner-page-news-right-content">
                                    <div class="box_news_KN_content">
                                        <h3><a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>"><%=listItem[i].Name %></a></h3>
                                        <p class="box_news_KN_content_detail">
                                             <%=!string.IsNullOrEmpty(listItem[i].Summary)?listItem[i].Summary:listItem[i].Summary2 %>
                                        </p>
                                        <div class="box_news_KN_content_icon">
                                            <p>
                                                <span class="box_news_KN_content_icon_date"><%=string.Format("{0:dd-MM-yyyy HH:mm}",listItem[i].Updated) %></span>
                                                <%--  <span class="box_news_KN_content_icon_cmt"><i class="fa fa-comment-o" aria-hidden="true"></i>500</span>--%>
                                                <span class="box_news_KN_content_icon_view"><i class="fa fa-eye" aria-hidden="true"></i><%=listItem[i].View %></span>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="banner-page-news-right-img">
                                    <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>">
                                        <img src="<%=listItem[i].File.Replace("~/","/") %>" alt="<%=listItem[i].Name %>"></a>
                                </div>
                            </div>
                        </div>
                        <%} %>
                        <!-- end item -->

                        <!-- end item -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
