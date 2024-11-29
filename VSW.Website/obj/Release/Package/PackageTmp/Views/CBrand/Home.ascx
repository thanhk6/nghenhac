<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var listItem = ViewBag.Data as List<ModBrandEntity>;
%>

<section id="partner">
    <div class="subtitle"><div><span>Nhãn hiệu</span></div></div>
    <div class="container-fluid">
        <div id="slide-partner" class="slide-kh">
            <%for (var i = 0; listItem != null && i < listItem.Count; i++){
                    if (string.IsNullOrEmpty(listItem[i].File)) continue;
            %>
            <figure>
                <div>
                    <a href="<%=ViewPage.GetURL(listItem[i].Code) %>" title="<%=listItem[i].Name %>">
                        <img src="<%=Utils.GetResizeFile(listItem[i].Icon,4,245,245) %>" alt="<%=listItem[i].Name %>" />
                    </a>
                </div>
            </figure>
            <%} %>
        </div>
    </div>
</section>