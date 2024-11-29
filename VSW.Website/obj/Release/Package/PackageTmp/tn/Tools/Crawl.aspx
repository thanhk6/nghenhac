<%@ Page Language="C#" AutoEventWireup="true" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!VSW.Lib.Global.CPLogin.IsLogin() || !VSW.Lib.Global.CPLogin.CurrentUser.IsAdministrator)
        {
            Response.Redirect("Login.aspx?ReturnPath=" + Server.UrlEncode(Request.RawUrl));
            return;
        }
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        string url = Request.Form["Url"].ToString();
        int MenuID = VSW.Core.Global.Convert.ToInt(Request.Form["MenuID"]);
        int Page = VSW.Core.Global.Convert.ToInt(Request.Form["Page"]);

        if (string.IsNullOrEmpty(url) || url == "- Đường link crownspace -")
        {
            lbResult.Text += "Chưa nhập đường link.<br />";
            return;
        }
        if (MenuID < 1)
        {
            lbResult.Text += "Chưa chọn chuyên mục.<br />";
            return;
        }
        if (Page < 0)
        {
            lbResult.Text += "Chưa chọn số trang.<br />";
            return;
        }

        string count = VSW.Lib.Global.Crawler.getdenled(url, MenuID, 1, 1, Page);




        lbResult.Text = "Download thành công " + count + "sản phẩm";
    }
</script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lấy tin tự động</title>
</head>
<body>
    <form id="form1" runat="server">

        <p style="text-align: center">
            <h1>Lấy tin tự động</h1>
        </p>

        <table width="100%" cellpadding="10" cellspacing="10" style="border: 1px dotted #CCC; border-collapse: collapse;">
            <thead>
                <tr>
                    <th width="50%">Link nguồn</th>
                    <th width="30%">Chuyên mục đích</th>
                    
                    <th width="20%">Số trang</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <input type="text" name="Url" id="Url" value="" placeholder="https://kgvietnam.com/sanpham/" style="width: 400px;" />&nbsp;
                    </td>
                    <td>
                        <select name="MenuID" id="MenuID">
                            <option value="0">- chọn chuyên mục -</option>
                            <%=Utils.ShowDdlMenuByType("Product", 1, 0)%>
                        </select>
                    </td>

                   

                    <td>
                        <select name="Page" id="Page">
                            <option value="0">- chọn số trang -</option>
                            <%for (int i = 1; i <= 200; i++)
                                {%>
                            <option value="<%=i %>">- <%=i %>-</option>
                            <%} %>
                        </select>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center; padding-top: 30px; color: #FF0000; font-weight: bold;">
            <div style="text-align: center; padding-top: 30px; color: #FF0000; font-weight: bold;">
                <asp:Button ID="btnRun" runat="server" OnClick="btnRun_Click" Text="Thực hiện" Width="111px" />
                <br />
                <asp:Literal ID="lbResult" runat="server"></asp:Literal>
            </div>
        </div>
        <div></div>
    </form>
</body>
</html>
