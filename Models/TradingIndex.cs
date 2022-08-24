using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.Models
{
        [Table("tradingindex")]
        public class tradingindex
        {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Int32 id { get; set; }
            public string name { get; set; }
            public string indexName { get; set; }

            public Int32? symbolToken { get; set; }

            public string type { get; set; }

            public Int32? price { get; set; }

            public DateTime? expiry { get; set; }
            public string strike { get; set; }

            public Double? lotsize { get; set; }

            public string instrumenttype { get; set; }
            public string exch_seg { get; set; }

            public Double? tick_size { get; set; }
            public Double? indexvalue { get; set; }
        }
    }
