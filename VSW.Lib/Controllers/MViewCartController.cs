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