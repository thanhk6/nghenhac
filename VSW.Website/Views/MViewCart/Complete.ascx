<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var item = ViewBag.Data as ModOrderEntity;
    var model = ViewBag.Model as MViewCartModel;

    var cart = new Cart();

    long total = 0;
%>



