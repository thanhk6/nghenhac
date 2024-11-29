var page = 1;
function paging_product(url) {
    $('.loading').show();
    //atr
    var checkbox = $('.right-property li input[type="checkbox"]:checked').map(function () {
        return this.value;
    }).get();

    var atr = checkbox.join('-');

    var sort = $('.filterTopCtgr li input[type="checkbox"]:checked').map(function () {
        return this.value;
    }).get();
    url = url + '?page=' + page + '&sort=' + sort + '&atr=' + atr;
    console.log('paging:' + url);
    $.ajax({
        url: url,
        type: 'get',
        dataType: 'html',
        success: function (data) {
            data = $(data).find('.product-fs').html();
            data = $.trim(data);

            if (data == '')
                $('.pagination a').hide();

            $('.product-fs').append(data);
            if (page > 1) {
                jQuery('.main-page-danhmuc').scrollTop(9000 * page);
            }
            //$('.product-fs li').click(function () {
            //    location.href = $(this).data('url');
            //})

            $('.loading').hide();
        },
        error: function () { }
    });
}

function filter_product(url) {
    $('.loading').show();
    //atr
    var checkbox = $('.right-property li input[type="checkbox"]:checked').map(function () {
        return this.value;
    }).get();
    var atr = checkbox.join('-');
    var sort = $('.filterTopCtgr li input[type="checkbox"]:checked').map(function () {
        return this.value;
    }).get();
    url = url + '?page=' + page + '&sort=' + sort + '&atr=' + atr;


    console.log('filter:' + url);
    $.ajax({
        url: url,
        type: 'get',
        dataType: 'html',
        success: function (data) { 
           // window.location.hash = atr; 
            data = $(data).find('.product-fs').html();
            data = $.trim(data);
            $('.product-fs').html(data);

            $('.product-fs li').click(function () {
                location.href = $(this).data('url');                
            });
            $('.loading').hide();
        },
        error: function () { }
    });
}
function filter_sort(url) {
    var sort = document.getElementById('sort').value;
    url = url + '?page=' + page+ '&sort=' + sort;
    $.ajax({
        url: url,
        type: 'get',
        dataType: 'html',
        success: function (data) {
            // window.location.hash = atr; 
            data = $(data).find('.product-fs').html();
            data = $.trim(data);
            $('.product-fs').html(data);

            $('.product-fs li').click(function () {
                location.href = $(this).data('url');
            });

            $('.loading').hide();
        },
        error: function () { }
    });
}
function search(keyword) {
    $.ajax({
        url: '/ajax/GetSearch.html',
        data: 'Keyword=' + keyword,
        type: 'GET',
        success: function (data) {
            var node1 = data.Node1;

            $('.result-container').html(node1);

            $('.result-container').show();
        }
    });
}
function checkout() {
    $.ajax({
        url: '/ajax/CheckOut.html',
        data: $('#cart_form').serialize(),
        type: 'POST',
        success: function (data) {
            var html = data.Node1;
            var params = data.Node2;
            if (params != '') {
                //$('.cart-submit').val('Lỗi. F5 thử lại');
                sw_alert('Thông báo !', params);
                return;
            }
            if (html != '') {
                //$('.cart-submit').val('Hoàn thành');
                sw_alert('Thông báo !', html, '/');
                return Location.href = "/trang-chu.html";
            }
        }
    });
}
function trade(SiteCode) {
    $.ajax({
        url: '/' + SiteCode + '/ajax/Trade.html',
        data: new FormData($('#trade_form')[0]),
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            var html = data.Node1;
            var params = data.Node2;

            if (params != '') {
                $('#trade_id').html(params);
                return;
            }
            if (html != '') {
                $('#trade_id').html(html);
                return;
            }
        }
    });
}
function contact(SiteCode) {
    $.ajax({
        url: '/' + SiteCode + '/ajax/Contact.html',
        data: $('#contact_form').serialize(),
        type: 'POST',
        success: function (data) {
            var html = data.Node1;
            var params = data.Node2;
            if (params != '') {
                $('#contact_id').html(params);
                return;
            }
            if (html != '') {
                $('#contact_id').html(html);
                return;
            }
        }
    });
}



function subscribe() {
    $('.loading').show();
    var files = $('#CvFile').prop('files')[0];
    var FormDataJson = new FormData();
    FormDataJson.append('CvFile', files);
    FormDataJson.append('Name', $('#Name').val());
    FormDataJson.append('Phone', $('#Phone').val());
    FormDataJson.append('Email', $('#Email').val());
    FormDataJson.append('Content', $('#Content').val());
    FormDataJson.append('Company', $('#Company').val());
    FormDataJson.append('Gender', $('.check-box').val());
    FormDataJson.append('address', $('#address').val());
    //console.log(FormDataJson);
    $.ajax({
        url: '/ajax/Subscribe.html',
        data: FormDataJson,
        type: 'POST',
        enctype: 'multipart/form-data',
        processData: false, // tell jQuery not to process the data
        contentType: false, // tell jQuery not to set contentType
        dataType: "json",
        success: function (data) {
            var html = data.Node1;
            var params = data.Node2;
           
            if (params != '') {
                sw_alert('Thông báo !', params);
                return
            }
            $('.loading').hide();
            sw_alert('Thông báo !', 'Cảm ơn bạn đã đăng ký. Chúng tôi sẽ gọi lại cho bạn sớm nhất.');                   
        }
    });
}
function popup() {
    $.ajax({
        url: '/ajax/Popup.html',
        data: $('#popup_form').serialize(),
        type: 'POST',
        success: function (data) {
            var node2 = data.Node2;

            if (node2 != '') {
                $('.popup-error').html(node2);
                return;
            }
            $("#popup-modal").modal('hide');

            sw_alert('Thông báo !', 'Cảm ơn bạn đã đăng ký. Chúng tôi sẽ gọi lại cho bạn sớm nhất.');
        }
    });
}
function feedback() {
    $.ajax({
        url: '/ajax/Feedback.html',
        data: $('#feedback_form').serialize(),
        type: 'POST',
        success: function (data) {
            var node2 = data.Node2;
            if (node2 != '') {
                sw_alert('Thông báo !', node2);
                return;
            }
            $("#feeback-modal").modal('hide');

            sw_alert('Thông báo !', 'Cảm ơn bạn đã đăng ký. Chúng tôi sẽ gọi lại cho bạn sớm nhất.');
        }
    });
}
//bình luận đánh giá sao
function add_comment(ParentID, ProductID, Vote, Name, Content, Phone, ValidCode) {
    var dataString = 'ParentID=' + ParentID + '&ProductID=' + ProductID + '&Vote=' + Vote + '&Name=' + encodeURIComponent(Name) + '&Content=' + encodeURIComponent(Content) + '&Phone=' + encodeURIComponent(Phone) + '&ValidCode=' + encodeURIComponent(ValidCode);
    var vote = $('input[name=Vote]:checked', '#CommentForm').val();
    $.ajax({
        type: "get",
        url: "/ajax/CommentPOST.html",
        data: {
            ParentID: ParentID,
            ProductID: ProductID,
            Vote: vote,
            Name: Name,
            Phone: Phone,
            ValidCode: ValidCode,
            Content: Content
        },
        dataType: "json",
        success: function (data) {
            var content = data.Node1;
            var params = data.Node2;

            if (params != '') {
                Swal.fire({
                    title: params,
                    type: 'error',
                    confirmButtonText: 'OK'
                })
                return;
            }
            if (content != '') {
                Swal.fire({
                    title: content,
                    type: 'success',
                    confirmButtonText: 'OK'
                })

                $("#CommentForm")[0].reset();
            }
        },
        error: function (data) { }
    });
}
function pagingproduct(url) {
    $('.loading').show();
    //atr
    var checkbox = $('.right-property li input[type="checkbox"]:checked').map(function () {
        return this.value;
    }).get();
    var atr = checkbox.join('-');
    var sort = $('.filterTopCtgr li input[type="checkbox"]:checked').map(function () {
        return this.value;
    }).get();
    url = url + '?page=' + page + '&sort=' + sort + '&atr=' + atr;
    console.log('paging:' + url);
    $.ajax({
        url: url,
        type: 'get',
        dataType: 'html',
        success: function (data) {
            data = $(data).find('.product-fs').html();
            data = $.trim(data);
            if (data == '')
                $('.pagination a').hide();
            //$('.product-fs').append(data);
            $('.product-fs').append(data);
            $('.product-fs li').click(function () {
                location.href = $(this).data('url');
            });

            $('.loading').hide();
        },
        error: function () { }
    });
}

function add_cart(id, returnpath) {
    location.href = '/gio-hang/Add.html?ProductID=' + id + '&Quantity=1&returnpath=' + returnpath;
}
function delete_cart(index) {
    //Location.href = '/gio-hang/Delete.html?index=' + index;
    location.href = '/gio-hang/Delete.html?Index=' + index;
}
function update_cart(index, quantity, returnpath) {
    location.href = '/gio-hang/Update.html?Index=' + index + '&Quantity=' + quantity + '&returnpath=' + returnpath;
}
function search(keyword) {
    $.ajax({
        url: '/ajax/GetSearch.html',
        data: 'Keyword=' + keyword,
        type: 'GET',
        success: function (data) {
            var node1 = data.Node1;

            $('.result-container').html(node1);

            $('.result-container').show();
        }
    });
}