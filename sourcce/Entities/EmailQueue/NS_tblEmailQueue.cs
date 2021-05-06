using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.NS_tblEmailQueue
{
    public class NS_tblEmailQueue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string ToEmail { get; set; }

        [Required]
        [StringLength(60)]
        public string FromEmail { get; set; }

        [Required]
        [StringLength(120)]
        public string SubjectEmail { get; set; }

        [Required]
        [StringLength(1000)]
        public string BodyEmail { get; set; }
    }
}
