using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class ProductsViewModel
    {
        public string ProductName { get; set; }
        public short InstalledLicenses { get; set; }
        public string ProductFamily { get; set; }
        public string AdditionalComments { get; set; }
        public string ProductVersion { get; set; }
    }

    public class AdditionalCommentsViewModel
    {
        public string ProductFamilyName { get; set; }
        public string AdditionalComments { get; set; }
    }
}