using DTOs;
using System.Collections.Generic;

namespace IRepositories
{
    public interface ITranslationXLanguageRepository
    {
        string TranslateByIdentifierText(string pIdentifierText, int pLanguageId);
        string TranslateByIdentifierId(int pIdentifierId, int pLanguageId);

        IEnumerable<TranslatorFileModelDTO> GetTranslationFileObject(int pLanguageId);
    
    }
}
