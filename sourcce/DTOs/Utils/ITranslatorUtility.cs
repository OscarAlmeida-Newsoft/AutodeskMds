using System.Collections.Generic;

namespace DTOs.Utils
{
    public interface ITranslatorUtility
    {

        void SaveJsonLanguageFile(int pLanguageId, IEnumerable<TranslatorTextModelDTO> pLanguageFileObject);
        int GetCultureIdentifier(string pCultureCode);
        IEnumerable<ImplementedCulturesDTO> GetImplementedCultures();

        string TranslateTextByIdentifier(string pTextIdentifier, int pLanguageId);
        string TranslateTextById(int pTextId, int pLanguageId);
        void UpdateFileModel();

    }
}
