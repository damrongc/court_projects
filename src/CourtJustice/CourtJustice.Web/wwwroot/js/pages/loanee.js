var table;


$(function () {

    getLoanee();
    onSelectCusId();
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

function onSelectCusId() {
    $(document).on('click', '#btnLoaneeSelected', function () {

        var url = $('#hdGetLoaneeByKey').val();
        var id = $(this).attr("data-id");
        var name = $(this).attr("data-name");

        if (url == '' || url == undefined) {
            alert('something wrong!');
        }

        $('#txtCusId').val(id);
        $('#lblCustomerName').text(`${id}: ${name}`);
        $.ajax({
            type: "GET",
            url: url + "/" + id,
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                $("#view-loanee").html(res.html);
                activaTab('remark-1');
            }
        });


    });
}

function activaTab(tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
};

//var actionSection = `<ul class="navbar-nav">
//            <li class="nav-item  dropdown d-none align-items-center d-lg-flex d-none">
//              <a class="dropdown-toggle btn btn-outline-secondary btn-fw"  href="#" data-toggle="dropdown" id="pagesDropdown">
//              <span class="nav-profile-name">Action</span>
//              </a>
//              <div class="dropdown-menu dropdown-menu-right navbar-dropdown" aria-labelledby="pagesDropdown">
//                <a class="dropdown-item">ออก Notice</a>
//                <a class="dropdown-item">ขออนุมัติฟ้อง-เอกสาร</a>
//                <a class="dropdown-item">ขออนุมัติส่วนลด-เอกสาร</a>
//              </div>
//            </li>
//          </ul>`

getLoanee = () => {
    var url = $("#hdGetWithPaging").val();
    var loanTaskStatusId = $("#ddlLoanTaskStatusFilter").val();
    var employerCode = $("#ddlEmployerFilter").val();

    $('#txtCusId').val(null);
    $('#lblCustomerName').text(null);
    $("#view-loanee").html(null);
    activaTab('loanee-1');

    table = $('#tbl_loanee').DataTable({
        "destroy": true,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.loanTaskStatusId = loanTaskStatusId;
                d.employerCode = employerCode;
            }
        },
        "ordering": false,
        "fixedHeader": true,
        "aLengthMenu": [[5,10, 25, 50, 100], [5,10, 25, 50, 100]],
        "iDisplayLength": 5,
        "scrollCollapse": true,
        "scrollX": true,
        "scrollY": 300,
        "autoWidth": false,
        "language": {
            search: "_INPUT_",
            searchPlaceholder: "Search..."
        },
        "columns": [
            { data: "contractNo", name: "contractNo", class: "text-nowrap bg-light", },
            { data: "contractDate", name: "contractDate", class: "text-nowrap bg-light", },
            //{
            //    data: "contractDate", name: "contractDate", class: "text-nowrap bg-light", render: function (data, type, row) {
            //        return formatDate(data);
            //    }
            //},
            { data: "cusId", name: "cusId", class: "text-nowrap bg-light" },
            { data: "nationalityId", name: "nationalityId", class: "text-nowrap bg-light" },
            {
                data: "name", name: "name", class: "text-nowrap bg-light",
                render: function (data, type, row) {
                    return "<a href='#' id='btnLoaneeSelected' data-id='" + row.cusId + "' data-name='" + data + "'>" + data + "</a>";
                }
            },
            { data: "gender", name: "gender", class: "text-nowrap", },
            { data: "maritalStatus", name: "maritalStatus", class: "text-nowrap", },
            { data: "assignDate", name: "assignDate", class: "text-nowrap", },
            { data: "expireDate", name: "expireDate", class: "text-nowrap", },
            //{
            //    data: "assignDate", name: "assignDate", class: "text-nowrap", render: function (data, type, row) {
            //        return formatDate(data);
            //    }
            //},
            //{
            //    data: "expireDate", name: "expireDate", class: "text-nowrap", render: function (data, type, row) {
            //        return formatDate(data);
            //    }
            //},
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
            { data: "debtAge", name: "debtAge", class: "text-nowrap", },
            { data: "employerWorkGroup", name: "employerWorkGroup", class: "text-nowrap", },
            { data: "productCode", name: "productCode", class: "text-nowrap", },
            {
                data: "totalPayment", name: "totalPayment", class: "text-right", render: function (data, type, row) {
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
            { data: "companyName", name: "companyName", class: "text-nowrap", },
            {
                data: "salary", name: "salary", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            { data: "occupationName", name: "occupationName", class: "text-nowrap", },
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
            //{
            //    data: "firstPaidDate", name: "firstPaidDate", class: "text-nowrap", render: function (data, type, row) {
            //        return formatDate(data);
            //    }
            //},
            { data: "firstPaidDate", name: "firstPaidDate", class: "text-nowrap", },
            {
                data: "lastPaidAmount", name: "lastPaidAmount", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },
            //{
            //    data: "lastPaidDate", name: "lastPaidDate", class: "text-nowrap", render: function (data, type, row) {
            //        return formatDate(data);
            //    }
            //},
            //{
            //    data: "dueDate", name: "dueDate", class: "text-nowrap", render: function (data, type, row) {
            //        return formatDate(data);
            //    }
            //},
            { data: "lastPaidDate", name: "lastPaidDate", class: "text-nowrap", },
            { data: "dueDate", name: "dueDate", class: "text-nowrap", },
            { data: "followContractNo", name: "followContractNo", class: "text-nowrap", },
  
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

updateLoanee = () => {
    var url = $('#hdUpdateLoanee').val();
    var cusId = $('#txtCusId').val();
    //var employeeCode = $('#ddlEmployees').val();
    //var occupationId = $('#ddlOccupations').val();
    var loanTypeCode = $('#ddlLoanTypes').val();
    var loanTaskStatusId = $('#ddlLoanTaskStatus').val();

    var name = $('#txtName').val();
    var homeAddress1 = $('#txtHomeAddress1').val();
    var homeAddress2 = $('#txtHomeAddress2').val();
    var homeAddress3 = $('#txtHomeAddress3').val();
    var homeAddress4 = $('#txtHomeAddress4').val();
    var telephoneHome = $('#txtTelephoneHome').val();
    var mobileHome = $('#txtMobileHome').val();

    var idenAddress1 = $('#txtIdenAddress1').val();
    var idenAddress2 = $('#txtIdenAddress2').val();
    var idenAddress3 = $('#txtIdenAddress3').val();
    var idenAddress4 = $('#txtIdenAddress4').val();
    var mobileEmg = $('#txtMobileEmg').val();

    var companyName = $('#txtCompanyName').val();
    var occupationName = $('#txtOccupationName').val();
    var officeAddress1 = $('#txtOfficeAddress1').val();
    var officeAddress2 = $('#txtOfficeAddress2').val();
    var officeAddress3 = $('#txtOfficeAddress3').val();
    var officeAddress4 = $('#txtOfficeAddress4').val();
    var telephoneOffice = $('#txtTelephoneOffice').val();
    var mobileOffice = $('#txtMobileOffice').val();


    var updateLoaneeRequest = {};
    updateLoaneeRequest.CusId = cusId;
    updateLoaneeRequest.Name = name;
    updateLoaneeRequest.HomeAddress1 = homeAddress1;
    updateLoaneeRequest.HomeAddress2 = homeAddress2;
    updateLoaneeRequest.HomeAddress3 = homeAddress3;
    updateLoaneeRequest.HomeAddress4 = homeAddress4;
    updateLoaneeRequest.TelephoneHome = telephoneHome;
    updateLoaneeRequest.MobileHome = mobileHome;

    updateLoaneeRequest.IdenAddress1 = idenAddress1;
    updateLoaneeRequest.IdenAddress2 = idenAddress2;
    updateLoaneeRequest.IdenAddress3 = idenAddress3;
    updateLoaneeRequest.IdenAddress4 = idenAddress4;
    updateLoaneeRequest.MobileEmg = mobileEmg;

    updateLoaneeRequest.CompanyName = companyName;
    updateLoaneeRequest.OccupationName = occupationName;
    updateLoaneeRequest.OfficeAddress1 = officeAddress1;
    updateLoaneeRequest.OfficeAddress2 = officeAddress2;
    updateLoaneeRequest.OfficeAddress3 = officeAddress3;
    updateLoaneeRequest.OfficeAddress4 = officeAddress4;
    updateLoaneeRequest.TelephoneOffice = telephoneOffice;
    updateLoaneeRequest.MobileOffice = mobileOffice;
    updateLoaneeRequest.LoanTypeCode = loanTypeCode;
    //updateLoaneeRequest.OccupationId = occupationId;
    updateLoaneeRequest.LoanTaskStatusId = loanTaskStatusId;

    $("#loaderbody").show();
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(updateLoaneeRequest),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                swal({
                    title: "Success",
                    text: "Update is successfully",
                    icon: "success"
                });
            } else {
                swal({
                    title: "Error",
                    text: res.message,
                    icon: "error"
                });
            }
            $("#loaderbody").hide();
        },
        error: function (err) {
            $("#loaderbody").hide();
            console.log(err);
        }
    });
}

navigateTo = (route) => {
    window.location.href = route;
}
