<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
%>
<div class="beadcrumb-tuyen">
<div class="container">
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <ol vocab="https://schema.org/" typeof="BreadcrumbList" class="breadcrumb">
                <%= Utils.GetMapPage(ViewPage.CurrentPage) %>
            </ol>
        </div>
    </div>
</div>
</div>
