<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%  var item = ViewBag.Data as ModAdvEntity;
    if (item == null || string.IsNullOrEmpty(item.File)) return; %>
<div class="modal fade" id="banner_popup">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-body">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <a href="<%=item.URL %>" title="<%=item.Name %>" rel="nofollow">
                    <img src="<%=item.File.Replace("~/", "/") %>" alt="<%=item.Name %>" />
                </a>
            </div>
        </div>
    </div>
</div>

