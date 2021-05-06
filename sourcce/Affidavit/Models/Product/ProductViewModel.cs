using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Product
{
    public class ProductViewModel
    {
        public short ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductNameDisplay { get; set; }
        public string ProductVersion { get; set; }
        public byte ProductFamilyID { get; set; }
        public string ProductVersionGroup { get; set; }
        public bool OEMFlag { get; set; }
        public bool IsActive { get; set; }
        public short? OrderVersion { get; set; }
        public bool IsCal { get; set; }
        public bool IsCommercial { get; set; }
        public bool IsCorporate { get; set; }
        public bool IsSupported { get; set; }
        public short? DisplayOrder { get; set; }
    }
}