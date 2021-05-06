using Affidavit.Helpers;
using DTOs.Landing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Product
{
    public class ManageProductViewModel
    {
        public List<ProductListViewModel> ProductList { get; set; }
        public NSPageSettings pageSettings { get; set; }
        public ProductFiltersViewModel filters { get; set; }
    }
}