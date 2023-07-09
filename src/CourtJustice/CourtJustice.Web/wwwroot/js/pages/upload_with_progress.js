var isUpload = true;
function importLoanee(inputId) {
    isUpload = false;
    var employerCode = document.getElementById("ddlEmployer").value;
    var input = document.getElementById(inputId);
    var url = document.getElementById("hidUploadUrl").value;
    var files = input.files;
    var formData = new FormData();
    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
        formData.append("employerCode", employerCode);
    }
    $("#btnImport").attr("disabled", "disabled");
    startUpdatingProgressIndicator();
    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                if (data.isvalid) {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                } else {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                }
                $('#btnImport').removeAttr("disabled");
                isUpload = true;
            }
        }
    );
}

function importCancelLoanee(inputId) {
    isUpload = false;
    var input = document.getElementById(inputId);
    var url = document.getElementById("hidUploadUrl").value;
    var files = input.files;
    var formData = new FormData();
    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    $("#btnImport").attr("disabled", "disabled");
    startUpdatingProgressIndicator();
    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                if (data.isvalid) {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                } else {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                }
                $('#btnImport').removeAttr("disabled");
                isUpload = true;
            }
        }
    );
}


function importPayment(inputId) {
    isUpload = false;
    var input = document.getElementById(inputId);
    var url = document.getElementById("hidUploadUrl").value;
    var files = input.files;
    var formData = new FormData();
    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    $("#btnImport").attr("disabled", "disabled");
    startUpdatingProgressIndicator();
    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",

            success: function (data) {
                if (data.isvalid) {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                } else {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                }
                $('#btnImport').removeAttr("disabled");
                isUpload = true;
            }
        }
    );
}


function importLoaneeRemark(inputId) {
    isUpload = false;
    var input = document.getElementById(inputId);
    var url = document.getElementById("hidUploadUrl").value;
    var files = input.files;
    var formData = new FormData();
    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    $("#btnImport").attr("disabled", "disabled");
    startUpdatingProgressIndicator();
    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",

            success: function (data) {
                if (data.isvalid) {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                } else {
                    alert(data.message);
                    stopUpdatingProgressIndicator();
                }
                $('#btnImport').removeAttr("disabled");
                isUpload = true;
            }
        }
    );
}


//function importManual(inputId) {
//    isUpload = false;
//    var meterTypeId = document.getElementById("ddlMeterType").value;  //$('#ddlMeterType').val();
//    var input = document.getElementById(inputId);
//    var url = document.getElementById("hidUploadUrl").value;
//    var files = input.files;
//    var formData = new FormData();
//    for (var i = 0; i != files.length; i++) {
//        formData.append("files", files[i]);
//        formData.append("meterTypeId", meterTypeId);
//    }
//    startUpdatingProgressIndicator();
//    $.ajax(
//        {
//            url: url,
//            data: formData,
//            processData: false,
//            contentType: false,
//            type: "POST",
//            success: function (data) {

//                if (data.isvalid) {
//                    alert(data.message);
//                    stopUpdatingProgressIndicator();
//                } else {
//                    alert(data.message);
//                    stopUpdatingProgressIndicator();
//                }

//                isUpload = true;
//            }
//        }
//    );
//}

var intervalId;
function startUpdatingProgressIndicator() {
    var url = document.getElementById("hidProgressUrl").value;
    $("#progress").show();
    intervalId = setInterval(
        function () {
            // We use the POST requests here to avoid caching problems (we could use the GET requests and disable the cache instead)
            $.post(
                url,
                function (progress) {
                    //$("#bar").css({ width: progress + "%" });
                    $("#progress").css({ width: progress + "%" });
                    $("#label").html(progress + "%");
                }
            );
        },
        10
    );
}

function stopUpdatingProgressIndicator() {
    $("#progress").hide();
    clearInterval(intervalId);
}



