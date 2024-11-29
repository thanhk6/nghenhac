<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<script runat="server">
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
    int[] ArrAtr = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ArrAtr = ViewBag.ArrAtr;

        if (ArrAtr != null && Array.IndexOf(ArrAtr, -1) > -1)
            ArrAtr = null;
    }
</script>
<% 
    string sort = VSW.Core.Web.HttpQueryString.GetValue("sort").ToString();

    var listBrand = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage, true);

    var listChildPage = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage, false);
    if ((listChildPage == null || listChildPage.Count == 0) && ViewPage.CurrentPage.ParentID != 1)

    {
        var parentPage = SysPageService.Instance.GetByID_Cache(ViewPage.CurrentPage.ParentID);

        listChildPage = SysPageService.Instance.GetByParent_Cache(parentPage, false);
    }
    var listItem = ViewBag.Data as Dictionary<WebPropertyEntity, List<WebPropertyEntity>>;

    if (listItem == null) return;
%>
<div class="hidden-lg hidden-md">
    <div class="body__overlay"></div>
    <ul id="akr-filter" class="top-bread">
        <li class="select">
            <div class="title">Hãng</div>
            <div class="list-filter-product fade-in">
                <ul class="ul-b-2">
                    <li class="col-xs-12">
                        <div class="side-content">
                            <h5>Thương hiệu<span><a href="#"></a></span></h5>
                            <div class="toggle-content2">
                                <ul class="ul-b filterBrand">
                                    <%for (var i = 0; listBrand != null && i < listBrand.Count; i++)
                                        { %>
                                    <li data-url="<%=ViewPage.GetPageURL(listBrand[i])%>">
                                        <div class="checkbox form-group ">
                                            <label>
                                                <input type="checkbox" name="" <%if (ViewPage.CurrentPage.ID == listBrand[i].ID)
                                                    {%>
                                                    checked="checked" <% }%> /><i class="input-helper"></i> <%=listBrand[i].Name %>̣̣ (<%=listBrand[i].Count1 %>)</label>
                                        </div>
                                    </li>
                                    <%} %>
                                </ul>
                            </div>
                        </div>
                    </li>

                </ul>
            </div>
        </li>
        <li class="select">
            <div class="title">Loại</div>
            <div class="list-filter-product fade-in">
                <ul class="ul-b-2">
                    <li class="col-xs-12">
                        <div class="side-content">
                            <h5>chuyên mục<span><a href="#"></a></span></h5>
                            <div class="toggle-content2">
                                <ul class="ul-b filterBrand">
                                    <%for (var j = 0; listChildPage != null && j < listChildPage.Count; j++)
                                        { %>
                                    <li data-url="<%=ViewPage.GetPageURL(listChildPage[j])%>">
                                        <div class="checkbox form-group">
                                            <label>
                                                <input type="checkbox" name="" <%if (ViewPage.CurrentPage.ID == listChildPage[j].ID)
                                                    {%>
                                                    checked="checked" <% }%> />
                                                <i class="input-helper"></i><%=listChildPage[j].Name %> (<%=listChildPage[j].Count %>)</label>
                                        </div>
                                    </li>
                                    <%} %>
                                </ul>
                            </div>
                        </div>
                    </li>

                </ul>
            </div>
        </li>
        <li class="select">
            <div class="title">Tính năng</div>
            <div class="list-filter-product fade-in">
                <ul class="ul-b-2 right-property">
                    <li class="col-xs-12">
                        <div class="side-content">
                            <h5>Thương hiệu<span><a href="#"></a></span></h5>
                            <div class="toggle-content2">
                                <ul class="ul-b right-property ">
                                    <li class="sort-property">
                                        <div class="checkbox form-group">
                                            <label>
                                                <input type="checkbox" id="new_asc" data-url="<%=ViewPage.CurrentURL %>" name="sort" <% if (sort == "new_asc")
                                                    {%>checked="checked"
                                                    <%} %> /><i class="input-helper"></i>Sản Phẩm mới nhất</label>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="checkbox form-group">
                                            <label>
                                                <input type="checkbox" name="sort" <% if (sort == "price_as")
                                                    {%>
                                                    checked="checked" <% }%> /><i class="input-helper"></i>Từ thấp đến cao
                                            </label>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="checkbox form-group">
                                            <label>
                                                <input type="checkbox" name="sort" <% if (sort == "price_desc")
                                                    {%>checked="checked"
                                                    <%} %> /><i class="input-helper"></i> Tư cao đến thấp</label>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </li>

                </ul>
            </div>
        </li>
    </ul>
</div>
