using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoldDashboard.ViewModels
{
    public class AddRoleViewModel
    {

            [Required]
            public string RoleName { get; set; }
    }
}