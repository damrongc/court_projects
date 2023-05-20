


$(function () {

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


        var txtLoaneeRemarkId  = $("#LoaneeRemarkId")
        var txtRemark = $("#Remark");    
        var txtCusId = $("#txtCusId");


        var errorMessage = "";
        var isValid = true;

       
        if (txtRemark.val() == '' || txtRemark.val() == undefined) {
            isValid = false;
            errorMessage += txtRemark.attr('data-val-required') + '\n\r';
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

        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(loaneeRemark),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "ข้อมูล หมายเหตุ บันทึกเรียบร้อยแล้ว.",
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
