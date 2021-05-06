using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class AnwerCompanyByServerViewModel
    {
        public int CompanyID { get; set; }
        public short CampaignID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public short ProductID { get; set; }
        public int ResponseDataTypeID { get; set; }
        public string ProductName { get; set; }
        public int LicenseID { get; set; }
    }
}