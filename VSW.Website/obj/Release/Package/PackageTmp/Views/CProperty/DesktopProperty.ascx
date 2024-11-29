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

    if ((listBrand == null || listBrand.Count == 0) && ViewPage.CurrentPage.ParentID != 1)
    {
        var parentPage = SysPageService.Instance.GetByID_Cache(ViewPage.CurrentPage.ParentID);

        listBrand = SysPageService.Instance.GetByParent_Cache(parentPage, true);
    }

    var listChildPage = SysPageService.Instance.GetByParent_Cache(ViewPage.CurrentPage, false);

    if ((listChildPage == null || listChildPage.Count == 0) && ViewPage.CurrentPage.ParentID != 1)
    {
        var parentPage = SysPageService.Instance.GetByID_Cache(ViewPage.CurrentPage.ParentID);
        listChildPage = SysPageService.Instance.GetByParent_Cache(parentPage, false);
    }

    var listItem = ViewBag.Data as Dictionary<WebPropertyEntity, List<WebPropertyEntity>>;

    if (listItem == null || listItem.Count < 0)
    {
        return;
    }
%>



<div class="col-md-3 hidden-sm hidden-xs">
    <div class="wp-sidebar-fillter">
        <div class="box-fillter bg-white">
            <p class="h3-title-sidebar">Bộ lọc sản phẩm </p>


            <div class="aside-filter">
                <div class="aside-hidden-mobile">
                    <%if (listBrand.Count > 0)
                        { %>
                    <aside class="aside-item filter-vendor">
                        <div class="aside-title">
                            <p class="title-head margin-top-0"><span>Thương hiệu</span></p>
                        </div>
                        <div class="aside-content filter-group">
                            <ul class="filter-vendor ul-b filterBrand">
                                <%for (var i = 0; listBrand != null && i < listBrand.Count; i++)
                                    {
                                        string imgbrand = !string.IsNullOrEmpty(listBrand[i].File) ? listBrand[i].File.Replace("~/", "/") : "";
                                %>
                                <li class="filter-item filter-item--check-box filter-item--green" data-url="<%=ViewPage.GetPageURL(listBrand[i])%>">
                                    <span>
                                        <label data-filter="<%=ViewPage.GetPageURL(listBrand[i]) %>" class="ant-shoe">
                                            <input type="checkbox" id="filter-ant-shoe" <%if (ViewPage.CurrentPage.ID == listBrand[i].ID)
                                                {%>checked="checked"
                                                <%}%> />
                                            <i class="fa"></i>
                                        </label>

                                        <%=listBrand[i].Name %><span style="float: right"> <%=listBrand[i].Count1%></span>
                                    </span>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </aside>
                    <%} %>
                    <%if (listChildPage != null && listChildPage.Count > 0)
                        { %>
                    <aside class="aside-item filter-vendor">
                        <div class="aside-title">
                            <p class="title-head margin-top-0"><span>Chủng loại</span></p>
                        </div>
                        <div class="aside-content filter-group">
                            <ul class="filter-vendor ul-b filterBrand">
                                <%for (var i = 0; listChildPage != null && i < listChildPage.Count; i++)
                                    {
                                        string imgbrand = !string.IsNullOrEmpty(listChildPage[i].File) ? listChildPage[i].File.Replace("~/", "/") : "";
                                %>
                                <li class="filter-item filter-item--check-box filter-item--green" data-url="<%=ViewPage.GetPageURL(listChildPage[i])%>">
                                    <span>
                                        <label data-filter="<%=ViewPage.GetPageURL(listChildPage[i]) %>" class="ant-shoe">
                                            <input type="checkbox" id="filter-ant-shoe" <%if (ViewPage.CurrentPage.ID == listChildPage[i].ID)
                                                {%>checked="checked"
                                                <%}%> />
                                            <i class="fa"></i>
                                        </label>
                                        <%=listChildPage[i].Name %> <span style="float: right"><%=listChildPage[i].Count%></span>
                                    </span>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </aside>
                    <%} %>
                    <%if (listItem != null && listItem.Count > 0)
                        { %>
                    <%foreach (var item in listItem.Keys)
                        {
                            var listChildItem = listItem[item];
                    %>
                    <aside class="aside-item filter-price">
                        <%if (listChildItem.Count > 0)
                            { %>
                        <div class="aside-title">
                            <p class="title-head margin-top-0"><span><%=item.Name %></span></p>
                        </div>
                        <%}%>
                        <div class="aside-content filter-group">
                            <ul class="ul-b right-property">
                                <%for (var k = 0; listChildItem != null && k < listChildItem.Count; k++)
                                    {
                                        int MenuID = ViewPage.CurrentPage.MenuID;
                                        int BrandID = ViewPage.CurrentPage.BrandID;
                                        var a = ModProductService.Instance.CreateQuery()
                                        .Where(o => o.Activity == true)
                                        //.WhereIn(o=>o.ID,ModPropertyService.Instance.CreateQuery())
                                        .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))

                                        .Where(BrandID > 0, o => o.BrandID == BrandID).ToList_Cache();
                                        if (listChildItem[k] == null) continue;
                                %>
                                <%if (ArrAtr != null && System.Array.IndexOf(ArrAtr, listChildItem[k].ID) > -1)
                                    {%>
                                <li class="filter-item filter-item--check-box filter-item--green" onclick="location.href = '<%=GetRemoveAtr(listChildItem[k].ID) %>'">
                                    <span>

                                        <label for="filter-id1">
                                            <input type="checkbox" name="checkbox" checked>
                                            <i class="fa"></i>
                                        </label>
                                        <%=listChildItem[k].Name%>                                                                              
                                    </span>
                                </li>
                                <%} %>
                                <%else
                                    { %>
                                <li class="filter-item filter-item--check-box filter-item--green" onclick="location.href = '<%=GetAddAtr(listChildItem, listChildItem[k].ID) %>'">
                                    <span>
                                        <label for="filter-id1">
                                            <input type="checkbox" name="checkbox">
                                            <i class="fa"></i>
                                        </label>
                                        <%=listChildItem[k].Name%>
                                        <%} %>
                                    </span>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                    </aside>
                    <%}
                        } %>
                </div>
            </div>
        </div>
    </div>
</div>
