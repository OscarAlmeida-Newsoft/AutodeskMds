﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOMSight.Models.Account
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "Company name*")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Contact name*")]
        public string ContactName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }
        public string Industry { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password*")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Number of devices*")]
        public int NumberOfDevices { get; set; }
    }
}