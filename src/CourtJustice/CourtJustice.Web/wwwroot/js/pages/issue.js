$(function () {

    $('#dtp_issue_date').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
        closeOnDateSelect: true
    });

    $("#ddl_product").select2({
        maximumSelectionLength: 1,
        dropdownAutoWidth: false,
        placeholder: "Search..."
    });

    $('#btn_add_product').on('click', function () {
        var url = $('#hdGetStockLocation').val();
        var productCode = $("#txtProductCodeLookup").val();
        var productName = $("#txtProductNameLookup").val();
        //var locationCode = $("#txtLocationLookup").val();
        var issueQty = $("#txtIssueQty").val();

        if (productCode == '' || productCode == undefined) {
            swal({
                title: "Error",
                text: "กรุณาเลือก สินค้า!",
                icon: "error"
            });
            return false;
        }
        if (issueQty == '' || issueQty == undefined) {
            swal({
                title: "Error",
                text: "กรุณาเลือก ระบุจำนวน!",
                icon: "error"
            });
            return false;
        } else {
            if (parseInt(issueQty) <= 0) {
                swal({
                    title: "Error",
                    text: "กรุณาเลือก ระบุจำนวน!",
                    icon: "error"
                });
                return false;
            }
        }


        var tableRow = $('#tbl_issue_detail tbody tr');
        var valid = true;
        tableRow.each(function () {
            var productCell = $(this).find(".product_code-cell");

            var cellProductCode = $(productCell).attr("data-id");
            if (productCode == cellProductCode) {
                swal({
                    title: "Error",
                    text: "มีสินค้าที่ต้องการเบิกแล้ว!",
                    icon: "error"
                });
                valid = false;
                $("#txtProductCodeLookup").val(null);
                $("#txtProductNameLookup").val(null);
                $("#txtLocationLookup").val(null);
                $("#txtIssueQty").val("1");
                return false;
            }
        });
        if (valid) {

            try {
                $("#loaderbody").show();
                $.ajax({
                    type: "GET",
                    url: url + "?product_code=" + productCode,
                    contentType: "application/json; charset=utf-8",
                    success: function (res) {
                        console.log(res);
                        var markup = "<tr><td class='product_code-cell' data-id='" + productCode + "'>"
                            + productCode + "</td>"
                            + "<td class='product_name-cell'>" + productName + "</td>"
                            + "<td class='issue_qty-cell'>" + issueQty + "</td>"
                            + "<td class='issue_location-cell'>" + res + "</td>"

                            //+ "<td><button class='btn btn-info btnEdit btn-sm text-white'><i class='fa fa-edit'></i></button>"
                            + "<td><button class='btn btn-danger btnDelete btn-sm text-white'><i class='fa fa-trash'></i></button></td>"
                            + "</tr>";
                        $("#tbl_issue_detail tbody").append(markup);
                        $("#txtProductCodeLookup").val(null);
                        $("#txtProductNameLookup").val(null);
                        $("#txtLocationLookup").val(null);
                        $("#txtIssueQty").val("1");
                    }
                })
            } catch (e) {
                console.log(e);
            }
            finally {
                $("#loaderbody").hide();
            }


        }
    });

    //$('#tbl_issue_detail').on('click', '.btnEdit', function () {
    //    var tableRow = $(this).closest('tr');

    //    var productCode = tableRow.find('.product_code-cell').attr('data-id');
    //    var productName = tableRow.find('.product_name-cell').text();
    //    var issueQty = tableRow.find('.issue_qty-cell').text();

    //    $("#txtProductCodeLookup").val(productCode);
    //    $("#txtProductNameLookup").val(productName);
    //    $("#txtIssueQty").val(issueQty);


  
    //});

    $('#tbl_issue_detail').on('click', '.btnDelete', function () {
        $(this).closest('tr').remove();
    });


    $('#btn_save').on('click', function () {
        var url = '';
        var iss_doc_no = $("#iss_doc_no").val();
        if (iss_doc_no == '' || iss_doc_no == undefined) {
            url = $("#hdSaveIssue").val();
        } else {
            url = $("#hdEditIssue").val();
        }

        
        const tableRow = $('#tbl_issue_detail tbody tr');
        var idx = 0;
     
        const issueDetails = [];

        tableRow.each(function () {
            const product_code_cell = $(this).find(".product_code-cell");
            const product_name = $(this).find(".product_name-cell").html();
            const issue_qty = $(this).find(".issue_qty-cell").html();
            const location_code = $(this).find(".issue_location-cell").html();

            const product_code = $(product_code_cell).attr("data-id");

            let issueDetail = {};
            issueDetail.product_code = product_code;
            issueDetail.product_name = product_name;
            issueDetail.issue_qty = parseInt(issue_qty);
            issueDetail.location_code = location_code;
            issueDetails.push(issueDetail);
            idx++;
        });
        if (idx == 0) {
            swal({
                title: "Error",
                text: "กรุณาเลือกสินค้าที่ต้องการเบิก",
                icon: "error"
            });
            return false;
        }

        let issue = {};
        issue.iss_doc_no = iss_doc_no;
        issue.iss_doc_date = $("#dtp_issue_date").val();
        issue.approver = $("#ddl_approvers").val();
        issue.remark = $("#remark").val();
        issue.issue_reason_id = parseInt( $("#ddl_reasons").val());
   
        issue.remark = $("#remark").val();
        issue.issue_details = issueDetails;
        try {
            $("#loaderbody").show();
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(issue),
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


getByDate = () => {

    var url = $("#hdGetByDate").val();
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
            }
        })
    } catch (e) {
        console.log(e);
    }
    finally {
        $("#loaderbody").hide();

    }

}

