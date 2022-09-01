using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.ViewModels
{
    public class UserListModel
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string text
        {
            get
            {
                return firstName + " "+ lastName + " - " + id;
            }
        }

        
    }
}