using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DTOs;

namespace Repositories
{
    public class TranslationXLanguageRepository : Repository<NS_TblTranslationXLanguage>, ITranslationXLanguageRepository
    {

        private AffidavitContext dbContext;
        public TranslationXLanguageRepository(AffidavitContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<TranslatorFileModelDTO> GetTranslationFileObject(int pLanguageId)
        {

            //First load the tranlations for the language
            //IEnumerable<NS_TblTranslationXLanguage> languageTranslations = dbContext.NS_TranslatorXLanguage.Where(x => x.LanguageId == pLanguageId);
            IEnumerable<TranslatorFileModelDTO> translationFile = null;
            //Get translations for the selected language
            //IEnumerable<TranslatorFileModelDTO> translationFile = dbContext.NS_TranslatorAdministrator
            //    .GroupJoin(languageTranslations   
            //        , TA => TA.Id
            //        , TXL => TXL.TranslationAdministrationId
            //        , (TA, TXL) => new { TA, TXL = TXL.FirstOrDefault() })
            //        .Select(o => new TranslatorFileModelDTO
            //        {
            //            Id = o.TA.Id,
            //            TextIdentifier = o.TA.TextIdentifier,
            //            ApplyLeadsTemplate = o.TA.ApplyLeadsTemplate,
            //            IsForDeveloperUse = o.TA.IsForDeveloperUse,
            //            TranslatedText = o.TXL.TranslationText ?? o.TA.DefaultTextValue

            //        });

            return translationFile;
        }

        public string TranslateByIdentifierId(int pIdentifierId, int pLanguageId)
        {
            //var translation = dbContext.NS_TranslatorXLanguage

            //    .Join(dbContext.NS_TranslatorAdministrator
            //    , TXL => TXL.TranslationAdministrationId
            //    , TA => TA.Id,
            //    (TXL, TA) => new { TXL, TA })
            //    .Where(y => y.TXL.LanguageId == pLanguageId && y.TA.Id == pIdentifierId)
            //    .Select(o => new { Translator = o.TXL.TranslationText ?? string.Empty })
            //    ;


            //return translation?.First().Translator ?? pIdentifierId.ToString();
            return string.Empty;
        }

        public string TranslateByIdentifierText(string pIdentifierText, int pLanguageId)
        {
            //var translation = dbContext.NS_TranslatorXLanguage

            //    .Join(dbContext.NS_TranslatorAdministrator
            //    , TXL => TXL.TranslationAdministrationId
            //    , TA => TA.Id,
            //    (TXL, TA) => new { TXL, TA })
            //    .Where(y => y.TXL.LanguageId == pLanguageId && y.TA.TextIdentifier.Equals(pIdentifierText))
            //    .Select(o => new { Translator = o.TXL.TranslationText ?? string.Empty })
            //    ;


            //return translation?.First().Translator ?? pIdentifierText;
            return string.Empty;
        }



    }
}
