<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% 
    var listItem = ViewBag.Data as List<SysPageEntity>;
    string url = ViewPage.SearchURL;
    //var listBrand = ModBrandService.Instance.GetByLang(ViewPage.CurrentLang.ID);
%>


<div class="sidebar__persional">
    <ul class="sidebar__list">
        <!-- sidebar__item--active -->
        <li class="sidebar__item js__sidebar-tabs js__main-color sidebar__item--active">
            <i class="far fa-play-circle"></i>
            Cá Nhân
        </li>
        <li class="sidebar__item js__sidebar-tabs js__main-color">
            <i class="fas fa-compact-disc"></i>
            Khám Phá
        </li>
        <li class="sidebar__item js__sidebar-tabs js__main-color">
            <i class="fas fa-chart-line"></i>
            #zingchart
        </li>
        <li class="sidebar__item js__main-color sidebar__item-radio js__toast">
            <i class="fas fa-broadcast-tower"></i>
            Radio
        <span>Live</span>
        </li>
        <li class="sidebar__item js__main-color js__toast">
            <i class="far fa-list-alt"></i>
            Theo Dõi
        </li>
    </ul>
</div>
 <div class="sidebar__line"></div>
 <div class="sidebar__library">
     <div class="sidebar__library-top">
         <ul class="sidebar__library-top-list sidebar__list">
             <!-- sidebar__item--active -->
             <li class="sidebar__item js__main-color js__toast">
                 <i class="fas fa-music"></i>
                 Nhạc Mới
             </li>
             <li class="sidebar__item js__main-color js__toast">
                 <i class="fab fa-buromobelexperte"></i>
                 Thể Loại
             </li>
             <li class="sidebar__item js__main-color js__toast">
                 <i class="fas fa-star"></i>
                 Top 100
             </li>
             <li class="sidebar__item js__main-color js__toast">
                 <i class="fas fa-photo-video"></i>
                 MV
             </li>
         </ul>
     </div>
   
     
 </div>
