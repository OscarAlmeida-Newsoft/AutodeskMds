using Affidavit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class AdminIndustryInsightsGridVM
    {
        public IEnumerable<AdminIndustryInsightsVM> Details { get; set; }
        public NSPageSettings PageSettings { get; set; }
    }
}