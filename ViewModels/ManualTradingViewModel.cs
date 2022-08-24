using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.ViewModels
{
    public class ManualTradingViewModel
    {
        public string IndexName { get; set; }
        public decimal Price { get; set; }
        public int LotSize { get; set; }
        public string TrasactionType { get; set; }
    }
}