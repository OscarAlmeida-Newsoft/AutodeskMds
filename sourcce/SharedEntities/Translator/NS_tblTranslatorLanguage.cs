
using System;

namespace SharedEntities
{
    public class NS_tblTranslatorLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FlagPath { get; set; }

        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        
    }
}
