using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldDashboard.ViewModels
{
    public class GiveReferralMoneyViewModel
    {
        public string userId { get; set; }
        public decimal Userpercent { get; set; }
        public int count { get; set; }
        public List<string> refferedUserId { get; set; }
    }
}