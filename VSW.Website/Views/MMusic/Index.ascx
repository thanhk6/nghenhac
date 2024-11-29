<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<script runat="server">
    public string SortMode
    {
        get
        {
            string sort = VSW.Core.Web.HttpQueryString.GetValue("sort").ToString().ToLower().Trim();
            if (sort == "update" || sort == "new_asc" || sort == "view_desc" || sort == "price_asc" || sort == "price_desc")                return sort;
            return "update";
        }
    }
    public string GetAddAtr(List<WebPropertyEntity> listItem, int propertyID)
    {
        if (ArrAtr == null)
            return ViewPage.CurrentURL + "?atr=" + propertyID;
        var listAtr = new List<int>();
        listAtr.AddRange(ArrAtr);
        foreach (var item in listItem)
        {
            if (Array.IndexOf(ArrAtr, item.ID) > -1)
                listAtr.Remove(item.ID);
        }
        if (!listAtr.Contains(propertyID))
            listAtr.Add(propertyID);
        return ViewPage.CurrentURL + "?atr=" + string.Join("-", listAtr.ToArray());
    }
    public string GetRemoveAtr(int propertyID)
    {
        var listAtr = new List<int>();
        listAtr.AddRange(ArrAtr);
        if (listAtr.Contains(propertyID))
            listAtr.Remove(propertyID);
        if (listAtr.Count == 0)
            return ViewPage.CurrentURL;
        return ViewPage.CurrentURL + "?atr=" + string.Join("-", listAtr.ToArray());
    }
    public string GetURL(string sKey, string sValue)
    {
        string strUrl = string.Empty;
        string strKey = string.Empty;
        string strValue = string.Empty;
        for (int i = 0; i < ViewPage.PageViewState.Count; i++)
        {
            strKey = ViewPage.PageViewState.AllKeys[i];
            strValue = ViewPage.PageViewState[strKey].ToString();
            if (strKey.ToLower() == sKey.ToLower() || strKey.ToLower() == "vsw" || strKey.ToLower() == "v" || strKey.ToLower() == "s" || strKey.ToLower() == "w" || strKey.ToLower().Contains("web."))
                continue;
            if (strUrl == string.Empty)
                strUrl = "?" + strKey + "=" + HttpContext.Current.Server.UrlEncode(strValue);
            else
                strUrl += "&" + strKey + "=" + HttpContext.Current.Server.UrlEncode(strValue);
        }
        strUrl += (strUrl == string.Empty ? "?" : "&") + sKey + "=" + sValue;
        return strUrl;
    }
    int[] ArrAtr = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ArrAtr = ViewBag.ArrAtr;
    }
</script>
<%
    var listItem = ViewBag.Data as List<ModProductEntity>;

    var model = ViewBag.Model as MProductModel;
    int count;

    count = ViewBag.coutProduct;
    //string sort = VSW.Core.Web.HttpQueryString.GetValue("sort").ToString();
%>
<section class="main-page-danhmuc content_page-danhmuc">
    <h1 class="logo-text"><%=ViewPage.CurrentPage.PageTitle %></h1>
    <div class="container text_content">
        <h1 class="h1_title">Danh sách sản phẩm</h1>
        <div class="des">Những sản phẩm <%=ViewPage.CurrentPage.Name %> chất lượng tốt nhất.</div>
    </div>
    <div class="container">
        <VSW:StaticControl Code="CProperty" DefaultLayout="MobileProperty" runat="server" />
        <div class="row row_width--b">
            <VSW:StaticControl Code="CProperty" DefaultLayout="DesktopProperty" runat="server" />

            <div class="col-md-9 col-sm-12 col-xs-12">
                <div class="filter-top-pro hidden-sm hidden-xs">
                    <div class="left">
                        <p>Sắp xếp theo: </p>
                        <div class="aside-content filter-group">
                            <select name="sort" id="sort" class="form-control" data-url="<%=ViewPage.CurrentURL %>">
                                <option value="new_asc">Mới nhất</option>
                                <option value="price_asc">Từ thấp đến cao</option>
                                <option value="price_desc">Từ cao đến thấp</option>
                            </select>
                        </div>
                    </div>
                    <%if (count > 0)
                        {%>
                     <div class="right">
                        có <span class="do"><%=count%></span> sản phẩm phù hợp.
                    </div>
                    <%} %>
                </div>
                <div class="wp-main-danhmuc-sp">
                    <div class="wp-list-sp-danhmuc">
                        <div class="row product-fs">
                            <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                                { %>
                            <div class="col-md-4 col-sm-4 col-xs-6">
                                <div class="wp-item-sp wp-item-sp-page item-sp-b">
                                    <div class="img img-cover">
                                        <a href="<%=ViewPage.GetURL(listItem[i].BrandID,listItem[i].Code) %>" title="<%=listItem[i].Name %>" class="h_1">
                                            <img class="lazy" title="<%=listItem[i].Name %>" alt="<%=listItem[i].Name %>" src="<%=Utils.GetResizeFile(listItem[i].File,4,245,245) %>">
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
                                        <h3 class="h2_title"><a href="<%=ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code)%>"><%=listItem[i].Name %></a></h3>
                                        <%-- <div class="rating">
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
                                            <%=string.Format("{0:#,##0}", listItem[i].Price)%>đ <%if (listItem[i].SellOffPercent > 0)
                                                                                                    { %><span class="SellOff">(-<%=listItem[i].SellOffPercent %>%)</span><%} %>
                                        </div>
                                        <%}
                                            else
                                            { %>
                                        <div class="int">Liên hệ để nhận báo giá</div>
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
                            <!-- end item sp -->
                        </div>

                        <div class="phantrang text-center">
                            <ul class="pagination">
                                <%= GetPagination(model.page, model.PageSize, model.TotalRecord)%>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="text_post_content effect7" id="effect7">
                    <p>
                        <%=ViewPage.CurrentPage.Content %>
                    </p>

                </div>

                <%if (!string.IsNullOrEmpty(ViewPage.CurrentPage.Content))
                    {%>
                <div class="text_post_content">

                    <p class="show-more" style="display: block;">
                        <a class="readmore" id="show-more-page"><i class="fa fa-search"></i></a>
                    </p>
                </div>
                <%} %>


            </div>
        </div>
    </div>
</section>





