using SharedEntities;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCFSharedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITranslatorServices" in both code and config file together.
    [ServiceContract]
    public interface ITranslatorServices
    {
        [OperationContract]
        IEnumerable<TranslatorFileModel> TranslatorFileModel(int pLanguageId);

        [OperationContract]
        ProcessResult CreateTranslatorLanguage(NS_tblTranslatorLanguage pModel);

        [OperationContract]
        ProcessResult UpdateTranslatorLanguage(NS_tblTranslatorLanguage pModel);

        [OperationContract]
        ProcessResult DeleteTranslatorLanguage(NS_tblTranslatorLanguage pModel);

        [OperationContract]
        IEnumerable<NS_tblTranslatorLanguageDropDown> GetTranslatorLanguageDropDown();


        [OperationContract]
        ProcessResult CreateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel);

        [OperationContract]
        ProcessResult UpdateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel);

        [OperationContract]
        ProcessResult DeleteTranslatorAdministrator(NS_TblTranslatorAdministrator pModel);

        [OperationContract]
        IEnumerable<NS_TblTranslatorAdministrator> GetTranslatorAdministrator();



        [OperationContract]
        ProcessResult CreateTranslatorXLanguage(NS_TblTranslationXLanguage pModel);

        [OperationContract]
        ProcessResult UpdateTranslatorXLanguage(NS_TblTranslationXLanguage pModel);

        [OperationContract]
        ProcessResult DeleteTranslatorXLanguage(NS_TblTranslationXLanguage pModel);

        [OperationContract]
        IEnumerable<NS_TblTranslationXLanguage> GetTranslationXLanguage();




    }
}
