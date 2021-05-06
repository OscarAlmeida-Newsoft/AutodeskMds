using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Landing
{
    public class LandingCampaignFilterModel
    {
        public string Campaing { get; set; }
        public string Country { get; set; }
        public bool ShortingByCampaing { get; set; }
        public string CreatedBy { get; set; }
        public bool ShortingByCreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool ShortingByCreatedDate { get; set; }
        public string EditedBy { get; set; }
        public string EditedDate { get; set; }
        public bool? Status { get; set; }
    }
}
