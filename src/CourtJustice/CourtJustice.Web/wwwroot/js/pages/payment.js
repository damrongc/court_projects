
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

AddOrEdit = (form) => {
    try {
        var payment = {};

        var txtPayMentId = $("#PaymentId");
        var txtPaymentSeq = $("#PaymentSeq");
        var txtPatmentDate = $("#PaymentDate");
        var txtAmoung = $("#Amount");
        var txtFee = $("#Fee");


        var errorMessage = "";
        var isValid = true;
        if (txtPayMentId.val() == '' || txtPayMentId.val() == undefined) {
            isValid = false;
            errorMessage += txtPayMentId.attr('data-val-required') + '\n\r';
        }
        if (txtPaymentSeq.val() == '' || txtPaymentSeq.val() == undefined) {
            isValid = false;
            errorMessage += txtPaymentSeq.attr('data-val-required') + '\n\r';
        }
        if (txtPatmentDate.val() == '' || txtPatmentDate.val() == undefined) {
            isValid = false;
            errorMessage += txtPatmentDate.attr('data-val-required') + '\n\r';
        }
        if (txtAmoung.val() == '' || txtAmoung.val() == undefined) {
            isValid = false;
            errorMessage += txtAmoung.attr('data-val-required') + '\n\r';
        }
        if (txtFee.val() == '' || txtFee.val() == undefined) {
            isValid = false;
            errorMessage += txtFee.attr('data-val-required') + '\n\r';
        }

        if (!isValid) {
            swal({
                title: "Error",
                text: errorMessage,
                icon: "error"
            });
            return false;
        }


        payments.PaymentId = txtPayMentId.val();
        payments.PaymentSeq = txtPaymentSeq.val();
        payments.PaymentDate = txtPatmentDate.val();
        payments.Amount = txtAmoung.val();
        payments.Fee = txtFee.val();
        payments.cusId = cusId;


        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(payments),
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







