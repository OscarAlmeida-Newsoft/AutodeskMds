using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Landing
{
    public class ReplaceMultipleLandingDTO
    {
        public Guid landingFrom { get; set; }
        public IEnumerable<Guid> landingTo { get; set; }
    }
}
