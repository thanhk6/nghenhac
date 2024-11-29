<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    string url = ViewPage.SearchURL;
    //var listBrand = ModBrandService.Instance.GetByLang(ViewPage.CurrentLang.ID);
%>
<div class="music__option">
    <ul class="music__option-list js__backgroundColor">
        <!-- top-music__option-item -->
        <li class="tabs-item music__option-item js__main-color music__option-item--active">Tổng quan
        </li>
        <li class="tabs-item music__option-item js__main-color">Bài hát</li>
        <li class="tabs-item music__option-item js__main-color">Playlist</li>
        <li class="tabs-item music__option-item js__main-color">Nghệ sĩ</li>
        <li class="music__option-item mobile-hiden  js__main-color js__toast">
            <i class="fas fa-ellipsis-h"></i>
        </li>
    </ul>
</div>