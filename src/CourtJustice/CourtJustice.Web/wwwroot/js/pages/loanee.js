var table;


$(function () {
    //$('#ddlEmployer').select2();
    //$('#ddlLoanTaskStatus').select2();


    //$('#txtLastPaidDate').datetimepicker({
    //    timepicker: false,
    //    format: 'd-m-Y',
    //    closeOnDateSelect: true
    //});
    //$('#txtFirstPaidDate').datetimepicker({
    //    timepicker: false,
    //    format: 'd-m-Y',
    //    closeOnDateSelect: true
    //});
    //$('#txtDueDate').datetimepicker({
    //    timepicker: false,
    //    format: 'd-m-Y',
    //    closeOnDateSelect: true
    //});
    //$('#txtFollowUpDate').datetimepicker({
    //    timepicker: false,
    //    format: 'd-m-Y',
    //    closeOnDateSelect: true
    //});
    getLoanee();
    onSelectCusId();

    //$('#asset-tab').on('click', function () {
    //    var url = $(this).attr('data-url');
    //    console.log(url);
    //    alert('asset-tab');
    //});
});


function showNotice(url) {
    //$.ajax({
    //    type: "GET",
    //    url: url,
    //    contentType: "application/json; charset=utf-8",
    //    success: function (res) {
    //    }
    //})
    window.open(url, '_blank');
    return false;
}
//function showAssetCarTab(url) {
//    console.log(url);
//    if (url == '' || url == undefined) {
//        alert('something wrong at showAssetCarTab!');
//        return false;
//    }
//    var id = $('#txtCusId').val();
//    console.log(id);
//    $.ajax({
//        type: "GET",
//        url: url + "/" + id,
//        contentType: "application/json; charset=utf-8",
//        success: function (res) {
//            $("#view-asset-land").html(res.html);
//            //console.log(res.html);
//        }
//    })
//}

/*function showPaymentTab(url) {
    console.log(url);
    if (url == '' || url == undefined) {
        alert('something wrong at showPaymentTab!');
        return false;
    }
    var id = $("txtLoanNumber").val();
    console.log(id);
    $.ajax({
        type: "GET",
        url: url + "/" + id,
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#view-payment").html(res.html);
            //console.log(res.html);
        }
    })

}*/

function onSelectCusId() {
    $(document).on('click', '#btnLoaneeSelected', function () {

        var url = $('#hdGetLoaneeByKey').val();
        var id = $(this).attr("data-id");

        if (url == '' || url == undefined) {
            alert('something wrong!');
        }
        //alert(cusId);

        //var idx = parseInt($(this).attr("data-idx"));
        //$('#form-modal-lookup').modal('hide');
        console.log("onSelectCusId");
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

getLoanee = () => {
    var url = $("#hdGetWithPaging").val();
    var bucketId = $("#ddlLoanTaskStatus").val();
    var employerCode = $("#ddlEmployer").val();
    table = $('#tbl_loanee').DataTable({
        "destroy": true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.bucketId = bucketId;
                d.employerCode = employerCode;
            }
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
            { data: "contractNo", name: "contractNo", class: "text-nowrap bg-primary text-white", },
            {
                data: "contractDate", name: "contractDate", class: "text-nowrap bg-primary text-white", render: function (data, type, row) {
                    return formatDate(data);
                }
            },
            { data: "cusId", name: "cusId", class: "text-nowrap bg-primary text-white" },
            { data: "nationalityId", name: "nationalityId", class: "text-nowrap bg-primary text-white" },
            {
                data: "name", name: "name", class: "text-nowrap bg-primary text-white",
                render: function (data, type, row) {
                    return "<a href='#' id='btnLoaneeSelected'  data-id='" + row.cusId + "'>" + data + "</a>";
                }
            },
            //{ data: "phoneNumber", name: "phoneNumber", class: "text-nowrap", },
            //{ data: "address", name: "address", class: "text-nowrap", },
            //{ data: "address1", name: "address1", class: "text-nowrap", },
            //{ data: "address2", name: "address2", class: "text-nowrap", },
            //{ data: "occupationName", name: "occupationName", class: "text-nowrap", },
            //{ data: "loanTypeName", name: "loanTypeName", },
            {
                data: "installmentsByContract", name: "installmentsByContract", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            { data: "term", name: "term", class: "text-right", },
            {
                data: "loanAmount", name: "loanAmount", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "woBalance", name: "woBalance", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            { data: "homeAddress1", name: "homeAddress1", class: "text-nowrap", },
            { data: "homeAddress2", name: "homeAddress2", class: "text-nowrap", },
            { data: "homeAddress3", name: "homeAddress3", class: "text-nowrap", },
            { data: "homeAddress4", name: "homeAddress4", class: "text-nowrap", },
            { data: "telephoneHome", name: "telephoneHome", class: "text-nowrap", },
            { data: "mobileHome", name: "mobileHome", class: "text-nowrap", },
            { data: "idenAddress1", name: "idenAddress1", class: "text-nowrap", },
            { data: "idenAddress2", name: "idenAddress2", class: "text-nowrap", },
            { data: "idenAddress3", name: "idenAddress3", class: "text-nowrap", },
            { data: "idenAddress4", name: "idenAddress4", class: "text-nowrap", },
            { data: "mobileEmg", name: "mobileEmg", class: "text-nowrap", },
            { data: "officeAddress1", name: "officeAddress1", class: "text-nowrap", },
            { data: "officeAddress2", name: "officeAddress2", class: "text-nowrap", },
            { data: "officeAddress3", name: "officeAddress3", class: "text-nowrap", },
            { data: "officeAddress4", name: "officeAddress4", class: "text-nowrap", },
            { data: "telephoneOffice", name: "telephoneOffice", class: "text-nowrap", },
            { data: "mobileOffice", name: "mobileOffice", class: "text-nowrap", },
            {
                data: "overdueAmount", name: "overdueAmount", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "totalPenalty", name: "totalPenalty", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "closingAmount", name: "closingAmount", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "rcvAmtStatus", name: "rcvAmtStatus", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "rcvAmtBeforeWO", name: "rcvAmtBeforeWO", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "rcvAmtAfterWO", name: "rcvAmtAfterWO", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "firstPaidDate", name: "firstPaidDate", class: "text-nowrap", render: function (data, type, row) {
                    return formatDate(data);
                }
            },
            {
                data: "lastPaidAmount", name: "lastPaidAmount", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            {
                data: "lastPaidDate", name: "lastPaidDate", class: "text-nowrap", render: function (data, type, row) {
                    return formatDate(data);
                }
            },
            {
                data: "dueDate", name: "dueDate", class: "text-nowrap", render: function (data, type, row) {
                    return formatDate(data);
                }
            },
            //{ data: "(string)null", searchable: false, className: "w100", sortable: false, defaultContent: actionSection }
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

    new $.fn.dataTable.FixedColumns(table, {
        leftColumns: 5
    });
}

navigateTo = (route) => {
    window.location.href = route;
}
