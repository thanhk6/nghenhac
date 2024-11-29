using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using VSW.Lib.Global;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using VSW.Lib.Models;
using System.Collections.Specialized;
using HtmlAgilityPack;
using System.Xml.Xsl;
namespace VSW.Lib.Global
{
    public static class Crawler
    {
        #region with ajax (not change url)
        static HtmlDocument SubmitFormValues(NameValueCollection fv, string url)
        {
            // Attach a temporary delegate to handle attaching
            // the post back data
            HtmlWeb.PreRequestHandler handler = delegate (HttpWebRequest request)
            {
                var payload = AssemblePostPayload(fv);
                var buff = Encoding.ASCII.GetBytes(payload.ToCharArray());

                request.ContentLength = buff.Length;
                request.ContentType = "application/x-www-form-urlencoded";

                var reqStream = request.GetRequestStream();
                reqStream.Write(buff, 0, buff.Length);

                return true;
            };

            var web = new HtmlWeb();
            web.PreRequest += handler;
            var doc = web.Load(url, "POST");
            web.PreRequest -= handler;

            return doc;
        }

        private static string AssemblePostPayload(NameValueCollection fv)
        {
            var sb = new StringBuilder();
            foreach (var key in fv.AllKeys)
            {
                sb.Append("/" + key + "/" + fv.Get(key));
            }
            return sb.ToString().Substring(1);
        }
        public class GZipWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = (HttpWebRequest)base.GetWebRequest(address);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                return request;
            }
        }

        #endregion

        #region base
        private static HtmlDocument StripHtmlTags(string str)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(str);

            return doc;
        }
        #endregion

        #region gzip
        //gzip
        //public class GZipWebClient : WebClient
        //{
        //    protected override WebRequest GetWebRequest(Uri address)
        //    {
        //        HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
        //        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        //        return request;
        //    }
        //}

        //method with gzip
        //public int InsertLangVanHoa(string url, int menuId, int langID, int countPage, string dataUrl, string desUrl)
        //{
        //    int page = 0;
        //    int Count = 0;

        //    //khoi tao + mo ket noi database
        //    SqlConnection conn = new SqlConnection(VSW.Core.Data.ConnectionString.Default);
        //    conn.Open();

        //    HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
        //    HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();

        //    using (var wc = new GZipWebClient())
        //    {
        //        wc.Encoding = Encoding.UTF8;
        //        string html = wc.DownloadString(url);
        //        doc.LoadHtml(html);
        //    }

        //    while (page < countPage)
        //    {
        //        //xu ly phan trang
        //        page++;
        //        //string urlPage = url.Replace(".html", "") + "/page/" + page + ".html";

        //        if (doc != null)
        //        {
        //            //tach 1 tin
        //            var listNode = doc.DocumentNode.SelectNodes("//ul[@class=\"layout-news\"]/li");

        //            for (int i = 0; listNode != null && i < listNode.Count; i++)
        //            {
        //                string title = string.Empty, summary = string.Empty, file = string.Empty, content = string.Empty, detailUrl = string.Empty;

        //                HtmlDocument temp = StripHTMLTags(listNode[i].InnerHtml);

        //                //get url
        //                var _NodeTitle = temp.DocumentNode.SelectNodes("//span[@class=\"head\"]/strong/a");
        //                if (_NodeTitle == null) continue;

        //                detailUrl = "http://langvanhoa.vn" + _NodeTitle[0].Attributes["href"].Value.Trim();
        //                title = _NodeTitle[0].InnerText.Trim();

        //                HtmlAgilityPack.HtmlDocument _DocDetail = new HtmlDocument();
        //                using (var wc = new GZipWebClient())
        //                {
        //                    wc.Encoding = Encoding.UTF8;
        //                    string html = wc.DownloadString(detailUrl);
        //                    _DocDetail.LoadHtml(html);
        //                }

        //                if (_DocDetail == null) continue;

        //                //tach thanh phan tin
        //                var _NodeSummary = temp.DocumentNode.SelectNodes("//span[@class=\"head\"]/p");
        //                if (_NodeSummary != null) summary = _NodeSummary[0].InnerText.Trim();

        //                var _NodeFile = temp.DocumentNode.SelectNodes("//img");
        //                if (_NodeFile != null) file = "http://langvanhoa.vn" + _NodeFile[0].Attributes["src"].Value.Trim().Replace("@", "");

        //                string[] arrFile = !string.IsNullOrEmpty(file) ? file.Split('/') : null;
        //                string fileData = !string.IsNullOrEmpty(file) ? dataUrl + (arrFile != null ? arrFile[arrFile.Length - 1] : "") : string.Empty;
        //                string fileDes = !string.IsNullOrEmpty(file) ? desUrl + (arrFile != null ? arrFile[arrFile.Length - 1] : "") : string.Empty;
        //                if (!string.IsNullOrEmpty(file)) Global.DownloadImage.SaveImage(file, fileDes);

        //                //get content
        //                var _NodeDetail = _DocDetail.DocumentNode.SelectNodes("//p[@class=\"kBody\"]");
        //                if (_NodeDetail != null) content = _NodeDetail[0].InnerHtml.Trim();

        //                //insert database
        //                int value = Insert(menuId, langID, title, fileData, summary, content, conn);
        //                if (value > 0) Count++;
        //            }
        //        }
        //    }

        //    //dong ket noi database
        //    conn.Close();
        //    return Count;
        //}
        #endregion


        #region kgvietnam.com

        public static string GetKG(string url, int menuID, int brandID, int langID, int totalPage)
        {
            string directory = string.Empty;

            var brand = ModBrandService.Instance.GetByID(brandID);

            if (brand != null)

                directory = brand.Name;

            var menu = WebMenuService.Instance.GetByID(menuID);


            if (menu != null)
                directory += "/" + menu.Name;

            Directory.Create(HttpContext.Current.Server.MapPath("~/Data/upload/images/Product/" + directory + "/"));
            int page = 0;
            int count = 0;
            int count2 = 0;
            int quet = 0;
            var sp7 = false;

            var web = new HtmlWeb();
            var doc = new HtmlDocument();

            while (page <= totalPage)
            {
                //xu ly phan trang
                string urlPage = url + (page > 0 ? (page * 6).ToString() : string.Empty);

                doc = web.Load(urlPage);

                if (doc != null)
                {
                    //tach 1 tin
                    //var listNode = doc.DocumentNode.SelectNodes(@"//div[contains(@class,""shopcolcate"")]");

                    var listNode = doc.DocumentNode.SelectNodes(@"//div[@class=""col-md-3 col-sm-6 shopcol shopcolcate""]");
                    for (int i = 0; listNode != null && i < listNode.Count; i++)
                    {
                        quet++;
                        if (quet >= 7)
                        {
                            sp7 = true;
                        }
                        string price = string.Empty,
                            model = string.Empty,
                            file = string.Empty,
                             fileDetailX = string.Empty,
                            content = string.Empty;
                        var temp = StripHtmlTags(listNode[i].InnerHtml);

                        //get title + url


                        var nodeName = temp.DocumentNode.SelectNodes(@"//h3[@class=""name""]/a");



                        if (nodeName == null) continue;

                        string name = nodeName[0].InnerText.Trim();
                        string code = Data.GetCode(name);


                        var productExist = ModProductService.Instance.CreateQuery().Where(o => o.Activity == true && o.Code == code).ToSingle();


                        if (productExist != null)
                        {
                            var detail2 = new HtmlWeb();

                            var docDetail2 = detail2.Load(nodeName[0].Attributes["href"].Value.Trim());
                            if (docDetail2 == null) continue;
                            //get file detail

                            var nodeFileDetail = docDetail2.DocumentNode.SelectNodes("//div[@class='image-slider']/img");

                            if (nodeFileDetail != null)
                            {
                                fileDetailX = nodeFileDetail[0].Attributes["src"].Value.Trim();
                                if (!string.IsNullOrEmpty(fileDetailX))
                                {
                                    fileDetailX = fileDetailX.Replace("_270x357", "").Replace("/.t", "");

                                    string ext = Path.GetExtension(fileDetailX);
                                    Image.SaveFromUrl(fileDetailX, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/detail-" + code + ext));
                                    fileDetailX = "~/Data/upload/images/Product/" + directory + "/detail-" + code + ext;
                                }
                            }

                            productExist.FileDetail = fileDetailX;

                            ModProductService.Instance.Save(productExist, o => new { o.FileDetail });
                            count2++;
                        }

                        else
                        {
                            //get price
                            var nodePrice = temp.DocumentNode.SelectNodes(@"//span[@class=""amount""]");
                            if (nodePrice != null)
                                price = nodePrice[0].InnerText.Replace(",", "").Replace("VND", "").Trim();

                            //get file
                            var nodeFile = temp.DocumentNode.SelectNodes("//img");

                            if (nodeFile != null)
                            {
                                file = nodeFile[0].Attributes["src"].Value.Trim();

                                if (!string.IsNullOrEmpty(file))
                                {
                                    file = file.Replace("_270x357", "").Replace("/.t", "");

                                    string ext = Path.GetExtension(file);
                                    Image.SaveFromUrl(file, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/" + code + ext));
                                    file = "~/Data/upload/images/Product/" + directory + "/" + code + ext;
                                }
                            }


                            var detail = new HtmlWeb();

                            var docDetail = detail.Load(nodeName[0].Attributes["href"].Value.Trim());


                            if (docDetail == null) continue;

                            //get file
                            //var nodeFileDetail = docDetail.DocumentNode.SelectNodes("//div[@class='slick-track']//img");
                            //if (nodeFile != null)
                            //{
                            //    file = nodeFile[0].Attributes["src"].Value.Trim();
                            //    if (!string.IsNullOrEmpty(file))
                            //    {
                            //        file = file.Replace("_270x357", "").Replace("/.t", "");

                            //        string ext = Path.GetExtension(file);
                            //        Image.SaveFromUrl(file, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/" + code + ext));
                            //        file = "~/Data/upload/images/Product/" + directory + "/" + code + ext;
                            //    }
                            //}

                            //get file detail

                            var nodeFileDetail = docDetail.DocumentNode.SelectNodes("//div[@class='slick-track']//img");

                            if (nodeFileDetail != null)
                            {
                                fileDetailX = nodeFileDetail[0].Attributes["src"].Value.Trim();
                                if (!string.IsNullOrEmpty(fileDetailX))
                                {
                                    fileDetailX = fileDetailX.Replace("_270x357", "").Replace("/.t", "");

                                    string ext = Path.GetExtension(fileDetailX);
                                    Image.SaveFromUrl(fileDetailX, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/detail-" + code + ext));
                                    fileDetailX = "~/Data/upload/images/Product/" + directory + "/detail-" + code + ext;
                                }
                            }

                            //get model
                            var nodeModel = docDetail.DocumentNode.SelectNodes(@"//span[@class=""sku""]");
                            if (nodeModel != null)
                                model = nodeModel[0].InnerText.Trim();

                            //get content
                            var nodeContent = docDetail.DocumentNode.SelectNodes(@"//div[@id=""tab-description""]");
                            if (nodeContent != null)
                            {
                                var tempContent = StripHtmlTags(nodeContent[0].InnerHtml.Trim());

                                //file in content
                                var nodeFileContent = tempContent.DocumentNode.SelectNodes("//img");
                                for (int f = 0; nodeFileContent != null && f < nodeFileContent.Count; f++)
                                {
                                    string fileDetail = nodeFileContent[f].Attributes["src"].Value.Trim();
                                    if (!string.IsNullOrEmpty(fileDetail))
                                    {
                                        string extDetail = Path.GetExtension(fileDetail);
                                        Image.SaveFromUrl(fileDetail, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/" + code + "-" + f + extDetail));
                                        nodeFileContent[f].SetAttributeValue("src", @"/Data/upload/images/Product/" + directory + "/" + code + "-" + f + extDetail);
                                    }
                                }

                                content = HttpUtility.HtmlDecode(tempContent.DocumentNode.InnerHtml.Trim());
                            }

                            //insert database
                            var item = new ModProductEntity
                            {
                                ID = 0,
                                MenuID = menuID,
                                BrandID = brandID,
                                Name = name,
                                Code = code,
                                Model = model,
                              
                                File = file,
                                FileDetail = fileDetailX,
                                Content = content,
                                Order = GetMaxOrder(),
                                Published = DateTime.Now,
                                Updated = DateTime.Now,
                                Activity = true
                            };


                            int recordID = ModProductService.Instance.Save(item);

                            var chekproductfile = ModProductService.Instance.Getcode(code);
                            if (chekproductfile != null)
                            {

                            }




                            if (recordID > 0) count++;

                            //update url
                            ModCleanURLService.Instance.InsertOrUpdate(item.Code, "Product", item.ID, item.MenuID, langID);
                        }
                    }
                }

                page++;
            }

            return "Thêm mới: " + count + " sản phẩm;" + "Cập nhật ảnh chi tiết cho : " + count2 + " sản phẩm";
        }

        #endregion
        #region bep68.com


        public static string getdenled(string url, int menuID, int brandID, int langID, int totalPag)

        {
            string directory = string.Empty;

            var brand = ModBrandService.Instance.GetByID(brandID);
            var menu = WebMenuService.Instance.GetByID(menuID);

            if (menu != null)

                directory += "/" + Data.GetCode(menu.Name);

            string path = HttpContext.Current.Server.MapPath("~/Data/upload/images/Product/" + directory + "/");

            //var b = HttpContext.Current.Server;
            Directory.Create(path);
            //b.CreateObject("~/Data/upload/images/Product/" + directory + "/"); 
            int page = totalPag;
            int count = 0;
            int count2 = 0;
            int quet = 0;
            var sp7 = false;
            var web = new HtmlWeb();
            var doc = new HtmlDocument();



            while (page <= totalPag && page>0)
            {
                string urlPage;
                if (page <= 1)
                {
                    //urlPage = url + (page > 0 ? (page * 6).ToString() : string.Empty);

                    urlPage = url;
                }

                else
                {
                    urlPage = url + "/page/" + page+"/";
                }

                doc = web.Load(urlPage);

                var listNode = doc.DocumentNode.SelectNodes(@"//div[@class=""col-md-3""]");

                for (int i = 0; listNode != null && i < listNode.Count; i++)
                {
                    quet++;
                    if (quet >= 7)
                    {
                        sp7 = true;
                    }
                    string price = string.Empty;
                    string price2 = string.Empty,
                      model = string.Empty,
                      file = string.Empty,
                      fileDetailX = string.Empty,
                      content = string.Empty,
                      Specifications = string.Empty;
                    var temp = StripHtmlTags(listNode[i].InnerHtml);
                    //get title + url
                    var nodeName = temp.DocumentNode.SelectNodes(@"//h3[@class=""iProBox__name""]");
                    if (nodeName == null) continue;
                    string name = nodeName[0].InnerText.Replace("(Hết hàng)","").Trim();

                    string code = Data.GetCode(name);
                    var productExist = ModProductService.Instance.CreateQuery().Where(o => o.Activity == true && o.Code == code).ToSingle();
                   
                    var nodePrice = temp.DocumentNode.SelectNodes(@"//div[@class=""iProBox__sale""]");

                    if (nodePrice != null)
                     price = nodePrice[0].InnerText.Replace("vnđ", "").Replace("Giá", "").Replace(".","").Trim();

                  
                    var nodePrice2 = temp.DocumentNode.SelectNodes(@"//div[@class=""iProBox__price""]");
                    if (nodePrice2 != null)
                    price2 = nodePrice2[0].InnerText.Replace("vnđ", "").Replace("Giá NY:", "").Replace(".","").Trim();


                    var NodeFile = temp.DocumentNode.SelectNodes(@"//div[@class=""iProBox__pic""]/img");
                    if (NodeFile != null)
                    {
                        file = NodeFile[0].Attributes["src"].Value.Replace("-300x300","").Trim();

                        if (!string.IsNullOrEmpty(file))
                        {
                            //file = file;
                            string ext = Path.GetExtension(file);
                            Image.SaveFromUrl(file, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/" + code + ext));
                            file = "~/Data/upload/images/Product/" + directory + "/" + code + ext;
                        }
                    }



                    var detail = new HtmlWeb();
                    var loaturldetail = temp.DocumentNode.SelectNodes(@"//a[@class=""iProBox__item table_pro""]");

                    var docDetail = detail.Load(loaturldetail[0].Attributes["href"].Value.Trim());

                    if (docDetail == null) continue;

                    var nodeContent = docDetail.DocumentNode.SelectNodes(@"//div[@id=""tab-additional_information""]");

                    if (nodeContent != null)
                    {
                        var tempContent = StripHtmlTags(nodeContent[0].InnerHtml.Trim());
                        //file in content
                        var nodeFileContent = tempContent.DocumentNode.SelectNodes("//img");
                        for (int f = 0; nodeFileContent != null && f < nodeFileContent.Count; f++)
                        {
                            string fileDetail = nodeFileContent[f].Attributes["src"].Value.Trim();
                            if (!string.IsNullOrEmpty(fileDetail))
                            {
                                string extDetail = Path.GetExtension(fileDetail);
                                Image.SaveFromUrl(fileDetail, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/" + code + "-" + f + extDetail));
                                nodeFileContent[f].SetAttributeValue("src", @"/Data/upload/images/Product/" + directory + "/" + code + "-" + f + extDetail);
                            }
                        }
                        var nodea = tempContent.DocumentNode.SelectNodes("//a");

                        for (int k = 0; nodea != null && k < nodea.Count; k++)
                        {
                            string link = nodea[k].Attributes["href"].Value.Trim();

                            nodea[k].SetAttributeValue("href", " ");
                        }
                        content = HttpUtility.HtmlDecode(tempContent.DocumentNode.InnerHtml.Trim());
                    }
                  
                    var nodeSpecifications = docDetail.DocumentNode.SelectNodes(@"//div[@class=""prodPost__ct""]");

                     if (nodeSpecifications != null)
                    {
                        var tempSpecifications = StripHtmlTags(nodeSpecifications[0].InnerHtml.Trim());

                        Specifications = HttpUtility.HtmlDecode(tempSpecifications.DocumentNode.InnerHtml.Trim());
                    }

                  ////  var anh = docDetail.DocumentNode.SelectNodes(@"//div[@class=""sp-large""]");
                  //  var anh = docDetail.DocumentNode.SelectNodes(@"//div[@class=""sp-wrap sp-non-touch""]");

                  //  if (anh != null)
                  //  {
                  //      file = anh[0].Attributes["src"].Value.Trim();

                  //      string ext = Path.GetExtension(file);
                  //      Image.SaveFromUrl(file, HttpContext.Current.Server.MapPath(@"~/Data/upload/images/Product/" + directory + "/" + code + ext));
                  //      file = "~/Data/upload/images/Product/" + directory + "/" + code + ext;

                  //  }

                    var item = new ModProductEntity
                    {
                        ID = 0,
                        MenuID = menuID,
                        Name = name,
                        Code = code,
                        //Model = model,
                      
                        File = file,
                        FileDetail = fileDetailX,
                        Content = content,
                        Specifications = Specifications,
                        Order = GetMaxOrder(),
                        Published = DateTime.Now,
                        Updated = DateTime.Now,
                        Activity = true,
                        
                    };
                    int recordID = ModProductService.Instance.Save(item);
                    var chekproductfile = ModProductService.Instance.Getcode(code);
                   


                    if (recordID > 0) count++;

                    //update url
                    ModCleanURLService.Instance.InsertOrUpdate(item.Code, "Product", item.ID, item.MenuID, langID);


                }

                page--;
            }
            return "Thêm mới: " + count + " sản phẩm;" + "Cập nhật ảnh chi tiết cho : " + count2 + " sản phẩm";
        }



        #endregion
        private static int GetMaxOrder()
        {
            return ModProductService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }
        private static int getproductdetail()
        {
            return ModProductFileService.Instance.CreateQuery()
                .Max(o => o.Order)
                .ToValue().ToInt(0) + 1;
        }



        #region utilities
        public static string GetGold()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://hcm.24h.com.vn/ttcb/giavang/giavang.php");

            if (doc == null) return string.Empty;

            var _ResultNode = doc.DocumentNode.SelectNodes("//table[@class=\"tb-giaVang\"]");
            if (_ResultNode == null) return string.Empty;

            return "<table class=\"tb-giaVang\" width=\"100%\" cellspacing=\"1\" cellpadding=\"1\" border=\"0\">" + _ResultNode[0].InnerHtml + "</table>";
        }

        public static string GetKQXS()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://ketquaxoso.24h.com.vn/");

            if (doc == null) return string.Empty;

            var _ResultNode = doc.DocumentNode.SelectNodes("//div[@id=\"content\"]");
            if (_ResultNode == null) return string.Empty;

            HtmlDocument temp = StripHtmlTags(_ResultNode[0].InnerHtml);

            _ResultNode = temp.DocumentNode.SelectNodes("//table[@style=\"margin-top:6px;\"]");

            return "<table class=\"tb-giaVang\" width=\"100%\" cellspacing=\"1\" cellpadding=\"1\" border=\"0\">" + _ResultNode[0].InnerHtml + "</table>";
        }

        public static string GetWeather()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://hn.24h.com.vn/ttcb/thoitiet/thoitiet.php");

            if (doc == null) return string.Empty;

            var _ResultNode = doc.DocumentNode.SelectNodes("//div[@class=\"thoiTietBox\"]");
            if (_ResultNode == null) return string.Empty;

            HtmlDocument temp = StripHtmlTags(_ResultNode[0].InnerHtml);

            _ResultNode = temp.DocumentNode.SelectNodes("//table[@width=\"100%\"]");

            return "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" + _ResultNode[0].InnerHtml + "</table>";
        }



        public static string GetRate()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://hcm.24h.com.vn/ttcb/tygia/tygia.php");

            if (doc == null) return string.Empty;

            var _ResultNode = doc.DocumentNode.SelectNodes("//table[@class=\"tb-giaVang\"]");
            if (_ResultNode == null) return string.Empty;

            string result = string.Empty;
            if (!string.IsNullOrEmpty(_ResultNode[0].InnerHtml)) result = _ResultNode[0].InnerHtml.Replace("images/btdown.gif", "/Content/utils/html/images/btdown.gif").Replace("images/btup.gif", "/Content/utils/html/images/btup.gif");

            return "<table class=\"tb-giaVang\" width=\"100%\" cellspacing=\"1\" cellpadding=\"1\" border=\"0\">" + result + "</table>";
        }


        public static string GetStock()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://chungkhoan.24h.com.vn/");

            if (doc == null) return string.Empty;

            var _ResultNode = doc.DocumentNode.SelectNodes("//table[@id=\"tableContainer\"]");
            if (_ResultNode == null) return string.Empty;

            string result = string.Empty;
            if (!string.IsNullOrEmpty(_ResultNode[0].InnerHtml)) result = _ResultNode[0].InnerHtml.Replace("/images/btdown.gif", "/Content/utils/html/images/btdown.gif").Replace("/images/btup.gif", "/Content/utils/html/images/btup.gif").Replace("/images/btnochange.jpg", "/Content/utils/html/images/btnochange.jpg");

            return "<table class=\"tb-giaVang\" width=\"100%\" cellspacing=\"1\" cellpadding=\"1\" border=\"0\">" + result + "</table>";
        }
        #endregion
    }
}

