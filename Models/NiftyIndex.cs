using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.Models
{
    [Table("niftyindex")]
    public class NiftyIndex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 id { get; set; }
        public decimal close_price { get; set; }
        public DateTime index_time { get; set; }
        public DateTime? index_time_org { get; set; }
        public DateTime added_date { get; set; }

    }
}