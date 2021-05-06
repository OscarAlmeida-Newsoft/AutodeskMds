
using System;
using System.ComponentModel.DataAnnotations;

namespace SharedEntities
{
    public class NS_TblTranslationXLanguage
    {
        public int Id { get; set; }
        public int TranslationAdministrationId { get; set; }
        public int LanguageId { get; set; }

        [StringLength(2000)]
        public string TranslationText { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        
    }
}
