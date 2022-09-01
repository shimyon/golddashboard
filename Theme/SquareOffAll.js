function squareOffAllData() {
    $.ajax({
        url: "/Trading/QueareOff",
        type: "get",
        success: function (data) {
            debugger
            if (data.stopProcess == 0) {
                $('#StartStopProcess').text("Start Process");
            }
            if (data.stopProcess == 1) {
                $('#StartStopProcess').text("Stop Process");
            }
        }
    });
}

$(document).ready(function () {
    squareOffAllData();
})