$(function () {
    
});

getBankActions = () => {
    var url = $("#hdGetBankActionRoute").val();
    var employerCode = $("#ddlEmployer").val();
    try {
        $("#loaderbody").show();
        $.ajax({
            type: "GET",
            url: url + "/" + employerCode ,
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
    var bankPersonId =parseInt( $('#txtBankPersonId').val());
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
    request.BankActionId =parseInt(bankActionId);
    request.BankPersonId = bankPersonId;
    request.BankPersonCodeId = bankPersonCodeId;
    request.BankPersonCodeName = bankPersonCodeName;

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

closePopup = () => {
    $('#form-modal .modal-body').html('');
    $('#form-modal .modal-title').html('');
    $('#form-modal').modal('hide');
}

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





