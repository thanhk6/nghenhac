<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>


<div class="modal fade loading" id="modal-baogia">
    <div class="modal-dialog ">
        <div class="modal-content">
            <div class="input-baogia">
                <form method="post" name="subscribe_form" id="subscribe_form" enctype="multipart/form-data">
                    <div class="row-input">
                        <label>Anh/chị:  <span>*</span></label>
                        <div>
                            <input name="Gender" type="radio" class="check-box" value="Nam" />Nam
                                    <input name="Gender" type="radio" class="check-box" value="Nữ" />Nữ
                        </div>
                    </div>
                    <div class="row-input">
                        <label>Tên:  <span>*</span></label>
                        <input type="text" name="Name" id="Name" placeholder="Nhập tên" class="input-text">
                    </div>
                    <div class="row-input">
                        <label>Số điện thoại:  <span>*</span></label>
                        <input type="text" name="Phone" id="Phone" placeholder="Nhập số điện thoại" class="input-text">
                    </div>
                    <div class="row-input">
                        <label>Email:  </label>
                        <input type="text" name="Email" id="Email" placeholder="Nhập địa chỉ Email" class="input-text">
                    </div>
                    <div class="row-input">
                        <label>Công ty:  </label>
                        <input type="text" name="Company"id="Company" placeholder="Nhập tên công ty" class="input-text">
                    </div>
                    <div class="row-input">
                        <label>Địa chỉ công trình:  <span>*</span></label>
                        <input type="text" name="address" id="address" placeholder="Nhập địa chỉ công trình" class="input-text">
                    </div>
                    <div class="row-input">
                        <label>Yêu cầu: </label>
                        <textarea name="Content" id="content" placeholder="Nội dung"></textarea>
                    </div>

                    <div class="row-input">
                        <label>Đính kèm file danh sách sản phẩm:  <span>*</span></label>
                        <input type="file" name="CvFile" id="CvFile">
                    </div>

                    <%--<div class="btn-baogia-child">
                        <button id="subscribe-btn">Nhận báo giá</button>
                    </div>--%>

                     <div class="btn-baogia-child">
                        <input type="button" id="btnbaogia" class="subscribe-btn" value="Đăng ký nhận báo giá">
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
