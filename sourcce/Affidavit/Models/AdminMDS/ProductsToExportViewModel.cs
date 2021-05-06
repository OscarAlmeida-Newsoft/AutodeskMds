using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class ProductsToExportViewModel
    {
        //public short ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public short? InstalledLicenses { get; set; }
        public string CompanyName { get; set; }
        public short TotalQuantityOfDesktops { get; set; }
        public short TotalQuantityPCWithOtherOS { get; set; }
        public short TotalQuantityOfPhysicalServers { get; set; }
        public short TotalQuantityOfVirtualServers { get; set; }
    }
}