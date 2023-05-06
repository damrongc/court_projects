$(function () {

    // DataTable Buttons
    //$("#tbl_product").on("click", ".js-action", function (event) {
    //    var btnId = $(this).attr("id");
    //    var productCode = getRecordId($(this));
    //    console.log(productCode);
    //    var url = $("#hdIndexProductLookup").val();

    //    $.ajax({
    //        type: 'GET',
    //        url: url,
    //        success: function (res) {
    //            $('#form-modal-lookup .modal-body').html(res);
    //            $('#form-modal-lookup .modal-title').html('Product List');
    //            $('#form-modal-lookup').modal('show');
    //        }
    //    })
    //});

    //$('#form-modal-lookup').on('shown.bs.modal', function () {
    //    getProductLookup();

    //});

    //$("#tbl_product_lookup").on("click", ".product-selected", function (event) {
    //    var code = $(this).attr("data-code");
    //    var name = $(this).attr("data-name");
    //    console.log(code);
    //    console.log(name);
    //});

    $(document).on('click', '#btnSelected', function () {

        console.log("btnSelected");
        var code = $(this).attr("data-code");
        var name = $(this).attr("data-name");
        console.log(code);
        console.log(name);

        $('#form-modal-lookup').modal('hide');

        $('#txtProductCodeLookup').val(code);
        $('#txtProductNameLookup').val(name);
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
                    //var productData ='$row.product_code' row.product_code + '|' + row.product_name;
                    var productData = `${row.product_code}-${row.product_name}`;
                    return "<a href='#' id='btnSelected'  data-code='" + row.product_code + "' data-name='" + row.product_name + "' >" + data + "</a>";
                }
            },
            { data: "description", name: "description" },
            { data: "remark", name: "remark", },
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
