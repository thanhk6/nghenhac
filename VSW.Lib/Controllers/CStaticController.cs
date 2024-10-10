using System;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Static", Code = "CStatic", IsControl = true, Order = 4)]
    public class CStaticController : Controller
    {
        //[Core.MVC.PropertyInfo("Default[MenuID-false|MenuID2-false],City[MenuID-true|MenuID2-false],SearchBox[MenuID-false|MenuID2-true]")]
        //public string LayoutDefine;

        public override void OnLoad()
        {
        }

        public void ActionAddPOST(MFeedbackModel1 model)
        {
            var item = new ModFeedbackEntity();
            item.Name = model.Name;
            item.Email = model.Email;
            item.Phone = model.Phone;
            if (model.Name.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Họ và tên.");

            if (!Global.Utils.IsEmailAddress(item.Email.Trim()))
                ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ email.");

            if (model.Phone.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Số điện thoại liên hệ.");
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
                item.IP = Core.Web.HttpRequest.IP;
                item.Created = DateTime.Now;

                ModFeedbackService.Instance.Save(item);
                //gui mail
                #region send mail

                var html = @"  <b>Chú ý: Đây là email trả lời tự động. Nếu muốn phản hồi - Quý khách vui lòng gửi email về địa chỉ <span style=""color:#f00"">" + WebResource.GetValue("Web_Email") + @"</span></b><br /><br /><br />";

                html += "<b>THÔNG TIN KHÁCH HÀNG</b><br /><br />";

                html += "<b>Họ và tên:</b> : " + item.Name + "<br />";
                html += "<b>Email:</b> : " + item.Email + "<br />";
                html += "<b>Điện thoại:</b> : " + item.Phone + "<br />";
                html += "<b>Nội dung:</b> : " + (!string.IsNullOrEmpty(item.Content) ? item.Content.Replace("\n", "<br />") : "") + "<br /><br /><br />";

                var domain = ViewPage.Request.Url.Host;
                var listEmail = item.Email.Trim() + "," + WebResource.GetValue("Web_Email");
                //gui mail cho quan tri va khach hang
                Mail.SendMail(
                    listEmail,
                    "noreply@gmail.com",
                    domain,
                    domain + "- Thông tin liên hệ - ngày " + $"{DateTime.Now:dd/MM/yyyy HH:mm}",
                    html
                );

                #endregion send mail
                //xoa trang
                item = new ModFeedbackEntity();
                ViewPage.Alert("Cảm ơn bạn đã liên hệ với chúng tôi.<br /> Chúng tôi sẽ phản hồi lại trong thời gian sớm nhất.");
            }
            ViewBag.Data = item;
            ViewBag.Model = model;
        }
    }
    public class MFeedbackModel1
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}