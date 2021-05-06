using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AnswerCompanyDTO
    {
        public int CompanyID { get; set; }
        
        public int QuestionID { get; set; }
        
        public short CampaignID { get; set; }
        
        public int LicenseID { get; set; }

        public short ProductID { get; set; }

        public string Answer { get; set; }


        //Nuevo Modelo
        public bool IsVirtual { get; set; }
        public short? IsInside { get; set; }
        public bool IsNew { get; set; }

        public bool IsRealProductId { get; set; }



        public bool IsVLS { get; set; }
        public bool IsOEM { get; set; }
        public bool IsWEB { get; set; }
        public bool IsFPP { get; set; }
        public bool IsInstalledLicenses { get; set; }
        public string FamilyComplete { get; set; }
        public string ProductName { get; set; }
    }
}
