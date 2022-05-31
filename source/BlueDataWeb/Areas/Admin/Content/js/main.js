$(document).ready(function () {
    $(".inputSo").each(function () {

        if ($(this).val()) {
            var gt = $(this).val();
            $(this).val(gt.replace(".", ","));
        }

    });
    CheckSoInput();
});

function CheckSoInput() {
    $('.inputSo').keyup(function (e) {

        //keyCode = 188 dau
        if (e.keyCode == 188) {
            var n = occurrences(this.value, ","); //3

            if (n > 1) {
                $(this).val(this.value.substr(0, this.value.length - 1));
            }
        }
        this.value = this.value.replace(/[^0-9\,]/g, '');
    });

    $('.inputSoNguyen').keyup(function (e) {

        this.value = this.value.replace(/[^0-9]/g, '');
    });

}
//$('.inputSo').keyup(function (e) {

//    //keyCode = 188 dau
//    if (e.keyCode == 188) {
//        var n = occurrences(this.value, ","); //3

//        if (n > 1) {
//            $(this).val(this.value.substr(0, this.value.length - 1));
//        }
//    }
//    this.value = this.value.replace(/[^0-9\,]/g, '');
//});

//$('.inputSoNguyen').keyup(function (e) {

//    this.value = this.value.replace(/[^0-9]/g, '');
//});

function occurrences(string, subString, allowOverlapping) {

    string += "";
    subString += "";
    if (subString.length <= 0) return (string.length + 1);

    var n = 0,
        pos = 0,
        step = allowOverlapping ? 1 : subString.length;

    while (true) {
        pos = string.indexOf(subString, pos);
        if (pos >= 0) {
            ++n;
            pos += step;
        } else break;
    }
    return n;
}