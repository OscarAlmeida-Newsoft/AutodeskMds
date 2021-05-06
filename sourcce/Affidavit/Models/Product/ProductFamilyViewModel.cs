using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Product
{
    public class ProductFamilyViewModel
    {
        public short ProductFamilyID { get; set; }
        public string ProductFamilyName { get; set; }
        public string ProductFamilyImage { get; set; }
        public string ProductFamilyImageLarge { get; set; }
        public int ProductFamilyImageLargeWidth { get; set; }
        public int ProductFamilyImageLargeHeight { get; set; }
    }
}