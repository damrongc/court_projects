$(function () {



});

setStatus = (id, flag) => {
    console.log(flag);
    var url = $('#hdSetStatusRoute').val();

    swal({
        title: "ต้องการเปลี่ยนสถานะ ข้อมูล?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "GET",
                    url: `${url}?id=${id}&flag=${flag}`,
                    contentType: "application/json; charset=utf-8",
                    success: function (res) {
                        console.log(res);
                        if (res.isValid) {
                            swal({
                                title: "สำเร็จ",
                                text: "ข้อมูลเปลี่ยนสถานะ เรียบร้อยแล้ว",
                                icon: "success"
                            }).then((val) => {
                                window.location.href = res.returnUrl;
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




