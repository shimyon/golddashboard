using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldDashboard.ViewModels
{
    public class AddUserToRoleViewModel
    {
        public string RoleName { get; set; }
        public List<string> UserList { get; set; }
        public List<string> RemoveUserFromRoleList { get; set; }
    }
}