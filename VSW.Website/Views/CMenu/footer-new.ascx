<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    var page = ViewBag.Page as SysPageEntity;
%>
<div class="wp-footer1">
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-sm-6 col-xs-12">
                <div class="text-baogia">
                    <p class="text-baogia-p">Đăng ký  nhận báo giá</p>
                    <p>Đăng ký để nhận ngay báo giá cho các công trình lớn và khuyến mại từ chúng tôi</p>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="btn-baogia">
                    <a href="#" data-toggle="modal" data-target="#modal-baogia"><i class="fa fa-paper-plane" aria-hidden="true"></i>Nhận báo giá</a>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="wp-footer-main">
    <div class="container">
        <div class="tongdai hidden-lg hidden-md">
            <p>Tổng đài hỗ trợ</p>
            <p>Mua hàng - Góp ý - Bảo hành</p>
            <p>Gọi <a href="tel:{RS:Phone_Banhang}" ><span>{RS:Phone_Banhang}</span></a> </p>
            <p>Email: <a href="#">{RS:Email}</a></p>
        </div>


        <div class="footer-main bg-white">
            <div class="wp-footer2">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="wp-ft">
                            <div class="wf-menu-4">
                               {RS:lienhe}
                            </div>
                        </div>
                    </div>
                    <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                        {
                            var lischlid = SysPageService.Instance.GetByParent_Cache(listItem[i].ID);
                    %>
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="wp-ft">
                            <div class="wf-menu-1">
                                <h4 class="h3-title-ft"> <%=lischlid[i].Name %></h4>
                                <ul class="ul-b">
                                    <%for (var j = 0; lischlid != null && j < lischlid.Count; j++)
                                        { %>
                                    <li><a href="<%=ViewPage.GetPageURL(lischlid[j]) %>" title="<%=lischlid[j].Name %>""><span <%if (ViewPage.IsPageActived(lischlid[j]))
                                                                                 {%>
                                        <%} %>> <%=lischlid[j].Name %></span></a></li>
                                    <%} %>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <%} %>
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="wp-ft">
                            <div class="wf-menu-3">
                                <h4 class="h3-title-ft hidden-xs">Tổng đài gọi hỗ trợ</h4>
                                <ul class="ul-b hidden-xs">
                                    <li>Bán hàng: <a href="tel:{RS:Phone_Banhang}" style="font-weight: bold;">{RS:Phone_Banhang}
                                    </a>
                                    </li>
                                    <li>Bảo hành: <a href="tel:{RS:Phone_baohanh}" style="font-weight: bold;">{RS:Phone_baohanh}
                                    </a>
                                    </li>
                                    <li>Khiếu nại: <a href="tel:{RS:Phone_khieunai}" style="font-weight: bold;">{RS:Phone_khieunai}
                                    </a>
                                    </li>
                                </ul>
                                <div class="social">
                                    <h4 class="h3-title-ft">Kết nối với chúng tôi</h4>
                                    <ul class="ul-b">
                                        <li>
                                            <a target="_blank" href="{RS:link_facebook}"><i class="fa fa-facebook"></i></a>
                                        </li>
                                        <li>
                                            <a target="_blank" href="{RS:Link_google_plus}"><i class="fa fa-google-plus"></i></a>
                                        </li>
                                        <li>
                                            <a target="_blank" href="{RS:link_instagram}"><i class="fa fa-instagram"></i></a>
                                        </li>
                                        <li>
                                            <a target="_blank" href="{RS:link_youtube}"><i class="fa fa-youtube-play"></i></a>
                                        </li>
                                        <li>
                                            <a target="_blank" href="#"></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="footer-bottom">
    <div class="container">
        <div class="col-md-12">
            Bản quyền thuộc về  Homeled.vn
        </div>
    </div>
</div>
