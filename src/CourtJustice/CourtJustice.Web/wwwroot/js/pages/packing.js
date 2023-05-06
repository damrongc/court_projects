$(function () {

    $('#dtp_packing_date').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
        closeOnDateSelect: true
    });

    $("#ddl_product").select2({
        maximumSelectionLength: 1,
        dropdownAutoWidth: false,
        placeholder: "Search..."
    });
    $('#btn_save').on('click', function () {
        var packing_no = $("#packing_no").val();
        var url = ''
        if (packing_no == '' || packing_no == undefined) {
            url = $("#hdSavePacking").val();
        } else {
            url = $("#hdEditPacking").val();
        }



        var product_code = $("#txtProductCodeLookup").val();
        if (product_code == '' || product_code == undefined) {
            swal({
                title: "Error",
                text: "กรุณาเลือก สินค้า!",
                icon: "error"
            });
        }
        let packing = {};
        packing.packing_no = packing_no;
        packing.packing_date = $("#dtp_packing_date").val();
        packing.product_code = product_code;
        packing.qty = parseInt($("#qty").val());
        packing.remark = $("#remark").val();
        try {
            $("#loaderbody").show();
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(packing),
                contentType: "application/json; charset=utf-8",
                success: function (res) {
                    if (res.isValid) {
                        window.location.replace(res.returnUrl);
                    } else {
                        swal({
                            title: "Error",
                            text: res.message,
                            icon: "error"
                        });
                    }
                }
            })
        } catch (e) {
            console.log(e);
        }
        finally {
            $("#loaderbody").hide();

        }
    });

    //$('#form-modal-lookup').on('shown.bs.modal', function () {
    //    getProductLookup();

    //});

});

showProductLookup = (url, getUrl) => {
    console.log(url);
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal-lookup .modal-body').html(res);
            $('#form-modal-lookup .modal-title').html('Product List');
            $('#form-modal-lookup').modal('show');

            getProductLookup(getUrl);
        }
    })

}


getPackingByDate = () => {

    var url = $("#hdGetPackingByDate").val();



    var startDate = $("#txtStartDate").val();
    var endDate = $("#txtEndDate").val();

    var request = {};
    request.StartDate = startDate;
    request.EndDate = endDate;

    try {
        $("#loaderbody").show();
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(request),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.isValid) {
                    $("#view-table").html(res.html);
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();

                } else {
                    alert(res.message);
                }
                $("#loaderbody").hide();
            }
        })
    } catch (e) {
        $("#loaderbody").hide();
        console.log(e);
    }

}

getQRCode = (url) => {
    window.open(url, '_blank');
}
