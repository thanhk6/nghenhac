using System;
using System.Linq;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using Utils = VSW.Lib.Global.Utils;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: Giỏ hàng", Code = "MViewCart", Order = 50)]
    public class MViewCartController : Controller
    {
        public void ActionIndex(ModOrderEntity item, MViewCartModel model)
        {
            var cart = new Cart();
            if (cart.Items.Count < 1)
                ViewPage.Redirect("Giỏ hàng của bạn chưa có sản phẩm nào.", "/");
            ViewBag.Data = item;
            ViewBag.Model = model;
            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
            
            
        }
        //them gio hang
        public void ActionAdd(ModOrderEntity item, MViewCartModel model)
        {
            var cart = new Cart();
            var _item = cart.Find(new CartItem() { ProductID = model.ProductID });
            if (_item == null)
            {
                cart.Add(new CartItem
                {
                    ProductID = model.ProductID,
                    Quantity = 1
                });
            }
            else _item.Quantity++;
            cart.Save();
            ViewBag.Data = item;
            ViewBag.Model = model;
            ViewPage.Response.Redirect( ViewPage.ViewCartURL + "?returnpath=" + model.returnpath);
        }
        public void ActionDelete(ModOrderEntity item, MViewCartModel model)
        {
            var cart = new Cart();

            cart.Remove(cart.Items[model.Index]);
            cart.Save();

            ViewBag.Data = item;
            ViewBag.Model = model;

            ViewPage.Response.Redirect( ViewPage.ViewCartURL + "?returnpath=" + model.returnpath);
        }
        public void ActionUpdate(ModOrderEntity item, MViewCartModel model)
        {
            var cart = new Cart();
            var product = ModProductService.Instance.GetByID(cart.Items[model.Index].ProductID);

            cart.Items[model.Index].Quantity = model.Quantity;


            
            //cart.Remove(cart.Items[model.Index]);
            cart.Save();

            ViewBag.Data = item;
            ViewBag.Model = model;

            ViewPage.Response.Redirect( ViewPage.ViewCartURL + "?returnpath=" + model.returnpath);

        }

        public void ActionTransportPost(ModOrderEntity item, MViewCartModel model)
        {
            item = ObjectCookies<ModOrderEntity>.GetValue("Orders");

            TryUpdateModel(item);
            if (item.Payment == null)
                ViewPage.Message.ListMessage.Add("Chọn: Hình thức thanh toán.");
            //hien thi thong bao loi
            if (ViewPage.Message.ListMessage.Count > 0)
            {
                string message = string.Empty;
                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                    message += ViewPage.Message.ListMessage[i] + "<br />";

                ViewPage.Alert(message);
            }
            else
            {
                if (item.Payment == "1")
                {
                    item.Payment = "thanh toán khi nhận hàng";

                }
                if (item.Payment == "2")

                {
                    item.Payment = "chuyển khoản";
                }
                ObjectCookies<ModOrderEntity>.SetValue("Orders", item);

                ViewPage.Response.Redirect(ViewPage.ViewCartURL + "?step=3");
            }
            ViewBag.Data = item;
            ViewBag.Model = model;
        }
        public void ActionAddPOST(ModOrderEntity item, MViewCartModel model)
        {
            if (item.Name.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Họ và tên.");

            //if (!Utils.IsEmailAddress(item.Email.Trim()))
            //    ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ Email.");

            if (item.Phone.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Số điện thoại.");

            if (ViewPage.Message.ListMessage.Count > 0)
            {
                string message = string.Empty;
                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                    message += ViewPage.Message.ListMessage[i] + "<br />";

                ViewPage.Alert(message);
            }
            else
            {

                item.IP = Core.Web.HttpRequest.IP;

                item.Created = DateTime.Now;
                item.Code = "DH" + string.Format("{0:ddMMyyyy}", item.Created) + GetOrder();

                ObjectCookies<ModOrderEntity>.SetValue("Orders", item);

                ViewPage.Response.Redirect(ViewPage.ViewCartURL + "?step=2");
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }
        public void ActionOrder_Success(ModOrderEntity item, MViewCartModel model)
        {
            //string sVY = Core.Global.CryptoString.Decrypt(Core.Web.Session.GetValue("CaptchaANGKORICH").ToString()).Replace("CaptchaANGKORICH.Secure." + ViewPage.Request.UserHostAddress + "." + string.Format("yyyy.MM.dd.hh", DateTime.Now) + ".", string.Empty);
            //string sValidCode = model.ValidCode.Trim();

            //if (sVY == string.Empty || (sVY.ToLower() != sValidCode.ToLower()))
            //    ViewPage.Message.ListMessage.Add("Nhập: Mã bảo mật.");

            //hien thi thong bao loi
            var cart = new Cart();
            if (cart.Items.Count < 1)
            {
                ViewPage.Redirect("Giỏ hàng của bạn chưa có sản phẩm nào.", "/");
            }
            else
            {
                item = ObjectCookies<ModOrderEntity>.GetValue("Orders");
                //luu chi tiet don hang & send mail
                long total = 0;
                var html = @"   <b>Chú ý: Đây là email trả lời tự động. Nếu muốn phản hồi - Quý khách vui lòng gửi email về địa chỉ <span style=""color:#f00"">" + WebResource.GetValue("Web_Email") + @"</span></b><br /><br /><br />
                                <b>THÔNG TIN ĐƠN HÀNG</b><br /><br />
                                <b>Mã đơn hàng:</b> " + item.Code + @"<br />
                                <b>Ngày mua:</b> " + string.Format("{0:dd/MM/yyyy HH:mm}", item.Created) + @"<br />
                                <b>Địa chỉ IP:</b> " + item.IP + @"<br /><br /><br />
                                <b>DANH SÁCH SẢN PHẨM</b><br /><br />";

                for (var i = 0; i < cart.Count; i++)
                {
                    var product = ModProductService.Instance.GetByID(cart.Items[i].ProductID);
                    if (product == null) continue;

                    ModOrderDetailService.Instance.Save(new ModOrderDetailEntity()
                    {
                        ID = 0,
                        OrderID = item.ID,
                        ProductID = product.ID,
                        Quantity = cart.Items[i].Quantity,
                        Price = product.Price,
                        Name = product.Name
                    });

                    total += product.Price * cart.Items[i].Quantity;

                    html += @"      <b>Số thứ tự:</b> " + (i + 1) + @"<br />
                                    <b>Sản phẩm:</b> " + product.Name + @"<br />
                                    <b>Số lượng:</b> " + cart.Items[i].Quantity + @"<br />
                                    <b>Giá tiền:</b> " + string.Format("{0:#,##0}", product.Price) + @" đ<br />
                                    <b>Thành tiền:</b> " + string.Format("{0:#,##0}", cart.Items[i].Quantity * product.Price) + @" đ<br /><br /><br />
                    ";
                }

                //item.Orders = _Orders;
                item.Total = total;

                ModOrderService.Instance.Save(item);

                html += @"<b>TỔNG TIỀN:</b> " + string.Format("{0:#,##0}", total) + @" đ<br /><br />";

                html += @"<b>THÔNG TIN KHÁCH HÀNG</b><br /><br />";

                if (!string.IsNullOrEmpty(item.Name)) html += "<b>Họ và tên:</b> " + item.Name + "<br />";
                if (!string.IsNullOrEmpty(item.Phone)) html += "<b>Điện thoại:</b> " + item.Phone + "<br />";
                if (!string.IsNullOrEmpty(item.Address)) html += "<b>Địa chỉ:</b> " + item.Address + "<br />";
                if (!string.IsNullOrEmpty(item.Content)) html += "<b>Yêu cầu khác:</b>" + item.Content.Replace("\n", "<br />") + "<br />";

                //gui mail
                #region send mail

               var domain = ViewPage.Request.Url.Host;

                // var listEmail = item.Email.Trim() +"," + WebResource.GetValue("Web_Email");
                //var listEmail = WebResource.GetValue("Web_Email").Trim();

                 var listEmail = WebResourceService.Instance.CreateQuery().Where(o => o.Code == "Web_Email").ToSingle().Value;


                //gui mail cho quan tri va khach hang
                Mail.SendMail(
                 listEmail.Trim(),
                    "noreply@gmail.com",
                    domain,
                    domain + "- Thông tin đơn hàng - ngày " + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now),
                    html
                );

                #endregion send mail

                cart.RemoveAll();
                cart.Save();

                ViewPage.Redirect("Bạn đã mua hàng thành công.<br/> ");
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }
        #region private
        private static string GetOrder()
        {
            var maxId = ModOrderService.Instance.CreateQuery()
                                    .Max(o => o.ID)
                                    .ToValue()
                                    .ToInt();
            if (maxId <= 1) return "0000001";

            var result = string.Empty;
            for (var i = 1; i <= (7 - maxId.ToString().Length); i++)
            {
                result += "0";
            }

            return result + (maxId + 1);
        }

        #endregion private
    }
    public class MViewCartModel

    {
        
        public int Index { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; } = 1;

        public int[] ArrQuantity { get; set; }
        public string returnpath { get; set; }
        public string ValidCode { get; set; }
    }

}