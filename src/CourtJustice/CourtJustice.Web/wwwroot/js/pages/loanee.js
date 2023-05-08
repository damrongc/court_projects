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
});

var actionSection =`<ul class="navbar-nav">
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
getWithPaging =()=> {
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
            { data: "cusId", name: "cusId" },
            {
                data: "name", name: "name",
                render: function (data, type, row) {
                    return "<a href='#' id='btnSelected'  data-id='" + row.cusId + "'>" + data + "</a>";
                }
            },
            { data: "address", name: "address", },
            { data: "address1", name: "address1", },
            { data: "address2", name: "address2", },
            { data: "occupationName", name: "occupationName", },
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
