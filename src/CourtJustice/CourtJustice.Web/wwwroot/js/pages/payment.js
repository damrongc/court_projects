

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
                $("#view-asset-land").html(res.html);
            }

            //$("#view-asset-land").html(res);
            //console.log(res.html);
        }
    })
}

AddOrEdit = (form) => {
    try {
        var assetLand = {};

        var txtAssetLandId = $("#AssetLandId");
        var txtPosition = $("#Position");
        var txtAddressSetLookup = $("#txtAddressSetLookup");
        var txtGps = $("#Gps");
        var ddlLandOffices = $("#ddlLandOffices");
        var txtEstimatePrice = $("#EstimatePrice");
        var txtAddressDetail = $("#AddressDetail");
        var txtCusId = $("#txtCusId");

        var errorMessage = "";
        var isValid = true;
        if (txtAssetLandId.val() == '' || txtAssetLandId.val() == undefined) {
            isValid = false;
            errorMessage += txtAssetLandId.attr('data-val-required') + '\n\r';
        }
        if (txtPosition.val() == '' || txtPosition.val() == undefined) {
            isValid = false;
            errorMessage += txtPosition.attr('data-val-required') + '\n\r';
        }
        if (txtAddressSetLookup.val() == '' || txtAddressSetLookup.val() == undefined) {
            isValid = false;
            errorMessage += txtAddressSetLookup.attr('data-val-required') + '\n\r';
        }
        if (txtGps.val() == '' || txtGps.val() == undefined) {
            isValid = false;
            errorMessage += txtGps.attr('data-val-required') + '\n\r';
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


        assetLand.AssetLandId = txtAssetLandId.val();
        assetLand.Position = txtPosition.val();
        assetLand.Address = txtAddressSetLookup.val();
        assetLand.Gps = txtGps.val();
        assetLand.LandOfficeCode = ddlLandOffices.val();
        assetLand.EstimatePrice = txtEstimatePrice.val();
        assetLand.AddressDetail = txtAddressDetail.val();
        assetLand.CusId = txtCusId.val();
        //console.log(assetLand);


        $.ajax({
            type: 'POST',
            url: form.action,
            data: JSON.stringify(assetLand),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "ข้อมูล หลักทรัพย์ที่ดิน บันทึกเรียบร้อยแล้ว.",
                        icon: "success",
                    })
                        .then(() => {
                            $("#view-asset-land").html(res.html);
                            closePopupXL();
                        });
                }


                //if (res.isValid) {


                //    //if (typeof activateDraggable !== 'undefined' && $.isFunction(activateDraggable))
                //    //    activateDraggable();

                //    //if (typeof activateContextMenu !== 'undefined' && $.isFunction(activateContextMenu))
                //    //    activateContextMenu();
                //}
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







