<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var item = ViewBag.Data as ModOrderEntity;
    var model = ViewBag.Model as MViewCartModel;
    var cart = new Cart();
    long total = 0;
%>
<section class="contact-img-area">
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center">
                <div class="con-text">
                    <h2 class="page-title">Giỏ hàng</h2>
                    <p><a href="#">trang chủ</a> Giỏ hàng</p>
                </div>
            </div>
        </div>
    </div>
</section>

<div class="checkout-area">
    <div class="container">
        <div class="row">
            <div class="col-md-5 col-sm-12">
                <div class="ro-checkout-summary">
                    <div class="ro-title">
                        <h3 class="checkbox9">Giỏ hàng</h3>
                    </div>
                    <%for (int i = 0; i < cart.GetProduct().Count; i++)
                        {
                            var product = ModProductService.Instance.GetByID(cart.Items[i].ProductID);

                            if (product == null) continue;

                            var gift = ModGiftService.Instance.GetByID(cart.Items[i].GiftID);

                            total += cart.Items[i].Quantity * product.PriceSale;

                            string url = ViewPage.GetURL(product.Code);
                    %>


                    <div class="ro-body">
                        <div class="ro-item">
                            <div class="ro-image">
                                <a href="#">
                                    <img src="<%=Utils.GetResizeFile(product.File,4,500,500) %> " alt="">
                                </a>
                                <a href="javascript:void(0);" class="clickDele" onclick="delete_cart('<%=i%>')"><%--<i class="fa fa-trash"></i>--%>  xóa</a>
                            </div>
                            <div>
                                <div class="tb-beg">
                                    <a href="#"><%=product.Name %> </a>
                                </div>
                            </div>
                            <div>
                                <div class="ro-price">
                                    <span class="amount"><%=string.Format("{0:#,##0}", product.Price)%>₫</span>
                                </div>
                                <div class="ro-quantity ray">

                                    <input class="input-text qty text" onchange="updatecart('<%=i%>',this.value, '<%=model.returnpath %>')" type="number" title="Số lượng" value="<%=cart.Items[i].Quantity%>" min="1" step="1">
                                </div>

                                <div class="product-total">
                                    <span class="amount"><%=string.Format("{0:#,##0}", product.Price*cart.Items[i].Quantity)%>₫</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%} %>
                    <div class="ro-footer">
                        <div>
                            <p>
                                Tạm tính                                       
                                <span>
                                    <span class="amount"><%=string.Format("{0:#,##0}",total )%>₫</span>
                                </span>
                            </p>
                            <div class="ro-divide"></div>
                        </div>

                        <%--<div class="shipping">
                            <p>Phí giao hàng </p>
                            <div class="ro-shipping-method">
                                <p>
                                    Miễn phí
                                       
                                </p>
                            </div>
                            <div class="clearfix"></div>
                            <div class="ro-divide"></div>
                        </div>--%>
                        <div>
                            <p>
                                Tổng tiền cần thanh toán                                       
                                <span>
                                    <strong>
                                        <span class="amount"><%=string.Format("{0:#,##0}",total )%>₫

                                        </span>
                                    </strong>
                                </span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-7 col-sm-12">
                <div class="text">
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class=" ano complete">
                            <a href="#profile" aria-controls="home" role="tab" data-toggle="tab"></a>
                            <span>Địa chỉ</span>
                        </li>
                        <li role="presentation" class="ano active  ">
                            <a href="#profile" aria-controls="profile" role="tab" data-toggle="tab"></a>
                            <span>Vận chuyển</span>
                        </li>
                        <%--                        <li role="presentation" class="ano la">
                            <a href="#message" aria-controls="message" role="tab" data-toggle="tab"></a>
                            <span>Thông báo</span>
                        </li>--%>
                    </ul>


                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane" id="profile" style="display:block;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="top-check-text">
                                        <div class="check-down">
                                            <h3 class="checkbox9">Hình thức nhận hàng</h3>
                                        </div>
                                    </div>
                                    <form method="post" class="all-payment">
                                        <div class="all-paymet-border">
                                            <div class="payment-method">
                                                <div class="pay-top sin-payment">
                                                    <input id="payment_method_1" class="input-radio" type="radio" value="1" checked="checked" name="Payment">
                                                    <label for="payment_method_1">Thanh toán khi nhận hàng - COD </label>

                                                    <div class="payment_box payment_method_bacs">
                                             
                                                        <p class="form-tt">
                                                            <label>Địa chỉ:</label><input type="text" name="Address" placeholder="Nhập địa chỉ nhận hàng" value="">
                                                        </p>
                                                       
                                                    </div>
                                                </div>

                                                <div class="pay-top sin-payment">
                                                    <input id="payment_method_2" class="input-radio" type="radio" value="2" name="Payment">
                                                    <label for="payment_method_2">Chuyển khoản</label>
                                                    <div class="payment_box payment_method_bacs">
                                                       {RS:Webbank}
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row place-order">
                                                <input class="button alt" type="submit" name="_vsw_action[TransportPost]" value="Đặt hàng">
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
</div>






