$(function () {
    $("#image_file").change(function () {
        if (typeof (FileReader) != "undefined") {
            var dvPreview = $("#divImageMediaPreview");
            dvPreview.html("");
            $($(this)[0].files).each(function () {
                var file = $(this);
                var reader = new FileReader();
                reader.onload = function (e) {
                    var img = $("<img />");
                    //img.attr("style", "width: 50%; height:50%;object-fit:contain;");
                    img.attr("src", e.target.result);
                    dvPreview.append(img);
                }
                reader.readAsDataURL(file[0]);
            });
        } else {
            alert("This browser does not support HTML5 FileReader.");
        }
    });

    activatejQueryTableWithServerPaging();

    // DataTable Buttons
    $("#tbl_product").on("click", ".js-action", function (event) {
        var btnId = $(this).attr("id");
        var productCode = getRecordId($(this));
        if (btnId !== undefined && btnId == "btnEdit") {
            var url = $('#hdEditRoute').val();


            window.location.href = url + "/" + productCode;
        } else if (btnId !== undefined && btnId == "btnDelete") {
            var url = $('#hdDeleteRoute').val();

            swal({
                title: "ต้องการลบข้อมูล?",
                text: "ข้อมูลที่ถูกลบแล้วจะไม่สามารถนำกลับมาใช้งานได้!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        $.ajax({
                            type: 'DELETE',
                            url: url + "/" + productCode,
                            contentType: false,
                            processData: false,
                            success: function (res) {
                                if (res.isValid) {
                                    swal({
                                        title: "สำเร็จ",
                                        text: "ข้อมูลถูกลบเรียบร้อยแล้ว",
                                        icon: "success"
                                    }).then((val) => {
                                        activatejQueryTableWithServerPaging();
                                        //$("#view-all").html(res.html);
                                        //if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                                        //    activatejQueryTable();
                                    });
                                }
                                else {
                                    swal({
                                        title: "พบข้อผิดพลาด",
                                        text: res.message,
                                        icon: "error"
                                    });
                                }
                            },
                            error: function (err) {
                                console.log(err)
                            }
                        })
                    }
                });
        }





        //console.log(productCode);
        //var url = $("#hdIndexProductLookup").val();

        //$.ajax({
        //    type: 'GET',
        //    url: url,
        //    success: function (res) {
        //        $('#form-modal-lookup .modal-body').html(res);
        //        $('#form-modal-lookup .modal-title').html('Product List');
        //        $('#form-modal-lookup').modal('show');
        //    }
        //})


        //swal({
        //    title: "Error",
        //    text: "กรุณาเลือก สินค้า!",
        //    icon: "error"
        //});
        //if (btnId !== undefined && btnId == "btnEdit") {
        //    // Create Edit URL
        //    var href = '@Url.Action("Popup", "DynaGrid")?appGridName=@Model.AppGrid.GridName&masterRecordId=@Model.AppGrid.MasterRecordFKId&id=' + recordId + '';

        //    renderModalForDataTableButton(href);
        //} else if (btnId !== undefined && btnId == "btnDelete") {
        //    // Code 

        //} else {
        //    alert("Something Went Wrong - Unable To Redirect");
        //}
    });

    $('#form-modal-lookup').on('shown.bs.modal', function () {
        getProductLookup();
        console.log("lookup");
    });
});

getWithPaging = () => {
    activatejQueryTableWithServerPaging();
    //var url = $("#hdGetWithPaging").val();
    //var request = {};
    //request.ProductGroup = "";

    //try {
    //    $("#loaderbody").show();
    //    $.ajax({
    //        type: "POST",
    //        url: url,
    //        data: JSON.stringify(request),
    //        contentType: "application/json; charset=utf-8",
    //        success: function (res) {
    //            if (res.isValid) {
    //                $("#view-table").html(res.html);
    //                if (typeof activatejQueryTableWithServerPaging !== 'undefined' && $.isFunction(activatejQueryTableWithServerPaging))
    //                    activatejQueryTableWithServerPaging();

    //            } else {
    //                alert(res.message);
    //            }
    //            $("#loaderbody").hide();
    //        }
    //    })
    //} catch (e) {
    //    $("#loaderbody").hide();
    //    console.log(e);
    //}

}
function getProductLookup() {
    var url = $("#hdGetWithPaging").val();
    var table = $('#tbl_product_lookup').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json"
        },
        "ordering": false,
        //"fixedHeader": true,
        "aLengthMenu": [
            [5, 10, 15, -1],
            [5, 10, 15, "All"]
        ],
        "iDisplayLength": 10,
        "scrollCollapse": true,
        "scrollX": true,
        //"scrollY": 500,
        "autoWidth": false,
        "language": {
            search: ""
        },
        "columns": [
            {
                data: "image_path", name: "image_path", className: "w100",
                render: function (data, type, row) {
                    return "<a href='#' class='btn btn-dark btn-sm' onclick=alert('" + row.product_code + "'); >View Image</a>";
                    //return '<img src="images/energy_plus_logo_text.png" style="height: 50px;width:auto;" />';
                    //if (type === 'display') {
                    //    return '<span id="' + data.ID + '">' + data.FullName + '</span>';
                    //} else if (type === 'sort') {
                    //    return data.LastName;
                    //} else {
                    //    return data;
                    //}
                }
            },
            { data: "product_code", name: "product_code" },
            {
                data: "product_name", name: "product_name",
                render: function (data, type, row) {
                    return "<a href='#' onclick=productSelected('" + row.product_code + "'); >" + data + "</a>";

                }
            },
            { data: "description", name: "description" },
            { data: "remark", name: "remark", },
        ]

    });
    $(table).each(function () {
        var datatable = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'Search');
        search_input.removeClass('form-control-sm');
        // LENGTH - Inline-Form control
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        length_sel.removeClass('form-control-sm');
    });
}

function productSelected(value) {
    $('#form-modal-lookup').modal('hide');
    //$('#txtStartDate').val(value);

}

var table;
function activatejQueryTableWithServerPaging() {
    var url = $("#hdGetWithPaging").val();
    var productGroupCode = $('#ddl_product_group_code').val();
    table = $('#tbl_product').DataTable({
        'destroy': true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.productGroupCode = productGroupCode;
            }
        },
        "ordering": false,
        "fixedHeader": true,
        "aLengthMenu": [
            [5, 10, 15, 50,100],
            [5, 10, 15, 50,100]
        ],
        "iDisplayLength": 10,
        "scrollCollapse": true,
        "scrollX": true,
        "scrollY": 500,
        "autoWidth": false,
        "language": {
            search: "_INPUT_",
            searchPlaceholder: "Search..."
        },
        "columns": [
            {
                data: "image_path", name: "image_path", "render": function (data, row) {
                    return "<img src='images/products/" + data + "' />";
                }
            },
            {
                data: "product_code", name: "product_code", "render": function (data, row) {
                    return `<div class="badge badge-pill badge-success">${data}</div>`;
                },
            },
            { data: "product_name", name: "product_name" },
            { data: "description", name: "description" },
            { data: "product_group_name", name: "product_group_name" },
            { data: "reorder_point", name: "reorder_point", className: "text-right" },
            { data: "minimum_stock", name: "minimum_stock", className: "text-right" },
            { data: "maximum_stock", name: "maximum_stock", className: "text-right" },
            { data: "shelf_life", name: "shelf_life", className: "text-right" },
            { data: "unit_price", name: "unit_price", className: "text-right" },
            { data: "remark", name: "remark", },
            { data: "(string)null", searchable: false, className: "w100", sortable: false, defaultContent: "<a id='btnEdit' class='btn btn-sm btn-primary text-white js-action'><i class='fa fa-edit'></i></a><a id='btnDelete' class='btn btn-sm btn-danger text-white js-action'><i class='fa fa-trash'></i></a>" }
            //{
            //    "render": function (data, row) {
            //        console.log(data);
            //        console.log(row);
            //        return "<a href='#' class='btn btn-danger' onclick=alert('" + row.product_code + "'); >Delete</a>";
            //    }
            //},
        ]

    });
    $(table).each(function () {
        var datatable = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'Search');
        search_input.removeClass('form-control-sm');
        // LENGTH - Inline-Form control
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        length_sel.removeClass('form-control-sm');
    });





}
// Get Record Id Of Current Row
function getRecordId(selectedRow) {
    var data = $("#tbl_product").DataTable().row((selectedRow).parents("tr")).data();
    return data.product_code;
}

function renderModalForDataTableButton(href) {
    $.get(href, function (data) {
        $('#myModalContent').html(data);
        $('#myModal').modal('show');
    });
}
