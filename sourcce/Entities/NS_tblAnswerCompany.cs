using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class NS_tblAnswerCompany
    {
        public NS_tblAnswerCompany()
        {
            //this.Questions = new HashSet<NS_tblQuestion>();
        }

        [Key, Column(Order = 0)]
        public int CompanyID { get; set; }

        [Key, Column(Order = 1)]
        public int QuestionID { get; set; }

        [Key, Column(Order = 2)]
        public short CampaignID { get; set; }

        [Key, Column(Order = 3)]
        public int LicenseID { get; set; }

        [Key, Column(Order = 4)]
        [ForeignKey("Product")]
        public short ProductID { get; set; }
        public virtual NS_tblProduct Product { get; set; }

        [Key, Column(Order = 5)]
        public bool IsVirtual { get; set; }

        public short? IsInside { get; set; }

        public bool IsNew { get; set; }

        public bool IsRealProductId { get; set; }

        //[Required]
        [StringLength(500)]
        public string Answer { get; set; }


        public bool IsVLS { get; set; }


        public bool IsOEM { get; set; }

        
        public bool IsWEB { get; set; }

        public bool IsFPP { get; set; }

        public bool IsInstalledLicenses { get; set; }

        public virtual NS_tblQuestion Question { get; set; }
        
    }
}
