

$(function () {

});



function showGuaratorTab(url) {
    if (url == '' || url == undefined) {
        alert('something wrong at showGuratorTab!');
        return false;
    }

    var id = $('#txtCusId').val();
    if (id == '' || id == undefined) {
        swal({
            title: "Error",
            text: "กรุณาเลือกลูกหนี้!",
            icon: "error"
        });
        activaTab('loanee-1');
        return false;
    }
    $.ajax({
        type: "GET",
        url: url + "/" + id,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                $("#view-guarantor").html(res.html);
            }


        }
    })
}


AddOrEditGuarantor = (form) => {
    try {
        var gurantor = {};


        var txtGuarantorCode = $("#GuarantorCode");
        var txtFullName = $("#FullName");
        var txtPhoneNumber = $("#PhoneNumber");
        var txtAddressSetLookup = $("#txtAddressSetLookup");
        var txtAddressDetail = $("#AddressDetail");
        var txtCusId = $("#txtCusId");


        var errorMessage = "";
        var isValid = true;
    
        //if (txtGuarantorCode.val() == '' || txtGuarantorCode.val() == undefined) {
         //   isValid = false;
          //  errorMessage += txtGuarantorCode.attr('data-val-required') + '\n\r';
        //}
        if (txtFullName.val() == '' || txtFullName.val() == undefined) {
            isValid = false;
            errorMessage += txtFullName.attr('data-val-required') + '\n\r';
        }
        if (txtPhoneNumber.val() == '' || txtPhoneNumber.val() == undefined) {
            isValid = false;
            errorMessage += txtPhoneNumber.attr('data-val-required') + '\n\r';
        }
        if (txtAddressSetLookup.val() == '' || txtAddressSetLookup.val() == undefined) {
            isValid = false;
            errorMessage += txtAddressSetLookup.attr('data-val-required') + '\n\r';
        }
        if (txtAddressDetail.val() == '' || txtAddressDetail.val() == undefined) {
            isValid = false;
            errorMessage += txtAddressDetail.attr('data-val-required') + '\n\r';
        }

        

        if (!isValid) {
            swal({
                title: "Error",
                text: errorMessage,
                icon: "error"
            });
            return false;
        }

        gurantor.GuarantorCode = txtGuarantorCode.val();
        gurantor.FullName = txtFullName.val();
        gurantor.PhoneNumber = txtPhoneNumber.val();
        gurantor.Address = txtAddressSetLookup.val();
        gurantor.AddressDetail = txtAddressDetail.val();
        gurantor.CusId = txtCusId.val();

        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(gurantor),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "ข้อมูล ผู้ค้ำประะกัน บันทึกเรียบร้อยแล้ว.",
                        icon: "success",
                    })
                        .then(() => {
                            $("#view-guarantor").html(res.html);
                            closePopupXL();
                        });
                }
            },
            error: function (err) {
                console.log(err)
            }
        });
    } catch (ex) {
        console.log(ex)
    }
    return false;
}




confirmDeleteGuarantor = (url) => {
    console.log(url);
    var cusId = $('#txtCusId').val();
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
                    url: `${url}?cusId=${cusId}`,
                    contentType: "application/json; charset=utf-8",
                    success: function (res) {
                        if (res.isValid) {
                            swal({
                                title: "สำเร็จ",
                                text: "ลบข้อมูล เรียบร้อยแล้ว",
                                icon: "success"
                            }).then((val) => {
                                $("#view-guarantor").html(res.html);
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
