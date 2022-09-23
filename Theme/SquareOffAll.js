
function squareOffAllData() {
    $.ajax({
        url: "/Trading/StartStopProcess",
        type: "get",
        success: function (data) {
            debugger
            if (data.stopProcess == 0) {
                $('#StartStopProcess').text("Start Process");
                $('#StartStopProcess').addClass("badge-success");
            }
            if (data.stopProcess == 1) {
                $('#StartStopProcess').text("Stop Process");
                $('#StartStopProcess').removeClass("badge-success");
                $('#StartStopProcess').addClass("badge-danger");

            }
        }
    });
}


function squareOffAllDataClick() {
    var status = $('#StartStopProcess').text();
    $.ajax({
        url: "/Trading/QueareOffChange",
        type: "post",
        data: { "status": status },
        success: function (data) {
            debugger
            if (data.stopProcess == 0) {
                $('#StartStopProcess').text("Start Process");
                $('#StartStopProcess').addClass("badge-success");
            }
            if (data.stopProcess == 1) {
                $('#StartStopProcess').text("Stop Process");
                $('#StartStopProcess').removeClass("badge-success");
                $('#StartStopProcess').addClass("badge-danger");
 
            }
        }
    });
}


function GetUpComingHoliday() {
    $.ajax({
        url: "/Holiday/GetThisWeekHoliday",
        type: "get",
        dataType: "json",
        success: function (data) {

            $.each(data, function (key, value) {
                debugger
                $("#holidayCount").text(data.length);
                $("#NotifiactionDivQueary").after("<a href='#' class='dropdown-item'>" + value.holidayDateFormate + "<span class='float-right text-muted text-sm'>"+ value.nameOfEvent + "</span></a><div class='dropdown-divider'></div>");
            })
        }
    });
}

$(document).ready(function () {
    squareOffAllData();
    GetUpComingHoliday();
})