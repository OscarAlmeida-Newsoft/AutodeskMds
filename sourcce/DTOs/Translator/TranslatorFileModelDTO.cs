
using System.Collections.Generic;

namespace DTOs
{
    public class TranslatorTextModelDTO
    {
        public int Id { get; set; }
        public bool IsForDeveloperUse { get; set; }
        public bool ApplyLeadsTemplate { get; set; }
        public string TextIdentifier { get; set; }
        public string TranslatedText { get; set; }
    }

    public class TranslatorFileModelDTO
    {
        public IEnumerable<TranslatorTextModelDTO> LanguageFile { get; set; }
        public int LanguageId { get; set; }
    }
}
