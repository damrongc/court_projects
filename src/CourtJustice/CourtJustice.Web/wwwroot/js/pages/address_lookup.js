$(function () {
    $(document).on('click', '#btnSelected', function () {

        var url = $('#hdGetAddressSetById').val();
        var addressId = $(this).attr("data-id");
        var idx = parseInt($(this).attr("data-idx"));
        $('#form-modal-lookup').modal('hide');

        $.ajax({
            type: "GET",
            url: url + "/" + addressId,
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                //console.log(res);
                //console.log(idx);
                var addressDetail = `${res.subDistrictName} ${res.districtName} ${res.provinceName} ${res.postalCode}`
                switch (idx) {
                    case 0:
                        $('#AddressDetail').val(addressDetail);

                        //$('#AddressId').val(addressId);
                        //$('#txtProvince').val(res.provinceName);
                        //$('#txtDistrict').val(res.districtName);
                        //$('#txtSubdistrict').val(res.subDistrictName);
                        //$('#txtPostalCode').val(res.postalCode);
                        break;
                    case 1:
                        $('#AddressDetail1').val(addressDetail);
                        //$('#Address1Id').val(addressId);
                        //$('#txtProvince1').val(res.provinceName);
                        //$('#txtDistrict1').val(res.districtName);
                        //$('#txtSubdistrict1').val(res.subDistrictName);
                        //$('#txtPostalCode1').val(res.postalCode);
                        break;
                    case 2:
                        $('#AddressDetail2').val(addressDetail);
                        //$('#Address2Id').val(addressId);
                        //$('#txtProvince2').val(res.provinceName);
                        //$('#txtDistrict2').val(res.districtName);
                        //$('#txtSubdistrict2').val(res.subDistrictName);
                        //$('#txtPostalCode2').val(res.postalCode);
                        break;
                    case 3:
                        $('#AddressDetail3').val(addressDetail);
                        //$('#Address2Id').val(addressId);
                        //$('#txtProvince2').val(res.provinceName);
                        //$('#txtDistrict2').val(res.districtName);
                        //$('#txtSubdistrict2').val(res.subDistrictName);
                        //$('#txtPostalCode2').val(res.postalCode);
                        break;
                }
            }
        })


        //$('#txtProductCodeLookup').val(code);
        //$('#txtProductNameLookup').val(name);
    });
});
showLookup = (idx, url, getUrl) => {
    console.log(url);
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal-lookup .modal-body').html(res);
            $('#form-modal-lookup .modal-title').html('ข้อมูลที่อยู่');
            $('#form-modal-lookup').modal('show');
            getAddressSetLookup(idx, getUrl);
        }
    })

}
function getAddressSetLookup(idx, url) {
    //var url = $("#hdGetWithPaging").val();
    var table = $('#tbl_address_set_lookup').DataTable({
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
            { data: "addressId", name: "addressId" },
            {
                data: "subDistrictName", name: "subDistrictName",
                render: function (data, type, row) {
                    return "<a href='#' id='btnSelected'  data-id='" + row.addressId + "' data-idx='" + idx + "' >" + data + "</a>";
                }
            },
            { data: "districtName", name: "districtName", },
            {data: "provinceName", name: "provinceName",},
            { data: "postalCode", name: "postalCode", },
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
