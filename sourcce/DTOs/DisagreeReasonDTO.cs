using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DisagreeReasonDTO
    {
        public Guid LeadId { get; set; }
        public string DisagreeReason { get; set; }
    }
}
