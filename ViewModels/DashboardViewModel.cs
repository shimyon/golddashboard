using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.ViewModels
{
    public class DashboardViewModel
    {

        public decimal NiftyIndexClosePrice { get; set; }
        public DateTime NiftyIndexTime { get; set; }
        
        public string NiftyTime { get; set; }
        
        public decimal BannkNiftyIndexClosePrice { get; set; }
        public DateTime BannkNiftyIndexTime { get; set; }

        public string BankNiftyTime { get; set; }

        public List<BankNifty> BankNifty { get; set; }
        public List<Trading> Trading { get; set; }

        
        public string NiftyINdexTimeString { 
        get {
                return NiftyIndexTime.ToString("MM/dd/yyyy");
            }
        }

        



        public string BankNiftyINdexTimeString
        {
            get
            {
                return BannkNiftyIndexTime.ToString("MM/dd/yyyy");
            }
        }

        



    }


    public class BankNifty
    {
        public string indexName { get; set; }
        public decimal? price { get; set; }
        public DateTime createby { get; set; }

        public string BankNiftyToString
        {
            get
            {
                return createby.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }

    }

    public class Trading
    {

        public string indexName { get; set; }
        public decimal? price { get; set; }
        public DateTime createby { get; set; }

        public string TradingToString
        {
            get
            {
                return createby.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }

    }
}