<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    //var page = ViewBag.Page as SysPageEntity;
    //if (page == null) return;
    
    var listItem = ViewBag.Data as List<ModCommentEntity>;
    string title = ViewBag.Title;
%>

<div class="dichvu all">
    <div class="comment">
        <h2><%=title %></h2>
        <ul class="noidung_ykien" id="mobile_comment">
            <%for (int i = 0; listItem != null && i < listItem.Count; i++){%>
            <li>
                <h3><a href="<%=listItem[i].Name%>" target="_blank"><%=listItem[i].Name%></a></h3>
                <%--<h4>Trang thông tin của tập đoàn vinamilk</h4>--%>
                <a href="#">
                    <img src="<%=Utils.GetResizeFile(listItem[i].File, 2, 0, 150) %>" height="150" alt="<%=listItem[i].Name%>" />
                </a>
                <div class="content"><%=listItem[i].Summary%></div>
            </li>
            <%} %>
        </ul>
    </div>
</div>