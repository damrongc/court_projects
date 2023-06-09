﻿



$(function () {



});
getBankResults = () => {
    var url = $("#hdGetBankResultRoute").val();
    var employerCode = $("#ddlEmployer").val();
    try {
        $("#loaderbody").show();
        $.ajax({
            type: "GET",
            url: url + "/" + employerCode,
            //data: JSON.stringify(request),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                $("#loaderbody").hide();
                if (res.isValid) {
                    $("#view-table").html(res.html);
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();

                } else {
                    alert(res.message);
                }
            },
            error: function (xhr, status, error) {
                $("#loaderbody").hide();
                var errorMessage = xhr.status + ': ' + xhr.statusText + ' ' + xhr.responseText;
                alert('Error - ' + errorMessage);
            }
        })

    } catch (e) {
        console.log(e);
    }
    finally {
        $("#loaderbody").hide();
    }
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
                    success: function (res) {
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






