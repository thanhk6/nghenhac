using System;
using System.Linq;
using VSW.Core.Web;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using Setting = VSW.Lib.Global.Setting;
using Utils = VSW.Lib.Global.Utils;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Thanh toán", Code = "MCheckout", Order = 7)]
    public class MCheckoutController : Controller
    {
        public void ActionIndex(ModOrderEntity item, MCheckoutModel model)
        {
            var _Cart = new Cart();
            if (_Cart.Items.Count < 1) ViewPage.Alert("Giỏ hàng của bạn chưa có sản phẩm nào.", "/");

            ViewBag.Data = item;
            ViewBag.Model = model;

            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = "http://" + ViewPage.Request.Url.Host + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        } 
        
        public void ActionAddPOST(ModOrderEntity item, MCheckoutModel model)
        {
            if (item.Name.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Họ và tên.");

            //if (Utils.GetEmailAddress(item.Email.Trim()) == string.Empty)
            //    ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ Email.");

            if (item.Phone.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Số điện thoại.");
            
            if (item.Address.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ giao hàng.");

            string sVY = VSW.Core.Global.CryptoString.Decrypt(ViewPage.Session["CaptchaANGKORICH"].ToString()).Replace("CaptchaANGKORICH.Secure." + ViewPage.Request.UserHostAddress + "." + string.Format("yyyy.MM.dd.hh", DateTime.Now) + ".", string.Empty);
            string sValidCode = model.ValidCode.Trim();

            if (sVY == string.Empty || (sVY.ToLower() != sValidCode.ToLower()))
                ViewPage.Message.ListMessage.Add("Nhập mã an toàn.");

            //hien thi thong bao loi
            if (ViewPage.Message.ListMessage.Count > 0)
            {
                var message = ViewPage.Message.ListMessage.Aggregate(string.Empty, (current, t) => current + ("- " + t + "<br />"));

                ViewPage.Alert(message);
            }
            else
            {
                item.IP = HttpRequest.IP;
                item.Created = DateTime.Now;
                item.Code = "DH" + string.Format("{0:ddMMyyyy}", item.Created) + GetOrder();

                ObjectCookies<ModOrderEntity>.SetValue("Orders", item);

                //ViewPage.Response.Redirect(ViewPage.CompleteURL);
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

    public class MCheckoutModel
    {
        public string ValidCode { get; set; }

        public string returnpath { get; set; }
    }
}