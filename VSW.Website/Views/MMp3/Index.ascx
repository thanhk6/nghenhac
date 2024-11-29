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
   

    var model = ViewBag.Model as MProductModel;
    int count;

    count = ViewBag.coutProduct;
    //string sort = VSW.Core.Web.HttpQueryString.GetValue("sort").ToString();
%>
