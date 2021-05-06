using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Product
{
    public class ProductFiltersViewModel
    {
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string ProductFamily { get; set; }
        public string ProductVersionGroup { get; set; }
        public string Status { get; set; }
    }
}