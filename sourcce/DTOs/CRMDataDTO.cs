using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CRMDataDTO
    {
        public Guid LeadID { get; set; }
        public string Tittle { get; set; }
        public string AccountNumber { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string IndustryName { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CRMCampaignID { get; set; }
        public string CampaignName { get; set; }
        public string ConsultantName { get; set; }
        public string MicrosoftConsultantEmail { get; set; }
        public string MicrosoftConsultantPhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfPCs { get; set; }
        public string MobilePhone { get; set; }
    }
}
