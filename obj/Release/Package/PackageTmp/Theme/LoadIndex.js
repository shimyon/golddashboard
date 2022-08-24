$(document).ready(function () {

    getData();
    setInterval(function () {
        getData();
    }, 5000)
})

function getData() {


    $.ajax({
        url: "/Dashboard/GetDashboardData",
        type: "get",
        dataType: "json",
        success: function (data) {
            debugger
            $("#indexPrise").html("");
            $("#indexDate").html("");
            $("#BankNiftyIndex").html("");
            $("#BankNiftyDate").html("");

            $("#indexPrise").html("Nifty Index :" + data.NiftyIndexClosePrice);
            $("#indexDate").html(data.NiftyTime);
            $("#BankNiftyIndex").html("Bank Nifty:" + data.BannkNiftyIndexClosePrice);
            $("#BankNiftyDate").html( data.BankNiftyTime);
        }
    });
}