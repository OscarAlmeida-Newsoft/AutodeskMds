using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.EmailQueueDTO
{
    public class EmailQueueDTO
    {
        public int Id { get; set; }

        public string ToEmail { get; set; }
        
        public string FromEmail { get; set; }
        
        public string SubjectEmail { get; set; }
        
        public string BodyEmail { get; set; }
    }
}
