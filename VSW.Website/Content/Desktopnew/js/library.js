/*js menu mobile*/
$(document).ready(function ($) {
    $('#trigger-mobile').click(function () {
        $(".mobile-main-menu").addClass('active');

        $(".backdrop__body-backdrop___1rvky").addClass('active');
    });
    $('#close-nav').click(function () {
        $(".mobile-main-menu").removeClass('active');
        $(".backdrop__body-backdrop___1rvky").removeClass('active');
        // $(".only-home").removeClass("overflow_body");
    });
    $('.backdrop__body-backdrop___1rvky').click(function () {
        $(".mobile-main-menu").removeClass('active');
        $(".backdrop__body-backdrop___1rvky").removeClass('active');
    });
    $('#dangnhap_click').click(function () {
        $(".mobile-main-menu").removeClass('active');
        $(".backdrop__body-backdrop___1rvky").removeClass('active');
    });
    $('#dangky_click').click(function () {
        $(".mobile-main-menu").removeClass('active');
        $(".backdrop__body-backdrop___1rvky").removeClass('active');
    });

    $(window).resize(function () {
        if ($(window).width() > 1023) {
            $(".mobile-main-menu").removeClass('active');
            $(".backdrop__body-backdrop___1rvky").removeClass('active');
        }
    });
    $('.ng-has-child1 a .fa1').on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        $this.parents('.ng-has-child1').find('.ul-has-child1').stop().slideToggle();
        $(this).toggleClass('active')
        return false;
    });
    $('.ng-has-child1 .ul-has-child1 .ng-has-child2 a .fa2').on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        $this.parents('.ng-has-child1 .ul-has-child1 .ng-has-child2').find('.ul-has-child2').stop().slideToggle();
        $(this).toggleClass('active')
        return false;
    });
});

$(document).ready(function(){
    //scroll top
    $('.back-to-top a').click(function (n) {
        n.preventDefault();
        $('html, body').animate({
            scrollTop: 0
        }, 500)
    });
    $(window).scroll(function () {
        $(document).scrollTop() > 1e3 ? $('.back-to-top').addClass('display') : $('.back-to-top').removeClass('display')
    });
}); 
/*js filter */
$("#akr-filter .select .title").click(function () {
    $('.body__overlay').addClass('is-visible');
    if ($(this).parent().find(".list-filter-product").is(":visible"))
        $(this).parent().find(".list-filter-product").hide();

    else {
        $(".list-filter-product").not(this).parent().find(".list-filter-product").hide();
        $(this).parent().find(".list-filter-product").show();
    }
});


$('body').click(function (e) {
    var targ = $(".list-filter-product");
    var targ2 = $("#akr-filter .select .title");
    if (!targ.is(e.target) && targ.has(e.target).length === 0 && !targ2.is(e.target) && targ2.has(e.target).length === 0) {
        $(".list-filter-product").hide();
    }
});
$('#list-filter-active a').click(function () {
    $('.sub_hide').css("display", "none");
})

// -- js single-product count

$(".cart-plus-minus").prepend('<div class="dec qtybutton">-</div>');
$(".cart-plus-minus").append('<div class="inc qtybutton">+</div>');
$(".qtybutton").on("click", function () {
    var $button = $(this);
    var oldValue = $button.parent().find("input").val();
    if ($button.text() == "+") {
        var newVal = parseFloat(oldValue) + 1;
    } else {
        // Don't allow decrementing below zero
        if (oldValue > 1) {
            var newVal = parseFloat(oldValue) - 1;
        }
        else {
            newVal = 1;
        }
    }
    $button.parent().find("input").val(newVal);
    $button.parent().find("input").change()
    //update_cart(index, newVal, returnpath) {     
    //}
});

// -- js close_top_banner
$('.close_top_banner').click(function () {
    $('.er-banner-top').toggle();
})

// --  js CheckOut Page*/
$('[name="payment_method"]').on('click', function () {
    var $value = $(this).attr('value');
    $('.sub_show').slideUp();
    $('.payment_method_' + $value).slideToggle();
});
// --  js Giaohang Page*/
$('[name="gh_method"]').on('click', function () {
    var $value = $(this).attr('value');
    $('.sub_show').slideUp();
    $('.gh_method_' + $value).slideToggle();
});
// -- js xuathoad
$('#is_xhd').click(function () {
    $('#xhd-group').slideToggle(600);
});
// -- js magiamgia 
$('.mgg-code').click(function () {
    $('.mgg-inputcode').slideToggle(500);
});
/*js home slider banner ads*/
$('#banner-top').owlCarousel({
    loop: false,
    rewind: true,
    margin: 0,
    dots: false,
    nav: false,
    autoplayTimeout: 6000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            autoplay: true,
            items: 1
        },
        600: {
            autoplay: true,
            items: 2
        },
        1000: {
            autoplay: false,
            items: 3
        }
    }
})
/*js home sp-khuyenmai*/
$('#sp-km').owlCarousel({
    loop: false,
    rewind: true,
    margin: 15,
    dots: false,
    nav: true,
    autoplay: false,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 4
        },
        1000: {
            items: 6
        }
    }
});
$('.slider-giasoc').owlCarousel({
    loop: false,
    rewind: true,
    margin: 5,
    dots: false,
    nav: true,
    autoplay: false,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 3
        },
        1000: {
            items: 4
        }
    }
});
$('.slider-pro').owlCarousel({
    loop: false,
    rewind: true,
    margin: 0,
    dots: false,
    nav: true,
    autoplay: false,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 3
        },
        1000: {
            items: 4
        }
    }
});
$('.slider-brand').owlCarousel({
    loop: false,
    rewind: true,
    margin: 20,
    dots: true,
    nav: false,
    autoplay: false,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 3
        },
        1000: {
            items: 5
        }
    }
});
$('.slider-customer').owlCarousel({
    loop: false,
    rewind: true,
    margin: 10,
    dots: false,
    nav: false,
    autoplay: false,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 3
        },
        1000: {
            items: 7
        }
    }
});
$('.slider-policy').owlCarousel({
    loop: false,
    rewind: true,
    margin: 20,
    dots: true,
    nav: false,
    autoplay: false,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 3
        },
        1000: {
            items: 5
        }
    }
})
/*js home sp-khuyenmai*/
$('#sp-lq').owlCarousel({
    loop: false,
    rewind: true,
    margin: 0,
    dots: false,
    nav: true,
    autoplay: true,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 3
        },
        1000: {
            items: 4
        }
    }
})
/*js home banner-tab-mobile*/
$('#banner-home-tab').owlCarousel({
    loop: false,
    rewind: true,
    margin: 0,
    dots: true,
    nav: true,
    autoplay: true,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 1
        },
        1000: {
            items: 1
        }
    }
})
/*js home banner page*/
$('#banner-page-news').owlCarousel({
    loop: true,
    rewind: true,
    margin: 0,
    dots: true,
    nav: true,
    autoplay: false,
    autoplayTimeout: 8000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    navigationText: ["<i class='fa fa-long-arrow-left'></i>", "<i class='fa fa-long-arrow-right'></i>"],
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 1
        },
        1000: {
            items: 1
        }
    }
})
$('#banner-page').owlCarousel({
    loop: false,
    rewind: true,
    margin: 10,
    dots: false,
    nav: true,
    autoplay: true,
    autoplayTimeout: 5000,
    autoplaySpeed: 1500,
    smartSpeed: 1200,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 3
        }
    }
})
/*js home main banner*/
$(document).ready(function () {
    HomeBannerSlide_New2018();
    HomeBannerSlide_New2018_Nav();
});
function HomeBannerSlide_New2018() {
    if ($("#homebanner_new_slide").hasClass("homebanner_new_slide")) {
        var _eB_widthItem = -665;
        var _eB_widthAction = -153;
        var _eB_limitBanner = 4;
        var _eB_Action_Length = parseInt($(".homebanner_new_slide .eB_slide_action li").length);
        var _eB_Action_Width = parseInt($(".homebanner_new_slide .eB_slide_action li").width());
        $(".homebanner_new_slide .eB_slide_action").css("width", "" + parseInt(_eB_Action_Length * _eB_Action_Width) + "");
        $(".homebanner_new_slide .eB_slide_action li").eq(0).addClass("active");
        var setInterval_SlideNew2018 = setInterval(function () {
            var _itemInterval = parseInt($(".homebanner_new_slide .eB_slide_action li.active").attr("data-id")) + parseInt(1);
            var _lengthInterval = $(".homebanner_new_slide .eB_slide_action li").length;
            if (parseInt(_itemInterval) >= parseInt(_lengthInterval)) {
                _itemInterval = 0;
            }
            $(".homebanner_new_slide .eB_slide_action li").removeClass("active");
            $(".homebanner_new_slide .eB_slide_action li").eq(_itemInterval).addClass("active");
            $(".homebanner_new_slide .eB_slide_banner").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + (_itemInterval * _eB_widthItem) + "px, 0px, 0px)" });
            if (parseInt(_itemInterval + 1) >= parseInt(_eB_limitBanner) && parseInt(_itemInterval + 1) < parseInt($(".homebanner_new_slide .eB_slide_banner li").length)) {
                _itemInterval = parseInt(_itemInterval) - parseInt(_eB_limitBanner) + 2;
                $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthAction) + "px, 0px, 0px)" });
            }
            else if (_itemInterval <= _eB_limitBanner) {
                $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(0px, 0px, 0px)" });
            }
        }, 3000);
        $("#homebanner_new_slide").mouseenter(function () {
            clearInterval(setInterval_SlideNew2018);
        }).mouseleave(function () {
            setInterval_SlideNew2018 = setInterval(function () {
                var _itemInterval = parseInt($(".homebanner_new_slide .eB_slide_action li.active").attr("data-id")) + parseInt(1);
                var _lengthInterval = $(".homebanner_new_slide .eB_slide_action li").length;
                if (parseInt(_itemInterval) >= parseInt(_lengthInterval)) {
                    _itemInterval = 0;
                }
                $(".homebanner_new_slide .eB_slide_action li").removeClass("active");
                $(".homebanner_new_slide .eB_slide_action li").eq(_itemInterval).addClass("active");
                $(".homebanner_new_slide .eB_slide_banner").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + (_itemInterval * _eB_widthItem) + "px, 0px, 0px)" });
                if (parseInt(_itemInterval + 1) >= parseInt(_eB_limitBanner) && parseInt(_itemInterval + 1) < parseInt($(".homebanner_new_slide .eB_slide_banner li").length)) {
                    _itemInterval = parseInt(_itemInterval) - parseInt(_eB_limitBanner) + 2;
                    $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthAction) + "px, 0px, 0px)" });
                }
                else if (_itemInterval <= _eB_limitBanner) {
                    $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(0px, 0px, 0px)" });
                }
            }, 3000);
        });
        $(".homebanner_new_slide .eB_slide_action li").click(function () {
            $(".homebanner_new_slide .eB_slide_action li").removeClass("active");
            $(this).addClass("active");
            var _itemInterval = parseInt($(this).attr("data-id"));
            $(".homebanner_new_slide .eB_slide_banner").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthItem) + "px, 0px, 0px)" });
            if (parseInt(_itemInterval + 1) >= parseInt(_eB_limitBanner) && parseInt(_itemInterval + 1) < parseInt($(".homebanner_new_slide .eB_slide_banner li").length)) {
                _itemInterval = parseInt(_itemInterval) - parseInt(_eB_limitBanner) + 2;
                $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthAction) + "px, 0px, 0px)" });
            }
            else if (_itemInterval <= _eB_limitBanner) {
                $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(0px, 0px, 0px)" });
            }
        });
    }
};
function HomeBannerSlide_New2018_Nav() {
    $(".homebanner_new_slide .eB_slide_nav_left").click(function () {
        var _eB_widthItem = -665;
        var _eB_widthAction = -153;
        var _eB_limitBanner = 4;
        var _itemInterval = parseInt($(".homebanner_new_slide .eB_slide_action li.active").attr("data-id")) - parseInt(1);
        if (parseInt(_itemInterval) < 0) {
            _itemInterval = parseInt($(".homebanner_new_slide .eB_slide_banner li").length) - 1;
        }
        $(".homebanner_new_slide .eB_slide_action li").removeClass("active");
        $(".homebanner_new_slide .eB_slide_action li").eq(_itemInterval).addClass("active");
        $(".homebanner_new_slide .eB_slide_banner").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthItem) + "px, 0px, 0px)" });
        if (parseInt(_itemInterval) - parseInt(_eB_limitBanner) > 0) {
            _itemInterval = parseInt(_itemInterval) - parseInt(_eB_limitBanner) + 1;
            $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthAction) + "px, 0px, 0px)" });
        }
        else if (_itemInterval == 0) {
            $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthAction) + "px, 0px, 0px)" });
        }
    });
    $(".homebanner_new_slide .eB_slide_nav_right").click(function () {
        var _eB_widthItem = -665;
        var _eB_widthAction = -153;
        var _eB_limitBanner = 4;
        var _itemInterval = parseInt($(".homebanner_new_slide .eB_slide_action li.active").attr("data-id")) + parseInt(1);
        if (parseInt(_itemInterval) >= parseInt($(".homebanner_new_slide .eB_slide_banner li").length)) {
            _itemInterval = 0;
        }
        $(".homebanner_new_slide .eB_slide_action li").removeClass("active");
        $(".homebanner_new_slide .eB_slide_action li").eq(_itemInterval).addClass("active");
        $(".homebanner_new_slide .eB_slide_banner").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthItem) + "px, 0px, 0px)" });
        if (_itemInterval >= _eB_limitBanner) {
            _itemInterval = parseInt(_itemInterval) - parseInt(_eB_limitBanner) + 1;
            $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthAction) + "px, 0px, 0px)" });
        }
        else if (_itemInterval == 0) {
            $(".homebanner_new_slide .eB_slide_action").css({ "transition": "all 1000ms ease", "transform": "translate3d(" + parseInt(_itemInterval * _eB_widthAction) + "px, 0px, 0px)" });
        }
    });
};
$(document).ready(function () {
    // Hàm active tab nào đó
    function activeTab(obj) {
        // Xóa class active tất cả các tab
        $('.tab-wrapper ul li').removeClass('active');

        // Thêm class active vòa tab đang click
        $(obj).addClass('active');

        // Lấy href của tab để show content tương ứng
        var id = $(obj).find('a').attr('href');

        // Ẩn hết nội dung các tab đang hiển thị
        $('.tab-item').hide();

        // Hiển thị nội dung của tab hiện tại
        $(id).show();
    }

    // Sự kiện click đổi tab
    $('.tab-kn li').click(function () {
        activeTab(this);


        return false;
    });

    // Active tab đầu tiên khi trang web được chạy
    activeTab($('.tab-kn li:first-child'));
});
/*js show room footer*/
$(".location-list .loca-lv1 a").click(function () {
    var _id = $(this).attr('rel');
    $(".box-location").slideUp(100);
    $(".location-list .loca-lv1 a").removeClass("selected");
    if ($(_id).is(":visible")) {
        $(_id).slideUp(100);
        $(".location-list .loca-lv1 a").removeClass("selected");
    }
    else {
        $(_id).slideDown(600);
        $(this).addClass("selected");
    }
});
$(".location-list .loca-lv1 a").each(function () {
    var _id = $(this).attr('rel');
    var count_item = $(_id).find("li").length;
    $(this).children(".count-loca").append("(" + count_item + ")");
});
/*js banner khuyen mai*/
/*show search mobile*/
$(".btn-search-click").click(function () {
    $(this).toggleClass('open');
    $(".wp-search-top").slideToggle("slow", function () {
    });
});
/*show wp ft*/
// $(".wp-ft").click(function () {
//     $(this).toggleClass('open');
// });
/*js menu danh muc*/
$('.advance-menu .toggle-button').on('click', function (event) {
    event.preventDefault();
    var target = $(event.target);
    if (target.is("span")) {
        $(this).parents('.has-child').toggleClass('open');
        $(this).parents('.has-child').find('> .advance-sub-menu').slideToggle();
    }
});
/*js zoom img chi tiet sp*/
$(function () {
    $("#zoom1").glassCase({
        'widthDisplay': 600,
        'heightDisplay': 440,
        'isHoverShowThumbs': false,
        'nrThumbsPerRow': 5,
        'isSlowZoom': true,
        'colorIcons': '#000',
        'colorActiveThumb': '#F15129',
        'autoInnerZoom': false,
        'isZoomEnabled': false,
        'thumbsPosition': 'bottom'
    });
});
$(".toggle-user ").click(function () {
    $(".drop-user").slideToggle();
});
/*js doc them*/
//function showArticle() {
//    $(".post-danhgia").toggleClass("post-danhgiaFull");
//    $('.show-more').toggleClass("open");
//};
$('#js-show-more').click(function () {
    if ($('.post-toto').hasClass('expand')) {
        $('.post-toto').removeClass("expand");
        document.getElementById('js-show-more').innerHTML = " Đọc thêm";
    } else {
        $('.post-toto').addClass("expand");
        document.getElementById('js-show-more').innerHTML = " Thu Gọn";
    }
});
$(document).ready(function () {
    $(".icon-comment").click(function () {
        $(this).toggleClass("active");
        $(this).parent().toggleClass("active");
    })
})
/*navText:["<i class=\"fa fa-long-arrow-left\"></i>","<i class=\"fa fa-long-arrow-right\"></i>"],*/
// Create the Performance Observer instance.
const observer = new PerformanceObserver((list) => {
    for (const entry of list.getEntries()) {
        const fid = entry.processingStart - entry.startTime;
        console.log('FID:', fid);
    }
});
// Start observing first-input entries.
observer.observe({
    type: 'first-input',
    buffered: true,
});
// 25/10/2021
$('#js-show-tskt').click(function () {
    if ($('.table-responsive').hasClass('expand')) {
        $('html, body').animate({
            scrollTop: $(".wp-thongso").offset().top
        },1);
        $('.table-responsive').removeClass("expand");
      
        document.getElementById('js-show-tskt').innerHTML = " Đọc thêm";
    }
    else {
        $('.table-responsive').addClass("expand");
        document.getElementById('js-show-tskt').innerHTML = " Thu Gọn";
    }
});

$('#js-show-mtsp').click(function () {
    if ($('.post-danhgia').hasClass('post-danhgiaFull')) {
        $('html, body').animate({
            scrollTop: $(".wp-thongtin-sp").offset().top
        }, 1);

        $('.post-danhgia').removeClass("post-danhgiaFull");
        document.getElementById('js-show-mtsp').innerHTML = " Đọc thêm";
       
    }
    else {
        $('.post-danhgia').addClass("post-danhgiaFull");
        document.getElementById('js-show-mtsp').innerHTML = "Thu Gọn";     
    }
});

$('#show_content_seo').click(function () {
    if ($('.content_seo').hasClass('expand')) {
        $('.content_seo').removeClass("expand");
        document.getElementById('show_content_seo').innerHTML = " Đọc thêm";
    } else {
        $('.content_seo').addClass("expand");
        document.getElementById('show_content_seo').innerHTML = " Thu Gọn";
    }
});

$('#show-more-page').click(function () {
    if ($('.effect7').hasClass('expand')) {
        $('.effect7').removeClass("expand");
        document.getElementById('show-more-page').innerHTML = " Đọc thêm";
    } else {
        $('.effect7').addClass("expand");
        document.getElementById('show-more-page').innerHTML = " Thu Gọn";
    }
});


