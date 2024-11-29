<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var item = ViewBag.Data as ModProductEntity;
    var getpage = ViewPage.CurrentPage;
    var getBand1 = SysPageService.Instance.GetByParent_Cache(11239);
    var getBand = getBand1.Where(o => o.MenuID == item.BrandID).ToList();
    var getparentpage1 = SysPageService.Instance.GetByParent_Cache(getpage.ParentID);
    var getparentpage = getparentpage1.Where(o => o.BrandID <= 0).ToList();
    string img = !string.IsNullOrEmpty(item.File) ? item.File.Replace("~/", "/") : "";
    var listgift = item.GetGift();
    var listItem = ViewBag.Other as List<ModProductEntity>;
    var listFile = item.GetFile();
    var listGift = item.GetGift();
    var listband = item.GetBrand();
    var listShowroom = ModShowroomService.Instance.CreateQuery().OrderByAsc(o => o.Order).ToList();
    //if (!string.IsNullOrEmpty(item.File))
    //    item.File = listFile[0].File;
    //comment
    var listComment = ViewBag.Comment as List<ModCommentEntity>;
    int _TotalComment = listComment == null ? 0 : listComment.Count;
    var numberOneStar = 0; var onePer = 0;
    var numberTwoStar = 0; ; var twoPer = 0;
    var numberThreeStar = 0; ; var threePer = 0;
    var numberFourStar = 0; ; var fourPer = 0;
    var numberFiveStar = 0; ; var fivePer = 0;
    if (listComment != null)
    {
        var oneStar = listComment.FindAll(o => o.Vote == 1);
        numberOneStar = (oneStar == null) ? 0 : oneStar.Count;
        var twoStar = listComment.FindAll(o => o.Vote == 2);
        numberTwoStar = (twoStar == null) ? 0 : twoStar.Count;
        var threeStar = listComment.FindAll(o => o.Vote == 3);
        numberThreeStar = (threeStar == null) ? 0 : threeStar.Count;
        var fourStar = listComment.FindAll(o => o.Vote == 4);
        numberFourStar = (fourStar == null) ? 0 : fourStar.Count;
        var fiveStar = listComment.FindAll(o => o.Vote == 5);
        numberFiveStar = (fiveStar == null) ? 0 : fiveStar.Count;
    }
    if (_TotalComment > 0)
    {
        onePer = (numberOneStar / _TotalComment) * 100;
        twoPer = (numberTwoStar / _TotalComment) * 100;
        threePer = (numberThreeStar / _TotalComment) * 100;
        fourPer = (numberFourStar / _TotalComment) * 100;
        fivePer = (numberFiveStar / _TotalComment) * 100;
    }
    var average = _TotalComment == 0 ? 5 : Math.Round(((double)(5 * numberFiveStar + 4 * numberFourStar + 3 * numberThreeStar + 2 * numberTwoStar + 1 * numberOneStar) / _TotalComment), 1);
    var brand1 = SysPageService.Instance.CreateQuery().Where(o => o.MenuID == ViewPage.CurrentPage.MenuID && o.BrandID == item.BrandID).ToSingle_Cache();
    var brand = brand1 != null ? brand1 : ViewPage.CurrentPage;
%>
<div class="beadcrumb-tuyen">
    <div class="container">

        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <ol vocab="https://schema.org/" typeof="BreadcrumbList" class="breadcrumb">
                    <%= Utils.GetMapPageproductBrand(brand,item.Code,item.Name) %>
                    <%-- 
            <%if (brand != null){%>
            <li itemscope="itemscope" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="<%=ViewPage.GetPageURL(brand) %>" itemprop="url"><span itemprop="title"><%=brand.Name %></span></a></li>          
               <%} %>--%>
                </ol>
            </div>
        </div>
    </div>
</div>
<section class="main-page-chitiet">
    <div class="container">
        <div class="wp-main-padding bg-white">
            <div class="wp-main-ctsp bg-white">
                <div class="row">
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <div class="wp-img-zoom">

                            <ul id='zoom1' class='gc-start'>



                                <li>
                                    <img src="<%=item.File.Replace("~/","/") %>" alt="<%=item.Alt!=null?item.Alt:item.Name %>" />
                                </li>

                                <%if (listFile != null && listFile.Count > 0)
                                    { %>
                                <%for (int i = 0; listFile != null && i < listFile.Count; i++)
                                    {
                                        string ImgGift = !string.IsNullOrEmpty(listFile[i].File) ? listFile[i].File.Replace("~/", "") : "";
                                %>
                                <li>
                                    <img src="<%=ImgGift %>" alt="<%=listFile[i].Alt!=null?listFile[i].Alt:item.Name %>" /></li>
                                <%} %>

                                <%} %>
                            </ul>

                        </div>
                    </div>
                    <div class="col-md-4 col-sm-6 col-xs-12">
                        <div class="list_tag_pro">
                            <ul>

                                <li><a href=""><%=ViewPage.CurrentPage.Name %></a></li>

                                <%if (getBand != null && getBand.Count > 0)
                                    {%>
                                <li><a href="<%=ViewPage.GetPageURL(getBand[0]) %>"><%=getBand[0].Name %></a></li>
                                <%} %>
                            </ul>
                        </div>
                        <div class="wp-info-right">
                            <h1 class="h1-title-ct"><%=item.Name %></h1>
                            <%if (item.Price > 1)
                                { %>
                            <p class="price-ct">
                                Giá bán: <span><b class="do"><%=string.Format("{0:#,##0}",item.Price) %>đ</b></span> <%if (item.SellOff > 0)
                                                                                                                         { %> <span class="phantram_giam">(-<%=item.SellOffPercent %>%) </span><%} %>
                            </p>
                            <%}

                                else
                                { %>
                            <p class="price-ct">Giá bán: <span><b class="do">Liên hệ</b></span></p>
                            <%} %>
                            <%if (item.Price2 > 0)
                                { %>
                            <%--<div class="hpl18-item-pmarket"><%=string.Format("{0:#,##0}", item.Price2) %><span>đ</span> </div>--%>

                            <div class="giagoc_tietkiem">
                                <p class="giaban-goc"><span class="text_giagoc">Giá thị trường: </span><span class="giagoc"><%=string.Format("{0:#,##0}",item.Price2) %>đ</span></p>
                                <p class="tietkiem"><span class="text_tietkiem">Tiết kiệm: </span><span class="giatietkiem"><%=string.Format("{0:#,##0}",item.SellOff) %>đ</span></p>
                            </div>

                            <%} %>
                            <div class="box-qua">
                                <span class="icon-qua">
                                    <img src="/Content/Desktopnew/images/icon-qua2.png" alt="<%=item.Alt!=null?item.Alt:item.Name %>"></span>

                                <!-- <ul class="ul-qua"> -->
                                <%if (listGift != null && listGift.Count > 0)
                                    {
                                        long tongtien = 0;
                                %>
                                <%for (int n = 0; listGift != null && n < listGift.Count; n++)
                                    {
                                        tongtien = tongtien + listGift[n].Price;
                                %>
                                <%=listGift[n].Content%>
                                <%}%>
                                <!-- <li>Bộ quà tặng trị giá <%=string.Format("{0:#,##0}",tongtien)%>đ</li> -->
                                <%}%>
                                <!-- </ul> -->
                            </div>
                            <div class="btn-mua-ngay">
                                <a href="javascript:void(0)" class="btn1 btn btn-danger buy_nowdetail" data-returnpath="<%=ViewPage.ReturnPath%>" data-id="<%=item.ID %>">{RS:Muangay}</a>

                                <a href="tel:{RS:phone}" class="btn2 btn btn-default">{RS:Goingay} </a>
                            </div>
                            <div class="uu-dai">
                                {RS:Uu_dai_san_pham}
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-12 col-xs-12">
                        <div class="wp-list-sieuthi">
                            <div class="title-box">
                                <h3 class="h2-title-box">Showroom Đèn led  </h3>
                            </div>
                            <%if (listShowroom != null && listShowroom.Count > 0)
                                { %>
                            <ul class="ul-b list-sieuthi">
                                <%for (int l = 0; listShowroom != null && l < listShowroom.Count; l++)
                                    { %>
                                <li class="item-sieuthi">
                                    <p class="p-name"><b>Cơ sở <%=l + 1 %>: </b></p>
                                    <p class="p-post"><%= listShowroom[l].Name %></p>
                                </li>
                                <%} %>
                            </ul>
                            <%} %>
                        </div>
                        <div class="wp-why">
                            <%--  {RS:Tai_sao_nen_chon}--%>


                            <div class="title-box">
                                <h3 class="h2-title-box">Tại sao mua sắm ở Homeled.vn  ?</h3>
                            </div>

                            <div class="wsupport-s">
                                <ul>
                                    <li>
                                      


                                          <img src="/Content/Desktopnew/images/icon/knowledge.png"/>
                                        <p>
                                            nhiều năm<br />
                                            kinh nghiệm
                                        </p>
                                    </li>
                                    <li>
                                        <img src="/Content/Desktopnew/images/icon/piggy-bank.png" />
                                        <p>
                                            Cam kết<br />
                                            giá tốt nhất
                                        </p>
                                    </li>
                                    <li>
                                        <img src="/Content/Desktopnew/images/icon/guaranteed.png" />
                                        <p>
                                            Bảo hành<br />
                                            chính hãng
                                        </p>
                                    </li>

                                    <li>
                                        <img src="/Content/Desktopnew/images/icon/plumber.png" />
                                        <p>
                                            Lắp đặt<br />
                                            chuyên nghiệp
                                        </p>
                                    </li>
                                    <li>
                                        <img src="/Content/Desktopnew/images/icon/delivery-van.png"/>
                                        <p>
                                            Vận chuyển<br />
                                            toàn quốc
                                        </p>
                                    </li>
                                    <li>
                                        <img src="/Content/Desktopnew/images/icon/easy-return.png" />
                                        <p>
                                            Nhận đổi trả<br />
                                            hàng lỗi
                                        </p>
                                    </li>
                                </ul>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="wp-thongtin-sp">
                <div class="row">
                    <div class="col-md-7 col-sm-12 col-xs-12">
                        <div class="left-tongquan bg-white">
                            <div class="wp-title-box tilte-style-2 text-left" id="lentren1">
                                <p class="h2-title-box"><span>Mô tả sản phẩm</span></p>
                            </div>
                            <div class="post-danhgia" id="mo-ta-san-pham">

                                <%=item.Content %>
                            </div>
                            <!-- <p class="show-more" style="display: block;" onclick="showArticle();">
                                <a href="#mo-ta-san-pham" class="readmore"><i class="fa fa-search"></i></a>
                            </p> -->
                            <div class="show-more">
                                <a class="readmore" id="js-show-mtsp">Đọc thêm  </a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 col-sm-12 col-xs-12">
                        <div class="wp-thongso bg-white">
                            <div class="wp-title-box tilte-style-2 text-left" id="tskt">
                                <p class="h2-title-box"><span>Thông số kỹ thuật</span></p>
                            </div>
                            <div class="table-responsive">
                                <%=item.Specifications %>
                            </div>
                        </div>
                        <div class="show-more">
                            <a class="readmore" id="js-show-tskt">Đọc thêm  </a>
                        </div>
                    </div>
                </div>
            </div>

            <%-- <div class="wp-sp-tt-toto">
                <div class="wp-title-box tilte-style-2 text-left">
                    <h2 class="h2-title-box"><span>Mục tư vấn  </span></h2>
                </div>
                <div class="post-toto" id="muc-tu-van">
                    <article>
                        <% =listband.Content%>
                    </article>
                </div>
                <!-- <div class="show-more">
                    <a href="#muc-tu-van" class="readmore" id="js-show-more">Đọc thêm  </a>
                </div> -->
            </div>--%>

            <div class="wp-sp-lienquan">
                <div class="wp-title-box tilte-style-2 text-left">
                    <h4 class="h2-title-box"><span>Sản phẩm liên quan</span></h4>
                </div>
                <div class="wp-list-sp sp-km">
                    <div id="sp-km" class="owl-carousel owl-theme">
                        <%for (var i = 0; listItem != null && i < (listItem.Count > 15 ? 15 : listItem.Count); i++)
                            { %>
                        <div class="item">
                            <div class="wp-item-sp wp-item-sp-page item-sp-b">
                                <div class="img img-cover">
                                    <a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>" title="<%=listItem[i].Name %>" class="h_1">
                                        <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Name %>" src="<%=Utils.GetResizeFile(listItem[i].File,4,245,245)%>">
                                    </a>
                                    <div class="sale">
                                        <%if ((listItem[i].State & 1) == 1)
                                            { %>
                                        <span class="icon-54">Sale</span>
                                        <%} %>
                                        <%if ((listItem[i].State & 4) == 4)
                                            { %>
                                        <span class="icon-55">Mới</span>
                                        <%} %>
                                        <%if ((listItem[i].State & 2) == 2)
                                            {%>
                                        <span class="icon-56">Hot</span>
                                        <%} %>
                                    </div>
                                </div>
                                <div class="text">
                                    <p class="h2_title"><a href="<%=ViewPage.GetURL(listItem[i].MenuID,listItem[i].Code) %>"><%=listItem[i].Name %></a></p>
                                    <%--<div class="rating">
                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                        <span class="int">35</span>
                                    </div>--%>
                                </div>
                                <div class="price">
                                    <%if (listItem[i].Price > 0)
                                        { %>
                                    <div class="int">
                                        <%=string.Format("{0:#,##0}", listItem[i].Price)%>đ<%if (listItem[i].SellOffPercent > 0)
                                                                                               { %><span class="SellOff">(-<%=listItem[i].SellOffPercent %>%)</span><%} %>
                                    </div>
                                    <%} %>

                                    <%else
                                        { %>
                                    <div class="int">Liên hệ để nhận báo giá </div>
                                    <%} %>

                                    <%if (listItem[i].Price2 > 0)
                                        { %>
                                    <div class="sale"><%=string.Format("{0:#,##0}", listItem[i].Price2) %>đ</div>
                                    <%} %>
                                    <%if (listItem[i].SellOff > 0)
                                        { %>
                                    <div class="des">Tiết kiệm <%=string.Format("{0:#,##0}", listItem[i].SellOff)%> đ</div>

                                    <%} %>
                                </div>
                            </div>
                        </div>
                        <%} %>
                        <!-- end item -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>



