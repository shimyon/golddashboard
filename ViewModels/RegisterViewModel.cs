﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoldDashboard.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime DepositDate { get; set; }
        public decimal Percent { get; set; }
        public bool isActive { get; set; }
        public string ReferralPerson { get; set; }
        public string GoldUserUserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

        public int GoldUserId { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }

    }
}