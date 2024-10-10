<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<div class="footer-bottom">
    <div class="container">
        <div class="row">
            <div class="col-xs-12 text-center">
                <p>Copyright 2019 - Bản quyền của Công ty cổ phần Angkorich</p>
            </div>
        </div>
    </div>
</div>
<div class="mess-fixed messger">
    <a href="{RS:link_facebok}">
        <img src="/Content/desktop/css/images/messger.png"></a>
   
</div>

<div class="mess-fixed zalo">
    <a href="{RS:ZALO}">
        <img src="/Content/desktop/css/images/zl.png" /></a>
</div>

<div class="mess-fixed phone">
    <a href="tel: {RS:hotline}">
        <img src="/Content/desktop/css/images/phone.png" />
    </a>
  
</div>
<%--xem nhanh--%>

<div class="quick-view modal fade in" id="quick-view">
    <div class="container">
        <div class="row">
            <div id="view-gallery">
                <div class="d-table">
                    <div class="d-tablecell">
                        <div class="modal-dialog">
                            <div class="main-view modal-content" id="ContentQuickView">
                            </div>
                        </div>
                    </div>
                </div>
                <!-- single-product item end -->
            </div>
        </div>
    </div>
</div>





