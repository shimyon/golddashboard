using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldDashboard.ViewModels
{
    public class AddDataViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public decimal Deposite { get; set; }
        public decimal Balance { get; set; }
        public decimal PerDayProfit { get; set; }
        public decimal PerWeekProfit { get; set; }
        public decimal PerMonthProfit { get; set; }

    }
}