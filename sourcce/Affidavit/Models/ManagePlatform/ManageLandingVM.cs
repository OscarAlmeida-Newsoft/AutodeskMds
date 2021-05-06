using Affidavit.Helpers;
using DTOs.Landing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.ManagePlatform
{
    public class ManageLandingVM
    {
        public IEnumerable<LandingCampaignDTO> landingCampaignPagination { get; set; }
        public NSPageSettings pageSettings { get; set; }

        public LandingCampaignFilterModel filters { get; set; }
    }
}