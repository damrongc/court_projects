
$(function () {


    $('#form-modal-xl').on('shown.bs.modal', function () {
        $(".dateonly").datetimepicker({
            timepicker: false,
            format: 'd-m-Y',
            closeOnDateSelect: true
        });
    });

});

function showRemarkTab(url) {
    if (url == '' || url == undefined) {
        alert('something wrong at showRemarkTab!');
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
                $("#view-remark").html(res.html);
            }


        }
    })
}


AddOrEditLoaneeRamark = (form) => {
    try {
        var loaneeRemark = {};


        var txtLoaneeRemarkId = $("#LoaneeRemarkId")
        var txtRemark = $("#Remark");
        var txtCusId = $("#txtCusId");
        var txtTransactionDatetime = $("#txtTransactionDatetime");
        var txtAppointmentDate = $("#txtAppointmentDate");
        var txtAppointmentContract = $("#AppointmentContract");
        var txtContractNo = $("#ContractNo");
        var txtAmount = $("#Amount");
        var ddlBankActionCode = $("#ddlBankActionCode");
        var ddlBankResultCode = $("#ddlBankResultCode");
        var ddlCompanyActionCode = $("#ddlCompanyActionCode");
        var ddlCompanyResultCode = $("#ddlCompanyResultCode");

   

        var errorMessage = "";
        var isValid = true;


        if (txtRemark.val() == '' || txtRemark.val() == undefined) {
            isValid = false;
            errorMessage += txtRemark.attr('data-val-required') + '\n\r';
        }

        if (txtTransactionDatetime.val() == '' || txtTransactionDatetime.val() == undefined) {
            isValid = false;
            errorMessage += txtTransactionDatetime.attr('data-val-required') + '\n\r';
        }

        if (txtAppointmentDate.val() == '' || txtAppointmentDate.val() == undefined) {
            isValid = false;
            errorMessage += txtAppointmentDate.attr('data-val-required') + '\n\r';
        }
        if (txtAppointmentContract.val() == '' || txtAppointmentContract.val() == undefined) {
            isValid = false;
            errorMessage += txtAppointmentContract.attr('data-val-required') + '\n\r';
        }
        if (txtContractNo.val() == '' || txtContractNo.val() == undefined) {
            isValid = false;
            errorMessage += txtContractNo.attr('data-val-required') + '\n\r';
        }

        if (txtAmount.val() == '' || txtAmount.val() == undefined) {
            isValid = false;
            errorMessage += txtAmount.attr('data-val-required') + '\n\r';
        } else {
            if (parseInt(txtAmount.val()) <= 0) {
                isValid = false;
                errorMessage += "ยอดจ่าย ต้องมากกว่า 0" + '\n\r';
            }
        }

        if (ddlBankActionCode.val() == '' || ddlBankActionCode.val() == undefined) {
            isValid = false;
            errorMessage += ddlBankActionCode.attr('data-val-required') + '\n\r';
        }
        if (ddlBankResultCode.val() == '' || ddlBankResultCode.val() == undefined) {
            isValid = false;
            errorMessage += ddlBankResultCode.attr('data-val-required') + '\n\r';
        }
        if (ddlCompanyActionCode.val() == '' || ddlCompanyActionCode.val() == undefined) {
            isValid = false;
            errorMessage += ddlCompanyActionCode.attr('data-val-required') + '\n\r';
        }
        if (ddlCompanyResultCode.val() == '' || ddlCompanyResultCode.val() == undefined) {
            isValid = false;
            errorMessage += ddlCompanyResultCode.attr('data-val-required') + '\n\r';
        }

        if (!isValid) {
            swal({
                title: "Error",
                text: errorMessage,
                icon: "error"
            });
            return false;
        }

        loaneeRemark.LoaneeRemarkId = txtLoaneeRemarkId.val();
        loaneeRemark.Remark = txtRemark.val();
        loaneeRemark.CusId = txtCusId.val();
        loaneeRemark.AppointmentContract = txtAppointmentContract.val();
        loaneeRemark.TransactionDatetime = moment(txtTransactionDatetime.val(), "DD-MM-YYYY h:mm").format()
        loaneeRemark.AppointmentDate = moment(txtAppointmentDate.val(), "DD-MM-YYYY").format()
        loaneeRemark.ContractNo = txtContractNo.val();
        loaneeRemark.Amount = txtAmount.val();
        loaneeRemark.BankActionCodeId = ddlBankActionCode.val();
        loaneeRemark.BankResultCodeId = ddlBankResultCode.val();
        loaneeRemark.CompanyActionCodeId = ddlCompanyActionCode.val();
        loaneeRemark.CompanyResultCodeId = ddlCompanyResultCode.val();


        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(loaneeRemark),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "ข้อมูล รายงายผลการติดตาม บันทึกเรียบร้อยแล้ว.",
                        icon: "success",
                    })
                        .then(() => {
                            $("#view-remark").html(res.html);
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




confirmDeleteLoaneeRemark = (url) => {
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
                                $("#view-remark").html(res.html);
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
