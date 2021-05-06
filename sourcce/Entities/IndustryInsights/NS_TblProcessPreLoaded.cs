using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblProcessPreLoaded
    {
        public int Id { get; set; }
        public int IndustryId { get; set; }
        public int ActivityId { get; set; }
        public int TranslatorAdministratorNameId { get; set; }
        public int TranslatorAdministratorDescriptionId { get; set; }

        public virtual NS_tblIndustry Industry { get; set; }
        public virtual NS_TblActivity Activity { get; set; }
        
    }
}
