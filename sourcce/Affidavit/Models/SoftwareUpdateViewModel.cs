using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using DTOs;

namespace Affidavit.Models
{
    public class SoftwareUpdateViewModel
    {
        public GeneralInfoViewModel GeneralInformation { get; set; }
        public FamilyViewModel Families { get; set; }
        public AdditionalInfoViewModel AdditionalInformation { get; set; }
        public CompanyViewModel DatosBasicos { get; set; }
        public short CampaignID { get; set; }
        public int CompanyId { get; set; }
        public string LeadID { get; set; }
        public bool IsFinalVersion { get; set; }
        public int TabNumber { get; set; }
        //public IEnumerable<productosxVersionViewModel> productos { get; set; }
    }
}