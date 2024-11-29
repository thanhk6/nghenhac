<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var item = ViewBag.Data as ModAdvEntity;
    if (item == null || string.IsNullOrEmpty(item.File)) return;
    var cart = new Cart();
%>
  <%if (ViewPage.CurrentPage.Activity==true&&ViewPage.CurrentPage.ModuleCode!="MNews" &&ViewPage.CurrentPage.ModuleCode!="MProduct")

      {%><h1 class="logo-text"><%=ViewPage.CurrentPage.PageTitle %></h1><%} %>

<div class="col-md-2 col-sm-12 col-xs-12 col-2-edit_b">
    <div class="wp-logo text-center">
        <a href="<%=item.URL%>">
            <img src="<%=item.File.Replace("~/","/") %>" alt="home led " class="logo"></a>
        <div class="btn btn-default btn-search-cart hidden-lg hidden-md">
            <div class="icon-cart">
                <a href="gio-hang.html">
                    <i class="fa fa-shopping-cart"></i>
                    <span><%=cart.Count %></span>
                </a>
            </div>
        </div>
    </div>
</div>
<div class="col-md-7 col-sm-12 col-xs-12 col-5-edit2_b">
    <div class="wp-search-top">
        <form method="post" onsubmit="doSearch(); return false;">
            <div class="search-top">
                <input type="text" class="form-control" id="keyword" name="keyword" placeholder="Bạn cần tìm hay tư vấn gì hôm nay...">
                <a href="javascript:void(0)" class="btn btn-default btn-search" onclick="doSearch()"><i class="fa fa-search"></i></a>
            </div>
        </form>
    </div>
</div>
<div class="col-md-3 hidden-sm hidden-xs col-5-edit_b">
    <div class="wp-showrom-hotline-cart ds-flex ali-c ju-c ">
        <div class="wp-hotline ds-flex ali-c">           
            <div class="icon-hotline">
                <i class="fa fa-phone-square"></i>
            </div>                
            <div class="text-hotline">
                <a href="tel:{RS:Top_Hotline}"><b>Hotline: {RS:Top_Hotline}</b></a>
                <span>{RS:tu_van}</span>
            </div>
        </div>
        <div class="wp-show-room ds-flex ali-c">
            <div class="icon-cart">
                <a href="gio-hang.html">
                    <i class="fa fa-shopping-cart"></i>
                    <span><%=cart.Count %></span>
                </a>
            </div>
        </div>
    </div>
</div>
