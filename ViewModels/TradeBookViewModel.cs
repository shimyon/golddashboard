using AngelOneAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.ViewModels
{
    public class TradeBookViewModel
    {
        public int id { get; set; }
        public long orderId { get; set; }
        public string clientId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string tradingSymbol { get; set; }
        public int buyQty { get; set; }
        public int sellQty { get; set; }
        public int Balance { get; set; }
        public int? currentPrice { get; set; }
        public int? stopLoss { get; set; }
        public int? buyPrice { get; set; }
        public string TransationType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}