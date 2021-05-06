using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
    public class LanguageController : BaseController
    {
        private ITranslatorService translatorService;

        public LanguageController(ITranslatorService pTranslatorService)
        {
            translatorService = pTranslatorService;
        }


        public void UpdateLanguage(int? pLanguageId)
        {
            if(pLanguageId.HasValue)
            {
                translatorService.SaveTranslationFileObject(pLanguageId.Value);
            }           
        }
    }
}