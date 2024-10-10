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
        public void ActionQuickView(QuickViewModel model)
        {

            var json = new Json();
            var tongdai = WebResourceService.Instance.CreateQuery().Where(o => o.Code == "Tongdaituvan").ToSingle().Value;

            var Web_Hotline = WebResourceService.Instance.CreateQuery().Where(o => o.Code == "hotline").ToSingle().Value;

            if (model.ProductID < 0)
            {
                ViewBag.Model = model;
                json.Create();
            }

            var item = ModProductService.Instance.GetByID(model.ProductID);
            if (item == null)
            {

                ViewBag.Model = model;
                json.Create();
            }
            else
            {
                var listFile = item.GetFile();
                json.Instance.Node1 += @"
                                        <div class=""modal-footer"" data-dismiss=""modal"">
                                            <span>x</span>
                                        </div>
                                        <div class=""row"">

                                            <div class=""col-xs-12 col-sm-6"">
                                                <div class=""quick-image"">
                                                    <div class=""single-quick-image tab-content text-center"">";
                for (int i = 0; listFile != null && i < listFile.Count; i++)
                {
                    string active;
                    switch (i)
                    {
                        case 0:
                            active = "active";
                            break;
                        default:
                            active = "";
                            break;

                    }
                    json.Instance.Node1 += @"<div class=""tab-pane fade in " + active + @" "" id=""" + i + 1 + @""">
                                                            <img src = """ + listFile[i].File.Replace("~/", "/") + @""" alt="""" />
                                                        </div>";
                }
                json.Instance.Node1 += @"</div>
                                                    <div class=""quick-thumb"">
                                                        <div class=""nav nav-tabs"">
                                                            <ul>";
                for (int j = 0; listFile != null && j < listFile.Count; j++)
                {

                    json.Instance.Node1 += @"<li><a data-toggle=""tab"" href=""#" + j + 1 + @" "">
                                                                    <img src=""" + listFile[j].File.Replace("~/", "/") + @""" alt=""quick view"" />        
                                                                             </a></li>";
                }

                json.Instance.Node1 += @"</ul></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class=""col-xs-12 col-sm-6"">
                                                <div class=""quick-right"">
                                                    <div class=""quick-right-text"">
                                                        <h3>" + item.Name + @"</h3>
                                                        <div class=""product-sets"">
                                                            <div class=""pro-rating cendo-pro"">
                                                                <div class=""pro_one"">
                                                                    <div class=""tf-stars tf-stars-svg""><span style = ""width: 86% !important"" class=""tf-stars-svg""></span></div>
                                                                </div>
                                                                <p class=""rating-links"">
                                                                    <a href=""#"" > 4.5 </ a > (14 reviews)
															
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <div class=""descr-text"">
                                                            <ul>" + item.Summary + @"
                                                                
                                                            </ul>
                                                        </div>
                                                        <div class=""productdecor-price"">
                                                            <strong class=""price"">
                                                                <del><span>" + string.Format("{0:#,##0}", item.Price2) + @" VNĐ</span></del>
                                                                <span>" + string.Format("{0:#,##0}", item.Price) + @" VNĐ</span>
                                                            </strong>
                                                        </div>
                                                        <div class=""product-sets"">
                                                            <div class=""qty-block"">
                                                                <fieldset id = ""product-actions-fieldset"" >
                                                                    <button class=""btn buy_now"" type=""button"" data-id=""" + item.ID + @""" data-returnpath=""" + ViewPage.ReturnPath + @""" ><i class=""pe-7s-cart""></i>Mua ngay<span>(Giao hàng từ 2 - 4 tiếng)</span></button>


                                                                    <button class=""btn btn-cart"" type=""button"" tel =""098949903""><i class=""pe-7s-call""></i>Gọi đặt mua<span>(Hỗ trợ 24/7)</span></button>
                                                                </fieldset>
                                                            </div>
                                                        </div>
                                                        <div class=""whotline hidden-xs"">
                                                            <li>
                                                                <span>Tổng đài tư vấn(8:00 - 19:00)</span>
                                                                <p class=""tdtv"">" + tongdai + @"</p>
                                                            </li>
                                                            <li>
                                                                <span>Hotline(24/7)</span>
                                                                <p class=""hotline"">" + Web_Hotline + @"</p>
                                                            </li>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div> ";
                ViewBag.Model = model;
                json.Create();
            }
        }
        //public void ActionCheckOut(CheckOutModel model)
        //{
        //    Json json = new Json();
        //    //TryUpdateModel(item);
        //    if (model.Name.Trim() == string.Empty)
        //        ViewPage.Message.ListMessage.Add("bạn chưa nhập tên");

        //    if (model.Phone.Trim() == string.Empty)
        //        ViewPage.Message.ListMessage.Add("bạn chưa nhập số điện thoại");

        //    if (model.Email.Trim() == string.Empty)
        //        ViewPage.Message.ListMessage.Add("bạn chưa nhập Email");
        //    if (model.Address.Trim() == string.Empty)
        //        ViewPage.Message.ListMessage.Add("ban chưa nhập địa chỉ");
        //    if (ViewPage.Message.ListMessage.Count > 0)
        //    {
        //        for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
        //        {
        //            string Messger = string.Empty;
        //            Messger += ViewPage.Message.ListMessage[i] + "</br>";
        //            json.Instance.Node2 += Messger;
        //        }
        //    }
        //    else
        //    {
        //        var ItemOder = new ModOrderEntity
        //        {
        //            Name = model.Name,
        //            Phone = model.Phone,
        //            Email = model.Email,
        //            Address = model.Address,
        //            ID = 0,
        //            IP = Core.Web.HttpRequest.IP,
        //            Created = DateTime.Now,
        //            Content = model.Content,
        //            Code = "DH" + String.Format("{0:ddMMyyyy)", DateTime.Now) + GetOrder()

        //        };
        //    }
        //}
        public void ActionCheckOut(ModOrderEntity item, CheckoutModel model)
        {
            var json = new Json();

            TryUpdateModel(item);
            if (item.Name.Trim() == string.Empty)

                ViewPage.Message.ListMessage.Add("Nhập: Họ và tên.");

            if (item.Phone.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Số điện thoại.");

            else if (!Global.Utils.IsPhone(item.Phone.Trim()))
                ViewPage.Message.ListMessage.Add("Số điện thoại không đúng.");

            if (item.Email.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ Email.");
            if (!Utils.IsEmailAddress(item.Email.Trim()))
               ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ Email không hợp lệ.");

            if (item.Address.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: Địa chỉ giao hàng.");

            if (item.Invoice && item.CompanyName.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: tên công ty.");
            if (item.Invoice && item.CompanyTax.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập: mã số thuế.");

            //hien thi thong bao loi
            if (ViewPage.Message.ListMessage.Count > 0)
            {
                string message = string.Empty;
                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                    message += ViewPage.Message.ListMessage[i] + "<br />";
                json.Instance.Node2 += message;
            }

            else
            {
                //luu don hang
                item.IP = Core.Web.HttpRequest.IP;
                item.Created = DateTime.Now;
                item.Code = "DH" + string.Format("{0:ddMMyyyy}", item.Created) + GetOrder();
                    ModOrderService.Instance.Save(item);

                //luu chi tiet don hang & send mail
                long total = 0;
                var cart = new Cart();
                var html = @"<!DOCTYPE html><html lang=""en"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;""><head></head><body style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; background-color: #fff; background-image: none; background-repeat: repeat; background-position: top left; background-attachment: scroll; color: #222; font-size: 13px; font-family: Arial,Helvetica,sans-serif;"">
    <div class=""container"" style=""padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: auto; margin-bottom: auto; margin-right: auto; margin-left: auto; max-width: 650px;"">
        <table class=""table_mail"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; background-color: transparent; border-spacing: 0; border-collapse: collapse; font-size: 14px; max-width: 600px;"">
            <tbody style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td class="""" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-spacing: 0; border-collapse: collapse; font-size: 14px; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #dee2e3; width: 100%!important; background-color: transparent;"">
                            <tbody style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                    <td width=""50%"" height=""49"" valign=""middle"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; width: 50%; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                                        <a href=""http://khoinguyen.angkorich.com/"" target=""_blank"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                            <img src=""cid:logo"" width=""100"" alt="""" class=""CToWUd"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                        </a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <a href=""#URLBanner#"" title="""" target=""_blank"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                            <img src=""cid:banner"" alt="""" class="""" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                        </a>
                    </td>
                </tr>
                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <div class=""mt20"" style=""margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: 20px;""><strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">Kính chào quý khách " + item.Name + @",</strong></div>
                        <div class=""mt10 mb20"" style=""margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: 10px; margin-bottom: 20px;"">
                            vlxd.vn vừa nhận được <strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; color: #f36f21;"">đơn hàng " + item.Code + @" </strong>của quý khách đặt ngày <strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">" + item.Created + @" </strong>với hình thức thanh toán là <strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
" + ((item.Payment == "0") ? "Thanh toán khi nhận hàng - COD" : "Chuyển khoản") + @"
</strong>. Chúng tôi sẽ gửi thông báo đến quý khách qua một email khác ngay khi sản phẩm được giao cho đơn vị vận chuyển.
                        </div>
                    </td>
                </tr>
                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; background-color: #f2f4f6; border-top-width: 2px; border-top-style: solid; border-top-color: #646464; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-spacing: 0; border-collapse: collapse; font-size: 14px; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #dee2e3; width: 100%!important; background-color: transparent;"">
                            <tbody style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                    <td width=""50%"" height=""49"" valign=""middle"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; width: 50%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;"">
                                        <div style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; color: #646464;"">Hình thức thanh toán:</div>
                                        <div class=""mt5 mb10"" style=""margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: 5px; margin-bottom: 10px;""><strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">";
                if (item.Payment == "0") html += "Thanh toán khi nhận hàng - COD<br />";
                if (item.Payment == "1") html += "Chuyển khoản<br />"; html += @"</strong></div>
                                    </td>
                                    <td width=""50%"" height=""49"" class="""" valign=""bottom"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; text-align: left; width: 50%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;"">
                                        <div style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; color: #646464;"">Đơn hàng sẽ được giao đến:</div>
                                        <strong class=""mt10"" style=""margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; color: #f36f21; display: block; margin-top: 10px;"">" + item.Name + @"</strong>
                                        <strong class=""mt10 diBlock"" style=""margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: 10px; display: block!important;"">Email: " + item.Email + @"
                                            <br style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                            Phone: " + item.Phone + @"
                                            <br style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                            Địa chỉ: " + item.Address + @"
                                            <br style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                        </strong>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td align=""left"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <div class=""mt20 mb10"" style=""margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: 20px; margin-bottom: 10px;"">
                            <strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">Sau đây là thông tin chi tiết về đơn hàng:</strong>
                        </div>
                    </td>
                </tr>";

                for (var i = 0; i < cart.Count; i++)
                {
                    var product = ModProductService.Instance.GetByID(cart.Items[i].ProductID);
                    if (product == null) continue;
                    ModOrderDetailService.Instance.Save(new ModOrderDetailEntity()
                    {
                        OrderID = item.ID,
                        ProductID = product.ID,
                        Quantity = cart.Items[i].Quantity,
                        Price = product.PriceSale,
                        Name = product.Name
                    });
                    total += product.PriceSale * cart.Items[i].Quantity;
                    html += @"<tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; border-width: 1px; border-style: dashed; border-color: #e7ebed; border-bottom-style: none; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <table style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; border-spacing: 0; background-color: transparent;"">
                            <tbody style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                    <td class="""" valign=""top"" align=""center"" height=""120"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 10px; padding-bottom: 10px; padding-right: 0; padding-left: 0;"">
                                        <a href="""" target=""_blank"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                            <img src=""" + (string.IsNullOrEmpty(product.File) ? "" : ("http://vlxdkn.vn/" + product.File.Replace("~/", "/"))) + @""" class=""CToWUd"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; width: 120px;"">
                                        </a>
                                    </td>
                                    <td valign=""top"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;"">
                                        <div style=""margin-top: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-bottom: 5px;"">
                                            <a class="""" href="""" target=""_blank"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; text-decoration: none; color: #292929;"">" + product.Name + @"</a>
                                        </div>
                                        <div style=""margin-top: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-bottom: 5px; color: #646464;"">Số lượng: " + cart.Items[i].Quantity + @"</div>
                                    </td>
                                    <td valign=""top"" align=""right"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 10px; padding-right: 10px; padding-bottom: 0; padding-left: 0;""><strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">" + string.Format("{0:#,##0}", product.PriceSale) + @" VND</strong></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>";
                }
                html += @"<tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; border-width: 1px; border-style: dashed; border-color: #e7ebed; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <div style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                            <table border=""0"" width=""100%"" cellpadding=""0"" cellspacing=""0"" align=""left"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-spacing: 0; border-collapse: collapse; font-size: 14px; width: 100%!important; background-color: transparent;"">
                                <tbody style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                    <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                        <td valign=""top"" align=""right"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 10px; padding-right: 10px; color: #646464; width: 80%; padding-bottom: 0; padding-left: 0;"">Tổng cộng:</td>
                                        <td valign=""top"" align=""right"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 10px; padding-right: 10px; color: #292929; min-width: 140px; padding-bottom: 0; padding-left: 0;"">" + string.Format("{0:#,##0}", total) + @" VND</td>
                                    </tr>
                                    <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                                        <td valign=""top"" align=""right"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; width: 80%; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;""></td>
                                        <td valign=""top"" align=""right"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 5px; padding-right: 10px; padding-bottom: 10px; font-size: 13px; color: #646464; min-width: 140px; padding-left: 0;""></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
 <tr style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">
                    <td valign=""top"" style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; border-collapse: collapse; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;"">
                        <div style=""margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: 20px;"">
                            <strong style=""margin-top: 0; margin-bottom: 0; margin-right: 0; margin-left: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box;"">Ghi chú khách hàng:</strong>
                        </div>

                        <div style=""padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; margin-top: 5px; margin-bottom: 20px; margin-right: 0; margin-left: 5px; color: #646464;"">
                            •&nbsp;&nbsp;" + item.Content + @"
                            </div>
                    </td>
                </tr>
              
            </tbody>
        </table>
    </div>
</body></html>";

                item.Total = total;

                try
                {
                    ModOrderService.Instance.Save(item);
                }
                catch (Exception ex)
                {
                    Error.Write(ex.Message);
                }

                html += @"<b>TỔNG TIỀN:</b> " + string.Format("{0:#,##0}", total) + @" đ<br /><br />";

                html += @"<b>THÔNG TIN KHÁCH HÀNG</b><br /><br />";

                if (!string.IsNullOrEmpty(item.Name)) html += "<b>Họ và tên:</b> " + item.Name + "<br />";
                if (!string.IsNullOrEmpty(item.Phone)) html += "<b>Điện thoại:</b> " + item.Phone + "<br />";
                if (!string.IsNullOrEmpty(item.Email)) html += "<b>Email:</b> " + item.Email + "<br />";
                if (!string.IsNullOrEmpty(item.Address)) html += "<b>Địa chỉ:</b> " + item.Address + "<br />";

                //if (!string.IsNullOrEmpty(item.Payment)) html += "<b>Hình thức thanh toán:</b>"+item.Payment +"<br />";

                if (!string.IsNullOrEmpty(item.Content)) html += "<b>Yêu cầu khác:</b>" + item.Content.Replace("\n", "<br />") + "<br />";

                //gui mail
                try
                {
                    #region send mail

                    var domain = ViewPage.Request.Url.Host;
                    var listEmail = Global.WebResource.GetValue("Web_Email");
                    if (string.IsNullOrEmpty(listEmail))
                        listEmail = "nguyenthanh1289@gmail.com";

                    if (!string.IsNullOrEmpty(item.Email))
                        listEmail += "," + item.Email;

                    //gui mail cho quan tri va khach hang
                    Mail.SendMail(
                        listEmail,
                        "noreply@gmail.com",
                        domain,
                        domain + "- Thông tin đơn hàng - ngày " + string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now),
                        html
                    );

                    #endregion send mail
                }
                catch (Exception ex)
                {
                    Error.Write(ex.Message);
                }

                cart.RemoveAll();
                cart.Save();
                ViewBag.Data = item;
                ViewBag.Model = model;
                //string vrCode = Guid.NewGuid().ToString();
                //Cookies.SetValue("vrCode", vrCode);
                //json.Instance.Node2 += vrCode;
                json.Instance.Node2 += "mua hàng thành công";
            }
            json.Create();
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


