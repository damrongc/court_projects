$(function () {
    $(document).on('click', '#btnSelected', function () {
        var code = $(this).attr("data-code");
        var name = $(this).attr("data-name");
        var location = $(this).attr("data-location");
        $('#form-modal-lookup').modal('hide');
        $('#txtProductCodeLookup').val(code);
        $('#txtProductNameLookup').val(name);
        $('#txtLocationLookup').val(location);
    });
});

function getProductLookup(url) {
    //var url = $("#hdGetWithPaging").val();
    var table = $('#tbl_product_lookup').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json",
        },
        "ordering": false,
        //"fixedHeader": true,
        "aLengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
        "iDisplayLength": 10,
        "scrollCollapse": true,
        "scrollX": true,
        "autoWidth": false,
        "language": {
            search: ""
        },
        "columns": [
            //{
            //    data: "image_path", name: "image_path", className: "w100",
            //    render: function (data, type, row) {
            //        return "<a href='#' class='btn btn-dark btn-sm' onclick=alert('" + row.product_code + "'); >View Image</a>";
            //        //return '<img src="images/energy_plus_logo_text.png" style="height: 50px;width:auto;" />';
            //        //if (type === 'display') {
            //        //    return '<span id="' + data.ID + '">' + data.FullName + '</span>';
            //        //} else if (type === 'sort') {
            //        //    return data.LastName;
            //        //} else {
            //        //    return data;
            //        //}
            //    }
            //},
            { data: "product_code", name: "product_code" },
            {
                data: "product_name", name: "product_name",
                render: function (data, type, row) {
                    return "<a href='#' id='btnSelected'  data-code='" + row.product_code + "' data-name='" + row.product_name + "' data-location='" + row.location_code + "'>" + data + "</a>";
                }
            },

            //{ data: "description", name: "description" },
            //{
            //    data: "production_date", name: "production_date", render: function (data, type, row) {
            //        return moment(data).format('DD-MM-YYYY');//moment(data,'DD-MM-YYYY');
            //    }
            //},
            { data: "stock_qty", name: "stock_qty" },
            //{ data: "location_code", name: "location_code" },
            //{ data: "remark", name: "remark", },
        ]

    });

    //$('#btnSelected').on('click', function () {
    //    console.log("aasa");
    //});

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
