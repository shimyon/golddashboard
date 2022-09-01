using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.Models
{
    [Table("settings")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int squareOff { get; set; }
        public int stopProcess { get; set; }
        public int squreOffBankNifty { get; set; }
    }
}