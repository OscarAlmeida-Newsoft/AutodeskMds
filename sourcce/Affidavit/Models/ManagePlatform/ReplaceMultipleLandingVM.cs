using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.ManagePlatform
{
    public class ReplaceMultipleLandingVM
    {
        public Guid landingFrom { get; set; }
        public IEnumerable<Guid> landingTo { get; set; }
    }
}