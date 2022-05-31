$(function () {
    // Document.ready -> link up remove event handler
    $(".RemoveLink").click(function () {
        //// Get the id from the link
        var recordToDelete = $(this).attr("data-id");
        if (recordToDelete != '') {
            clearUpdateMessage();
            // Perform the ajax post
            $.post("/ShoppingCart/RemoveFromCart", { "id": recordToDelete },
                function (data) {
                    // Successful requests get here
                    // Update the page elements
                    //if (data.ItemCount == 0) {
                    $('.row-index-' + data.DeleteId).fadeOut('slow');
                    //} else {
                    //    $('#item-count-' + data.DeleteId).text(data.ItemCount);
                    //}
                    $('.cart-total').text(checkMoney(data.CartTotal) + ' VND');
                    //$('#Sum').text(data.Sumcart);
                    //$('#update-message').text(htmlDecode(data.Message));
                    //create("default", { title: 'Thông báo', text: data.Message });
                    $('#cart-status').text('' + data.CartCount + ' items');
                    //$('#carttotal').text('(' + checkMoney(data.CartTotal) + ') VND');
                    //if (data.CartTotal == 0) {
                    //    $('.testtotal').val('true');
                    //}
                });
        }
    });

    //$(".RefreshQuantity").click(function () {
    $('.onlyNumber').change(function () {
        showOverlay();

        // Get the id from the link
        var recordToUpdate = $(this).attr("data-id");
        //Set quantity number to 0 if input value is empty
        var countToUpdate = 0;
        //if ($("#" + $(this).attr("txt-id")).val().trim() !== '') {
        countToUpdate = $(this).val();

        if ($(this).val() == 0)
        {
            $(this).val(1);
            alert("Vui lòng chọn số lượng lớn hơn 0");
            return false;
        }

        if (recordToUpdate != '') {
            clearUpdateMessage();
            // Perform the ajax post
            $.post("/ShoppingCart/UpdateCartCount", { "id": recordToUpdate, "cartCount": countToUpdate },
            function (data) {
 
                hideOverlay();

                // Successful requests get here
                // Update the page elements
                if (data.ItemCount == 0) {
                    $('.row-index' + recordToUpdate).fadeOut('slow');
                }
                //$('#update-message').text(htmlDecode(data.Message));

                //Check for not process the callback data when server error occurs
                if (data.ItemCount != -1) {
                    $('.cart-total').text(checkMoney(data.CartTotal) + ' VND');
                    //$('#Sum').text(data.Sumcart);
                    $('#cart-status').text('' + data.CartCount);
                    $('.sumitem-' + recordToUpdate).text(checkMoney(data.SumItemcart))
                    //create("default", { title: 'Thông báo', text: 'Cập nhật giỏ hàng thành công' });
                    //$('#carttotal').text('(' + checkMoney(data.CartTotal) + ') VND');
                }
            });
        }
    });
});

function clearUpdateMessage() {
    // Reset update-message area
    $('#update-message').text('');
}
function htmlDecode(value) {
    if (value) {
        return $('<div />').html(value).text();
    }
    else {
        return '';
    }
}

function checkMoney(AmountValue) {
    //var AmountValue = document.getElementById('Gia').value;
    var num = parseFloat(AmountValue);
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    num = (((sign) ? '' : '-') + num);

    return num;
}

$('#City_Province').change(function () {
    showOverlay();

    $.getJSON('/Payment/DistrictList/' + $('#City_Province').val(), function (data) {

        hideOverlay();


        $('#ship').text(checkMoney(data.PriceSum));
        $('#shiptemp').val(data.PriceSum);
        $('.TotalOder').text(checkMoney(data.TotalOrder));
        var items = '<option value=' + 0 + '>--Chọn Quận/Huyện--</option>';
        $.each(data.SelectDis, function (i, district) {
            items += "<option value='" + district.Value + "'>" + district.Text + "</option>";
        });
        $('#District').html(items);
        var items2 = '<option value=' + 0 + '>--Chọn Phường/Xã--</option>';
        $('#Ward').html(items2);
    });
});

$('#District').change(function () {
    showOverlay();
    $.getJSON('/Payment/WardList/' + $('#District').val(), function (data) {
        hideOverlay();

        if (data.PriceSum != -1) {
            $('#ship').text(checkMoney(data.PriceSum));
            $('#shiptemp').val(data.PriceSum);
            $('.TotalOder').text(checkMoney(data.TotalOrder));
        }
        var items = '<option value=' + 0 + '>--Chọn Phường/Xã--</option>';
        $.each(data.SelectDis, function (i, ward) {
            items += "<option value='" + ward.Value + "'>" + ward.Text + "</option>";
        });
        $('#Ward').html(items);
    });
});

$("#emailre2").keyup(function () {
    if (!isValidEmailAddress($('.emailre2').val())) {
        document.getElementById('Checkemail2').innerHTML = 'Mật khẩu kh&#244;ng hợp lệ';
        $('#register').attr('disabled', 'disabled');
    }
    else {
        document.getElementById('Checkemail2').innerHTML = '';
        $('#register').attr('disabled', false);
    }
});

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}

$("#repassword").keyup(function () {
    var pass = $('#Password').val();
    var repass = document.getElementById('repassword').value;
    if (pass != repass) {
        document.getElementById('Checkpass').innerHTML = 'Mật khẩu kh&#244;ng tr&#249;ng';
        $('#register').attr('disabled', 'disabled');
    }
    else {
        document.getElementById('Checkpass').innerHTML = '';
        $('#register').attr('disabled', false);
    }
});

$(function () {
    $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });
});


 
$(document).ready(function () {
    $('form .addpay').submit(function () {
        if ($('#Home_Number').val().trim() == '' || $('#Street').val().trim() == '' || $('#Name').val().trim() == ''
            || $('#Phone').val().trim() == '' || $('#City_Province').val() == '' || $('#District').val() == '' || $('#Ward').val() == ''
            || $('#Home_Number').val() == null || $('#Street').val() == null || $('#Name').val() == null
            || $('#Phone').val() == null || $('#City_Province').val() == null || $('#District').val() == null || $('#Ward').val() == null) {
            create("themeroller", { title: 'Error!', text: 'Vui lòng điền đầy đủ thông tin thanh toán' }, {
                custom: true,
                expires: false
            });
            return false;
        }

        var id = $('input[name="PaymentTransactionId"]:checked ').val();
        if (id == null) {
            create("themeroller", { title: 'Error!', text: 'Vui lòng chọn hình thức thanh toán' }, {
                custom: true,
                expires: false
            });
            return false;
        }
        else
            return true;
    });
});

var config = {};
config = {
    changeMonth: true,
    duration: 'fast',
    changeYear: true,
    numberOfMonths: 1,
    selectOtherMonths: true,
    showOtherMonths: true,
    firstDay: 1,
    dayNames: ['CN', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'],
    monthNamesShort: ['Thg 1', 'Thg 2', 'Thg 3', 'Thg 4', 'Thg 5', 'Thg 6', 'Thg 7', 'Thg 8', 'Thg 9', 'Thg 10', 'Thg 11', 'Thg 12'],
    yearRange: '1902:2013',
    dateFormat: 'dd/mm/yy'
};

//if (jQuery.ua.browser == 'Internet Explorer') {
//    $(window).load(function () {
//        $('.datepicker').datepicker(config);
//    });
//} else {
//    $('.datepicker').datepicker(config);
//}

function checkPhone2() {
    var p = $('.phone2').val();

    if (isNaN(p) == true || p.length < 9 || p.length > 12) {
        document.getElementById("checkphone").innerHTML = "@UserRes.phoneinvalid";
        return false;
    }
    return true;
}

function CheckBirthday() {
    var birthday = $('.UInfor').val();
    var reg = /(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d/;
    if (birthday.match(reg)) {
        return true;;
    }
    else {
        document.getElementById("checkbirthdayRe").innerHTML = "@UserRes.birthdayinvalid";
        document.getElementById("repassword").focus();
        return false;
    }
}

function test() {
    return true;
}

$('#FormUserInfo').submit(function () {
    return !!(checkPhone2() & CheckBirthday());
});

