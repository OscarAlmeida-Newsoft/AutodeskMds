using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.ChooseAnAction
{
    public class ChooseAnActionVM
    {
        public bool IsMDSFinished { get; set; }
        public bool ShowMyplatformManualModal { get; set; }
        public double MDSPercentage { get; set; }
        public int? IdIndustry { get; set; }
    }
}