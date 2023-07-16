
$(function () {



});


getCompanyActionCode = () => {
    const url_get = $("#hdGetAllCompanyActionRoute").val();
    $.ajax({
        type: 'GET',
        url: url_get,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.isValid) {
                $("#view-table").html(data.html);
                if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                    activatejQueryTable();
            } else {
                alert(data.message)
            }
        },
        error: function (err) {
            console.log(err);
        }
    });

}

saveCompanyResultCode = () => {
    const url = $("#hdAddOrEditRoute").val();
    const companyActionId = $("#txtCompanyActionId").val();
    const companyResultId = parseInt($('#txtCompanyResultId').val());
    const companyResultCodeId = $('#txtCompanyResultCodeId').val();
    const companyResultCodeName = $('#txtCompanyResultCodeName').val();

    if (companyResultCodeId == "" || companyResultCodeId == undefined) {
        swal({
            title: "พบข้อผิดพลาด",
            text: "กรุณาระบุข้อมูล Result Code[บริษัท]",
            icon: "warning",
            dangerMode: true,
        }).then((_) => {
            $('#txtCompanyResultCodeId').focus();
        });
        return;
    }
    if (companyResultCodeName == "" || companyResultCodeName == undefined) {
        swal({
            title: "พบข้อผิดพลาด",
            text: "กรุณาระบุข้อมูล คำอธิบาย",
            icon: "warning",
            dangerMode: true,
        }).then((_) => {
            $('#txtCompanyResultCodeName').focus();
        });
        return;
    }
    let request = {};
    request.CompanyActionId = parseInt(companyActionId);
    request.CompanyResultId = companyResultId;
    request.CompanyResultCodeId = companyResultCodeId;
    request.CompanyResultCodeName = companyResultCodeName;

    $("#loaderbody").show();
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                swal({
                    title: "สำเร็จ",
                    text: "บันทึกข้อมูล เรียบร้อยแล้ว",
                    icon: "success"
                }).then((_) => {
                    closePopup();
                    getCompanyActionCode();
                });
            } else {
                swal({
                    title: "Error",
                    text: res.message,
                    icon: "error"
                });
            }
            $("#loaderbody").hide();
        },
        error: function (err) {
            $("#loaderbody").hide();
            console.log(err);
        }
    });

}

deleteCompanyResultCode = () => {
    const url = $('#hdDeleteCompanyResultCodeRoute').val();
    const id = parseInt($('#txtCompanyResultId').val());
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
                    url: `${url}/${id}`,
                    contentType: "application/json; charset=utf-8",
                    success: function (res) {
                        if (res.isValid) {
                            swal({
                                title: "สำเร็จ",
                                text: "ลบข้อมูล เรียบร้อยแล้ว",
                                icon: "success"
                            }).then((_) => {
                                closePopup();
                                getCompanyActionCode();
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







