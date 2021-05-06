using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CompanyContactsDTO
    {
        public int CompanyID { get; set; }
        
        public short CampaignID { get; set; }

        [Display(Name = "LabelNombreContacto", ResourceType = typeof(Resources.Resources))]
        public string ContactName { get; set; }

        [Display(Name = "LabelEmail", ResourceType = typeof(Resources.Resources))]
        public string CompanyEmail { get; set; }

        [Display(Name = "LabelTelefono", ResourceType = typeof(Resources.Resources))]
        public string CompanyPhone { get; set; }
    }
}
