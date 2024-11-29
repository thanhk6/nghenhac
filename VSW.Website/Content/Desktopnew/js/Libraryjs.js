//phân code
//js loc khi click vao cac gia tri
$('.right-property li input').on('change', function () {

    $('input[type="checkbox"][name="' + $(this).attr('name') + '"]').not(this).prop('checked', false);

    filter_product($(this).data('url'));
});
//$('sort-property ').on('change', function () {
//    $('input[type="checkbox"][name="' + $(this).attr('name') + '"]').not(this).prop('checked', false);
//});
// loc thương hiệu
$('.category li,.filterBrand li,.product-fs li').click(function () {
    location.href = $(this).data('url');
});
$('.filterBrand li').click(function () {

    location.href = $(this).data('url');
});
$('#sort').on('change', function () {

    var sort = document.getElementById('sort').value;
    var url = $(this).data('url');   
    filter_sort(url);
});
// nhân báo giá
$('.subscribe-btn').click(function () {
    subscribe();
    //location.reload();
});


$('#subscribe_form').keypress(function (event) {
    var keycode = event.keyCode || event.which;
    if (keycode == '13') {
        subscribe();
    }
});
function get_code(str) {
    return remove_unicode(str).replace(/[^A-Z0-9]/gi, '');
}
function remove_unicode(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a');
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e');
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, 'i');
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o');
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u');
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y');
    str = str.replace(/đ/g, 'd');
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|"| |"|\&|\#|\[|\]|~|$|_/g, '-');
    str = str.replace(/-+-/g, '-');
    str = str.replace(/^\-+|\-+$/g, '');
    return str;
}
function formatDollar(value) {
    return value.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1.');
}
function createCookie(name, value, minutes) {
    var expires = '';
    if (minutes) {
        var date = new Date();
        date.setTime(date.getTime() + (minutes * 60 * 1000));
        expires = '; expires=' + date.toGMTString();
    }
    document.cookie = name + '=' + value + expires + '; path=/';
}
function readCookie(name) {
    var nameEQ = name + '=';
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    createCookie(name, '', -1);
}
function add_cart(productID, quantity, returnpath) {
    if (productID < 1) {
        sw_alert('Thông báo !', 'Sản phẩm không tồn tại.');
        return;
    }
    if (quantity < 1) {
        sw_alert('Thông báo !', 'Số lượng mua phải từ 1 sản phẩm.');
        return;
    }

    location.href = '/gio-hang/Add.html?ProductID=' + productID + '&Quantity=' + quantity + '&returnpath=' + returnpath;
}
function update_cart(productID, quantity, returnpath) {
    location.href = '/gio-hang/Update.html?ProductID=' + productID + '&Quantity=' + quantity + '&returnpath=' + returnpath;
}
function delete_cart(productID, returnpath) {
    sw_confirm('Thông báo !', 'Bạn chắc chắn muốn xóa ?', '/gio-hang/Delete.html?ProductID=' + productID + '&returnpath=' + returnpath)
}
function change_captcha() {
    var e = Math.floor(Math.random() * 999999); document.getElementById('imgValidCode').src = '/Ajax/Security.html?Code=' + e;
}
//mua ngay 
$('.buy_nowdetail').click(function () {
    addcart($(this).data('id'), $(this).data('returnpath'));
});


function addcart(id, returnpath) {
    location.href = '/gio-hang/Add.html?ProductID=' + id + '&Quantity=1&returnpath=' + returnpath;
}
function doSearch() {
    var keyword = $('#keyword').val();
    //var min = $('#min').val().split(',').join('');;
    //var max = $('#max').val().split(',').join('');
    if (keyword == '' && (keyword.length < 2 || keyword == 'Bạn cần tìm hay tư vấn gì hôm nay...')) {
        sw_alert('Thông báo !', 'Từ khóa phải nhiều hơn 1 ký tự.');
        return;
    }
    url = '/tim-kiem.html?keyword=' + keyword;
    location.href = url;

}

$('#btnOrder').click(function () {
    checkout();
});

$('.pagination a').click(function () {
    page++;
    paging_product($(this).data('url'));
});


//$(document).ready(function ($) {
//    "use strict";
//    awe_lazyloadImage()
//});
//function awe_lazyloadImage() {
//    setTimeout(function () {
//        var i = $("[data-lazyload]");
//        i.length > 0 && i.each(function () {
//            var i = $(this),
//                e = i.attr("data-lazyload");
//            i.appear(function () {
//                i.removeAttr("height").attr("src", e)
//            }, {
//                accX: 0,
//                accY: 120
//            }, "easeInCubic")
//        })
//    }, 500)
//}
//window.awe_lazyloadImage = awe_lazyloadImage;
//liên hệ tư vấn 
$('#contact').click(function () {
    feedback();
});
// đăng ký
//$('#btn-dangky').click(function () {
//    register();
//});
