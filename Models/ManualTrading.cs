﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.Models
{
    [Table("manualtrading")]
    public class ManualTrading
    {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Int32 id { get; set; }
            public string clientId { get; set; }
            public string tradingSymbol { get; set; }
            public string optionType { get; set; }
            public string transactionType { get; set; }
            public decimal price { get; set; }
            public DateTime addedDate { get; set; }
            public int lotsize { get; set; }
            public int IsExecute { get; set; }

    }
}