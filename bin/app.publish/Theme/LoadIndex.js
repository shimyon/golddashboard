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

            $("#LayoutindexPrise").html("");
            $("#LayoutindexDate").html("");
            $("#LayoutBankNiftyIndex").html("");
            $("#LayoutBankNiftyDate").html("");

            $("#LayoutindexPrise").html("Nifty Index : " + data.NiftyIndexClosePrice);
            $("#LayoutindexDate").html(data.NiftyTime);
            $("#LayoutBankNiftyIndex").html("Bank Nifty : " + data.BannkNiftyIndexClosePrice);
            $("#LayoutBankNiftyDate").html( data.BankNiftyTime);
        }
    });
}