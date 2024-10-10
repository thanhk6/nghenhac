<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var item = ViewBag.Data as ModOrderEntity;
    var model = ViewBag.Model as MViewCartModel;

    var listAddress = ModAddressService.Instance.CreateQuery().Where(o => o.Activity == true).OrderByAsc(o => o.ID).ToList_Cache();

    var cart = new Cart();

    long total = 0;
%>

<section class="sec-main-content">
       <form method="post" name="cart_form" id="cart_form">
    <div class="container">        
        <div class="box-thongtin-sp">
            <div class="table-responsive">
                <table cellpadding="0" border="0" class="table_cart">
                    <tbody>
                        <%for (int i = 0; cart.Items != null && i < cart.Items.Count; i++)
                            {
                                var product = ModProductService.Instance.GetByID(cart.Items[i].ProductID);
                                if (product == null) continue;
                                long price = 0;
                                price = product.Price;

                                total += cart.Items[i].Quantity * price;
                                string url = ViewPage.GetURL(product.Code);
                        %>
                        <tr>
                            <td class="product_cart" width="75">
                                <a href="#">
                                    <img src="<%=Utils.GetResizeFile(product.File, 4, 245, 245) %>" style="border: solid 1px #ccc;">
                                </a>
                                <p class="text-center">
                                  <button type="button" class="btn btn-sm btn-danger" onclick="delete_cart('<%=i %>')"><i class="fa fa-trash"></i><//button>
                                </p>
                            </td>
                            <td>
                                <a href="#"><b><%=product.Name%></b></a>
                                <div class="clear"></div>
                               <span style="text-decoration: line-through"><%=string.Format("{0:#,##0}", product.Price)%></span>đ
													<br>
                                <b class="red"><span id="price_pro_29353"><%=string.Format("{0:#,##0}", product.Price2)%></span>đ</b>
                                
                                <div class="cart-quantity">
                                    <span class="minus">-</span>
                                    <input class="listcart" data-i="<%=i%>" value="<%=cart.Items[i].Quantity%>" onchange="" size="15">
                                   
                                    <span class="plus">+</span>
                                </div>
                            </td>
                        </tr>                    
                        <tr>
                           
                        </tr>
                        <%} %>
                    </tbody>
                </table>


            </div>
        </div>
               
        <div class="box-form-info">
            <div class="row">
                <div class="col-md-6">
                    <h4 class="title red"><b class="red">1. Thông tin khách hàng</b></h4>
                 
                        <div class="form-group row">
                            <label class="control-label col-md-3" for="hoten">Họ tên*:</label>
                            <div class="col-md-9">
                                <input type="text" class="form-control" name="Name" id="hoten" placeholder="nhập họ và tên">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="control-label col-md-3" for="telphone">Số điện thoại*:</label>
                            <div class="col-md-9">
                                <input type="text" class="form-control"  name="Phone" id="telphone" placeholder="Nhập số điện thoại liên lạc">
                            </div>
                        </div>
                        
                        <div class="form-group row">
                            <label class="control-label col-md-3" for="hoten">Yêu cầu khác*:</label>
                            <div class="col-md-9">
                                <input type="text" class="form-control" name="Content" value="<%=item.Content %>" id="diachi" placeholder="Địa chỉ cụ thể giao hàng">
                            </div>
                        </div>
                     <div class="form-group row">
                                <input type="text" class="form-control w30p security pull-left" name="ValidCode" id="ValidCode" required="required" autocomplete="off" value="" size="40" placeholder="Mã bảo mật" />
                                <img src="/ajax/Security.html" class="vAlignMiddle pl10" id="imgValidCode" alt="security code">
                                <a href="javascript:void(0)" onclick="change_captcha()" title="Tạo mã khác" rel="nofollow">
                                    <img src="/Content/images/icon-refreh.png" alt="refresh security code">
                                </a>
                            </div>
                      
                </div>
                <div class="col-md-6">
                    <div class="box-cachthucthanhtoan">
                        <h4 class="title red"><strong>2. Hình thức thanh toán</strong></h4>
                        <div class="radio">
                            <label id="cod">
                                <input type="radio"  name="Payment"  value="1">COD
                            </label>
                        </div>
                        <div class="radio">
                            <label id="atm">
                                <input type="radio"  name="Payment" value="2">Chuyển khoản
                            </label>
                            <div class="sub_show">
                                Quý khách chuyển khoản trước theo thông tin dưới đây:
                                <br>
                                Công Ty Cổ Phần Máy Tính TRANAN COMPUTER
                                <br>
                                Địa chỉ: Đường 196, Phố Nối, thị trấn Bần Yên Nhân, huyện Mỹ Hào, tỉnh Hưng Yên
                                <br>
                                MST: 0901007970
                                <br>
                                ACB chi nhánh Hà Nội - 14856879
                                <br>
                                VCB chi nhánh Chương Dương - 154 100 153 7202
                            </div>
                        </div>                      
                    </div>
                </div>
            </div>
            <div class="text-center">
               <a href="javascript:void(0)" id="btnOrder" type="submit" onclick="document.cart_form.submit()" class="btn btn-danger payoffline choosepayment" rel="nofollow">Gửi đơn hàng</a>                  
                  <input type="hidden" name="_vsw_action[AddPOST]" />
                  <input type="submit" name="_vsw_action[AddPOST]" style="display: none" />
            </div>
        </div>
    </div>
            </form>
</section>
