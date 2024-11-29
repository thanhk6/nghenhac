<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    string url = ViewPage.SearchURL;
    //var listBrand = ModBrandService.Instance.GetByLang(ViewPage.CurrentLang.ID);
%>
<div class="wp-menu-top">
    <div class="container">
        <div class="nav_top">
            <div id="trigger-mobile" class="btn btn_all--nav">
                <div class="wp-menu-mobile">
                    <img src="/Content/Desktopnew/images/icon/bar.png" alt="">
                    <span>Tất cả</span>
                </div>
            </div>
            <div class="list_navbar hidden-sm hidden-xs">
                <nav class="navbar navbar-default">
                    <div class="container-fluid">
                        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                            <ul class="nav navbar-nav">
                                <%for (var i = 0; listItem != null && i < listItem.Count; i++)
                                    { %>
                                <li class="dropdown">
                                    <a href="<%=ViewPage.GetPageURL(listItem[i])%>" class="dropdown-toggle"  aria-haspopup="true" aria-expanded="false"> <%=listItem[i].Name %> <%--<span class="caret"></span>--%></a>
                                   <%-- <ul class="dropdown-menu">
                                        <li><a href="#">Gạch lát nền 1</a>
                                        </li>
                                        <li><a href="#">Gạch lát nền 2</a>
                                        </li>
                                        <li><a href="#">Gạch lát nền 3</a>
                                        </li>
                                        <li><a href="#">Gạch lát nền 4</a>
                                        </li>
                                        <li><a href="#">Gạch lát nền 5</a>
                                        </li>
                                    </ul>--%>
                                </li>
                                <%} %>                              
                            </ul>
                        </div>
                    </div>
                </nav>
            </div>
        </div>
    </div>
</div>
