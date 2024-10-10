//product
function productUpload() {
    $('.loading').show();

    var arrFile = $('#ArrFile').get(0).files;
    var formData = new FormData();
    for (var i = 0; i < arrFile.length; i++) {
        formData.append(arrFile[i].name, arrFile[i]);
    }
    $.ajax({
        url: '/' + window.CPPath + '/Ajax/ProductUpload.aspx?ProductID=' + $('#RecordID').val(),
        type: 'POST',
        enctype: 'multipart/form-data',
        processData: false,
        contentType: false,
        cache: false,
        data: formData,
        xhr: function () {
            var xhr = $.ajaxSettings.xhr();
            if (xhr.upload) {
                xhr.upload.addEventListener('progress', function (event) {
                    var percent = 0;
                    var position = event.loaded || event.position;
                    var total = event.total;
                    if (event.lengthComputable) {
                        percent = Math.ceil(position / total * 100);
                    }
                    //update progressbar
                    $('.progress-bar').css({ 'width': + percent + '%', 'display': 'block' });
                    $('.progress-label').css({ 'display': 'block' }).text(percent + '%');
                }, true);
            }
            return xhr;
        }
    }).done(function (data) {
        var node1 = data.Node1;
        var node2 = data.Node2;

        if (node2 != '') {
            sw_alert('Thông báo !', node2);
        }

        $('#list-file').html(node1);

        $('.progress-bar').css({ 'display': 'none' });
        $('.progress-label').css({ 'display': 'none' });

        $('.loading').hide();
    });
}

function productUpdate(value) {
    $.ajax({
        type: 'POST',
        url: '/' + window.CPPath + '/Ajax/ProductUpdate.aspx',
        data: 'ProductID=' + $('#RecordID').val() + '&Value=' + value,
        success: function (data) {
        },
        error: function () { }
    });
}
function productRemove(id) {
    $.ajax({
        type: 'POST',
        url: '/' + window.CPPath + '/Ajax/ProductRemove.aspx',
        data: 'ProductID=' + $('#RecordID').val() + '&FileID=' + id,
        success: function (data) {
            var node1 = data.Node1;
            var node2 = data.Node2;

            if (node2 != '') {
                sw_alert('Thông báo !', node2);
            }

            $('#list-file').html(node1);
        },
        error: function () { }
    });
}
//end product
//combo
function UpCombo(productID, quantity, price, comboID) {
    $('.loading').show();

    $.ajax({
        url: '/' + window.CPPath + '/Ajax/UpCombo.aspx?ProductID=' + productID + '&Quantity=' + quantity + '&Price=' + price + '&ComboID=' + comboID,
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            var node1 = data.Node1;
            var node2 = data.Node2;

            if (node2 != '') {
                sw_alert('Thông báo !', node2);
                return;
            }

            $('#list-combo').html(node1);

            $('.combo-save').on('click', function () {
                var recordID = $(this).data('id');
                if (recordID > 0) {
                    UpCombo($('#RecordID').val(), $('#Quantity-' + recordID).val(), $('#Price-' + recordID).val(), recordID);
                }
                else {
                    UpCombo($('#RecordID').val(), $('#Quantity-0').val(), $('#Price-0').val(), '0');
                }
            });

            $('.combo-remove').on('click', function () {
                location.href = '/' + window.CPPath + '/ModProduct/Remove.aspx/ComboID/' + $(this).data('id');
            });

            sw_alert('Thông báo !', 'Lưu combo thành công');

            $('.loading').hide();
        },
        error: function () {
            sw_alert('Thông báo !', 'Có lỗi xảy ra khi thao tác. F5 và thử lại.');
            $('.loading').hide();
        }
    });
}
//end combo

// phần   ảnh phụ
function ShowFile() {
    var finder = new CKFinder();
    finder.basePath = '../';
    finder.selectActionFunction = refreshFile;
    finder.popup();
    return false;
}

function refreshFile(arg) {
    addFile(arg);
}
function addFile(file) {
    $.ajax({
        type: 'post',
        url: '/' + window.CPPath + '/Ajax/AddFile.aspx',
        data: {
            Name: $('#Name').val(),
            MenuID: $('#MenuID').val(),
            ProductID: $('#RecordID').val(),
            File: file
        },
        dataType: 'json',
        success: function (data) {
            var js = data.Node3;
            var params = data.Node1;
            var content = data.Node2;

            if (params != '') {
                sw_alert('Thông báo !', params);
                return;
            }
            if (js != '') {
                location.href = '/' + window.CPPath + '/ModProduct/Add.aspx/RecordID/'+ node3;
                return;
            }
            $('#list-file').html(content);
        },
        error: function (status) { }
    });
}
function DeleteFile(id) {
    $.ajax({
        type: 'POST',
        url: '/' + window.CPPath + '/Ajax/DeleteFile.aspx',
        data: 'ProductID=' + $('#RecordID').val() + '&FileID=' + id,
        success: function (data) {
            var node1 = data.Node1;
            var node2 = data.Node2;

            if (node2 != '') {
                $('#list-file').html(data.Node2);
            }
            if (node1 != '') {
                sw_alert('Thông báo !',Note1);
            }
        },
        error: function () { }
    });
}
//thay doi vi tri anh phu
function upFile(file) {
    $.ajax({
        type: 'post',
        url: '/' + window.CPPath + '/Ajax/UpFile.aspx',
        data: {
            ProductID: $('#RecordID').val(),
            File: file
        },
        dataType: 'json',
        success: function (data) {
            var content = data.Node2;
            var orr = data.Node1
            if (content != '') {
                $('#list-file').html(content);
            }
            if (orr != '') {
                sw_alert('Thông báo !', Note1);
            }
        },
        error: function (status) { }
    });
}
function downFile(file) {
    $.ajax({
        type: 'post',
        url: '/' + window.CPPath + '/Ajax/DownFile.aspx',
        data: {
            ProductID: $('#RecordID').val(),
            File: file
        },
        dataType: 'json',
        success: function (data) {
            var content = data.Node2;
            var orr = data.Node1;
            if (content != '') {
                $('#list-file').html(content);
            }
            if (orr!='') {
                sw_alert('Thông báo !', Note1);
            }
        },
        error: function (status) { }
    });
}
// end thúc phần ảnh phụ
function CloseGift(arg) {
    if (window.opener)
        window.opener.refreshGift(arg);
    else
        window.parent.refreshGift(arg);

    window.close();
}


function refreshGift(arg) {
    addGift(arg);
}

//kết thúc phần chọn quà tặng
function ShowFileLanding() {
    var finder = new CKFinder();
    finder.basePath = '../';
    finder.selectActionFunction = refreshFileLanding;
    finder.popup();

    return false;
}
function refreshFileLanding(arg) {
    var langID = $("#LandID").val();
    addFileLanding(langID, arg);
}
// chọn quà tăng 
function ShowGiftForm(sValue) {
    window.open('/' + window.CPPath + '/FormGift/Index.aspx?Value=' + sValue, '', 'width=1024, height=800, top=80, left=200,scrollbars=yes');
    return false;
}
function addGift(giftID) {
    $.ajax({
        type: 'post',
        url: '/' + window.CPPath + '/Ajax/AddGift.aspx',
        data: {
            Name: $('#Name').val(),
            MenuID: $('#MenuID').val(),
            ProductID: $('#RecordID').val(),
            GiftID: giftID
        },
        dataType: 'json',
        success: function (data) {
            var js = data.node3;
            var params = data.Node2;
            var content = data.Node1;

            if (params != '') {
                sw_alert('Thông báo !', params);
                return;
            }
            //if (js != '') {
            //    location.href = '/' + window.CPPath + '/ModProduct/Add.aspx/RecordID/' + js;
            //    return;
            //}

            $('#list-gift').html(content);
        },
        error: function (status) { }
    });
}
function deleteGift(giftID) {
    $.ajax({
        type: 'post',
        url: '/' + window.CPPath + '/Ajax/DeleteGift.aspx',
        data: {
            ProductID: $('#RecordID').val(),
            GiftID: giftID
        },
        dataType: 'json',
        success: function (data) {
            var content = data.Node2;

            $('#list-gift').html(content);
        },
        error: function (status) { }
    });
}
function upGift(giftID) {
    $.ajax({
        type: 'post',
        url: '/' + window.CPPath + '/Ajax/UpGift.aspx',
        data: {
            ProductID: $('#RecordID').val(),
            GiftID: giftID
        },
        dataType: 'json',
        success: function (data) {
            var content = data.Node2;

            $('#list-gift').html(content);
        },
        error: function (status) { }
    });
}
function downGift(giftID) {
    $.ajax({
        type: 'post',
        url: '/' + window.CPPath + '/Ajax/DownGift.aspx',
        data: {
            ProductID: $('#RecordID').val(),
            GiftID: giftID
        },
        dataType: 'json',
        success: function (data) {
            var content = data.Node2;

            $('#list-gift').html(content);
        },
        error: function (status) { }
    });
}

// hiên thị ảnh phụ
function ShowImageForm(sValue) {
    window.open('/' + window.CPPath + '/FormImageProduct/Index.aspx?Value=' + sValue, '', 'width=1024, height=800, top=80, left=200,scrollbars=yes');
    return false;
}




