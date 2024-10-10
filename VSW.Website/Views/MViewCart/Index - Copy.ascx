<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var item = ViewBag.Data as ModOrderEntity;
    var model = ViewBag.Model as MViewCartModel;

    var cart = new Cart();

    long total = 0;
%>

<section class="Cartone">
    <div class="wrapcenter">

        <form method="post" name="cart_form">
            <div class="card shopping-cart">
                <div class="card-header bg-dark text-light text-center">ĐƠN ĐẶT HÀNG</div>
                <div class="card-body">
                    <div class="text-center mb20">
                        <p class="datedh all"><%=string.Format("{0:f}", DateTime.Now) %></p>
                    </div>
                    <table class="table table-striped table-my-order">
                        <thead>
                            <tr>
                                <th scope="col">Ảnh</th>
                                <th scope="col">Tên sản phẩm</th>
                                <th scope="col" style="width: 15%;" class="text-center">Số lượng</th>
                                <th scope="col" class="text-center">Đơn giá</th>
                                <th scope="col" class="text-center">Tổng đơn giá</th>
                                <th class="text-center">Tùy chọn</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%for (int i = 0; i < cart.Items.Count; i++){
                                    var product = ModProductService.Instance.GetByID(cart.Items[i].ProductID);
                                    if (product == null) continue;

                                    var listGift = product.GetGift();

                                    total += cart.Items[i].Quantity * product.Price;
                                    string url = ViewPage.GetURL(product.Code);
                            %>
                            <tr>
                                <td><img src="<%=Utils.GetResizeFile(product.File, 4, 100, 100) %>" class="img-thumb-small-table" alt="<%=product.Name %>"></td>
                                <td>
                                    <a href="<%=ViewPage.GetURL(product.Code) %>" class="titleprd"><%=product.Name %></a>
                                    <ul class="list-quatang">
                                        <%for (int g = 0; g < listGift.Count; g++){%>
                                        <li>-&nbsp;Tặng: <%=listGift[g].Name %> <%if (listGift[g].Price > 0){%>trị giá <b><%=string.Format("{0:#,##0}", listGift[g].Price) %>₫</b><%} %></li>
                                        <%} %>
                                    </ul>
                                </td>
                                <td>
                                    <input type="number" class="form-control text-center" min="1" max="9999" onchange="update_cart('<%=i %>', this.value, '<%=model.returnpath %>')" value="<%=cart.Items[i].Quantity %>" />
                                </td>

                                <%if (product.Price > 0){%>
                                <td class="text-center"><%=string.Format("{0:#,##0}", product.Price) %><sup>₫</sup></td>
                                <td class="text-center"><%=string.Format("{0:#,##0}", product.Price * cart.Items[i].Quantity) %><sup>₫</sup></td>
                                <%}else {%>
                                <td class="text-center">Liên hệ</td>
                                <td class="text-center">Liên hệ</td>
                                <%} %>

                                <td class="text-center">
                                    <button type="button" class="btn btn-sm btn-danger" onclick="delete_cart('<%=i %>', '<%=model.returnpath %>')"><i class="fa fa-trash"></i></button>
                                </td>
                            </tr>
                            <%} %>
                            <tr>
                                <td colspan="5">Đơn giá tạm tính</td>

                                <%if(total > 0) {%>
                                <td class="text-right"><%=string.Format("{0:#,##0}", total) %><sup>₫</sup></td>
                                <%}else {%>
                                <td class="text-right">Liên hệ</td>
                                <%} %>
                            </tr>
                            <tr>
                                <td colspan="5"><strong>Tổng tiền thanh toán</strong></td>

                                <%if(total > 0) {%>
                                <td class="text-right"><%=string.Format("{0:#,##0}", total) %><sup>₫</sup></td>
                                <%}else {%>
                                <td class="text-right">Liên hệ</td>
                                <%} %>
                            </tr>
                        </tbody>
                    </table>
                    <hr />

                    <div class="w50 pull-left">
                        <div class="form-info-muahang row">
                            <div class="form-group">
                                <div class="col-md-12 mb10">
                                    <h4 class="title">Thông tin khách hàng</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <strong>Họ và tên:</strong>
                                    <input type="text" class="form-control mt10" name="Name" value="<%=item.Name %>" placeholder="Họ tên đầy đủ người nhận hàng" />
                                </div>
                                <div class="span1"></div>
                                <div class="col-md-6 col-sm-6 col-xs-12">
                                    <strong>Số điện thoại:</strong>
                                    <input type="text" class="form-control mt10" name="Phone" value="<%=item.Phone %>" placeholder="số điện thoại liên hệ" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12 mb10"><strong>Yêu cầu khác</strong></div>
                                <div class="col-md-12 mb10">
                                    <input type="text" class="form-control" name="Content" value="<%=item.Content %>" placeholder="Bạn có thêm yêu cầu gì khác ? (Không bắt buộc)" />
                                </div>
                            </div>

                            <div class="form-group selectoder">
                                <div class="col-md-12">
                                    <p><b>Để được phục vụ nhanh hơn,</b> hãy chọn thêm:</p>
                                </div>
                                <div class="form-inline">
                                    <div class="radio radio-danger">
                                        <input type="radio" name="Formality" id="Shipping" <%if (!item.Formality){%>checked="checked"<%} %> value="0" />
                                        <label for="Shipping">&nbsp;Địa chỉ giao hàng</label>
                                    </div>
                                    <div class="radio radio-danger">
                                        <input type="radio" name="Formality" id="Showroom" <%if (item.Formality){%>checked="checked"<%} %> value="1" />
                                        <label for="Showroom">&nbsp;Đến Showroom xem hàng</label>
                                    </div>
                                </div>
                                <div class="boxAdr ul-menu-muiten Shipping">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <textarea class="form-control" name="Address" rows="5" placeholder="Số nhà, tên đường, Phường / Xã"><%=item.Address %></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="boxAdr ul-menu-muiten Showroom">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="list-goShowroom" id="locationCart"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <div class="w50 pull-left">
                        <div class="form-info-muahang">
                            <div class="form-group">
                                <div class="col-md-12 mb10">
                                    <h4 class="title">Hình thức thanh toán</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="radio radio-primary">
                                        <input type="radio" name="Payment" id="Direct" value="1" />
                                        <label for="Direct">Nhận hàng và thanh toán</label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="radio radio-primary">
                                        <input type="radio" name="Payment" id="Amortization" value="2" />
                                        <label for="Amortization">Thanh toán trả góp (<a href="#" rel="nofollow">Chính sách trả góp</a>)</label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="radio radio-primary">
                                        <input type="radio" name="Payment" id="ATM" value="3">
                                        <label for="ATM">Chuyển khoản qua ngân hàng (<a href="#bank-box" data-toggle="collapse" rel="nofollow">Xem chi tiết</a>)</label>
                                    </div>
                                    <VSW:StaticControl Code="CStatic" VSWID="vswStaticBank" DefaultLayout="Bank" runat="server" />
                                </div>
                            </div>
                            <div class="form-group col-md-12">
                                <input type="text" class="form-control w30p security pull-left" name="ValidCode" id="ValidCode" required="required" autocomplete="off" value="" size="40" placeholder="Mã bảo mật" />
                                <img src="/ajax/Security.html" class="vAlignMiddle pl10" id="imgValidCode" alt="security code">
                                <a href="javascript:void(0)" onclick="change_captcha()" title="Tạo mã khác" rel="nofollow">
                                    <img src="/Content/pc/images/icon-refreh.png" alt="refresh security code">
                                </a>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <a href="/" class="btn pull-left btn-stop-order btn-order" rel="nofollow"><i class="fa fa-times-circle"></i>&nbsp;Hủy mua hàng</a>
                    <a href="javascript:void(0)" onclick="document.cart_form.submit()" class="btn btn-order pull-right btn-next-order"><i class="fa fa-shopping-cart"></i>&nbsp;Xác nhận đặt hàng</a>

                    <input type="hidden" name="_vsw_action[AddPOST]" />
                    <input type="submit" name="_vsw_action[AddPOST]" style="display: none" />
                </div>

            </div>
        </form>
    </div>
</section>

<div style="display: none">
    <VSW:StaticControl Code="CAddress" VSWID="vswAdressMiddleDetail" DefaultLayout="MiddleDetail" runat="server" />
</div>