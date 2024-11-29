<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    string url = ViewPage.SearchURL;
    //if (ViewPage.CurrentModule.Code == "MProduct")
    //    url = ViewPage.CurrentURL;
    var cart = new Cart();
%>
<div class="col-md-5 col-sm-12 col-xs-12 col-5-edit2">
    <div class="wp-search-top">
        <form method="post" onsubmit="doSearch(); return false;">
            <div class="search-top">
                <input type="text" class="form-control" id="keyword" name="keyword" placeholder="Bạn cần tìm hay tư vấn gì hôm nay..." value="" autocomplete="off">
                <a href="javascript:void(0)" class="btn btn-default btn-search" onclick="doSearch()"><i class="fa fa-search"></i></a>
            </div>
            <div class="result-container"></div>
        </form>
    </div>
</div>
<div class="col-md-5 hidden-sm hidden-xs col-5-edit">
    <div class="wp-showrom-hotline-cart ds-flex ali-c ju-c ">
        <div class="wp-hotline ds-flex ali-c">
            {RS:Top_Hotline}
        </div>
        <div class="wp-show-room ds-flex ali-c">
            <div class="icon-cart">
                <a href="gio-hang.html">
                    <img src="/Content/desktop/images/shopping-cart.png">
                    <span><%=cart.Count %></span></a>
            </div>
        </div>
    </div>
</div>


