using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.ViewModels
{
    public class AddUserViewModel
    {

        public string userId { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Int64? Phone { get; set; }
        public string angel_user { get; set; }
        public string angel_password { get; set; }
        public string active { get; set; }
    }
}