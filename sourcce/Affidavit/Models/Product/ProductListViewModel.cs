using Affidavit.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Product
{
    public class ProductListViewModel
    {
        public short ProductID { get; set; }

        [TranslatorDisplay("Old_LabelProductName")]
        public string ProductName { get; set; }

        [TranslatorDisplay("Old_LabelProductVersion")]
        public string ProductVersion { get; set; }

        [TranslatorDisplay("Old_LabelProductFamily")]
        public byte ProductFamilyID { get; set; }

        public string ProductVersionGroup { get; set; }

        public bool OEMFlag { get; set; }

        public bool IsActive { get; set; }

        public short? OrderVersion { get; set; }

        public string FamilyName { get; set; }

        public string NameDisplay { get; set; }

        public short? DisplayOrder { get; set; }

        public bool IsCal { get; set; }

        public bool IsCorporate { get; set; }

        public bool IsCommercial { get; set; }

        public bool IsSupported { get; set; }
    }
}