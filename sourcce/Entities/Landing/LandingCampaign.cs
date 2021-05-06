using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Landing
{
    public class LandingCampaign
    {
        public Guid Id { get; set; }
        public string Campaing { get; set; }
        public string LandingText { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? EditedById { get; set; }
        public virtual User EditedBy { get; set; }
        public DateTime EditedDate { get; set; }
        public bool Status { get; set; }
        public int CountryId { get; set; }
        public virtual NS_tblCountry Country { get; set; }

        public ICollection<LeadInformation> LeadsInfo { get; set; }
    }
}
