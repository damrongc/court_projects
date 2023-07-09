$(function () {
    //$(".dateonly").datetimepicker({
    //    timepicker: false,
    //    format: 'd-m-Y',
    //    closeOnDateSelect: true
    //});
});


getReceiptReport = (url) => {
    $("#loaderbody").show();
    var startDate = $("#txtStartDate").val();
    var endDate = $("#txtEndDate").val();
    var employerCode = $("#ddlEmployerFilter").val();


    var request = {};
    request.EmployerCode = employerCode;
    request.StartDate = startDate;
    request.EndDate = endDate;

    try {

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(request),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                $("#loaderbody").hide();
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
        $("#loaderbody").hide();
        console.log(e);
    }

}

exportReceiptReport = (url, urlDownload) => {
    $("#loaderbody").show();
    var startDate = $("#txtStartDate").val();
    var endDate = $("#txtEndDate").val();
    var employerCode = $("#ddlEmployerFilter").val();


    var request = {};
    request.EmployerCode = employerCode;
    request.StartDate = startDate;
    request.EndDate = endDate;
    $.ajax({
        type: "POST",
        url: url,
        datatype: 'json',
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#loaderbody").hide();
            if (res.fileName != "") {
                window.location.href = urlDownload + "/?fileName=" + res.fileName;
            }
        },
        error: function (xhr, status, error) {
            $("#loaderbody").hide();
            var errorMessage = xhr.status + ': ' + xhr.statusText + ' ' + xhr.responseText;
            alert('Error - ' + errorMessage);
        }
    })
}

getRemarkReport = (url) => {
    $("#loaderbody").show();
    var startDate = $("#txtStartDate").val();
    var endDate = $("#txtEndDate").val();
    var employerCode = $("#ddlEmployerFilter").val();
    var request = {};
    request.EmployerCode = employerCode;
    request.StartDate = startDate;
    request.EndDate = endDate;

    try {

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(request),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                $("#loaderbody").hide();
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
        $("#loaderbody").hide();
        console.log(e);
    }

}

exportRemarkReport = (url, urlDownload) => {
    $("#loaderbody").show();
    var startDate = $("#txtStartDate").val();
    var endDate = $("#txtEndDate").val();
    var employerCode = $("#ddlEmployerFilter").val();

    var request = {};
    request.EmployerCode = employerCode;
    request.StartDate = startDate;
    request.EndDate = endDate;
    $.ajax({
        type: "POST",
        url: url,
        datatype: 'json',
        data: JSON.stringify(request),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#loaderbody").hide();
            if (res.fileName != "") {
                window.location.href = urlDownload + "/?fileName=" + res.fileName;
            }
        },
        error: function (xhr, status, error) {
            $("#loaderbody").hide();
            var errorMessage = xhr.status + ': ' + xhr.statusText + ' ' + xhr.responseText;
            alert('Error - ' + errorMessage);
        }
    })
}