<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var listItem = ViewBag.Data as List<ModBrandEntity>;

%>
<div class="col-md-3 col-sm-4 col-xs-12">
    <div class="brand-cate">
        <div class="brand-cate-title">
            Thương hiệu bán chạy						
        </div>
        <div class="brand-content">
            <%if (listItem.Count > 0)
                { %>
            <ul>
                <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                    {
                        if (listItem[i].File == null) continue;
                %>
                <li><a href="<%=ViewPage.GetURL(listItem[i].Code)%>">
                    <div class="brand-content-img">
                        <img src="<%=listItem[i].File.Replace("~/", "/") %>">
                    </div>
                    <div class="brand-content-start">
                        <%-- <p>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </p>--%>
                        <%-- <p>(20 đánh giá)</p>--%>
                    </div>

                </a></li>
                <%}%>
            </ul>
            <%}  %>
        </div>
    </div>
</div>
