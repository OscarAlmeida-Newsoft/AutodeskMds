using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class IndustryDTO
    {
        public short IndustryID { get; set; }
                
        public string IndustryName { get; set; }
                
        public string IndustrySpanishName { get; set; }

        //public virtual ICollection<NS_tblCompany> NS_tblCompany { get; set; }
    }
}
