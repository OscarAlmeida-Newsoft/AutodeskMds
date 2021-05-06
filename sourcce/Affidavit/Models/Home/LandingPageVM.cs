using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Home
{
    public class LandingPageVM
    {
        public Guid LandingId { get; set; }
        public string LandingText { get; set; }
        public string Tittle { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string MicrosoftConsultantEmail { get; set; }
        public string ConsultantName { get; set; }
        public string ConsultantPhoneNumber { get; set; }
        public string LastName { get; set; }
        public Guid LeadId { get; set; }
        public short? LanguageID { get; set; }
        public bool SurveyResponse { get; set; }
    }
}