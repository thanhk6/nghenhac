<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    var page = ViewBag.Page as SysPageEntity;
    //var listNews = ModNewsService.Instance.CreateQuery().ToList();
%>
<div class="mobile-main-menu">


    <div class="drawer-header">
        <a href="javascript:void(0)">
            <div class="drawer-header--auth">

                <%if (WebLogin.IsLogin())
                    {%>
                <div class="_object">
                    <img src="/Content/Desktopnew/images/user.png" alt="">
                </div>
                <div class="_body">

                    <%=WebLogin.CurrentUser.Name%>
                </div>
                <%}
                    else
                    {%>
                <div class="_object">
                    <img src="/Content/Desktopnew/images/user.png" alt="">
                </div>
                <div class="_body">
                    ĐĂNG NHẬP<br>
                    Nhận nhiều ưu đãi hơn 
                </div>

                <%} %>
            </div>
        </a>
    </div>


    <%if (!WebLogin.IsLogin())
        {%>
    <ul class="ul-first-menu">
        <li><a href="#" id="dangnhap_click" data-toggle="modal" data-target="#dangnhap"><i class="fa fa-sign-in"></i>Đăng nhập</a>
        </li>
        <li><a href="#" id="dangky_click" data-toggle="modal" data-target="#dangky"><i class="fa fa-user"></i>Đăng ký</a>
        </li>
    </ul>
    <%} %>

    <div class="la-scroll-fix-infor-user">
        <div class="la-nav-menu-items">
            <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                {
                    var lismenu = SysPageService.Instance.GetByParent_Cache(listItem[i].ID);
            %>
            <ul class="la-nav-list-items ul-b">
                <p class="h3_title--navbar"> <a href="<%=ViewPage.GetPageURL(listItem[i]) %>"> <%=listItem[i].Name %>  </a></p>

                    <%--   <%=listItem[i].Name %>  --%>

                <%for (var j = 0; lismenu != null && j < lismenu.Count; j++)
                    {
                        var lismenuParen = SysPageService.Instance.GetByParent_Cache(lismenu[j], false);
                %>
                <li class="ng-scope ng-has-child1">
                    <a href="<%=ViewPage.GetPageURL(lismenu[j]) %>" class="lv1-a"><%=lismenu[j].Name %>
                        <%if (lismenuParen.Count > 0)
                            { %><i class="fa fa-plus fa1" aria-hidden="true">
                            </i><%}%></a>
                    <ul class="ul-has-child1 ul-b">
                        <%for (var k = 0; lismenuParen != null && k < lismenuParen.Count; k++)
                            { %>
                        <li class="ng-scope ng-has-child2"><a href="<%=ViewPage.GetPageURL(lismenuParen[k]) %>"><%=lismenuParen[k].Name %></a>
                        </li>
                        <%} %>
                    </ul>
                </li>
                <%} %>
            </ul>
            <%}%>
        </div>
    </div>
</div>


<!-- modal dang nhap -->
<div class="modal fade css_login_KN_HHT" id="dangnhap" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">

            <div class="modal-body">
                <div class="wp-login">
                    <div class="right">
                        <div class="box-login">
                            <div class="wp-top">
                                <img src="/Data/upload/images/Adv/Untitled-2(1).png" alt=" Home led">
                            </div>
                            <div class="wp-middle">
                                <form method="post" name="form-locgin" id="form-locgin">
                                    <div class="wp-form-login">
                                        <div class="wp-input form-group">
                                            <p>Tên đăng nhập:</p>
                                            <input type="text" class="form-control" placeholder="Tên đăng nhập">
                                        </div>
                                        <div class="wp-input form-group">
                                            <p>Mật khẩu:</p>
                                            <input type="password" class="form-control" placeholder="Mật khẩu">
                                        </div>
                                        <div class="wp-button">
                                            <button class="btn btn-login">Đăng nhập</button>
                                            <%-- <p class="hoac">Hoặc</p>--%>
                                            <%--<ul class="login-mxh">
                                                <li><a href="#">
                                                    <img src="/Content/Desktopnew/images/facebook.png" alt=""></a></li>
                                                <li><a href="#">
                                                    <img src="/Content/Desktopnew/images/zalo-icon.png" alt=""></a></li>
                                                <li><a href="#">
                                                    <img src="/Content/Desktopnew/images/google.png" alt=""></a></li>
                                            </ul>--%>
                                        </div>
                                        <div class="next_regis" data-target="#dangky">
                                            <p>Bạn chưa có tài khoản? <span><a href="javascript:void(0)" data-target="#dangky" id="tab-dangky">Đăng kí</a></span></p>
                                        </div>
                                    </div>
                                </form>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- modal dang ky -->
<div class="modal fade css_login_KN_HHT" id="dangky" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="wp-login">
                    <div class="right">
                        <div class="box-login">
                            <div class="wp-top">
                                <img src="/Data/upload/images/Adv/Untitled-2(1).png" alt=" Home led">
                            <div class="wp-middle">
                                <form method="post" name="register-form" id="register-form">
                                    <div class="wp-form-login">
                                        <div class="wp-input form-group">
                                            <p>Tên đăng nhập:</p>
                                            <input type="text" class="form-control" name="Name" placeholder="Tên đăng nhập">
                                        </div>
                                        <div class="wp-input form-group">
                                            <p>Nhập sô điện thoại :</p>
                                            <input type="number" class="form-control" name="Phone" placeholder="Nhập sô điên thoại">
                                        </div>
                                        <div class="wp-input form-group">
                                            <p>Nhập Email:</p>
                                            <input type="text" class="form-control" name="Email" placeholder="Nhập lại mật khẩu">
                                        </div>
                                        <div class="wp-input form-group">
                                            <p>Mật khẩu:</p>
                                            <input type="password" class="form-control" name="Password" placeholder="Mật khẩu">
                                        </div>
                                        <div class="wp-input form-group">
                                            <p>Nhập lại mật khẩu:</p>
                                            <input type="password" class="form-control" name="Password2" placeholder="Nhập lại mật khẩu">
                                        </div>
                                        <div class="wp-button">
                                            <button type="button" class="btn btn-login" onclick="register()">Đăng Ký</button>
                                        </div>
                                        <div class="next_regis">
                                            <p>Bạn đã có tài khoản? <span><a href="javascript:void(0)" id="tab-dangnhap">Đăng nhập</a></span></p>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<script>
    $('#tab-dangnhap').click(function () {
        $('#dangnhap').modal('show');
        $('#dangky').modal('hide');
    });
    $('#tab-dangky').click(function () {
        $('#dangky').modal('show');
        $('#dangnhap').modal('hide');
    });
</script>
