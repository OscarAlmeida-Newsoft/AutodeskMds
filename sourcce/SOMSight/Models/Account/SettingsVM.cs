using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOMSight.Models.Account
{
    public class SettingsVM
    {

        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string AssignedConsultant { get; set; }
        public string ConsultantEmail { get; set; }
        public DateTime? SubscriptionDate { get; set; }


    }
}