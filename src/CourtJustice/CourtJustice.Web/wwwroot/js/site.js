
//$.ajaxSetup({
//    error: function (xhr) {
//        alert('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
//    }
//});
function activatejQueryTable() {
    $('.data-table').DataTable({
        //fixedHeader: true,
        aLengthMenu: [
            [5, 10, 15, -1],
            [5, 10, 15, "All"]
        ],
        iDisplayLength: 10,
        scrollCollapse: true,
        scrollX: true,
        //scrollY: 500,
        autoWidth: false,
        language: {
            search: ""
        },
    });
    $('.data-table').each(function () {
        var datatable = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'Search');
        search_input.removeClass('form-control-sm');
        // LENGTH - Inline-Form control
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        length_sel.removeClass('form-control-sm');
    });
}


function reSetOffsetPoint() {
    $.each($('.meter-position'), function (idx, item) {
        $(item).offset({ left: $(item).attr('data-xpos'), top: $(item).attr('data-ypos') });
    });
}


$(function () {
    activatejQueryTable();

    //$(".form-control").each(function () {

    //    var $t = $(this);
    //    var ht = $t.attr("height");
    //    ht = ht ? ht + "px" : "auto";
    //    $t.css("height", ht);
    //});

});
$(document).ready(function () {
    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = d.getFullYear() + '-' +
        (month < 10 ? '0' : '') + month + '-' +
        (day < 10 ? '0' : '') + day;

    $("#loaderbody").hide();

    //$(document).bind('ajaxStart', function () {
    //    $("#loaderbody").show();
    //}).bind('ajaxStop', function () {
    //    $("#loaderbody").hide();
    //});
    $(".date").datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
        closeOnDateSelect: true,
        defaultDate: new Date()
    });
    $('.date').val(output);

    $('#form-modal').on('shown.bs.modal', function () {
        $(".date").datetimepicker({
            timepicker: false,
            format: 'Y-m-d',
            closeOnDateSelect: true
        });
        $('.date').val(output);

        $(".datetime").datetimepicker({
            format: 'Y-m-d',
            /*minDate: '-1970/01/02',//yesterday is minimum date(for today use 0 or -1970/01/01)*/
            maxDate: '0',
            closeOnDateSelect: true,
            timepicker: false,
        });
        $('.datetime').val(output);
    });

    $('#form-modal-xl').on('shown.bs.modal', function () {
        $(".date").datetimepicker({
            timepicker: false,
            format: 'Y-m-d',
            closeOnDateSelect: true
        });
    });

    //$('#form-modal-fullscreen').on('shown.bs.modal', function () {
    //    $(".date").datetimepicker({
    //        timepicker: false,
    //        format: 'Y-m-d',
    //        closeOnDateSelect: true
    //    });
    //});

    if ($(".js-example-basic-multiple").length) {
        $(".js-example-basic-multiple").select2();
    }

});
showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal({ backdrop: 'static', keyboard: false }, 'show');

        }
    })
}

showInPopupXL = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal-xl .modal-body').html(res);
            $('#form-modal-xl .modal-title').html(title);
            $('#form-modal-xl').modal({ backdrop: 'static', keyboard: false }, 'show');


        }
    })
}
showInPopupFullScreen = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal-table .modal-body').html(res);
            $('#form-modal-table .modal-title').html(title);
            $('#form-modal-table').modal({ backdrop: 'static', keyboard: false }, 'show');
        }
    })
}

showInPopupFullScreenTable = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal-table .modal-body').html(res);
            $('#form-modal-table .modal-title').html(title);
            $('#form-modal-table').modal({ backdrop: 'static', keyboard: false }, 'show');
        }
    })
}
closePopup = () => {
    $('#form-modal .modal-body').html('');
    $('#form-modal .modal-title').html('');
    $('#form-modal').modal('hide');
}
closePopupXL = () => {
    $('#form-modal-xl .modal-body').html('');
    $('#form-modal-xl .modal-title').html('');
    $('#form-modal-xl').modal('hide');
}


function formatNumber(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    //return (num).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
}

function formatDate(data) {
    if (moment(data).year() == 1) return '';
    return moment(data).add(543, 'year').format("DD-MM-YYYY");
}
function formatDatetime(data) {
    if (moment(data).year() == 1) return '';
    return moment(data).add(543, 'year').format("DD-MM-YYYY h:mm");
}

InputToUpper = (obj) => {
    if (obj.value != "") {
        obj.value = obj.value.toUpperCase();
    }
}

jQueryAjaxPost = form => {
    try {
        $.validator.unobtrusive.parse(form);
        if ($(form).valid()) {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        swal({
                            title: "Completed",
                            text: "Your data has been saved.",
                            icon: "success"
                        }).then((val) => {
                            //loadList();
                            $("#view-all").html(res.html);
                            if (($("#form-modal").data('bs.modal') || {})._isShown) {
                                $('#form-modal .modal-body').html('');
                                $('#form-modal .modal-title').html('');
                                $('#form-modal').modal('hide');
                            }

                            if (($("#form-modal-xl").data('bs.modal') || {})._isShown) {
                                $('#form-modal-xl .modal-body').html('');
                                $('#form-modal-xl .modal-title').html('');
                                $('#form-modal-xl').modal('hide');
                            }

                            if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                                activatejQueryTable();
                        });
                        //$('#view-all').html(res.html)
                        //$('#form-modal .modal-body').html('');
                        //$('#form-modal .modal-title').html('');
                        //$('#form-modal').modal('hide');
                    }
                    else {
                        //$('#form-modal .modal-body').html(res.html);
                        swal({
                            title: "Error",
                            text: res.message,
                            icon: "error"
                        });
                    }

                },
                error: function (err) {
                    console.log(err)
                }
            })
        }
    } catch (ex) {
        console.log(ex)
    }
    //to prevent default form submit event
    return false;
}

jQueryAjaxDelete = form => {
    try {
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this data!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: 'POST',
                        url: form.action,
                        data: new FormData(form),
                        contentType: false,
                        processData: false,
                        success: function (res) {
                            if (res.isValid) {
                                swal({
                                    title: "Deleted",
                                    text: "Your data has been deleted.",
                                    icon: "success"
                                }).then((val) => {
                                    $("#view-all").html(res.html);
                                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                                        activatejQueryTable();
                                });
                            }
                            else {
                                swal({
                                    title: "Error",
                                    text: res.message,
                                    icon: "error"
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err)
                        }
                    })
                }
            });
    } catch (ex) {
        console.log(ex)
    }
    return false;
}

function GetMeter() {

    $.ajax({
        type: 'GET',
        url: '/Layouts/GetMeters',
        success: function (res) {
            console.log(res);


        }
    })
    return false;

}

function changePassword(url) {
    var userId = $("#UserId").val();
    var password = $("#NewPassword").val();
    var confirmPassword = $("#ConfirmPassword").val();

    if (password != confirmPassword) {
        swal({
            title: "Error",
            text: "Password not match!",
            icon: "error"
        });
        return false;
    }
    var jsonData = {};
    jsonData.UserId = userId;
    jsonData.NewPassword = password;
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                if (($("#form-modal").data('bs.modal') || {})._isShown) {
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                swal({
                    title: "Change Password",
                    text: "Your data has been success.",
                    icon: "success"
                });
            }
        },
        error: function (err) {
            console.log(err)
        }
    })
    return false;
}

