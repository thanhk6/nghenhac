<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<form method="post" class="login-form" id="loginForm" name="loginForm">
    <h3 class="form-title">{RS:Login_LoginTitle}</h3>

    <div class="form-group row">
        <label class="col-md-12 col-form-label">{RS:Login_UserName}</label>
        <div class="col-md-12">
            <input type="text" class="form-control" name="UserName" />
        </div>
    </div>
    <div class="form-group row">
        <label class="col-md-12 col-form-label">{RS:Login_Password}</label>
        <div class="col-md-12">
            <input type="password" class="form-control"name="Password" />
        </div>
    </div>
    <div class="form-group row">
        <label class="col-md-12 col-form-label">{RS:Login_Language}</label>
        <div class="col-md-12">
            <select id="lang" name="lang" class="form-control" onchange="document.getElementById('_vsw_action').value='[ChangeLang]['+this.value+']';loginForm.submit();">
                <option <%if (CPViewPage.CurrentLang.Code == "vi-VN"){%> selected <%} %> value="vi-VN">Việt Nam</option>
                <option <%if (CPViewPage.CurrentLang.Code == "en-US"){%> selected <%} %> value="en-US">English</option>
            </select>
        </div>
    </div>

    <%= ShowMessage()%>

    <div class="form-group form-actions">
        <input type="hidden" id="_vsw_action" name="_vsw_action" value="Login" />
        <button type="submit" class="btn blue uppercase" onclick="loginForm.submit();">{RS:Login_LoginSubmit}</button>
    </div>
</form>