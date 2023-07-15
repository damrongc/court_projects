$(function () {
    //$('#tblBankResultCode').on('click', '.btnEdit', function () {
    //    alert('btnEdit');
    //    //var editUrl = $("#hdEditEnergyValueConfigUrl").val();
    //    //console.log(editUrl);

    //    //var tableRow = $(this).closest('tr');

    //    //var id = tableRow.find('.energyIDcell').attr('data-id');
    //    //var name = tableRow.find('.energyIDcell').text();
    //    //var lowerValue = tableRow.find('.lowerValueCell').text();
    //    //var highValue = tableRow.find('.higherValueCell').text();
    //    //var lowerMessage = tableRow.find('.lowerMessageCell').text();
    //    //var highMessage = tableRow.find('.higherMessageCell').text();
    //    //var alarmConfig = {};
    //    //alarmConfig.EnergyValueId = parseInt(id);
    //    //alarmConfig.Name = name;
    //    //alarmConfig.LowerValue = parseFloat(lowerValue);
    //    //alarmConfig.HigherValue = parseFloat(highValue);
    //    //alarmConfig.LowerMessage = lowerMessage;
    //    //alarmConfig.HigherMessage = highMessage;
    //    //$.ajax({
    //    //    type: 'POST',
    //    //    url: editUrl,
    //    //    data: JSON.stringify(alarmConfig),
    //    //    contentType: "application/json; charset=utf-8",
    //    //    success: function (res) {
    //    //        $('#form-modal .modal-body').html(res);
    //    //        $('#form-modal .modal-title').html('Edit ' + name);
    //    //        $('#form-modal').modal('show');

    //    //    }
    //    //})

    //});

    //$('#tblBankResultCode').on('click', '.rowDelete', function () {
    //    alert('btnDelete');
    //    $(this).closest('tr').remove();
    //});

});

showBankResultPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal-result .modal-body').html(res);
            $('#form-modal-result .modal-title').html(title);
            $('#form-modal-result').modal({ backdrop: 'static', keyboard: false }, 'show');
            $('#form-modal').modal('hide');
        }
    });
}

hideBankResultPopup = () => {
    $('#form-modal-result .modal-body').html('');
    $('form-modal-result .modal-title').html('');
    $('#form-modal-result').modal('hide');
    $('#form-modal').modal('show');
}

getBankActions = () => {
    var url = $("#hdGetBankActionRoute").val();
    var employerCode = $("#ddlEmployer").val();
    try {
        $("#loaderbody").show();
        $.ajax({
            type: "GET",
            url: url + "/" + employerCode,
            //data: JSON.stringify(request),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                $("#loaderbody").hide();
                if (res.isValid) {
                    $("#view-table").html(res.html);
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                } else {
                    alert(res.message);
                }
            },
            error: function (xhr, status, error) {
                $("#loaderbody").hide();
                var errorMessage = xhr.status + ': ' + xhr.statusText + ' ' + xhr.responseText;
                alert('Error - ' + errorMessage);
            }
        })

    } catch (e) {
        console.log(e);
    }
    finally {
        $("#loaderbody").hide();
    }
}

savePersonCode = (url) => {
    var employerCodeFilter = $("#ddlEmployer").val();

    var bankActionId = $("#txtBankActionId").val();
    var bankPersonId = parseInt($('#txtBankPersonId').val());
    var bankPersonCodeId = $('#txtBankPersonCodeId').val();
    var bankPersonCodeName = $('#txtBankPersonCodeName').val();

    if (bankPersonCodeId == "" || bankPersonCodeId == undefined) {
        swal({
            title: "พบข้อผิดพลาด",
            text: "กรุณาระบุข้อมูล Person Code[ธนาคาร]",
            icon: "warning",
            dangerMode: true,
        });
        $('#txtBankPersonCodeId').focus();
        return;
    }
    if (bankPersonCodeName == "" || bankPersonCodeName == undefined) {
        swal({
            title: "พบข้อผิดพลาด",
            text: "กรุณาระบุข้อมูล คำอธิบาย",
            icon: "warning",
            dangerMode: true,
        });
        $('#txtBankPersonCodeName').focus();
        return;
    }
    var request = {};
    request.EmployerCodeFilter = employerCodeFilter;
    request.BankActionId = parseInt(bankActionId);
    request.BankPersonId = bankPersonId;
    request.BankPersonCodeId = bankPersonCodeId;
    request.BankPersonCodeName = bankPersonCodeName;

    const bankResultCodeList = [];
    const tableRow = $('#tblBankResultCode tbody tr');
    tableRow.each(function () {
        const bankResultCodeIdCell = $(this).find(".BankResultCodeIdCell");
        const bankResultCodeId = $(this).find(".BankResultCodeIdCell").html();
        const bankResultCodeName = $(this).find(".BankResultCodeNameCell").html()

        const bankResultId = $(bankResultCodeIdCell).attr("data-id");
        let bankResultCode = {};
        bankResultCode.BankResultId = bankResultId;
        bankResultCode.BankResultCodeId = bankResultCodeId;
        bankResultCode.BankResultCodeName = bankResultCodeName;
        bankResultCodeList.push(bankResultCode);
    });

    request.BankResultCodes = bankResultCodeList;

    $("#loaderbody").show();
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                swal({
                    title: "สำเร็จ",
                    text: "บันทึกข้อมูล เรียบร้อยแล้ว",
                    icon: "success"
                }).then((val) => {
                    closePopup();
                    $("#view-table").html(res.html);
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                });
            } else {
                swal({
                    title: "Error",
                    text: res.message,
                    icon: "error"
                });
            }
            $("#loaderbody").hide();
        },
        error: function (err) {
            $("#loaderbody").hide();
            console.log(err);
        }
    });

}

deletePersonCode = (url) => {
    var employerCodeFilter = $("#ddlEmployer").val();
    var bankPersonId = parseInt($('#txtBankPersonId').val());

    var request = {};
    request.EmployerCodeFilter = employerCodeFilter;
    request.BankPersonId = bankPersonId;


    swal({
        title: "ต้องการลบ ข้อมูล?",
        text: "ถ้าลบข้อมูลแล้ว จะไม่สามารถนำกลับมาใช้งานได้",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(request),
                    contentType: "application/json; charset=utf-8",
                    success: function (res) {
                        if (res.isValid) {
                            swal({
                                title: "สำเร็จ",
                                text: "ลบข้อมูล เรียบร้อยแล้ว",
                                icon: "success"
                            }).then((val) => {
                                closePopup();
                                $("#view-table").html(res.html);
                                if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                                    activatejQueryTable();
                            });
                        } else {
                            swal({
                                title: "พบข้อผิดพลาด",
                                text: res.message,
                                icon: "error"
                            });
                        }
                    }
                });
            }
        });
    return false;

}


addDataToTable = () => {

    const txtBankResultCodeId = $('#txtBankResultCodeId');
    const txtBankResultCodeName = $('#txtBankResultCodeName');

    if (txtBankResultCodeId.val() == '' || txtBankResultCodeId.val() == undefined) {
        swal({
            title: "Error",
            text: "กรุณาระบุ Result Code!",
            icon: "error"
        }).then((val) => {
            txtBankResultCodeId.focus();
        });
        return false;
    }
    if (txtBankResultCodeName.val() == '' || txtBankResultCodeName.val() == undefined) {
        swal({
            title: "Error",
            text: "กรุณาระบุ คำอธิบาย Result Code!",
            icon: "error"
        }).then((val) => {
            txtBankResultCodeName.focus();
        });
        return false;
    }

    var valid = true;
    var tableRow = $('#tblBankResultCode tbody tr');
    tableRow.each(function () {
        var bankResultCodeIdCell = $(this).find(".BankResultCodeIdCell");
        if (txtBankResultCodeId.val() == bankResultCodeIdCell.html()) {
            swal({
                title: "Error",
                text: "Result Code มีอยู่แล้วในระบบ!",
                icon: "error"
            });
            valid = false;
            return false;
        }
    });
    if (valid) {
        var markup = "<tr>"
            + "<td class='BankResultCodeIdCell' data-id='0'>" + txtBankResultCodeId.val() + "</td>"
            + "<td class='BankResultCodeNameCell'>" + txtBankResultCodeName.val() + "</td>"
            + "<td class='w150'><div class='d-inline pull-right'><button class='btn btn-primary btn-sm' onclick ='edit_row($(this))'><i class='fa fa-edit'></i></button>"
            + "<button class='btn btn-danger btn-sm' onclick ='delete_row($(this))'><i class='fa fa-trash'></i></button>"
            + "</div></td>"
            + "</tr>";
        $("#tblBankResultCode tbody").append(markup);
        hideBankResultPopup();
    }
    return false;
}

editDataToTable = () => {
    const txtBankResultId = $('#txtBankResultId');
    const txtBankResultCodeId = $('#txtBankResultCodeId');
    const txtBankResultCodeName = $('#txtBankResultCodeName');

    if (txtBankResultCodeId.val() == '' || txtBankResultCodeId.val() == undefined) {
        swal({
            title: "Error",
            text: "กรุณาระบุ Result Code!",
            icon: "error"
        }).then((val) => {
            txtBankResultCodeId.focus();
        });
        return false;
    }
    if (txtBankResultCodeName.val() == '' || txtBankResultCodeName.val() == undefined) {
        swal({
            title: "Error",
            text: "กรุณาระบุ คำอธิบาย Result Code!",
            icon: "error"
        }).then((val) => {
            txtBankResultCodeName.focus();
        });
        return false;
    }

    const bankResultId = txtBankResultId.val();
    const bankResultCodeId = txtBankResultCodeId.val();
    const bankResultCodeName = txtBankResultCodeName.val();


    var tableRow = $('#tblBankResultCode tbody tr');
    tableRow.each(function (idx, cell) {
        var bankResultCodeIdCell = $(this).find(".BankResultCodeIdCell");
        var bankResultIdCell = $(bankResultCodeIdCell).attr("data-id");
        if (bankResultId == bankResultIdCell) {

            $(cell).find('.BankResultCodeIdCell').text(bankResultCodeId);
            $(cell).find('.BankResultCodeNameCell').text(bankResultCodeName);
            return false;
        }
    });
    hideBankResultPopup();
}

function edit_row(row) {
    var url = $('#hdEditBankResultRoute').val();

    var tableRow = row.closest('tr');

    var bankResultId = tableRow.find('.BankResultCodeIdCell').attr('data-id');
    var bankResultCodeId = tableRow.find('.BankResultCodeIdCell').text();
    var bankResultCodeName = tableRow.find('.BankResultCodeNameCell').text();
    console.log(url);
    console.log(bankResultId);
    console.log(bankResultCodeId);
    console.log(bankResultCodeName);

    var bankResultCode = {};
    bankResultCode.BankResultId = bankResultId;
    bankResultCode.BankResultCodeId = bankResultCodeId;
    bankResultCode.BankResultCodeName = bankResultCodeName;

    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(bankResultCode),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $('#form-modal-result .modal-body').html(res);
            $('#form-modal-result .modal-title').html('แก้ไข ' + bankResultCodeId);
            $('#form-modal-result').modal('show');
            $('#form-modal').modal('hide');
        }
    })

}

function delete_row(row) {
    var url = $('#hdDeleteBankResultRoute').val();
    var tableRow = row.closest('tr');
    var bankResultId = tableRow.find('.BankResultCodeIdCell').attr('data-id');
    $.ajax({
        type: "DELETE",
        url: `${url}/${bankResultId}`,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                row.closest('tr').remove();
            } else {
                swal({
                    title: "พบข้อผิดพลาด",
                    text: res.message,
                    icon: "error"
                });
            }
        }
    });

}

//saveBankResultCode = () => {
//    var txtBankResultCodeId = $('#txtBankResultCodeId');
//    var txtBankResultCodeName = $('#txtBankResultCodeName');

//    if (txtBankResultCodeId.val() == '' || txtBankResultCodeId.val() ==undefined) {
//        swal({
//            title: "Error",
//            text: "กรุณาระบุ Result Code!",
//            icon: "error"
//        }).then((val) => {
//            txtBankResultCodeId.focus();
//        });
//        return false;
//    }
//    if (txtBankResultCodeName.val() == '' || txtBankResultCodeName.val() == undefined) {
//        swal({
//            title: "Error",
//            text: "กรุณาระบุ คำอธิบาย Result Code!",
//            icon: "error"
//        }).then((val) => {
//            txtBankResultCodeName.focus();
//        });
//        return false;

//    }
//    hideBankResultPopup();

//    const bankResultCodeList = [];
//    const tableRow = $('#tblBankResultCode tbody tr');
//    tableRow.each(function () {
//        const bankResultCodeIdCell = $(this).find(".BankResultCodeIdCell");
//        const bankResultCodeId = $(this).find(".BankResultCodeIdCell").html();
//        const bankResultCodeName = $(this).find(".BankResultCodeNameCell").html()

//        const bankResultId = $(bankResultCodeIdCell).attr("data-id");
//        let bankResultCode = {};
//        bankResultCode.BankResultId = bankResultId;
//        bankResultCode.BankResultCodeId = bankResultCodeId;
//        bankResultCode.BankResultCodeName = bankResultCodeName;
//        bankResultCodeList.push(bankResultCode);
//    });

//    alert("saveResultCode");
//}

confirmDelete = (id) => {
    var url = $('#hdDeleteRoute').val();
    swal({
        title: "ต้องการลบ ข้อมูล?",
        text: "ถ้าลบข้อมูลแล้ว จะไม่สามารถนำกลับมาใช้งานได้",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "DELETE",
                    url: `${url}/${id}`,
                    contentType: "application/json; charset=utf-8",
                    success: function (res) {
                        if (res.isValid) {

                            swal({
                                title: "สำเร็จ",
                                text: "ลบข้อมูล เรียบร้อยแล้ว",
                                icon: "success"
                            }).then((val) => {
                                $("#view-table").html(res.html);
                                if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                                    activatejQueryTable();
                            });
                        } else {
                            swal({
                                title: "พบข้อผิดพลาด",
                                text: res.message,
                                icon: "error"
                            });
                        }
                    }
                });
            }
        });
    return false;
}





