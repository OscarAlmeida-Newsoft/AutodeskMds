using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.ManagePlatform
{
    public class SaveLandingVM
    {
        public Guid? LandingId { get; set; }
        public string LandingCampaign { get; set; }
        public string LandingText { get; set; }
        public int CountryId { get; set; }
        public Guid CreatedById { get; set; }
    }
}