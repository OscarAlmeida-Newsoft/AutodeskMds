using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LeadInformationDTO
    {
        public const string USERCODE_PREFIX = "CMPNUC";

        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public string LeadUser { get; set; }
        //public string LeadPassword { get; set; }
        public bool AcceptLanding { get; set; }
        public string CommentsNoAccept { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditedDate { get; set; }
        public string AcceptedLandingText { get; set; }
        public Guid? IdLandingCampaig { get; set; }

        public string CompanyName { get; set; }
        public string CampaignName { get; set; }
        public int CountryID { get; set; }
        public string MicrosoftConsultantEmail { get; set; }
        public string CompanySAMLiveUserName { get; set; }

        public int?  SAM360OrganisationId { get; set; }
        public int? SAM360UserId { get; set; }
        public string SAM360CompanyUser { get; set; }
        public string SAM360CompanyPassword { get; set; }

        //Data used in conection with SAM Live
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }

        public int? IndustryIndInsId { get; set; }

    }
}
