<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<form id="vswForm" name="vswForm" method="post">

    <input type="hidden" id="_vsw_action" name="_vsw_action" />

    <div id="toolbar-box">
        <div class="t">
            <div class="t">
                <div class="t"></div>
            </div>
        </div>
        <div class="m">
            <div class="toolbar-list" id="toolbar">
                <%=GetListCommand("config|Xóa cache,space,post|Lưu,space,cancel|Đóng")%>
            </div>
            <div class="pagetitle icon-48-langmanager">
                <h2>Chuyên mục : Thêm nhiều</h2>
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

    <%= ShowMessage()%>

    <div id="element-box">
        <div class="t">
            <div class="t">
                <div class="t"></div>
            </div>
        </div>
        <div class="m">
            <div class="col width-100">
                <table class="admintable">
                    <tr>
                        <td class="key">
                            <label>Danh sách link:</label>
                        </td>
                        <td>
                            <textarea class="text_input" style="height: 200px; width: 98%" name="Redirections"></textarea>
                            <p>Nhập dạng:</p>
                            <p>Link cũ=Link mới -> Xuống dòng</p>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clr"></div>
        </div>
        <div class="b">
            <div class="b">
                <div class="b"></div>
            </div>
        </div>
    </div>
</form>