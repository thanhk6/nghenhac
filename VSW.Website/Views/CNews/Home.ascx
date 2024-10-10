<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var listItem = ViewBag.Data as List<ModNewsEntity>;
    string Img = !string.IsNullOrEmpty(listItem[0].File) ? listItem[0].File.Replace("~/", "/") : "";
    var page = ViewBag.Page as SysPageEntity;
    if (page == null) return;
%>
<section class="sec-06 mgb-35 news_section">
    <div class="container">
        <div class="title_section--b">
            <h2 class="h2_title">Thông tin hữu ích</h2>
        </div>
        <div class="row row-b">
            <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                { %>
            <div class="col-md-3 col-sm-6 col-xs-12">
                <div class="box_news_KN">
                    <div class="box_news_KN_img">
                        <figure>
                            <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>"class="h_1">
                                <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Name %>" src="<%=Utils.GetResizeFile(listItem[i].File,4,300,300)%>">
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
                                <span class="box_news_KN_content_icon_date"><%=string.Format("{0:dd-MM-yy HH:mm}",listItem[i].Published) %></span>
                           <%--     <span class="box_news_KN_content_icon_cmt"><i class="fa fa-comment-o" aria-hidden="true"></i>500</span>--%>
                                <span class="box_news_KN_content_icon_view"><i class="fa fa-eye" aria-hidden="true"></i><%=listItem[i].View %></span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <%} %>

            <!-- end item -->
        </div>
        <div class="new-xemthem">
            <a href="<%=page.Code %>">Tất cả bài viết
                   <i class="fa fa-long-arrow-right" aria-hidden="true"></i></a>
        </div>
    </div>
</section>
