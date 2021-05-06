using Affidavit.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Models.Product
{
    public enum IsOEM
    {
        False = 0,
        True = 1
    }

    public class CreateUpdateProductViewModel
    {
        public short ProductID { get; set; }

        [Required]
        [TranslatorDisplay("Old_LabelProductName")]
        public string ProductName { get; set; }

        [Required]
        [TranslatorDisplay("Old_LabelProductVersion")]
        public string ProductVersion { get; set; }

        [Required]
        [TranslatorDisplay("Old_LabelProductFamily")]
        public short ProductFamilyID { get; set; }
        public SelectList ProductFamilyList { get; set; }

        [Required]
        [Display(Name = "Product Version Group")]
        public string ProductVersionGroup { get; set; }

        //public bool OEMFlag { get; set; }
        [Required]
        [Display(Name = "Order Version")]
        public short? OrderVersion { get; set; }

        [Display(Name = "Is OEM Licence")]
        public IsOEM OEMFlag { get; set; }

        [Display(Name = "Is Active")]
        public IsOEM IsActive { get; set; }

        [Required]
        [Display(Name = "Name to Display")]
        public string NameDisplay { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public short? DisplayOrder { get; set; }

        [Display(Name = "Is CAL?")]
        public IsOEM IsCal { get; set; }

        [Display(Name = "Is corporate?")]
        public IsOEM IsCorporate { get; set; }

        [Display(Name = "Is commercial?")]
        public IsOEM IsCommercial { get; set; }

        [Display(Name = "Is supported?")]
        public IsOEM IsSupported { get; set; }
    }
}