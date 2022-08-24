using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.Models
{
    [Table("tradebook")]
    public class TradeBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 id { get; set; }
        public Int64 orderId { get; set; }
        public string clientId { get; set; }
        public string tradingSymbol { get; set; }
        public string optionType { get; set; }
        public string transactionType { get; set; }
        public double price { get; set; }
        public Int32 quantity { get; set; }
        public DateTime addedDate { get; set; }
    }
}