<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModProductModel;
    var listItem = ViewBag.Data as List<ModProductEntity>;

    long _Total = ViewBag.Total;
%>
<form id="vswForm" name="vswForm" method="post">

    <input type="hidden" id="_vsw_action" name="_vsw_action" />
    <input type="hidden" id="boxchecked" name="boxchecked" value="0" />

    <div id="toolbar-box">
        <div class="t">
            <div class="t">
                <div class="t"></div>
            </div>
        </div>
        <div class="m">
            <div class="toolbar-list" id="toolbar">
                <%=GetListCommand("import|Sửa giá,free|Xóa ảnh,new|Thêm,edit|Sửa,space,publish|Duyệt,unpublish|Bỏ duyệt,space,delete|Xóa,copy|Sao chép,space,config|Xóa cache")%>
            </div>
            <div class="pagetitle icon-48-article">
                <h2>Account</h2>
            </div>
            <div class="clr"></div>
        </div>
        <div class="b">
            <div class="b">
                <div class="b"></div>
            </div>
        </div>
    </div>
    <div class="clr"></div>

    <script type="text/javascript">

        var VSWController = "ModProduct";

        var VSWArrVar = [
            "filter_status", "Status",
            "filter_border", "BorderID",
            "filter_rank", "RankID",
            "filter_price", "PriceID",
            "filter_lang", "LangID",
            "limit", "PageSize"
        ];

        var VSWArrVar_QS = [
            "filter_search", "SearchText"
        ];

        var VSWArrQT = [
            "<%= model.PageIndex + 1 %>", "PageIndex",
            "<%= model.Sort %>", "Sort"
        ];

        var VSWArrDefault = [
            "1", "PageIndex",
            "1", "LangID",
            "20", "PageSize"
        ];
    </script>

    <%= ShowMessage()%>

    <div id="element-box">
        <div class="t">
            <div class="t">
                <div class="t"></div>
            </div>
        </div>
        <div class="m">

            <table>
                <tr>
                    <td width="100%">Lọc:
                        <input type="text" value="<%= model.SearchText %>" id="filter_search" class="text_area" onchange="VSWRedirect('Import');return false;" />
                        <button onclick="VSWRedirect('Import');return false;">Tìm kiếm</button>
                    </td>
                    <td nowrap="nowrap">
                        <%if(CPLogin.CurrentUser.IsAdministrator) {%>
                        <p>
                            Tổng tiền card: <span style="font-size:18px; color:red;"><%=string.Format("{0:#,##0}", _Total)%> VND</span>
                        </p>

                        <select id="filter_status" onchange="VSWRedirect('Import')" class="inputbox" size="1">
                            <option value="0">(Tình trạng)</option>
                            <option value="1" <%if(model.Status == 1) {%>selected<%} %>>Chưa bán</option>
                            <option value="2" <%if(model.Status == 2) {%>selected<%} %>>Đã bán</option>
                            <option value="3" <%if(model.Status == 3) {%>selected<%} %>>Đã đặt cọc</option>
                        </select>
                        <%} %>

                        <select id="filter_border" onchange="VSWRedirect('Import')" class="inputbox" size="1">
                            <option value="0">(Khung)</option>
                            <%= Utils.ShowDdlMenuByType2("Border", model.LangID, model.BorderID)%>
                        </select>
                        <select id="filter_rank" onchange="VSWRedirect('Import')" class="inputbox" size="1">
                            <option value="0">(Rank)</option>
                            <%= Utils.ShowDdlMenuByType2("Rank", model.LangID, model.RankID)%>
                        </select>
                        <select id="filter_price" onchange="VSWRedirect('Import')" class="inputbox" size="1">
                            <option value="0">(Khoảng giá)</option>
                            <%= Utils.ShowDdlMenuByType2("Price", model.LangID, model.PriceID)%>
                        </select>
                        Ngôn ngữ :<%= ShowDDLLang(model.LangID)%>
                    </td>
                </tr>
            </table>

            <table class="adminlist" cellspacing="1">
                <thead>
                    <tr>
                        <th width="1%">#</th>
                        <th width="1%">
                            <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                        </th>
                        <th class="title">
                            <%= GetSortLink("Tiêu đề", "Name")%>
                        </th>
                        <th style="width: 40px" nowrap="nowrap">
                            <%= GetSortLink("Ảnh", "File")%>
                        </th>
                        <th nowrap="nowrap">
                            <%= GetSortLink("Giá card", "Price")%>
                        </th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <td colspan="15">
                            <del class="container">
                                <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord)%>
                            </del>
                        </td>
                    </tr>
                </tfoot>
                <tbody>
                    <%for (var i = 0; listItem != null && i < listItem.Count; i++){ %>
                    <tr class="row<%= i%2 %>">
                        <td align="center">
                            <%= i + 1%>
                        </td>
                        <td align="center">
                            <%= GetCheckbox(listItem[i].ID, i)%>
                        </td>
                        <td>
                            <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)"><%= listItem[i].Name%></a>
                            <p class="smallsub">(<span>Mã</span>: <%= listItem[i].Code%>)</p>
                        </td>
                        <td align="center">
                            <%= Utils.GetMedia(listItem[i].File, 40, 40)%>
                        </td>
                        <td align="center">
                            <input type="text" class="text_input" style="width: 150px;" id="Price_<%=listItem[i].ID %>" name="Price_<%=listItem[i].ID %>" onkeyup="document.getElementById('sp_Price_<%=listItem[i].ID %>').innerHTML = formatDollar(this.value);" value="<%= listItem[i].Price %>" />
                            <input type="button" style="height: 25px; margin: 0 0 0 10px; cursor: pointer" onclick="update_price('<%=listItem[i].ID %>', jQuery(this).parent().find('.text_input').val())" value="Cập nhật" />
                            <span id="sp_Price_<%=listItem[i].ID %>" style="margin: 0 0 0 10px; height: 25px; line-height: 25px;"></span>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>

            <div class="clr"></div>
        </div>
        <div class="b">
            <div class="b">
                <div class="b"></div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function update_price(ProductID, Price) {
            var ranNum = Math.floor(Math.random() * 999999);
            var dataString = 'ProductID=' + ProductID + '&Price=' + Price + '&rnd=' + ranNum;

            jQuery.ajax({
                type: "get",
                url: "/{CPPath}/Ajax/UpdatePrice.aspx",
                data: dataString,
                dataType: "xml",
                success: function (req) {

                    jQuery(req).find("Item").each(function () {
                        
                        var content = jQuery(this).find("Html").text();
                        var params = jQuery(this).find("Params").text();
                        var js = jQuery(this).find("JS").text();

                        if (params != '') {
                            jQuery('#sp_Price_' + ProductID).html('<span style="color:#f00">' + params + '</span>');
                        }

                        if (content != '') {
                            jQuery('#Product_' + ProductID).val(content);
                            jQuery('#sp_Price_' + ProductID).html(js);
                        }
                    });

                },
                error: function () { }
            });
        }
    </script>
</form>
