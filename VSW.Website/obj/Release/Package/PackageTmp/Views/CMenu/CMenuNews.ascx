<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    var page = ViewBag.Page as SysPageEntity;
    var listNews = ModNewsService.Instance.CreateQuery().ToList();
    
%>

<div class="col-md-4 col-sm-12 col-xs-12 col-md-push-8">
    <div class="wp-list-chuyenmuc">
        <ul class="ul-b list-chuyenmuc">
            <%for (int i = 0; listItem !=null && i < listItem.Count; i++)
                { %>
            <li class="item-chuyenmuc">
                <a href="<%=ViewPage.GetPageURL(listItem[i]) %>">
                    <i class="fa fa-file-text-o"></i>
                    <h4 class="h4-title"><%=listItem[i].Name %></h4>
                   <%-- <span>Khuyến mại</span>--%>
                </a>
            </li>
            <%}%>         
        </ul>
         <%if(listNews!=null&&listNews.Count>0) {%>
        <%for (int i = 0; listNews != null && i < (listNews.Count > 4 ? 4 : listNews.Count); i++)
            {
                string imgnew = !string.IsNullOrEmpty(listNews[i].File) ? listNews[i].File.Replace("~/", "/") : " ";
                %>
        <div class="wp-img-ads2 img-cover hidden-sm hidden-xs">
            <a href="<%=ViewPage.GetURL(listNews[i].MenuID,listNews[i].Code) %>">
                <img src="<%=imgnew%>" alt="" title=""></a>
        </div>
        <%} %>
        <%} %>
    </div>
</div>
