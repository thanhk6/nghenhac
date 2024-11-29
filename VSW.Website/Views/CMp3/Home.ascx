<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    var listItem = ViewBag.Data as List<ModMp3Entity>;

    var list = ModMp3Service.Instance.CreateQuery().ToList_Cache();


%>
<div class="col l-9 m-12 s-12">
    <div class="option-all__songs">
        <ul class="option-all__songs-list songs-list">
           
            <% for(var i=0;list!=null&& i < list.Count; i++) {
                    %>
            <li class="songs-item js__song-item0 " data-index="<%=list[i].ID %>">
                <div class="songs-item-left">
                    <div style="background-image: url(<%=list[i].File.Replace("~/", "/")%>);" class="songs-item-left-img js__songs-item-left-img-0">
                        <div class="songs-item-left-img-playbtn"><i class="fas fa-play"></i></div>
                        <div class="songs-item-left-img-playing-box">
                            <img class="songs-item-left-img-playing" src="/Content/assets/img/songs/icon-playing.gif" alt="playing">
                        </div>
                    </div>

                    <div class="songs-item-left-body">
                        <h3 class="songs-item-left-body-name js__main-color"><%= list[i].Name %></h3>
                        <span class="songs-item-left-body-singer js__sub-color"><%=list[i].AuthorID %></span>
                    </div>
                </div>
                <div class="songs-item-center tablet-hiden mobile-hiden  js__sub-color">
                    <span><%=list[i].Name %> (Remix)</span>
                </div>
                <div class="songs-item-right mobile-hiden ">
                    <span class="songs-item-right-mv ipad-air-hiden"><i class="fas fa-photo-video js__main-color"></i></span>
                    <span class="songs-item-right-lyric ipad-air-hiden"><i class="fas fa-microphone js__main-color"></i></span>
                    <span class="songs-item-right-tym">
                        <i class="fas fa-heart songs-item-right-tym-first"></i>
                        <i class="far fa-heart songs-item-right-tym-last"></i>
                    </span>

                  <%--  <span class="songs-item-right-duration js__sub-color">${song.duration}</span>--%>


                    <span class="songs-item-right-more js__main-color"><i class="fas fa-ellipsis-h"></i></span>
                </div>
            </li>

            <%} %>


        </ul>


    </div>
</div>
