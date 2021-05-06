using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Models.Account
{
    public class ResetPasswordEmailVM
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }

        public string TitleHeader { get; set; }

        public string IntroduceText { get; set; }
     
      
    }
}