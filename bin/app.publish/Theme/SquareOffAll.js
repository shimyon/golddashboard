function squareOffAllData() {
    $.ajax({
        url: "/Trading/QueareOffAllRecord",
        type: "get",
        success: function (data) {
            debugger
            $('#squareOffAllData').removeAttr('href');
            $('#squareOffAllData').css("background-color", "grey");
            //if (data.squareOff == 0) {
            //    $('#QuareOff').prop('disabled', true);
            //} else
            //{
            //    $('#QuareOff').prop('disabled', false);
            //}

        }
    });
}