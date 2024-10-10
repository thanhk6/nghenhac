using System;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Quên mật khẩu", Code = "MForgot", Order = 50)]
    public class MForgotController : Controller
    {
        public void ActionIndex(MForgotModel model)
        {
            ViewBag.Model = model;

            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = "http://" + ViewPage.Request.Url.Host + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }
        public void ActionAddPOST(MForgotModel model)
        {
            if (!Utils.IsEmailAddress(model.Email.Trim()))
            {
                ViewPage.Alert("Nhập địa chỉ email.");
                return;
            }
            var webUser = ModWebUserService.Instance.GetByEmail(model.Email);
            if (webUser == null)
            {
                ViewPage.Alert("Chúng tôi không tìm thấy thông tin về tài khoản bạn yêu cầu.");
                return;
            }

            var tempPassword = Global.Random.GetRandom(5);
            var tempPasswordMd5 = Security.Md5(tempPassword);

            webUser.TempPassword = tempPasswordMd5;
            ModWebUserService.Instance.Save(webUser);

            var html = @"  <b>Chú ý: Đây là email trả lời tự động. Nếu muốn phản hồi - Quý khách vui lòng gửi email về địa chỉ <span style=""color:#f00"">" + WebResource.GetValue("Web_Email") + @"</span></b><br /><br /><br />
                                    <b>THÔNG TIN ĐĂNG NHẬP</b><br /><br />
                                    <b>Email đăng nhập:</b> " + webUser.Email + @"<br />
                                    <b>Mật khẩu:</b> " + tempPassword + @"<br />
                                ";
            //gui mail
            #region send mail

            var domain = ViewPage.Request.Url.Host;
            var listEmail = webUser.Email.Trim();

            //gui mail cho quan tri va khach hang
            Mail.SendMail(
                listEmail,
                "noreply@gmail.com",
                domain,
                domain + "- Thông tin tài khoản - ngày " + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now),
                html
            );

            #endregion send mail

            //thong bao
            ViewPage.Alert("Chúng tôi đã khởi tạo lại mật khẩu mới.<br /> Bạn hãy kiểm tra email để nhận thông tin tài khoản.");
        }
    }

    public class MForgotModel
    {
        public string Email { get; set; }
    }
}