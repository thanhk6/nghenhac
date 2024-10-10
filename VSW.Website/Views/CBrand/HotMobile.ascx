<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var listItem = ViewBag.Data as List<ModNewsEntity>;
    if (listItem == null) return;

    var item = listItem[0];
%>

<div class="col-sm-12 col-xs-12">
    <div class="wp-sidebar">
        <div class="title-sidebar">
            <h2 class="h2-title">Tin Hot</h2>
        </div>
        <div class="wp-list-tin-xemnhieu">
            <ul class="ul-b list-tin-xn">
                <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                    {
                        string url = ViewPage.GetURL(listItem[i].Code);

                %>
                <li><a href="<%=url %>"><%=listItem[i].Name %></a></li>
                <%} %>
            </ul>
        </div>
    </div>
</div>
