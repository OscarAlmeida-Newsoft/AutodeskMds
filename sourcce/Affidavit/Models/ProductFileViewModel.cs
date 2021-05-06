using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class ProductFileViewModel
    {
            public int CompanyID { get; set; }

            public short ProductID { get; set; }

            public short CampaignID { get; set; }

            public short FileNumber { get; set; }

            public string AlternativeName { get; set; }

            public string Extension { get; set; }
    }
}