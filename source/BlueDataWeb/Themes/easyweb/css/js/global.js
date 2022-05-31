var lazyLoadInstance = new LazyLoad({
    elements_selector: ".lazy"
});
$('.pagination li').click(function (event) {
    lazyLoadInstance.update();
});
$(document).ready(function () {
    $('.main-slider').slick({
        // lazyLoad: 'ondemand',
        pauseOnHover: false,
        infinite: true,
        accessibility: false,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 5000,
        speed: 1500,
        arrows: false,
        centerMode: false,
        dots: false,
        draggable: true,
        fade: false,
    });
    $('.main-slider').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
        $(".title-slide").hide();
    });
    $('.main-slider').on('afterChange', function (event, slick, currentSlide, nextSlide) {
        $(".title-slide").show();
        $(".title-slide").removeClass("animated");
        wow = new WOW(
            {
                boxClass: 'title-slide',
                animateClass: 'animated',
                offset: 0,
                mobile: true,
                live: true
            }
        )
        wow.init();
    });
});
function isEmpty(str, text) {
    if (str != "") return false;
    if (typeof (text) != 'undefined') {
        swal({
            title: lang_thongbao,
            text: text,
            icon: "error",
            button: false,
        });
    }
    return true;
}
function isPhone(str, text) {
    if ((str.length == 11 && (str.substr(0, 2) == 01)) || (str.length == 10 && (str.substr(0, 2) == 09)))
        return false;
    if (typeof (text) != 'undefined') {
        swal({
            title: lang_thongbao,
            text: text,
            icon: "error",
            button: false,
        });
    }
    return true;
}
function isEmail(str, text) {
    emailRegExp = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.([a-z]){2,4})$/;
    if (emailRegExp.test(str)) {
        return false;
    }
    if (typeof (text) != 'undefined') {
        swal({
            title: lang_thongbao,
            text: text,
            icon: "error",
            button: false,
        });
    }
    return true;
}
function isSpace(str, text) {
    for (var i = 0; i < str.length; i++) {
        if ((str.charAt(i)) == " ") {
            if (typeof (text) != 'undefined') {
                swal({
                    title: lang_thongbao,
                    text: text,
                    icon: "error",
                    button: false,
                });
            }
            return true;
        }
    }
    return false;
}
function isCharacters(str, text) {
    if (str == '') return false;
    var searchReg = /^[a-zA-Z0-9-]+$/;
    if (searchReg.test(str)) {
        return false;
    }
    if (typeof (text) != 'undefined') {
        swal({
            title: lang_thongbao,
            text: text,
            icon: "error",
            button: false,
        });
    }
    return true;
}
function isRepassword(str, str2, text) {
    if (str2 == '') return false;
    if (str == str2) return false;
    if (typeof (text) != 'undefined') {
        swal({
            title: lang_thongbao,
            text: text,
            icon: "error",
            button: false,
        });
    }
    return true;
}
function isCharacterlimit(str, text, intmin, intmax) {
    if (str == '') return false;
    intmin = parseInt(intmin);
    intmax = parseInt(intmax);
    if (str.length >= intmin && str.length <= intmax) {
        return false;
    }
    if (typeof (text) != 'undefined') {
        swal({
            title: lang_thongbao,
            text: text,
            icon: "error",
            button: false,
        });
    }
    return true;
}
$(document).ready(function () {
    $(".add_soluong").click(function (event) {
        var val = $("input.soluong").val();
        if (val != 0) {
            $("input.soluong").attr('value', parseInt(val) + 1);
        }
    });
    $(".minus_soluong").click(function (event) {
        var val = $("input.soluong").val();
        if (val > 1) {
            $("input.soluong").attr('value', parseInt(val) - 1);
        }
    });
});
$(document).ready(function () {
    $('img').each(function (index, element) {
        if (!$(this).attr('alt') || $(this).attr('alt') == '') {
            $(this).attr('alt', $(document).find("title").text());
        }
    });
});

$(document).ready(function () {
    $('#slide-out li').each(function (index, el) {
        if ($(this).children("ul").length) {
            $(this).prepend('<button class="btn waves-effect waves-light green darken-4"><i class="material-icons">add</i></button>');
        }
    });
});
$(document).on('click', '#slide-out li button', function (event) {
    event.preventDefault();
    $(this).toggleClass('more');
    $(this).parent('li').children('ul').toggle();
});
$(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() != 0) {
            $('#bttop').fadeIn();
        }
        else {
            $('#bttop').fadeOut();
        }
    });
    $('#bttop').click(function () {
        $('body,html').animate(
            { scrollTop: 0 }, 800);
    });
});
var fired = false;
$(document).ready(function () {
    $('.slick2').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        autoplay: false,
        autoplaySpeed: 5000,
        asNavFor: '.slick'
    });
    $('.slick').slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        asNavFor: '.slick2',
        dots: false,
        centerMode: false,
        focusOnSelect: true,
        responsive: [
            {
                breakpoint: 800,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 376,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                }
            }
        ]
    });
});
$(document).ready(function () {
    $(".slick").find('button.slick-prev').addClass('btn btn-floating waves-effect waves-light pulse red');
    $(".slick").find('button.slick-prev').html('<i class="material-icons">chevron_left</i>');
    $(".slick").find('button.slick-next').addClass('btn btn-floating waves-effect waves-light pulse red');
    $(".slick").find('button.slick-next').html('<i class="material-icons">chevron_right</i>');
});

$(document).on('click', '.btn_reset', function (event) {
    event.preventDefault();
    location.reload();
});
$(document).ready(function () {
    $('.line_dutoan button[type=submit]').addClass('btn_dutoan2');
    $('.line_dutoan button[type=submit]').attr('type', 'button');
});

$(document).ready(function () {
    $('table').each(function (index, element) {
        $(this).wrap("<div class='tbl_cover'></div>");
    });
});
$(document).ready(function () {
    $('div').each(function (index, element) {
        var attr = $(this).attr('dir');
        if (typeof attr !== typeof undefined && attr !== false) {
            $(this).removeAttr('style');
        }
    });
});

function LoadDataKT(type) {
    debugger;
    if (type == 1) {
        $(".dsPho").fadeOut();
        $(".dsBietThu").fadeIn();
        $("#btViewBT").addClass("active");
        $("#btViewNhaPho").removeClass("active");
        
    }
    else {
        $(".dsPho").fadeIn();
        $(".dsBietThu").fadeOut();

        $("#btViewNhaPho").addClass("active");
        $("#btViewBT").removeClass("active");
    }
}

$(document).ready(function () {
    var queryString = document.location.href.toLowerCase();

    $(".menuitem").removeClass('active');

    $(".menuitem").each(function () {
        var href = $(this).attr('href').toLowerCase();

        if (queryString == href && queryString.indexOf(href) == 0) {
            debugger

            $(this).addClass('active');
        }
        else if (queryString.indexOf(href) > 0 && queryString != href) {
            $(this).addClass('active');
        }
    });
});