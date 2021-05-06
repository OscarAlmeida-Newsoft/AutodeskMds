using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailTask
{
    public class CorreosAEnviar
    {
        public int Id { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string SubjectEmail { get; set; }
        public string BodyEmail { get; set; }
    }
}
