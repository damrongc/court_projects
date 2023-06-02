$(function () {
    $('.dateonly').datetimepicker({
        timepicker: false,
        format: 'd-m-Y',
        closeOnDateSelect: true
    });
    $('#form-modal').on('shown.bs.modal', function () {
        $(".dateonly").datetimepicker({
            timepicker: false,
            format: 'd-m-Y',
            closeOnDateSelect: true
        });
    });
    $("#ddlCourts").select2();
    $("#ddlLawyers").select2();
});

addAppointmentToTable = () => {
    const appointment_date = $("#txtAppointmentDate").val();
    const remark = $("#txtRemark").val();
    var valid = true;
    var message = "";
    if (appointment_date == "" || appointment_date == undefined) {
        message += "กรุณาระบุ วันนัดหมาย\n";
        valid = false;
    }
    if (remark == "" || remark == undefined) {
        message += "กรุณาระบุ หมายเหตุ\n";
        valid = false;
    }
    if (valid) {
        var markup = "<tr>"
            + "<td class='appointment_date'>" + appointment_date + "</td>"
            + "<td class='remark'> " + remark + "</td>"
            + "<td><button class='btn btn-sm btn-danger text-white btnDelete' data-id='0'><i class='fa fa-trash'></i></button></td>"
            + "</tr>";
        $("#tblAppointment tbody").append(markup);
        $('#form-modal').modal('hide');
    } else {
        swal({
            title: "Error",
            text: message,
            icon: "error"
        });
    }
}

$('#tblAppointment').on('click', '.btnDelete', function () {
    $(this).closest('tr').remove();
});

saveJusticeCase = () => {
    const url = $("#hdAppointmentConfigUrl").val();
    var cusId = $("#CusId").val();
    var blackCaseNo = $("#BlackCaseNo").val();
    var caseDate = $("#txtCaseDate").val();
    var approvalDate = $("#txtApprovalDate").val();
    //var judgmentDate = $("#txtJudgmentDate").val();
    var assetAmount = $("#AssetAmount").val();
    var feeCase = $("#FeeCase").val();
    var submissionDate = $("#txtSubmissionDate").val();
    var submissionResult = $("#SubmissionResult").val();
    var commitDate = $("#txtCommitDate").val();
    var postingDate = $("#txtPostingDate").val();
    var courtId = $("#ddlCourts").val();
    var caseResultId = parseInt($("#ddlCaseResults").val());
    var lawyers = $("#ddlLawyers").val();

    console.log(lawyers);

    var valid = true;
    var message = "";
    if (blackCaseNo == "" || blackCaseNo == undefined) {
        message += "กรุณาระบุ หมายเลขคดี\n";
        valid = false;
    }
    if (lawyers.length==0) {
        message += "กรุณาระบุ เลือกทนาย\n";
        valid = false;
    }
    const appointmentList = [];
    const tableRow = $('#tblAppointment tbody tr');
    tableRow.each(function () {

        const appointmentDate = $(this).find(".appointment_date").html();
        const remark = $(this).find(".remark").html();

        let appointment = {};
        appointment.AppointmentDate = appointmentDate;
        appointment.Remark = remark;
        appointmentList.push(appointment);
    });
    if (caseResultId == 1) {
        if (appointmentList.length == 0) {
            message += "กรุณาระบุ ระบุวันนัดหมาย เนื่องจากผลคดี=เลื่อน\n";
            valid = false;
        }
    }
    var justiceLawyers = [];
    for (var i = 0; i < lawyers.length; i++) {
        let lawyer = {};
        lawyer.LawyerId = lawyers[i];
        justiceLawyers.push(lawyer);
    }
    let justiceCase = {};
    justiceCase.CusId = cusId
    justiceCase.BlackCaseNo = blackCaseNo
    justiceCase.CaseDate = caseDate
    justiceCase.ApprovalDate = approvalDate
  /*  justiceCase.JudgmentDate = judgmentDate*/
    justiceCase.AssetAmount = assetAmount
    justiceCase.FeeCase = feeCase
    justiceCase.SubmissionDate = submissionDate
    justiceCase.SubmissionResult = submissionResult
    justiceCase.CommitDate = commitDate
    justiceCase.PostingDate = postingDate
    justiceCase.JusticeAppointments = appointmentList;
    justiceCase.CourtId = courtId;
    justiceCase.CaseResultId = caseResultId;
    justiceCase.JusticeLawyers = justiceLawyers;
    if (valid) {
        try {
            $("#loaderbody").show();
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(justiceCase),
                contentType: "application/json; charset=utf-8",
                success: function (res) {
                    if (res.isValid) {
                        window.location.replace(res.returnUrl);
                    };
                }
            })
        } catch (e) {
            console.log(e);
        } finally {
            $("#loaderbody").hide();
        }
    } else {
        swal({
            title: "Error",
            text: message,
            icon: "error"
        });
    }
    return false;
}