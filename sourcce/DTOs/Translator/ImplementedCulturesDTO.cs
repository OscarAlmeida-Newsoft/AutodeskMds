using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ImplementedCulturesDTO
    {
        public ImplementedCulturesDetailDTO Culture { get; set; }
    }

    public class ImplementedCulturesDetailDTO
    {
        public string CultureName { get; set; }
        public string CultureCode { get; set; }
        public int CultureIdentifier { get; set; }

    }
}
