 
      //$(function () {
      //    $('#main-menu').smartmenus({
      //        subMenusSubOffsetX: 1,
      //        subMenusSubOffsetY: -8
      //    });
      //});

//$("#owl-demo").owlCarousel({
//    navigation: false, // Show next and prev buttons

//    lazyLoad: true,
//    singleItem: true,
//    autoPlay: 8000,
//});

//$("#owl-demo1").owlCarousel({
//    autoPlay: 8000, //Set AutoPlay to 3 seconds
//    lazyLoad: true,
            
//    itemsCustom: [
//      [0, 2],
//      [800, 4],

//    ],
//    navigation: false
//});
     
function ShowMenu() {
    var x = document.getElementById('menu_content');
    if (x.style.display === 'block') {
        x.style.display = 'none';

        $("#menu_content").hide(300);

    } else {
        $("#menu_content").show(300);

    }
}
  
//$(document).ready(function () {
//    $(".ellipsis").dotdotdot();
//});

function Dangky() {
    debugger
    var Name = $("#FName").val();
    var Email = $("#FEmail").val();
    var TenCongTy = $("#FTenCongTy").val();
    
    var link = document.location.origin + "/lien-he?Name=" + Name + "&Email=" + Email + "&TenCongTy=" + TenCongTy;

    window.location = link;
}
 

$("#backtotop").click(function () {
    $("body,html").animate({
        scrollTop: 0
    }, 600);
});
$(window).scroll(function () {
    if ($(window).scrollTop() > 150) {
        $("#backtotop").addClass("visible");
    } else {
        $("#backtotop").removeClass("visible");
    }
});
 

 $(document).ready(function () {
       

        var queryString = document.location.href.toLowerCase();
      
        $(".menu_item").removeClass('menu_active');

        $(".menu_item").each(function () {

            var href = $(this).attr('href').toLowerCase();

            if (queryString == href && queryString.indexOf(href) == 0) {
                $(this).addClass('menu_active');
            }
            else if (queryString.indexOf(href) > 0 && queryString != href) {
                $(this).addClass('menu_active');
            }

        });
      
    });
