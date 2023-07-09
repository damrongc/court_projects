var tbl_remaintask;
var tbl_todo;
var colors = ['#FF530D', '#E82C0C', '#FF0000', '#E80C7A', '#E80C7A'];
Highcharts.setOptions({
    chart: {
        style: {
            fontFamily: 'Poppins'
        }
    },
    lang: {
        thousandsSep: ','
    },
    credits: {
        enabled: false
    }
});

$(function () {
    getRemianTask();
    getEmployeeTodo();
    getLoaneeSummary();
    getPaymentSummary();
});

async function getRemianTask() {
    console.log(new Date());
    var url = $("#hdGetRemainTaskPaging").val();
    //var employeeCode = $("txtEmployeeCode").val();
    tbl_remaintask = $('#tbl_remaintask').DataTable({
        "destroy": true,
        "processing": false,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                //d.employeeCode = employeeCode;
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
            searchPlaceholder: "ค้นหา..."
        },
        "columns": [

            {
                data: "taskDatetime", name: "taskDatetime", class: "text-nowrap", render: function (data, type, row) {
                    return formatDatetime(data);
                }
            },
            { data: "taskDetail", name: "taskDetail", class: "text-nowrap", },
            { data: "assignFromName", name: "assignFromName", class: "text-nowrap", },
        ]
    });


    $(tbl_remaintask).each(function () {
        var datatable = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'ค้นหา');
        search_input.removeClass('form-control-sm');
        // LENGTH - Inline-Form control
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        length_sel.removeClass('form-control-sm');
    });

    //new $.fn.dataTable.FixedColumns(table, {
    //    leftColumns: 5
    //});

    setTimeout(getRemianTask, 60000);
}


async function getEmployeeTodo() {

    var url = $("#hdGetEmployeeTodoPaging").val();
    tbl_todo = $('#tbl_todo').DataTable({
        "destroy": true,
        "processing": false,
        "serverSide": true,
        "ajax": {
            "url": url,
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                //d.employeeCode = employeeCode;
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
            searchPlaceholder: "ค้นหา..."
        },
        "columns": [

            {
                data: "appointmentDate", name: "appointmentDate", class: "text-nowrap", render: function (data, type, row) {
                    return formatDate(data);
                }
            },
            { data: "remark", name: "remark", class: "text-nowrap", },
            { data: "name", name: "name", class: "text-nowrap", },
            { data: "contractNo", name: "contractNo", class: "text-nowrap", },
            { data: "followContractNo", name: "followContractNo", class: "text-nowrap", },
            {
                data: "amount", name: "amount", class: "text-right", render: function (data, type, row) {
                    return formatNumber(data);
                }
            },

        ]
    });


    $(tbl_todo).each(function () {
        var datatable = $(this);
        // SEARCH - Add the placeholder for Search and Turn this into in-line form control
        var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', 'ค้นหา');
        search_input.removeClass('form-control-sm');
        // LENGTH - Inline-Form control
        var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
        length_sel.removeClass('form-control-sm');
    });

    //new $.fn.dataTable.FixedColumns(tbl_todo, {
    //    leftColumns: 5
    //});

    setTimeout(getEmployeeTodo, 60000);
}

async function getLoaneeSummary() {
    var url = $("#hdGetLoaneeSummary").val();
    $.ajax({
        type: "POST",
        url: url,
        /*        data: JSON.stringify(request),*/
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                console.log(res.data);
                createCharts("chart_loanee_count", res.data);

            } else {
                alert(res.message);
            }
        },

    })
}

async function getPaymentSummary() {
    var url = $("#hdGetPaymentSummary").val();
    $.ajax({
        type: "POST",
        url: url,
        /*        data: JSON.stringify(request),*/
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.isValid) {
                console.log(res.data);
                createToalChart("chart_loanee_total", res.data);

            } else {
                alert(res.message);
            }
        },

    })
}
function displayWithComma(data) {
    var value = parseFloat(data).toFixed(2).toString().split(".");
    var numValue = value[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (value[1] ? "." + value[1] : "");
    return numValue;
}

function createCharts(chartID, result) {
    Highcharts.chart({
        chart: {
            renderTo: chartID,
            zoomType: 'x'
        },
        title: {
            text: result.title
        },
        subtitle: {
            text: result.subTitle
        },
        xAxis: [{
            categories: result.categories,
            crosshair: true
        }],
        yAxis: [{ // Primary yAxis
            labels: {
                format: '{value:,.0f} คน',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            },
            title: {

                text: 'จำนวนลูกหนี้',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            }
        }, { // Secondary yAxis
            title: {
                text: 'ยอดหนี้',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            labels: {

                format: '{value:,.0f} บาท',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            opposite: true
        }],
        tooltip: {

            shared: true,
            crosshairs: false,
            pointFormat: '{point.y:,.2f}',
            formatter() {
                var output1 = `<span style=font-size:10px>${this.x}</span><br/>`
                this.points.forEach(point => {
                    output1 += `<span style="color:${point.color}">●</span> ${point.series.name}: <b>${Highcharts.numberFormat(point.y, 2)}</b><br/>`
                })
                return output1
            }
        },
        plotOptions: {
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:,.2f}'
                }
            }
        },

        series: [{
            name: 'ยอดหนี้',
            type: 'column',
            yAxis: 1,
            data: result.totalValues

        }, {
            name: 'จำนวนลูกหนี้',
            type: 'column',
            data: result.countValues
        }]
    });
}

function createToalChart(chartID, result) {
    Highcharts.chart(chartID, {
        chart: {
            type: 'column'
        },
        title: {
            text: result.title,
        },
        subtitle: {
            text: result.subTitle,
        },
        xAxis: [{
            categories: result.categories,
        }],
        yAxis: [{
            title: {
                text: 'ยอดหนี้',
            },
            labels: {

                format: '{value:,.0f} บาท',

            },
        }],
        plotOptions: {
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:,.2f}'
                }
            }
        },
        series: result.series
    });

}