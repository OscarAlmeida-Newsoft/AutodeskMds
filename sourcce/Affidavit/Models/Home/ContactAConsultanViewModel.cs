using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit
{
    public class ContactAConsultanViewModel
    {
        public string ConstultantEmail { get; set; }
        public string ConsultantName { get; set; }
        public string LeadEmail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public Guid LeadId { get; set; }
        public Guid CampaignId { get; set; }
        public string MessageText { get; set; }

    }
}