
$(function () {

});
function showAssetLandTab(url) {
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
        activaTab('remark-1');
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

confirmDeleteAssetLand = (url) => {
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
                                $("#view-asset-land").html(res.html);
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


previewImageAssetLand = (inputId) => {
    if (typeof (FileReader) != "undefined") {
        var dvPreview = $("#divAssetLandImagePreview");
        dvPreview.html("");
        var input = document.getElementById(inputId);
        //var files = input.files;

        $($(input)[0].files).each(function () {
            var file = $(this);
            var reader = new FileReader();
            reader.onload = function (e) {
                var img = $("<img />");
                img.attr("style", "width: 100%; height:100%;object-fit:contain;");
                img.attr("src", e.target.result);
                dvPreview.append(img);
            }
            reader.readAsDataURL(file[0]);
        });
    } else {
        alert("This browser does not support HTML5 FileReader.");
    }

}
confirmUploadImageAssetLand = (inputId) => {
    var url = $("#hdUploadRoute").val();
    var cusId = $('#txtCusId').val();
    var assetId = $('#AssetId').val();
    var input = document.getElementById(inputId);
    var files = input.files;
    if (files.length == 0) {
        swal({
            title: "พบข้อผิดพลาด",
            text: "กรุณาเลือกรูปภาพที่ต้องการอัพโหลด!",
            icon: "error"
        });
        return false;
    }
    var formData = new FormData();
    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    formData.append("AssetId", assetId);
    formData.append("CusId", cusId);
    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "อัพโหลด เรียบร้อยแล้ว",
                        icon: "success"
                    }).then((val) => {
                        $("#view-asset-land").html(res.html);
                        closePopupXL();
                    });
                } else {
                    swal({
                        title: "พบข้อผิดพลาด",
                        text: res.message,
                        icon: "error"
                    });
                }
            }
        }
    );
}

confirmDeleteImageAssetLand = (url) => {
    var cusId = $('#txtCusId').val();
    $.ajax(
        {
            url: `${url}?cusId=${cusId}`,
            contentType: "application/json; charset=utf-8",
            type: "DELETE",
            success: function (res) {
                if (res.isValid) {
                    swal({
                        title: "สำเร็จ",
                        text: "อัพโหลด เรียบร้อยแล้ว",
                        icon: "success"
                    }).then((val) => {
                        $("#view-asset-land").html(res.html);
                        closePopup();
                    });
                } else {
                    swal({
                        title: "พบข้อผิดพลาด",
                        text: res.message,
                        icon: "error"
                    });
                }
            }
        }
    );
}
