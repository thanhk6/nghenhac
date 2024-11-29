<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var item = ViewBag.Data as ModNewsEntity;
    var listItem = ViewBag.Other as List<ModNewsEntity>;
    string file = ViewPage.CurrentPage.File;
    if (string.IsNullOrEmpty(file))
        file = ViewPage.CurrentPage.GetParent().File;
    //var brand = SysPageService.Instance.CreateQuery().Where(o => o.MenuID == ViewPage.CurrentPage.MenuID && o.BrandID == item.BrandID).ToSingle_Cache();
%>
<style>
    .address_KN {
        background-image: url(Content/Desktopnew/skins/images/ke-phong-tam-06-noi-that-chung-cu.jpg);
    }
</style>
<div class="beadcrumb-tuyen">
    <div class="container">

        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <ol vocab="https://schema.org/" typeof="BreadcrumbList" class="breadcrumb">
                    <%= Utils.GetMapPageproduct(ViewPage.CurrentPage,item.Code,item.Name) %>
                </ol>
            </div>
        </div>
    </div>
</div>
<div id="fb-root"></div>
<script async defer crossorigin="anonymous" src="https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v10.0" nonce="8flHX32S"></script>
<script src="https://sp.zalo.me/plugins/sdk.js"></script>
<div class="main_page_details_KN">
    <section class="sec_details_news_KN">
        <div class="container">
            <h1 class="title_details_news_KN"><%=item.Name %></h1>
            <p class="the_article_summary_KN"><%=item.Summary!=string.Empty?item.Summary:string.Empty %></p>
            <div class="line_details_news_KN"></div>

            <%--<h5 class="question_content">Is Prescription Working?</h5>--%>
            <%=item.Content %>
            <div></div>
            <div class="author_share_KN">
                <div class="author_KN">
                    <img src="/Content/Desktopnew/skins/images/c.png" alt="homeled">

                    <span>Tác giả: homeled</span> <span>|</span> <span><%=string.Format("{0:dd-MM-yyyy HH:mm}",item.Updated) %></span>
                </div>
                <div class="share_KN">

                    <div class="share_fb_KN ">
                        <div class=" fb-share-button" style="margin-left: 5px" data-href="<%=ViewPage.GetURL(item.MenuID,item.Code) %>" data-layout="button_count" data-size="small"><a target="_blank" href="https://www.facebook.com/sharer/sharer.php?u=https%3A%2F%2Fvlxdkn.vn%2F&amp;src=sdkpreparse" class="fb-xfbml-parse-ignore">Chia sẻ</a></div>
                    </div>
                    <div class="share_zalo_KN">
                        <div class=" zalo-share-button " data-href="" data-oaid="579745863508352884" data-layout="2" data-color="blue" data-customize="false" style="margin-left: 5px"></div>
                    </div>
                </div>
            </div>
            <div style="clear: both"></div>
            <div class="effect7" style="overflow: hidden; padding-top: 5px; font-style: oblique;">
                <p>
                    <%=item.Summary2 %>
                </p>
            </div>
            <div class="clr"></div>
            <div class="line_details_news_KN"></div>
            <form method="post" name="contact_form" id="contact_form">
                <div class="regis_advisory_address">
                    <div class="advisory_KN">
                        <p>Liên hệ tư vấn</p>
                        <div class="advisory_KN_input">
                            <p>Họ và tên:</p>
                            <input type="text" class="form-control" name="Name" placeholder="Điền họ và tên của bạn">
                        </div>
                        <div class="advisory_KN_input">
                            <p>Số điện thoại:</p>
                            <input type="text" class="form-control" name="Phone" placeholder="Điền số điện thoại">
                        </div>
                        <div class="advisory_KN_input">
                            <p>Lời nhắn:</p>
                            <textarea class="form-control" name="Content" cols="40" rows="10" placeholder="Lời nhắn của bạn"></textarea>
                        </div>
                        <div class="btn_guithongtin">
                            <button type="button" id="contact">GỬI THÔNG TIN</button>
                        </div>
                    </div>
                    
                </div>
            </form>
        </div>
    </section>
</div>

<section class="product_hot_KN">
    <div class="title_news_01">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <p>
                        Có thể bạn quan tâm
                    </p>
                </div>
            </div>
        </div>
    </div>
    <!-- title_news -->
    <div class="list_news_01">
        <div class="container">
            <div class="row">
                <%for (var i = 0; listItem != null && i < (listItem.Count > 4 ? 4 : listItem.Count); i++)
                    { %>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="box_news_KN">
                        <div class="box_news_KN_img">
                            <figure>
                                <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="" class="h_1">
                                    <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Name %>" src="<%=listItem[i].File.Replace("~/","/") %>">
                                </a>
                            </figure>
                        </div>
                        <div class="box_news_KN_content">
                            <h3><a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code)%>" title=""><%=listItem[i].Name %></a></h3>
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
                </div>
                <%} %>
                <!-- end box -->

                <!-- end box -->
            </div>
        </div>
    </div>
</section>
