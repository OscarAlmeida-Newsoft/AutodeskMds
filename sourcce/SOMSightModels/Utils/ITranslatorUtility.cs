using SOMSightModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightModels.Utils
{
    public interface ITranslatorUtility
    {
        void SaveJsonLanguageFile(int pLanguageId, IEnumerable<TranslatorFileModelDTO> pLanguageFileObject);

        string TranslateTextByIdentifier(string pTextIdentifier, int pLanguageId);
        string TranslateTextById(int pTextId, int pLanguageId);
    }
}
