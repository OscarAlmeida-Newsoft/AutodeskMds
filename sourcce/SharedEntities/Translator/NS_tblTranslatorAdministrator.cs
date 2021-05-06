
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public class NS_TblTranslatorAdministrator
    {
        public int Id { get; set; }

        [Index("IDX_UNIQUE_TXTIDENTIFIER",IsUnique = true)]
        [StringLength(50)]
        public string TextIdentifier { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(2000)]
        public string DefaultTextValue { get; set; }

        public bool ApplyLeadsTemplate { get; set; }
        public bool IsForDeveloperUse { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        

    }
}
