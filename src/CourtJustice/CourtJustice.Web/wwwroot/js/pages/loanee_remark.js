﻿
$(function () {
    $('#form-modal-xl').on('shown.bs.modal', function () {
        $(".dateonly").datetimepicker({
            timepicker: false,
            format: 'd-m-Y',
            closeOnDateSelect: true
        });
    });
});

function getBankPersonCode() {
   
    const url = $('#hdGetBankPersonCodesUrl').val();
    const bankActionId = $('#ddlBankActionCode').val();

    $.getJSON(url, { id: bankActionId }, function (response) {
        var items = '';
        $('#ddlBankPersonCode').empty();
        $.each(response, function (i, data) {
            items += "<option value='" + data.value + "'>" + data.text + "</option>"
        });
        $('#ddlBankPersonCode').html(items);
        $('#ddlBankResultCode').empty();
        $('#ddlBankResultCode').html("<option value='0'>==กรุณาเลือก==</option>");
    });
}

function getBankResultCode() {

    const url = $('#hdGetBankResultCodesUrl').val();
    const bankPersonId = $('#ddlBankPersonCode').val();

    $.getJSON(url, { id: bankPersonId }, function (response) {
        var items = '';
        $('#ddlBankResultCode').empty();
        $.each(response, function (i, data) {
            items += "<option value='" + data.value + "'>" + data.text + "</option>"
        });
        $('#ddlBankResultCode').html(items);
    });
}


function getCompanyResultCode() {

    const url = $('#hdGetCompanyResultCodesUrl').val();
    const companyActionId = $('#ddlCompanyActionCode').val();

    $.getJSON(url, { id: companyActionId }, function (response) {
        var items = '';
        $('#ddlCompanyResultCode').empty();
        $.each(response, function (i, data) {
            items += "<option value='" + data.value + "'>" + data.text + "</option>"
        });
        $('#ddlCompanyResultCode').html(items);
    });
}


function showRemarkTab(url) {
    if (url == '' || url == undefined) {
        alert('something wrong at showRemarkTab!');
        return false;
    }

    const cusId = $('#txtCusId').val();
    if (cusId == '' || cusId == undefined) {
        swal({
            title: "Error",
            text: "กรุณาเลือกลูกหนี้!",
            icon: "error"
        });
        activaTab('remark-1');
        return false;
    }
    $.ajax({
        type: "GET",
        url: url + "/" + cusId,
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
        var txtLoaneeRemarkId = $("#txtLoaneeRemarkId")
        var txtEmployerCode = $("#txtEmployerCode")
        var txtRemark = $("#txtRemark");
        var txtCusId = $("#txtCusId");
        var txtAppointmentDate = $("#txtAppointmentDate");
        var txtFollowContractNo = $("#txtFollowContractNo");
        var txtAmount = $("#txtAmount");
        var ddlBankActionCode = $("#ddlBankActionCode");
        var ddlBankPersonCode = $("#ddlBankPersonCode");
        var ddlBankResultCode = $("#ddlBankResultCode");
        var ddlCompanyActionCode = $("#ddlCompanyActionCode");
        var ddlCompanyResultCode = $("#ddlCompanyResultCode");
        
        var errorMessage = "";
        var isValid = true;

        if (txtRemark.val() == '' || txtRemark.val() == undefined) {
            isValid = false;
            errorMessage += txtRemark.attr('data-val-required') + '\n\r';
        } else {
            var length = txtRemark.val().length;
            if (length > 99) {
                errorMessage += 'หมายเหตุ สามารถใส่ได้แค่ 99 ตัวอักษรเท่านั้น!';
            }
        }

        if (txtAppointmentDate.val() == '' || txtAppointmentDate.val() == undefined) {
            isValid = false;
            errorMessage += txtAppointmentDate.attr('data-val-required') + '\n\r';
        }

        if (txtFollowContractNo.val() == '' || txtFollowContractNo.val() == undefined) {
            isValid = false;
            errorMessage += txtFollowContractNo.attr('data-val-required') + '\n\r';
        }

        if (txtAmount.val() == '' || txtAmount.val() == undefined) {
            isValid = false;
            errorMessage += 'กรุณาระบุยอดจ่าย \n\r';
        }

        if (ddlBankActionCode.val() == '' || ddlBankActionCode.val() == undefined) {
            isValid = false;
            errorMessage += 'กรุณาระบุ Action Code[ธนาคาร] \n\r';
        }
        if (ddlBankResultCode.val() == '' || ddlBankResultCode.val() == undefined) {
            isValid = false;
            errorMessage += 'กรุณาระบุ Result Code[ธนาคาร] \n\r';
        }
        if (ddlBankPersonCode.val() == '' || ddlBankPersonCode.val() == undefined) {
            isValid = false;
            errorMessage += 'กรุณาระบุ Person Code[ธนาคาร] \n\r';
        }
        if (ddlCompanyActionCode.val() == '' || ddlCompanyActionCode.val() == undefined) {
            isValid = false;
            errorMessage += 'กรุณาระบุ Action Code[บริษัท] \n\r';
        }
        if (ddlCompanyResultCode.val() == '' || ddlCompanyResultCode.val() == undefined) {
            isValid = false;
            errorMessage += 'กรุณาระบุ Result Code[บริษัท] \n\r';
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
        loaneeRemark.AppointmentDate = moment(txtAppointmentDate.val(), "DD-MM-YYYY").format()
        loaneeRemark.FollowContractNo = txtFollowContractNo.val();
        loaneeRemark.Amount =parseFloat(txtAmount.val());
        loaneeRemark.BankActionId = parseInt(ddlBankActionCode.val());
        loaneeRemark.BankPersonId = parseInt(ddlBankPersonCode.val());
        loaneeRemark.BankResultId =parseInt( ddlBankResultCode.val());
        loaneeRemark.CompanyActionId = parseInt(ddlCompanyActionCode.val());
        loaneeRemark.CompanyResultId = parseInt(ddlCompanyResultCode.val());
        loaneeRemark.EmployerCode = txtEmployerCode.val();

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
                    }).then(() => {
                            $("#view-remark").html(res.html);
                            closePopupXL();
                        });
                } else {
                    swal({
                        title: "พบข้อผิดพลาด",
                        text: res.message,
                        icon: "error"
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
    const cusId = $('#txtCusId').val();
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
