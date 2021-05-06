using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedEntities;
using Microsoft.Practices.Unity;
using ISharedRepositories;
using SharedRepositories;
using SharedServices;

namespace WCFSharedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TranslatorServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TranslatorServices.svc or TranslatorServices.svc.cs at the Solution Explorer and start debugging.
    public class TranslatorServices : ITranslatorServices
    {
        private UnityContainer _unityContainer;

        public TranslatorServices()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterType<ITranslationRepository, TranslationRepository>();
        }

        public ProcessResult CreateTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();
            return _translatorServices.CreateTranslatorLanguage(pModel);
        }

        public ProcessResult UpdateTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();
            return _translatorServices.UpdateTranslatorLanguage(pModel);
        }

        public ProcessResult DeleteTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();
            return _translatorServices.DeleteTranslatorLanguage(pModel);
        }

        public IEnumerable<TranslatorFileModel> TranslatorFileModel(int pLanguageId)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.GetTranslationFileModel(pLanguageId);
        }

        public IEnumerable<NS_tblTranslatorLanguageDropDown> GetTranslatorLanguageDropDown()
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.GetTranslatorLanguageDropDown();
        }

        public ProcessResult CreateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.CreateTranslatorAdministator(pModel);
        }

        public ProcessResult UpdateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.UpdateTranslatorAdministrator(pModel);
        }

        public ProcessResult DeleteTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.DeleteTranslatorAdministrator(pModel);
        }

        public IEnumerable<NS_TblTranslatorAdministrator> GetTranslatorAdministrator()
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.GetTranslatorAdministrator();
        }

        public ProcessResult CreateTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.CreateTranslatorXLanguage(pModel);
        }

        public ProcessResult UpdateTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.UpdateTranslatorXLanguage(pModel);
        }

        public ProcessResult DeleteTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.DeleteTranslatorXLanguage(pModel);
        }

        public IEnumerable<NS_TblTranslationXLanguage> GetTranslationXLanguage()
        {
            SharedServices.TranslatorServices _translatorServices = _unityContainer.Resolve<SharedServices.TranslatorServices>();

            return _translatorServices.GetTranslatorXLanguage();
        }
    }
}
