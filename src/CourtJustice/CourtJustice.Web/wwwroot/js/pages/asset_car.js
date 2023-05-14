var cusId;

$(function () {



});


function showAssetLandTab(url) {
    if (url == '' || url == undefined) {
        alert('something wrong at showAssetLandTab!');
        return false;
    }
    var cusId = $('#txtCusId').val();
    if (cusId == '' || cusId == undefined) {
        swal({
            title: "Error",
            text: "กรุณาเลือกลูกหนี้!",
            icon: "error"
        });
        activaTab('asset-car-1');
        return false;
    }
    $.ajax({
        type: "GET",
        url: url + "/" + cusId,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                $("#view-asset-car").html(res.html);
            }


        }
    })
}

AddOrEdit = (form) => {
    try {
        var txt = $("#")


    } catch (ex) {
        console.log(ex)
    }
    return false;
}


confirmDelete = (id) => {
    var url = $('#hdDeleteRoute').val();
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
                    sauccess: function (res) {
                        console.log(res);
                        if (res.isValid) {
                            swal({
                                title: "สำเร็จ",
                                text: "ลบข้อมูล เรียบร้อยแล้ว",
                                icon: "success"
                            }).then((val) => {
                                $("#view-table").html(res.html);
                                if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                                    activatejQueryTable();
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





