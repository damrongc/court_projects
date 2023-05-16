

$(function () {

});


function showAssetCarTab(url) {
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
                $("#view-asset-car").html(res.html);
            }


        }
    })
}


AddOrEditAsserCar = (form) => {
    try {
        var assetCar = {};

        var txtChassisNumber = $("#ChassisNumber");
        var txtEngineNumber = $("#EngineNumber");
        var ddlCarTypes = $("#ddlCarTypes");
        var txtBrand = $("#Brand");
        var txtEstimatePrice = $("#EstimatePrice");
        var txtLicensePlate = $("#LicensePlate");
        var txtOwner = $("#Owner");
        var txtCusId = $("#txtCusId");

        var errorMessage = "";
        var isValid = true;
        //if (txtPayMentId.val() == '' || txtPayMentId.val() == undefined) {
        //    isValid = false;
        //    errorMessage += txtPayMentId.attr('data-val-required') + '\n\r';
        //}
        if (txtChassisNumber.val() == '' || txtChassisNumber.val() == undefined) {
            isValid = false;
            errorMessage += txtChassisNumber.attr('data-val-required') + '\n\r';
        }
        if (txtEngineNumber.val() == '' || txtEngineNumber.val() == undefined) {
            isValid = false;
            errorMessage += txtEngineNumber.attr('data-val-required') + '\n\r';
        }
        if (ddlCarTypes.val() == '' || ddlCarTypes.val() == undefined) {
            isValid = false;
            errorMessage += ddlCarTypes.attr('data-val-required') + '\n\r';
        }
        if (txtBrand.val() == '' || txtBrand.val() == undefined) {
            isValid = false;
            errorMessage += txtBrand.attr('data-val-required') + '\n\r';
        }
        if (txtEstimatePrice.val() == '' || txtEstimatePrice.val() == undefined) {
            isValid = false;
            errorMessage += txtEstimatePrice.attr('data-val-required') + '\n\r';
        } else {
            if (parseInt(txtEstimatePrice.val()) <= 0) {
                isValid = false;
                errorMessage += "ราคาประเมินต้องมากกว่า 0" + '\n\r';
            }
        }

        if (txtLicensePlate.val() == '' || txtLicensePlate.val() == undefined) {
            isValid = false;
            errorMessage += txtLicensePlate.attr('data-val-required') + '\n\r';
        }

        if (txtOwner.val() == '' || txtOwner.val() == undefined) {
            isValid = false;
            errorMessage += txtOwner.attr('data-val-required') + '\n\r';
        }

        if (!isValid) {
            swal({
                title: "Error",
                text: errorMessage,
                icon: "error"
            });
            return false;
        }


        assetCar.ChassisNumber = txtChassisNumber.val();
        assetCar.EngineNumber = txtEngineNumber.val();
        assetCar.CarTypeCode = ddlCarTypes.val();
        assetCar.Brand = txtBrand.val();
        assetCar.EstimatePrice = txtEstimatePrice.val();
        assetCar.LicensePlate = txtLicensePlate.val();
        assetCar.Owner = txtOwner.val();
        assetCar.CusId = txtCusId.val();


        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(assetCar),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "ข้อมูล การชำระ บันทึกเรียบร้อยแล้ว.",
                        icon: "success",
                    })
                        .then(() => {
                            $("#view-asset-car").html(res.html);
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




confirmDeleteAssetCar = (url) => {
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
                                $("#view-asset-car").html(res.html);
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





