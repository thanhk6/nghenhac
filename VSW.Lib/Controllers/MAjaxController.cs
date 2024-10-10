using System;
using System.Drawing.Imaging;
using VSW.Core.Global;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: MAjax", Code = "MAjax", Order = 10, Activity = false)]
    public class MAjaxController : Controller
    {
        public void ActionIndex()
        {

        }
        public void ActionUpdateCart(UpdateCardModel model)
        {
            var json = new Json();

            if (model.Quantity < 1) model.Quantity = 1;

            var cart = new Cart();
            cart.Items[model.Index].Quantity = model.Quantity;
            cart.Save();
            //long total = 0;
            //long pricecurrent = 0;
            //for (int i = 0; i < cart.Items.Count; i++)
            //{
            //    var product = ModProductService.Instance.GetByID(cart.Items[i].ProductID);
            //    if (product == null) continue;
                         
            //    total += product.Price * cart.Items[i].Quantity;
            //    //if (i == model.Index) pricecurrent = t;
            //}

            //json.Instance.Node1 = pricecurrent.ToString();

            //json.Instance.Node2 = total.ToString();

            //json.Instance.Node3 = Utils.FormatMoney(total) + "₫";

            json.Create();
        }

        public void ActionSecurity(SecurityModel model)
        {
            var json = new Json();

            Captcha.CaseSensitive = false;
            var ci = new Captcha(175, 35);
            ViewPage.Response.Clear();
            ViewPage.Response.ContentType = "image/jpeg";
            ci.Image.Save(ViewPage.Response.OutputStream, ImageFormat.Jpeg);
            ci.Dispose();
            ViewBag.Model = model;
            ViewPage.Response.End();
        }

        public void ActionGetChild(GetChildModel model)
        {
            var json = new Json();
            if (model.ParentID >0)
            {     
                var listItem = WebMenuService.Instance.CreateQuery()
                                    .Select(o => new { o.ID, o.Name })
                                    .Where(o => o.Activity == true && o.Type == "City" && o.ParentID == model.ParentID)
                                    .OrderByAsc(o =>new { o.Order})
                                    .ToList_Cache();
           // json.Instance.Node1 += @"<option value=""0"" selected=""selected"">- chọn quận / huyện -</option>";

            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                json.Instance.Node1 += @"<option value=""" + listItem[i].ID + @""" " + (listItem[i].ID == model.SelectedID ? @"selected=""selected""" : @"") + @">" + listItem[i].Name + @"</option>";
            }
        }
            ViewBag.Model = model;
            json.Create();
        }

        public void ActionGetSearch(GetSearchModel model)
        {
            var json = new Json();
            string keyword = !string.IsNullOrEmpty(model.Keyword) ? Data.GetCode(model.Keyword) : string.Empty;
            var listItem = ModProductService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true)
                                    .Where(!string.IsNullOrEmpty(keyword), o => (o.Model.Contains(keyword) || o.Code.Contains(keyword)) || o.Name.Contains(keyword))
                                    .OrderByDesc(o => new { o.Order, o.ID })
                                    .Take(10)
                                    .ToList_Cache();
            for (int i = 0; listItem != null && i < listItem.Count; i++)
            {
                string url = ViewPage.GetURL(listItem[i].Code);
                json.Instance.Node1 += @"<div class=""search-item"">
                                                <a href=""" + url + @""" title=""" + listItem[i].Name + @""">
                                                    <img src=""" + Utils.GetResizeFile(listItem[i].File, 4, 500, 500) + @""" alt=""" + listItem[i].Name + @""" />
                                                </a>
                                                <div class=""bg-virtual"">
                                                    <a href=""" + url + @""" title=""" + listItem[i].Name + @"""><p>" + listItem[i].Name + @"</p></a>
                                                </div>
                                            </div>";
            }
            ViewBag.Model = model;
            json.Create();
        }
        public void ActionSubscribe(ModSubscribeEntity item)
        {
            var json = new Json();

            TryUpdateModel(item);

            if (item.Gender.Trim() == string.Empty)
            {
                json.Instance.Node2 += "Bạn chưa chon giới tính </br>";

            }
            if (item.Name.Trim() == string.Empty)
            {
                json.Instance.Node2 += "Bạn chưa nhập tên </br>";

            }
            if (item.Phone == string.Empty)
            {
                json.Instance.Node2 += "Bạn chưa nhập sô điện thoại</br>";

            }

            if (item.address.Trim() == string.Empty)
            {

                json.Instance.Node2 += "Bạn chưa nhập địa chỉ công trình";
            }
            //if (item.Email.Trim() == string.Empty)
            //{
            //    json.Instance.Node2 += "Ban chưa nhâp Email</br>";
            //}
            //if (!Utils.IsEmailAddress(item.Email))
            //{
            //    json.Instance.Node2 += "Địa chỉ email không đúng định dạng</br>";

            //}
            //try
            //{
            //    string path = HttpContext.Server.MapPath("~") + "Files";
            //    string file = Request.Files[0].FileName;
            //    Request.Files[0].SaveAs(path + "\\" + file);
            //    var fileInfo = new FileInfo(path + file);
            //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            //}
            //catch { }

            //return Json(new { success = false }, JsonRequestBehavior.AllowGet);


            var posted1 = ViewPage.Request.Files;

            if (posted1 == null)
            {
                json.Instance.Node2 += "bạn chưa chon file sản phẩn";
            }
            if (posted1 != null && posted1.Count > 0)
            {
                var posted = ViewPage.Request.Files[0];
                string fileName = posted.FileName;
                if (fileName.Length > 1)
                {
                    if (posted.ContentLength > 5 * 1024 * 1024)
                    {
                        json.Instance.Node2 += ("dung lương không quá 10mb");
                    }
                    else
                    {
                        string path = ViewPage.Server.MapPath("~/Data/upload/files/CV/");
                        if (!System.IO.Directory.Exists(path))
                            System.IO.Directory.CreateDirectory(path);
                        string dbFile = "~/Data/upload/files/CV/" + Data.GetCode(System.IO.Path.GetFileNameWithoutExtension(fileName)) + System.IO.Path.GetExtension(fileName);
                        string file = ViewPage.Server.MapPath(dbFile);
                        posted.SaveAs(file);
                        item.CvFile = dbFile;
                    }
                }
            }


            if (ViewPage.Message.ListMessage.Count > 0)
            {
                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                {
                    string Messger = string.Empty;
                    Messger += ViewPage.Message.ListMessage[i] + "</br>";
                    json.Instance.Node2 += Messger;
                }
            }
            else
            {
                item.ID = 0;
                item.IP = Core.Web.HttpRequest.IP;
                item.Created = DateTime.Now;
                ModSubscribeService.Instance.Save(item);
            }

            //try
            //{
            //    var html = @" đây là sô phone liên hê mua giảm giá </br> sô phone:" + item.Phone + @" <h3> </h3>";
            //    var domain = ViewPage.Request.Url.Host;
            //    //var listEmail = WebResourceService.Instance.GetByCode_Cache("Web_Email", ViewPage.CurrentLang.ID);

            //    //  var email = ((listEmail != null && !string.IsNullOrEmpty(listEmail.Value)) ? "," : "") + item.Email;
            //    var listEmail = WebResourceService.Instance.CreateQuery().Where(o => o.Code == "Web_Email").ToSingle().Value;
            //    //var webEmail = WebResource.GetValue("Web_Email");
            //    //var listEmail = item.Email.Trim() + (!string.IsNullOrEmpty(webEmail) ? ("," + webEmail) : string.Empty);
            //    //gui mail cho quan tri va khach hang

            //    Mail.SendMail(
            //        listEmail.Trim(),
            //        "noreply@gmail.com",
            //        domain,
            //        domain + "- thông tin liên hệ " + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now),
            //        html
            //    );

            //}
            //catch (Exception ex)
            //{
            //    Error.Write(ex.Message);
            //}
            json.Create();
        }
        public void ActionPopup(ModFeedbackEntity item)
        {
            var json = new Json();
            TryUpdateModel(item);
            if (item.Name == string.Empty)
            {
                json.Instance.Node2 += "Nhập họ tên đầy đủ";
                json.Create();
            }
            if (item.Phone == string.Empty)
            {
                json.Instance.Node2 += "Nhập số điện thoại";
                json.Create();
            }
            if (item.Email == string.Empty)
            {
                json.Instance.Node2 += "Bạn chưa nhập địa chỉ email";
                json.Create();
            }
            else if (!Utils.IsEmailAddress(item.Email))
            {
                json.Instance.Node2 += "Địa chỉ email không đúng định dạng";
                json.Create();
            }

            if (item.Address == string.Empty)
            {
                json.Instance.Node2 += "Nhập số lượng dự kiến đặt mua";
                json.Create();
            }

            item.ID = 0;
            item.IP = Core.Web.HttpRequest.IP;
            item.Created = DateTime.Now;

            ModFeedbackService.Instance.Save(item);

            json.Create();
        }
        public void ActionFeedback(ModFeedbackEntity item)
        {
            var json = new Json();

            TryUpdateModel(item);

            if (item.Name.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Họ và tên.");

            if (item.Phone.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Số điện thoại.");


            //if (!Utils.IsEmailAddress(item.Email.Trim()))
            //   ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ email.");
            if (item.Content.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Nội dung liên hệ.");

            if (ViewPage.Message.ListMessage.Count > 0)
            {
                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                    json.Instance.Node2 += ViewPage.Message.ListMessage[i] + "<br />";
            }
            else
            {
                item.ID = 0;
                item.IP = Core.Web.HttpRequest.IP;
                item.Created = DateTime.Now;
                ModFeedbackService.Instance.Save(item);
                //try
                //{
                //    #region send mail
                //    var html = @" đây là Email trả lời tự động";

                //    var domain = ViewPage.Request.Url.Host;
                //    var listEmail = WebResourceService.Instance.GetByCode_Cache("Web_Email", ViewPage.CurrentLang.ID);

                //    var email = ((listEmail != null && !string.IsNullOrEmpty(listEmail.Value)) ? "," : "") + item.Email;

                //    //var webEmail = WebResource.GetValue("Web_Email");
                //    //var listEmail = item.Email.Trim() + (!string.IsNullOrEmpty(webEmail) ? ("," + webEmail) : string.Empty);
                //    //gui mail cho quan tri va khach hang

                //    Mail.SendMail(
                //        email,
                //        "noreply@gmail.com",
                //        domain,
                //        domain + "- thông tin liên hệ " + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now),
                //        html
                //    );
                //    #endregion send mail
                //}
                //catch (Exception ex)
                //{
                //    Error.Write(ex.Message);
                //}



            }

            json.Create();
        }
        public void ActionCommentPOST(CommentPOSTModel model)
        {
            var json = new Json();
            //if (!WebLogin.IsLogin())
            //{
            //    json.Instance.Params += "Bạn phải đăng nhập mới có thể bình luận.";
            //    json.Create();
            //}

            if (model.ProductID < 1)
            {
                json.Instance.Node2 += "Sản phẩm không tồn tại.";
                json.Create();
            }

            if (model.Vote < 1)
            {
                json.Instance.Node2 += "Bạn chưa đánh giá số sao.";
                json.Create();
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                json.Instance.Node2 += "Nhập họ và tên.";
                json.Create();
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                json.Instance.Node2 += "Nhập Số điện thoại.";
                json.Create();
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                json.Instance.Node2 += "Nhập nội dung bình luận.";
                json.Create();
            }
            if (model.ParentID == 0)
            {
                //if (string.IsNullOrEmpty(model.Email))
                //{
                //    json.Instance.Node2 += "Nhập email.";
                //    json.Create();
                //}

                string sVY = Core.Global.CryptoString.Decrypt(ViewPage.Session["CaptchaANGKORICH"].ToString()).Replace("CaptchaANGKORICH.Secure." + ViewPage.Request.UserHostAddress + "." + string.Format("yyyy.MM.dd.hh", DateTime.Now) + ".", string.Empty);
                string sValidCode = model.ValidCode.Trim();
                if (sVY == string.Empty || (sVY.ToLower() != sValidCode.ToLower()))
                {
                    json.Instance.Node2 += "Nhập mã an toàn";
                    json.Create();
                }
            }

            ModCommentEntity _item = new ModCommentEntity();

            _item.ID = 0;
            _item.ParentID = model.ParentID;
            _item.ProductID = model.ProductID;
            _item.Vote = model.Vote;
            _item.Name = model.Name;
            _item.Email = model.Email;
            _item.Phone = model.Phone;
            _item.Content = model.Content;
            _item.Created = DateTime.Now;
            _item.Activity = false;

            ModCommentService.Instance.Save(_item);

            //if (_item.ParentID == 0)
            //{
            //    json.Instance.Html += @"<div class=""comment_ask"">
            //                            <i class=""iconcom-user"">" + Utils.GetLetters(_item.Name) + @"</i>
            //                            <strong>" + _item.Name + @"</strong>
            //                            <div class=""infocom_ask"">" + _item.Content.Replace("\n", "<br />") + @"</div>
            //                            <div class=""relate_infocom"">
            //                                <span class=""reply"">Trả lời</span>
            //                                <span class=""date""><b class=""dot"">●</b>" + _item.Time + @"</span>
            //                            </div>
            //                        </div>";
            //    json.Instance.Params = "Cảm ơn bạn đã đóng góp ý kiên!<br/> Chờ ban quản trị phê duyệt để hiển thị.";
            //}
            //else
            //    json.Instance.Html += @"<div class=""comment_reply"">
            //                            <i class=""arrow_box""></i>
            //                            <div class=""comment_ask"">
            //                                <i class=""iconcom-user"">" + Utils.GetLetters(_item.Name) + @"</i>
            //                                <strong>" + _item.Name + @"</strong>
            //                                <div class=""infocom_ask"">" + _item.Content.Replace("\n", "<br />") + @"</div>
            //                                <div class=""relate_infocom"">
            //                                    <span class=""reply"">Trả lời</span>
            //                                    <span class=""date""><b class=""dot"">●</b>" + _item.Time + @"</span>
            //                                </div>
            //                            </div>
            //                            <div class=""clear""></div>
            //                        </div>";
            json.Instance.Node2 += "Cảm ơn bạn đã đóng góp ý kiên!.<br/> Chờ ban quản trị phê duyệt để hiển thị.";
            ViewBag.Model = model;
            json.Create();
        }
        public static string GetOrder()
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
       
        
      
        public void ActionRegistration(ModWebUserEntity item, Mregister model)
        {
            var json = new Json();
            if (string.IsNullOrEmpty(item.UserName))
            {
                ViewPage.Message.ListMessage.Add(" yêu cầu nhập tên đăng nhập");
            }
            if (string.IsNullOrEmpty(item.Password))
            {
                ViewPage.Message.ListMessage.Add("bạn chưa nhập mật khâu");
            }
            if (string.IsNullOrEmpty(model.Password2))
            {
                ViewPage.Message.ListMessage.Add("nhâp lại mât khẩu");
            }
            if (ViewPage.Message.ListMessage.Count > 0)
            {
                string messger = string.Empty;
                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                {

                    messger = ViewPage.Message.ListMessage[i] + "</br>";
                    json.Instance.Node1 += messger;
                }
            }
            else
            {
                item.ID = 0;
                item.IP = Core.Web.HttpRequest.IP;
                item.Password = Security.Md5(item.Password.Trim());
                item.Created = DateTime.Now;
                ModWebUserService.Instance.Save(item);
                WebLogin.SetLogin(item.ID, true);
            }
            json.Create();
        }
        public void ActionLogin(MLoginModel model)
        {
            ViewBag.Model = model;
            var json = new Json();
           
            var webUser = ModWebUserService.Instance.GetForLogin(model.Name, Security.Md5(model.Password));
            if (webUser == null)
            {
                json.Instance.Node1 = "Bạn chưa đăng nhập";
            }
            WebLogin.SetLogin(webUser.ID, model.IsSave);
            //Core.Global.Support.Clear();
            json.Instance.Node2 = "Success";
            json.Instance.Node3 = string.IsNullOrEmpty(model.returnpath) ? "/" : model.returnpath;
            json.Create();
        }
        public class SecurityModel
        {
            public string Code { get; set; }
        }
        public class GetSearchModel
        {
            public string Keyword { get; set; }
        }
        public class GetChildModel
        {
            public int ParentID { get; set; }
            public int SelectedID { get; set; }
        }
        public class CommentPOSTModel
        {
            public string ValidCode { get; set; }
            public string XNCode { get; set; }
            public int ParentID { get; set; }
            public int ProductID { get; set; }
            public int Vote { get; set; }

            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Content { get; set; }
        }
        public class QuickViewModel
        {
            public int ProductID { get; set; }
        }

        public class CheckoutModel
        {

        }
        public class Mregister
        {
            public string Password2 { get; set; }
        }
        public class MLoginModel
        {
            public int UserType { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public bool IsSave { get; set; }
            public string returnpath { get; set; }
        }
        public class MGetChildModel
        {
            public int id;
        }
        public class UpdateCardModel
        {
            public int Index { get; set; }
            public int Quantity { get; set; }
        }
    }
}


