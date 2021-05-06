using Entities.Landing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LeadInformation
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public string LeadUser { get; set; }
        //public string LeadPassword { get; set; }
        public bool AcceptLanding { get; set; }
        public string CommentsNoAccept { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditedDate { get; set; }

        public string AcceptedLandingText { get; set; }
        public Guid? IdLandingCampaig { get; set; }
        [ForeignKey("IdLandingCampaig")]
        public LandingCampaign LandingCampaign { get; set; }


        public string CompanyName { get; set; }
        public string CampaignName { get; set; }
        public int CountryID { get; set; }
        public string MicrosoftConsultantEmail { get; set; }
        public string CompanySAMLiveUserName { get; set; }

        public int ? SAM360OrganisationId { get; set; }
        public int? SAM360UserId { get; set; }
        public string SAM360CompanyUser { get; set; }
        public string SAM360CompanyPassword { get; set; }


        public DateTime? FirstLoginDate { get; set; }
        public DateTime? AcceptLandingDate { get; set; }
        public DateTime? NoParticipateDate { get; set; }
        public DateTime? SubmittedDate { get; set; }

        public int? IndustryIndInsId { get; set; }

        public virtual NS_tblIndustryIndIns IndustryIndIns { get; set; }

    }
}
