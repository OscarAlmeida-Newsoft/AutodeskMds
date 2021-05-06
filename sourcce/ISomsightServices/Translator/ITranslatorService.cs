using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOMSightServices
{
    public interface ITranslatorService
    {
        void SaveTranslationFileObject(int pLanguageId);
    }
}
