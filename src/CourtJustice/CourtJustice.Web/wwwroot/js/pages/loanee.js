var table;


$(function () {

    $('#txtLastPaidDate').datetimepicker({
        timepicker: false,
        format: 'd-m-Y',
        closeOnDateSelect: true
    });
    $('#txtFirstPaidDate').datetimepicker({
        timepicker: false,
        format: 'd-m-Y',
        closeOnDateSelect: true
    });
    $('#txtDueDate').datetimepicker({
        timepicker: false,
        format: 'd-m-Y',
        closeOnDateSelect: true
    });
    $('#txtFollowUpDate').datetimepicker({
        timepicker: false,
        format: 'd-m-Y',
        closeOnDateSelect: true
    });
    getWithPaging();

    onSelectCusId();


    //$('#asset-tab').on('click', function () {
    //    var url = $(this).attr('data-url');
    //    console.log(url);
    //    alert('asset-tab');
    //});
});

function showAssetLandTab(url) {
    console.log(url);
    var id = $('#txtCusId').val();
    console.log(id);
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#view-asset-land").html(res.html);
            //console.log(res.html);
        }
    })
}

function showAssetCarTab(url) {
    console.log(url);
    var id = $('#txtCusId').val();
    console.log(id);
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#view-asset-land").html(res.html);
            //console.log(res.html);
        }
    })
}

function onSelectCusId() {
    $(document).on('click', '#btnSelected', function () {

        var url = $('#hdGetLoaneeByKey').val();
        var id = $(this).attr("data-id");
        //alert(cusId);

        //var idx = parseInt($(this).attr("data-idx"));
        //$('#form-modal-lookup').modal('hide');

        $('#txtCusId').val(id);
        $.ajax({
            type: "GET",
            url: url + "/" + id,
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                $("#view-loanee").html(res.html);
                activaTab('loanee-1');

            }
        });


    });
}

function activaTab(tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
};

var actionSection = `<ul class="navbar-nav">
            <li class="nav-item  dropdown d-none align-items-center d-lg-flex d-none">
              <a class="dropdown-toggle btn btn-outline-secondary btn-fw"  href="#" data-toggle="dropdown" id="pagesDropdown">
              <span class="nav-profile-name">Action</span>
              </a>
              <div class="dropdown-menu dropdown-menu-right navbar-dropdown" aria-labelledby="pagesDropdown">
                <a class="dropdown-item">ออก Notice</a>
                <a class="dropdown-item">ขออนุมัติฟ้อง-เอกสาร</a>
                <a class="dropdown-item">ขออนุมัติส่วนลด-เอกสาร</a>
              </div>
            </li>
          </ul>`
getWithPaging = () => {
    var url = $("#hdGetWithPaging").val();
    table = $('#tbl_loanee').DataTable({
        "destroy": true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json",
        },
        "ordering": false,
        "fixedHeader": true,
        "aLengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
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
            { data: "loanNumber", name: "loanNumber" },
            { data: "cusId", name: "cusId", class: "text-nowrap" },
            {
                data: "name", name: "name", class: "text-nowrap",
                render: function (data, type, row) {
                    return "<a href='#' id='btnSelected'  data-id='" + row.cusId + "'>" + data + "</a>";
                }
            },
            { data: "phoneNumber", name: "phoneNumber", class: "text-nowrap", },
            { data: "address", name: "address", class: "text-nowrap", },
            { data: "address1", name: "address1", class: "text-nowrap", },
            { data: "address2", name: "address2", class: "text-nowrap", },
            { data: "occupationName", name: "occupationName", class: "text-nowrap", },
            //{ data: "loanTypeName", name: "loanTypeName", },
            {
                data: "installmentsByContract", name: "installmentsByContract", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "installmentsByAgree", name: "installmentsByAgree", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "lastPaidDate", name: "lastPaidDate", render: function (data, type, row) {
                    return moment(data).format("DD-MM-YYYY");
                }
            },
            {
                data: "firstPaidDate", name: "firstPaidDate", render: function (data, type, row) {
                    return moment(data).format("DD-MM-YYYY");
                }
            },
            {
                data: "intereteRate", name: "intereteRate", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "intereteRateAmount", name: "intereteRateAmount", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "overdueAmount", name: "overdueAmount", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "dueDate", name: "dueDate", class: "text-nowrap", render: function (data, type, row) {
                    return moment(data).format("DD-MM-YYYY");
                }
            },
            {
                data: "followUpDate", name: "followUpDate", class: "text-nowrap", render: function (data, type, row) {
                    return moment(data).format("DD-MM-YYYY");
                }
            },
            {
                data: "paidAmount", name: "paidAmount", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "paidInMonthAmount", name: "paidInMonthAmount", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "totalAmount", name: "totalAmount", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "remainingAmount", name: "remainingAmount", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "overdueDayAmount", name: "overdueDayAmount", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },

            { data: "(string)null", searchable: false, className: "w100", sortable: false, defaultContent: actionSection }
            //{ data: "(string)null", searchable: false, className: "w100", sortable: false, defaultContent: "<a id='btnEdit' class='btn btn-sm btn-primary text-white js-action'><i class='fa fa-edit'></i></a><a id='btnDelete' class='btn btn-sm btn-danger text-white js-action'><i class='fa fa-trash'></i></a>" }

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
