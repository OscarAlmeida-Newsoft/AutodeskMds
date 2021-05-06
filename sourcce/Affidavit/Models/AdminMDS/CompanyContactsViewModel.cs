using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class CompanyContactsViewModel
    {
        public int CompanyID { get; set; }

        public short CampaignID { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "Email Address")]
        public string CompanyEmail { get; set; }

        [Display(Name = "Phone Number")]
        public string CompanyPhone { get; set; }
    }
}