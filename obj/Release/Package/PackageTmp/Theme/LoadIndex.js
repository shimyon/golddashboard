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

            $("#LayoutindexPrise").html("<b>Nifty 50 : </b>" + data.NiftyIndexClosePrice);
            if (data.NiftyIndexClosePrice > data.Nifty50PreviouseDatePrice) {
                $("#Nifty50Up").show();
                $("#Nifty50Down").hide();
                $("#Nifty50Balance").css("color", "#65A02F");
                $("#Nifty50Percent").css("color","#65A02F")

            } else if (data.NiftyIndexClosePrice < data.Nifty50PreviouseDatePrice)
            {
                $("#Nifty50Up").hide();
                $("#Nifty50Down").show();
                $("#Nifty50Balance").css("color", "red");
                $("#Nifty50Percent").css("color", "red")

            }
            var balance = (data.NiftyIndexClosePrice - data.Nifty50PreviouseDatePrice).toFixed(2);
            var percent = ((data.NiftyIndexClosePrice - data.Nifty50PreviouseDatePrice) / data.Nifty50PreviouseDatePrice * 100).toFixed(2) + "%"
            if (balance > 0) {
                $("#Nifty50Balance").text("+"+balance);
                $("#Nifty50Percent").text("(" + percent + ")");

            } else {
                $("#Nifty50Balance").text("-" + balance);
                $("#Nifty50Percent").text("(" + percent + ")");
            }


            var BankNiftybalance = (data.BannkNiftyIndexClosePrice - data.BankNiftyPreviouseDatePrice).toFixed(2);
            var BankNiftypercent = ((data.BannkNiftyIndexClosePrice - data.BankNiftyPreviouseDatePrice) / data.BankNiftyPreviouseDatePrice * 100).toFixed(2) + "%"

            if (BankNiftybalance > 0) {
                $("#BankNifty50Balance").text("+" + BankNiftybalance);
                $("#BankNifty50Percent").text("(" + BankNiftypercent + ")");

            } else {
                $("#BankNifty50Balance").text("-"+BankNiftybalance);
                $("#BankNifty50Percent").text("(" + BankNiftypercent + ")");
            }
           

            /*$("#LayoutindexDate").html(data.NiftyTime);*/
            $("#LayoutBankNiftyIndex").html("<b>Nifty Bank:</b> " + data.BannkNiftyIndexClosePrice);
            if (data.BannkNiftyIndexClosePrice > data.BankNiftyPreviouseDatePrice) {
                $("#BankNiftyUp").show();
                $("#BankNiftyDown").hide();
                $("#BankNifty50Balance").css("color", "#65A02F");
                $("#BankNifty50Percent").css("color", "#65A02F")
            } else if (data.BannkNiftyIndexClosePrice < data.BankNiftyPreviouseDatePrice) {
                $("#BankNiftyUp").hide();
                $("#BankNiftyDown").show();
                $("#BankNifty50Balance").css("color", "red");
                $("#BankNifty50Percent").css("color", "red")
               
            }
            /*$("#LayoutBankNiftyDate").html( data.BankNiftyTime);*/
        }
    });
}

