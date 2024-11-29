<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var item = ViewBag.Data as ModOrderEntity;
    var model = ViewBag.Model as MViewCartModel;
    var cart = new Cart();
    if (cart == null) return;
    long total = 0;
%>
<section class="main-page-chitiet">
    <div class="container">
        <div class="orderbox">
            <div class="navigate"><a href="" title="">Tiếp tục mua hàng</a><label>Giỏ hàng của bạn</label></div>
            <div class="boxFormPayment">
                <ul class="listcart ul-b">
                    <%for (int i = 0; i < cart.Items.Count; i++)
                        {
                             var Product = ModProductService.Instance.GetByID(cart.Items[i].ProductID);
                            if (Product == null) continue;
                            string img = !string.IsNullOrEmpty(Product.File) ? Product.File.Replace("~/", "/") : " ";
                            total += Product.Price * cart.Items[i].Quantity;
                    %>
                    <li class="cartitem">
                        <div class="oimg">
                            <a href="" title="">
                                <img src="<%=img %>"></a>
                            <a href="javascript:void(0)" title="" class="odel" onclick="delete_cart('<%=i%>')">Xóa</a>
                        </div>
                        <div class="oname">
                            <h3><a href="" title="<%=Product.Name %>"><%=Product.Name %>"</a></h3>

                            <label><%=string.Format("{0:#,##0}",Product.Price*cart.Items[i].Quantity)%>₫</label>
                            <%--<div class="promotion newpro">
                                <span class="promo">Phiếu mua hàng 50,000đ</span>
                            </div>--%>
                            <div class="quantity" " >
                                <div class="cart-plus-minus">
                                    <input type="text" name="qtybutton" id="qtybutto" class="cart-plus-minus-box" onchange="load_cart(this.value,'<%=i%>');" value="<%=cart.Items[i].Quantity%>" min="1" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <%} %>
                </ul>
                <div class="total">
                    <div>
                        <span>Tổng tiền (<%=cart.Count %> sản phẩm):</span>
                        <b id="total_all"><%=string.Format("{0:#,##0}",total) %>₫</b>
                    </div>
                    <%--<div id="promotiondiscount">
                        <span>Phí vận chuyển:</span>
                        <i>20.000₫</i>
                    </div>
                    <div id="promotiondiscount">
                        <span>Giảm:</span>
                        <i>-50.000₫</i>
                    </div>
                    <div id="promotiondiscount" class="datcoc">
                        <span>Cần đặt cọc:</span>
                        <i>50.000₫</i>
                    </div>--%>

                    <div id="totalSum">
                        <span>Cần thanh toán:</span>
                        <b class="total_money"><%=string.Format("{0:#,##0}",total) %>₫</b>
                    </div>
                    <%--<div class="mgg-code">Sử dụng mã giảm giá</div>--%>
<%--                    <div class="mgg-inputcode">
                        <button type="button" id="btnCoupon">Áp dụng</button>
                        <input autocomplete="none" id="Coupon" maxlength="20" name="Coupon" placeholder="Nhập mã giảm giá" type="text" value="">
                        <label id="coupon-error" class="texterror"></label>
                    </div>--%>
                </div>
                <form class="form-list" name="cart_form" id="cart_form">
                    <div class="fpanel all">
                        <div class="title-user">Thông tin khách hàng</div>
                        <div class="form-group">
                            <label class="radioPure">
                                <input id="radio" type="radio" name="  Gender" value="anh"><span class="outer"><span class="inner"></span></span><i>Anh</i></label>
                            <label class="radioPure">
                                <input id="radio2" type="radio" name="  Gender" value="chi"><span class="outer"><span class="inner"></span></span><i>Chị</i></label>
                        </div>
                        <div class="row">
                            <div class="col-md-6 form-group">
                                <input type="text" class="form-control" name="Name" placeholder="Họ và tên">
                            </div>
                            <div class="col-md-6 form-group">
                                <input type="number" class="form-control" name="Phone" placeholder="Số điện thoại">
                            </div>
                        </div>
                        <div class="form-group">
                            <input type="text" class="form-control" name="Email" placeholder="Email">
                        </div>

                      <%--  <div class="row">
                            <div class="col-md-4 form-group">
                                <select class="form-control" name="CityID" id="CityID" onchange="get_child(this.value, $('#DistrictID').val(), '.list-district','<option value=0>Chọn Quận / Huyện</option>');">
                                    <option value="0">Chọn thành phố</option>
                                    <%=Utils.ShowDdlMenuByType2("City", 1, item.CityID) %>
                                </select>
                            </div>
                            <div class="col-md-4 form-group">

                                <select class="form-control list-district" name="DistrictID" id="DistrictID" onchange="get_child(this.value, $('#WardID').val(), '.list-ward','<option value=0>Chọn Phường / Xã</option>');">
                                     <option value="null">Chọn Quận/Huyện</option>
                                </select>
                            </div>
                            <div class="col-md-4 form-group">
                                <select class="form-control list-ward" name="WardID" id="WardID">
                                   <option value="null">Chọn Phường/Xã</option>
                                  
                                </select>
                            </div>
                        </div>--%>

                        <div class="form-group">
                            <input type="text" class="form-control" name="Address" placeholder="Số nhà, tòa nhà, tên thôn xóm, tên đường...">
                        </div>
                        <div class="form-group">
                            <textarea class="form-control" height="200" name="Content" rows="5" placeholder="Ghi chú thêm nếu có (không bắt buộc)"></textarea>
                        </div>

                     <%--   <div class="title-user">CHỌN HÌNH THỨC THANH TOÁN</div>
                        <div class="form-group">
                            <label class="radioPure">
                                <input id="payment_method_a" class="payment" data-id="a" type="radio" name="Payment" value="Thanh toán khi nhận hàng" checked><span class="outer"><span class="inner"></span></span><i>Thanh toán khi nhận hàng - COD</i></label>
                        </div>
                        <div class="form-group">
                            <label class="radioPure">
                                <input id="payment_method_b" class="payment" data-id="b" type="radio" name="Payment" value="Chuyển khoản qua ngân hàn"><span class="outer"><span class="inner"></span></span><i>Chuyển khoản qua ngân hàng</i></label>
                            <div class="sub_show payment_method_b">
                                {RS:Taikhoan}
                            </div>
                        </div>--%>

                        <div class="title-user">CHỌN CÁCH THỨC NHẬN HÀNG</div>
                        <div class="form-group">
                            <label class="radioPure">
                                <input id="gh_method_c" type="radio" name="gh_method" value="c" checked><span class="outer"><span class="inner"></span></span><i>Giao hàng tận nơi</i></label>
                            <label class="radioPure">
                                <input id="gh_method_d" type="radio" name="gh_method" value="d"><span class="outer"><span class="inner"></span></span><i>Nhận hàng tại showroom</i></label>
                            <div class="sub_show gh_method_d">
                                <div class="col-pd">
                                    <ul class="ul-b">
                                        <li>
                                            <label class="radioPure">
                                                <input type="radio" name="radio" value=""><span class="outer"><span class="inner"></span></span><i> 205 hữu hưng, Tây mỗ, Nam từ liêm, Hà Nội, Việt Nam</i></label></li>
                                        <%--<li>
                                            <label class="radioPure">
                                                <input type="radio" name="radio" value=""><span class="outer"><span class="inner"></span></span><i>111 Hoàng Quốc Việt, Nghĩa Đô, Cầu Giấy, Hà Nội, Việt Nam</i></label></li>--%>
                                    
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="itemCheckBox">
                                <input type="checkbox" id="is_xhd" name="Invoice" value="1"<%=item.Invoice ? "checked" : "" %>>
                                <i class="check-box"></i>
                                <span>Xuất hóa đơn công ty</span>
                            </label>
                        </div>

                        <div id="xhd-group">
                            <div class="form-group">
                                <input class="form-control" type="text"  autocomplete="off" name="CompanyName" value="<%=item.CompanyName %>" placeholder="Tên công ty">
                            </div>
                            <div class="form-group">
                                <input class="form-control" type="text" autocomplete="off" name="CompanyAddress" value="<%=item.CompanyAddress %>"" placeholder="Địa chỉ công ty">
                            </div>
                            <div class="form-group">
                                <input class="form-control" type="text" autocomplete="off" name="CompanyTax" value="<%=item.CompanyTax %>"" placeholder="Mã số thuế">
                            </div>
                        </div>

                        <div class="shockbuttonbox">
                            <a href="javascript:void(0)" class="buy_now btnOrder" id="btnOrder"><b>Thanh toán</b> <span>(Xem hàng, không mua không sao)</span> </a>
                        </div>
                    </div>
                </form>
            </div>
        </div>




        <%--<div class="orderbox">
                        <div class="navigate"><a href="" title="">Tiếp tục mua hàng</a><label>Giỏ hàng của bạn</label></div>
                        <div class="boxFormPayment">
                            <h3 class="checkbox9">
                                <i class="fa fa-check-circle"></i> QUÝ KHÁCH ĐÃ ĐẶT HÀNG THÀNH CÔNG
                                <p class="thankyouText">Cảm ơn quý khách đã đặt mua sản phẩm của chúng tôi ! <br> Đơn hàng của quý khách sẽ được nhân viên kiểm tra và giao hàng trong thời gian sớm nhất.</p>
                            </h3>
                            <div class="shockbuttonbox">
                                <a href="" class="buy_ins"><i class="akr-Headericon_Cart"></i> Tiếp tục mua hàng </a>
                            </div>
                        </div>
                    </div>--%>
    </div>
</section>
<script type="text/javascript">
    function get_child(ParentID, SelectedID, id, p_option_first) {
        $.ajax({
            url: "/ajax/GetChild.html",
            data: "ParentID=" + ParentID + "&SelectedID=" + SelectedID,
            type: "post",
            success: function (data) {

                var content = data.Node1;
                if (content != "") {
                    $(id).html(p_option_first+content);
                }
                else {
                    $(id).html("");
                }
            }
        });
    }


</script>


