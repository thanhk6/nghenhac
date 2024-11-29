using System;
using System.Drawing;
using VSW.Core.Web;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Đăng ký", Code = "MRegister", Order = 99)]
    public class MRegisterController : Controller
    {
        public void ActionIndex(ModWebUserEntity item, MRegisterModel model)
        {
            ViewBag.Data = item;
            ViewBag.Model = model;

            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = HttpRequest.Scheme + "://" + ViewPage.Request.Url.Host + Global.Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }

        public void ActionAddPOST(ModWebUserEntity item, MRegisterModel model)
        {
            if (!Global.Utils.IsEmailAddress(item.Email.Trim()))
                ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ email.");
            else if (ModWebUserService.Instance.GetByEmail(item.Email.Trim()) != null)
                ViewPage.Message.ListMessage.Add("Email đã tồn tại. Hãy chọn email khác.");

            if (item.Password.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Mật khẩu.");
            else if (item.Password.Trim().Length < 6 || item.Password.Trim().Length > 12)
                ViewPage.Message.ListMessage.Add("Mật khẩu phải từ 6-12 ký tự.");
            else if (item.Password.Trim() != model.Password2)
                ViewPage.Message.ListMessage.Add("Mật khẩu không đồng nhất.");

            if (item.Name.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Họ và tên.");

            if (item.Phone.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Số điện thoại.");

            string sVY = Core.Global.CryptoString.Decrypt(ViewPage.Session["CaptchaANGKORICH"].ToString()).Replace("CaptchaANGKORICH.Secure." + ViewPage.Request.UserHostAddress + "." + string.Format("yyyy.MM.dd.hh", DateTime.Now) + ".", string.Empty);
            string sValidCode = model.ValidCode.Trim();

            if (sVY == string.Empty || (sVY.ToLower() != sValidCode.ToLower()))
                ViewPage.Message.ListMessage.Add("Nhập mã an toàn.");

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
                item.ID = 0;
                item.Password = Security.Md5(item.Password.Trim());
                item.IP = HttpRequest.IP;
                item.Created = DateTime.Now;

                ModWebUserService.Instance.Save(item);

                WebLogin.SetLogin(item.ID, true);

               // ViewPage.AlertThenRedirect("Bạn đã đăng ký thành công.", "/");
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }
    }

    public class MRegisterModel
    {
        public string ValidCode { get; set; }
        public string Password2 { get; set; }
    }
}