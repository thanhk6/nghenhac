<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var item = ViewBag.Data as ModFeedbackEntity;
%>
<section class="banner-news-blog">
  <%--  <%if(!string.IsNullOrEmpty(ViewPage.CurrentPage.File)) {%>
    <img src="<%=ViewPage.CurrentPage.File.Replace("~/", "/") %>" alt="<%=ViewPage.CurrentPage.Name %>" />
    <%} %>--%>
    <div class="title-banner-newsblog">
        <h1>LIÊN HỆ VỚI KGVIETNAM</h1>
        <p class="slogan-th hidden-xs">Tầng 11 khối A, tòa nhà Sông Đà, đường Phạm Hùng, Phường Mỹ Đình 1, Quận Nam Từ Liêm, Thành phố Hà Nội, Việt Nam.</p>
    </div>
</section>
<section id="lh">
    <div class="container">
        <div class="row">
            <div class="form-contact">
                <div class="contact-info col-md-7 col-sm-6 col-xs-12">{RS:Web_Feedback}</div>
                <div class="col-md-5 col-sm-6 col-xs-12">
                    <h2>Gửi yêu cầu</h2>
                    <div class="row">
                        <form method="post" class="mt-form" name="feedback_form" id="feedback_form">
                            <p class="col-md-12 field"><input type="text" class="modal-mail" name="Name" placeholder="Họ và tên" /></p>
                            <p class="col-md-12 field"><input type="text" class="modal-mail" name="Phone" placeholder="Số điện thoại" /></p>
                            <p class="col-md-12 field"><input type="text" class="modal-mail" name="Email" placeholder="Email" /></p>
                            <p class="col-md-12 field"><textarea cols="40" rows="10" name="Content" placeholder="Nhập nội dung liên hệ"></textarea></p>
                            <p class="col-md-12 submit-wrap "><input type="button" class="btn-gui btn-primary feedback-btn" value="Gửi đi" /></p>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>