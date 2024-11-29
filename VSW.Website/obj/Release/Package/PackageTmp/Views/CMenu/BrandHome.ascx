<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    string url = ViewPage.SearchURL;
    //var listBrand = ModBrandService.Instance.GetByLang(ViewPage.CurrentLang.ID);
%>
<section class="sec-05 mgb35">
    <div class="container">
        <div class="wp-logo-brand">
            <div class="title_section--b">
                <h2 class="h2_title">Các thương hiệu hàng đầu</h2>
            </div>
            <div class="brand-new-kn">
                <div class="row">
                    <%for (var i = 0; listItem != null && i < (listItem.Count>15?15:listItem.Count); i++)
                        {

                    %>
                    <div class="col-md-3 col-sm-4 col-xs-6">
                        <a href="<%=ViewPage.GetPageURL(listItem[i]) %>">
                            <img  src="<%=Utils.GetResizeFile(listItem[i].File,4,245,245) %>" alt="<%=listItem[i].Name%>"></a>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</section>
