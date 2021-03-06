using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public class NS_TblIndustrySOMSight
    {
        public int Id { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }

        public virtual NS_TblTranslatorAdministrator TranslatorAdministrator { get; set; }
    }
}
