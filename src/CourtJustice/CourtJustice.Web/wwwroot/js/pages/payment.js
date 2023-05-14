
$(function () {
    //getWithPaging();
});
function showPaymentTab(url) {
    if (url == '' || url == undefined) {
        alert('something wrong at showAssetLandTab!');
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
                $("#view-payment").html(res.html);
            }

            //$("#view-asset-land").html(res);
            //console.log(res.html);
        }
    })
}

AddOrEditPayment = (form) => {
    try {
        var payment = {};

        var txtPaymentId = $("#PaymentId");
        var txtPaymentSeq = $("#PaymentSeq");
        var txtPaymentDate = $("#PaymentDate");
        var txtAmoung = $("#Amount");
        var txtFee = $("#Fee");
        var txtCusId = $('#txtCusId');

        var errorMessage = "";
        var isValid = true;
        //if (txtPayMentId.val() == '' || txtPayMentId.val() == undefined) {
        //    isValid = false;
        //    errorMessage += txtPayMentId.attr('data-val-required') + '\n\r';
        //}
        if (txtPaymentSeq.val() == '' || txtPaymentSeq.val() == undefined) {
            isValid = false;
            errorMessage += txtPaymentSeq.attr('data-val-required') + '\n\r';
        }
        if (txtPaymentDate.val() == '' || txtPaymentDate.val() == undefined) {
            isValid = false;
            errorMessage += txtPaymentDate.attr('data-val-required') + '\n\r';
        }
        if (txtAmoung.val() == '' || txtAmoung.val() == undefined) {
            isValid = false;
            errorMessage += txtAmoung.attr('data-val-required') + '\n\r';
        } else {
            if (parseInt(txtAmoung.val()) <= 0) {
                isValid = false;
                errorMessage += "ค่างวดต้องมากกว่า 0" + '\n\r';
            }
        }
        if (txtFee.val() == '' || txtFee.val() == undefined) {
            isValid = false;
            errorMessage += txtFee.attr('data-val-required') + '\n\r';
        } else {
            if (parseInt(txtFee.val()) <= 0) {
                isValid = false;
                errorMessage += "ค่าปรับต้องมากกว่า 0" + '\n\r';
            }
        }

        if (!isValid) {
            swal({
                title: "Error",
                text: errorMessage,
                icon: "error"
            });
            return false;
        }
        payment.PaymentId = txtPaymentId.val();
        payment.PaymentSeq = txtPaymentSeq.val();
        payment.PaymentDate = txtPaymentDate.val();
        payment.Amount = txtAmoung.val();
        payment.Fee = txtFee.val();
        payment.CusId = txtCusId.val();


        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(payment),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "ข้อมูล การชำระ บันทึกเรียบร้อยแล้ว.",
                        icon: "success",
                    })
                        .then(() => {
                            $("#view-payment").html(res.html);
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

DeletePayment = (url) => {
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
                                $("#view-payment").html(res.html);
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







