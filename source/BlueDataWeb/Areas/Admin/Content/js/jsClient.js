function showOverlay() {
    $('.overlay').css('display', 'block');
}
//$(document).ajaxSend(function () {
//    $('.overlay').css('display', 'block');

//});

//$(document).ajaxStop(function () {
//    $(".overlay").fadeOut("slow");

//});

function hideOverlay() {
    //alert("hideOverlay");
    $(".overlay").fadeOut("slow");
}

//$(document).on('submit', 'form', function () {
//    showOverlay();
//});

function AjaxEnd(data) {
    hideOverlay();
}

function AjaxStart(data) {
    showOverlay();
}




//All show using trigger js plugin
$('.modal').on('shown.bs.modal', function () {
    //$('.datepicker').datepicker({
    //    format: "dd/mm/yyyy",
    //    todayBtn: "linked",
    //    clearBtn: true,
    //    language: "vi",
    //    calendarWeeks: false,
    //    todayHighlight: true
    //});
  
    //$('.select22').select2({
    //    dropdownParent: $("#Modal_ThemDoanhNghiep")
    //});


    ////$('.select22').select2();
    //if(detectmob())
    //{

    //}
    //else {
    //    $('.select22').select2({
    //        dropdownParent: $("#Modal_ThemDoanhNghiep")
    //    });
    //}
});

function detectmob() {
    if (window.innerWidth <= 800 && window.innerHeight <= 600) {
        return true;
    } else {
        return false;
    }
}

 
  
 

$(function () {
    $.ajaxSetup({ cache: false });

    $(".btn-show-modal").click(function () {
        $("#myModalContent").html();

        $("#myModalContent").load(this.href, function () {
            $("#myModal").modal({
                backdrop: 'static',
                keyboard: true
            }, 'show');
        });
        return false;
    });

    $('#myModal').on('show.bs.modal', function () {
        //alert("open myModal");

        $(this).removeData();


    })

    $('#myModal').on('hidden.bs.modal', function () {
        //alert("close myModal");
        $(this).removeData();
    })

    $(".btn-show-modal-mini").click(function () {
        $("#myModalContentmini").load(this.href, function () {
            $("#myModalmini").modal({
                backdrop: 'static',
                keyboard: true
            }, 'show');
        });
        return false;
    });
});

// load modal and load jquery in it
$('#myModal').on('shown.bs.modal', function () {

    $('#_DataTable').DataTable();

    //$('.datepicker').datepicker({
    //    format: "dd/mm/yyyy",
    //    todayBtn: "linked",
    //    clearBtn: true,
    //    autoclose: true,
    //    language: "vi",
    //    calendarWeeks: false,
    //    todayHighlight: true
    //});

    //$('#daterange-container .input-daterange').datepicker({
    //    format: "dd/mm/yyyy",
    //    todayBtn: "linked",
    //    clearBtn: true,
    //    autoclose: true,
    //    language: "vi",

    //    todayHighlight: true
    //});
});

 

$('.showPoup_ThemDoanhNghiep').click(function (e) {
    //alert("showPoup_Vitri");
    //showOverlay();
    
    e.preventDefault();

    //console.log('aa');
    var $modal = $('#Modal_ThemDoanhNghiep');
    var $modalDialog = $('.modal_dialog_doanhnghiep');
    var href = $(this).prop('href');

    // không cho phép tắt modal khi click bên ngoài modal
    var option = { backdrop: 'static' };

    // kiểm tra (logic, điều kiện...)

    $modalDialog.load(href, function () {
        $modal.modal(option, 'show');
    });

    return false;
});
//var globalLazyLoad = new lazyload();

//var modalLazyLoad;

$('#Modal_ThemDoanhNghiep').on('shown.bs.modal', function () {

  
        //$('.lazy').lazyload({

        //});
  


    //alert("dd232f");
});

$('#Modal_ThemDoanhNghiep').on('hidden.bs.modal', function () {
    $(function () {
        showOverlay();
        //var url_getDoanhNghiep = $('#UrlDanhSachDoanhNghiep').val();

        //location.reload();

        hideOverlay();
    });
})

function ShowPopup_Modal_ThemDoanhNghiep(href) {
    //$('#Modal_ThemDoanhNghiep').modal('hide');

    showOverlay();

    //e.preventDefault();
    //alert(href);
    //console.log('aa');
    var $modal = $('#Modal_ThemDoanhNghiep');
    var $modalDialog = $('.modal_dialog_doanhnghiep');

    // không cho phép tắt modal khi click bên ngoài modal
    var option = { backdrop: 'static' };

    // kiểm tra (logic, điều kiện...)

    $modalDialog.load(href, function () {
        $modal.modal(option, 'show');
    });

    return false;
}

$('.showPoup_ThemDoanhNghiep_View').click(function (e) {
    //alert("showPoup_Vitri");
    showOverlay();

    e.preventDefault();

    //console.log('aa');
    var $modal = $('#Modal_ThemDoanhNghiep_View');
    var $modalDialog = $('.modal_dialog_doanhnghiep_View');
    var href = $(this).prop('href');

    // không cho phép tắt modal khi click bên ngoài modal
    var option = { backdrop: 'static' };

    // kiểm tra (logic, điều kiện...)

    $modalDialog.load(href, function () {
        $modal.modal(option, 'show');
    });

    return false;
});

$('#Modal_ThemDoanhNghiep_View').on('shown.bs.modal', function () {
    //alert("dd232f");
});

$('.Modal_ThemDoanhNghiep_View').on('hidden.bs.modal', function () {
    $(function () {
        //showOverlay();
        //var url_getDoanhNghiep = $('#UrlDanhSachDoanhNghiep').val();

        //location.reload();

        //hideOverlay();
    });
})

 
 
 

/// Kiem tra file
$(function () {
    $("input:file").change(function () {
        var fileName = $(this).val();
        var onlyExcel = $(this).attr("onlyExcel");
        var _validFileExtensions = [".doc", ".docx", ".pdf", ".jpeg", ".bmp", ".png", ".gif", ".jpg", ".dwg", ".dwf", ".dxf", ".xls"];
        var _validFileExtensions_only_Excel = [".xls"];

        if (onlyExcel == "1") {
            ValidateFile($(this), _validFileExtensions_only_Excel);
        }
        else {
            ValidateFile($(this), _validFileExtensions);
        }
    });
});

function ValidateFile(element, lstFileChoPhep) {
    var arrInputs = element;
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < lstFileChoPhep.length; j++) {
                    var sCurExtension = lstFileChoPhep[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        break;
                    }
                }

                //var ErrorMess = sFileName + " is invalid, allowed extensions are: " + lstFileChoPhep.join(", ")
                var ErrorMess = "";
                if (!blnValid) {
                    ErrorMess = "File không đúng định dạng cho phép";
                }

                var size;

                size = oInput.files[0].size;

                if (size > 10 * 1024 * 1024) {
                    ErrorMess = ErrorMess + " File không được lớn hơn 10MB";

                    blnValid = false;
                }

                if (!blnValid) {
                    element.val("");

                    var elememt_showText_file = element.next().children().first().val("");

                    toastr["error"](ErrorMess);

                    return false;
                }
            }
        }
    }

    return true;
}
 