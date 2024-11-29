<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    var page = ViewBag.Page as SysPageEntity;
  //  var listNews = ModNewsService.Instance.CreateQuery().ToList();

%>

<h1 class="logo-text"><%=ViewPage.CurrentPage.PageTitle %></h1>
<%for (var i = 0; listItem != null && i < listItem.Count; i++)
    {
        var listnew = ModNewsService.Instance.CreateQuery()
               .Where(o => o.Activity == true)
            .WhereIn(listItem[i].MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", listItem[i].MenuID, ViewPage.CurrentLang.ID))
              .OrderByDesc(o => o.Updated)
            .ToList_Cache();
%>

<section class="sec_list_new">
    <div class="name_list_news">
        <div class="container">
            <h3><%=listItem[i].Name %></h3>
            <a href="<%=ViewPage.GetPageURL(listItem[i]) %>">Xem tất cả <i class="fa fa-long-arrow-right" aria-hidden="true"></i></a>
        </div>
    </div>
    <div class="list_news_01">
        <div class="container">
            <div class="row">
                <%for (var k = 0; listnew != null && k < (listnew.Count > 4 ? 4 : listnew.Count); k++)
                    {%>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="box_news_KN">
                        <div class="box_news_KN_img">
                            <figure>
                                <a href="<%=ViewPage.GetURL(listnew[k].MenuID, listnew[k].Code) %>" title="<%=listnew[k].Name %>" class="h_1">
                                    <img class="lazy" title="<%=listnew[k].Name %>" alt="<%=listnew[k].Name %>" src="<%=listnew[k].File.Replace("~/","/") %>">
                                </a>
                            </figure>
                        </div>

                        <div class="box_news_KN_content">
                            <h3><a href="<%=ViewPage.GetURL(listnew[k].MenuID,listnew[k].Code) %>" title="<%=listnew[k].Name %>"><%=listnew[k].Name %></a></h3>

                            <p class="box_news_KN_content_detail">
                                <%=!string.IsNullOrEmpty(listnew[k].Summary)?listnew[k].Summary:listnew[k].Summary2%>
                            </p>
                            <div class="box_news_KN_content_icon">                        
                                <p>
                                    <span class="box_news_KN_content_icon_date"><%=string.Format("{0:dd-MM-yyyy HH:mm}",listnew[k].Updated) %></span>
                                    <%-- <span class="box_news_KN_content_icon_cmt"><i class="fa fa-comment-o" aria-hidden="true"></i>500</span>--%>
                                    <span class="box_news_KN_content_icon_view"><i class="fa fa-eye" aria-hidden="true"></i><%=listnew[k].View %></span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <%} %>
                <!-- end box -->
                <!-- end box -->
            </div>
        </div>
    </div>
</section>
<%} %>
                
                
