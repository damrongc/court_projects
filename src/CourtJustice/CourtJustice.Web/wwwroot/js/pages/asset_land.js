
//var table;
//var btnEdit;
$(function () {
    //getWithPaging();
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
        var txtAddressId = $("#AddressId");
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
        if (txtAddressId.val() == '' || txtAddressId.val() == undefined) {
            isValid = false;
            errorMessage += txtAddressId.attr('data-val-required') + '\n\r';
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
        assetLand.AddressId = txtAddressId.val();
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
                        text:"ข้อมูล หลักทรัพย์ที่ดิน บันทึกเรียบร้อยแล้ว.",
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


//getWithPaging = () => {
//    var url = $("#hdGetWithPaging").val();
//    table = $('#tbl_assetland').DataTable({
//        "destroy": true,
//        "processing": true,
//        "serverSide": true,
//        "ajax": {
//            "url": url,
//            "type": "POST",
//            "datatype": "json",
//        },
//        "ordering": false,
//        "fixedHeader": true,
//        "aLengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
//        "iDisplayLength": 10,
//        "scrollCollapse": true,
//        "scrollX": true,
//        "scrollY": 500,
//        "autoWidth": false,
//        "language": {
//            search: "_INPUT_",
//            searchPlaceholder: "Search..."
//        },
//        "columns": [
//            {
//                data: "assetLandId", name: "assetLandId",
//                 render: function (data, type, row) {
//                    return "<a href='#' id='btnSelected'  data-id='" + row.assetLandId + "'>" + data + "</a>";
//                }
//            },
//            {
//                data: "position", name: "position",

//            },
//            { data: "gps", name: "gps", },
//            { data: "landOfficeName", name: "landOfficeName", },
//            { data: "(string)null", searchable: false, className: "w100", sortable: false, defaultContent: "<a id='btnEdit' class='btn btn-sm btn-primary text-white js-action'><i class='fa fa-edit'></i></a><a id='btnDelete' class='btn btn-sm btn-danger text-white js-action'><i class='fa fa-trash'></i></a>" }

//        ]

//    });
//}


//$(table).each(function () {
//    var datatable = $(this);
//    // SEARCH - Add the placeholder for Search and Turn this into in-line form control
//    var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
//    search_input.attr('placeholder', 'Search');
//    search_input.removeClass('form-control-sm');
//    // LENGTH - Inline-Form control
//    var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
//    length_sel.removeClass('form-control-sm');
//});





