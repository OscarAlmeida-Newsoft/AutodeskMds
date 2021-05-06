using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MDSAccountDTO
    {
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public string ConsultantContact { get; set; }
        public string ConsultantEmail { get; set; }
        public string AccountUserName { get; set; }
    }
}
