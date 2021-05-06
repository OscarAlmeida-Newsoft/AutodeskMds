using SharedEntities;
using System.Collections.Generic;

namespace ISharedRepositories
{
    public interface ITranslationRepository
    {
        IEnumerable<TranslatorFileModel> GetTranslationFileModel(int pLanguageId);

        ProcessResult CreateTranslatorLanguage(NS_tblTranslatorLanguage pModel);
        ProcessResult UpdateTranslatorLanguage(NS_tblTranslatorLanguage pModel);
        ProcessResult DeleteTranslatorLanguage(NS_tblTranslatorLanguage pModel);
        IEnumerable<NS_tblTranslatorLanguageDropDown> GetTranslatorLanguageDropDown();

        ProcessResult CreateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel);
        ProcessResult UpdateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel);
        ProcessResult DeleteTranslatorAdministrator(NS_TblTranslatorAdministrator pModel);
        IEnumerable<NS_TblTranslatorAdministrator> GetTranslatorAdministrator();

        ProcessResult CreateTranslatorXLanguage(NS_TblTranslationXLanguage pModel);
        ProcessResult UpdateTranslatorXLanguage(NS_TblTranslationXLanguage pModel);
        ProcessResult DeleteTranslatorXLanguage(NS_TblTranslationXLanguage pModel);
        IEnumerable<NS_TblTranslationXLanguage> GetTranslationXLanguage();


    }
}
