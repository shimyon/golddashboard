using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.ViewModels
{
    public class IndexListViewModel
    {
        public string name { get; set; }
        public DateTime? expiryDate { get; set; }
        public double? indexValue { get; set; }
        public string type { get; set; }

    }
}