using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Models.Account
{
    public class RegisterEmailVM
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        
        public string CompanyName { get; set; }

        public string IntroduceText { get; set; }
        public string UrlSrcLink { get; set; }
        public string FinalText { get; set; }
    }
}