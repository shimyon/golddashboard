using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.Models
{

    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 id { get; set; }
        public string userId { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Int64? Phone { get; set; }
        public string angel_user { get; set; }
        public string angel_password { get; set; }
        public int? balance_amount { get; set; }
        public string apikey { get; set; }
        public string active { get; set; }
        public DateTime? addDate { get; set; }
      
    }
}