
var table;
$(function () {
 
});



function showAssetSalaryTab(url) {
    if (url == '' || url == undefined) {
        alert('something wrong at showAssetCarTab!');
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
                $("#view-asset-salary").html(res.html);
            }


        }
    })
}


AddOrEditAssetSalary = (form) => {
    try {
        var assetSalary = {};

       
        var txtAssetId = $("#AssetId");
        var txtCompany = $("#Company");
        var txtSalary = $("#Salary");
        var txtSalaryDate = $("#SalaryDate");
        var txtAddressSetLookup = $("#txtAddressSetLookup");
        var txtAddressDetail = $("#AddressDetail");
        var txtCusId = $("#txtCusId");


        var errorMessage = "";
        var isValid = true;
        //if (txtPayMentId.val() == '' || txtPayMentId.val() == undefined) {
        //    isValid = false;
        //    errorMessage += txtPayMentId.attr('data-val-required') + '\n\r';
        //}
        if (txtCompany.val() == '' || txtCompany.val() == undefined) {
            isValid = false;
            errorMessage += txtCompany.attr('data-val-required') + '\n\r';
        }
        if (txtSalaryDate.val() == '' || txtSalaryDate.val() == undefined) {
            isValid = false;
            errorMessage += txtSalaryDate.attr('data-val-required') + '\n\r';
        }
        if (txtAddressSetLookup.val() == '' || txtAddressSetLookup.val() == undefined) {
            isValid = false;
            errorMessage += txtAddressSetLookup.attr('data-val-required') + '\n\r';
        }
        if (txtAddressDetail.val() == '' || txtAddressDetail.val() == undefined) {
            isValid = false;
            errorMessage += txtAddressDetail.attr('data-val-required') + '\n\r';
        }
     
        if (txtSalary.val() == '' || txtSalary.val() == undefined) {
            isValid = false;
            errorMessage += txtAmoung.attr('data-val-required') + '\n\r';
        } else {
            if (parseInt(txtSalary.val()) <= 0) {
                isValid = false;
                errorMessage += "เงินเดือนต้องมากกว่า 0" + '\n\r';
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
    
        assetSalary.AssetId = txtAssetId.val();
        assetSalary.Company = txtCompany.val();
        assetSalary.SalaryDate = txtSalaryDate.val();
        assetSalary.Address = txtAddressSetLookup.val();
        assetSalary.AddressDetail = txtAddressDetail.val();
        assetSalary.Salary = txtSalary.val();
        assetSalary.CusId = txtCusId.val();

        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(assetSalary),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "ข้อมูล หลักทรัพย์เงินเดือน บันทึกเรียบร้อยแล้ว.",
                        icon: "success",
                    })
                        .then(() => {
                            $("#view-asset-salary").html(res.html);
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




confirmDeleteAssetSalary = (url) => {
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
                                $("#view-asset-salary").html(res.html);
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

