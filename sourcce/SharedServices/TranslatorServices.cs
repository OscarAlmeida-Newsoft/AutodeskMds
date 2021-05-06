using ISharedRepositories;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedServices
{
    public class TranslatorServices
    {
        readonly ITranslationRepository _translationRepository;

        public TranslatorServices(ITranslationRepository pTranslationRepository)
        {
            _translationRepository = pTranslationRepository;
        }

        public IEnumerable<TranslatorFileModel> GetTranslationFileModel(int pLanguageId)
        {
            return _translationRepository.GetTranslationFileModel(pLanguageId);
        }

        public ProcessResult CreateTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            return _translationRepository.CreateTranslatorLanguage(pModel);
        }

        public ProcessResult UpdateTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            return _translationRepository.UpdateTranslatorLanguage(pModel);
        }

        public ProcessResult DeleteTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            return _translationRepository.DeleteTranslatorLanguage(pModel);
        }

        public IEnumerable<NS_tblTranslatorLanguageDropDown> GetTranslatorLanguageDropDown()
        {
            return _translationRepository.GetTranslatorLanguageDropDown();
        }


        public ProcessResult CreateTranslatorAdministator(NS_TblTranslatorAdministrator pModel)
        {
            return _translationRepository.CreateTranslatorAdministrator(pModel);
        }

        public ProcessResult UpdateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            return _translationRepository.UpdateTranslatorAdministrator(pModel);
        }

        public ProcessResult DeleteTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            return _translationRepository.DeleteTranslatorAdministrator(pModel);
        }

        public IEnumerable<NS_TblTranslatorAdministrator> GetTranslatorAdministrator()
        {
            return _translationRepository.GetTranslatorAdministrator();
        }

        

        public ProcessResult CreateTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            return _translationRepository.CreateTranslatorXLanguage(pModel);
        }

        public ProcessResult UpdateTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            return _translationRepository.UpdateTranslatorXLanguage(pModel);
        }

        public ProcessResult DeleteTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            return _translationRepository.DeleteTranslatorXLanguage(pModel);
        }

        public IEnumerable<NS_TblTranslationXLanguage> GetTranslatorXLanguage()
        {
            return _translationRepository.GetTranslationXLanguage();
        }
    }
}
