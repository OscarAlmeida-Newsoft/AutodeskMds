
namespace SharedEntities
{
    public class TranslatorFileModel
    {
        public int Id { get; set; }
        public bool IsForDeveloperUse { get; set; }
        public bool ApplyLeadsTemplate { get; set; }
        public string TextIdentifier { get; set; }
        public string TranslatedText { get; set; }
    }
}
