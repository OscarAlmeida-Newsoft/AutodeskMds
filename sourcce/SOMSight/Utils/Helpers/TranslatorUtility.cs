using ISOMSightServices;
using Newtonsoft.Json;
using SOMSightModels.DTOs;
using SOMSightModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SOMSight.Utils
{
    public class TranslatorUtility : ITranslatorUtility
    {
        private IEnumerable<TranslatorFileModelDTO> _languageFile { get; set; }

        public TranslatorUtility(ISessionState _sessionState)
        {
            int _langId = 1;
            var _lang = _sessionState.Get(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY);
            if (_lang != null)
            {
                _langId = (int)_lang;
            }

            if (_languageFile == null)
            {
                _languageFile = GetFileModel(_langId);
            }
        }

        public void SaveJsonLanguageFile(int pLanguageId, IEnumerable<TranslatorFileModelDTO> pLanguageFileObject)
        {

            string langageFileString = JsonConvert.SerializeObject(pLanguageFileObject);

            string completePath = string.Format("{0}_Resource_{1}.json", HttpContext.Current.Server.MapPath("~/App_Data/"), pLanguageId);
            try
            {
                System.IO.File.WriteAllText(completePath, langageFileString, UTF8Encoding.UTF8);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string TranslateTextById(int pTextId, int pLanguageId)
        {
            string _translatedText = string.Empty;

            if (_languageFile != null)
            {
                var _search = _languageFile.Where(x => x.Id == pTextId);
                if (_search.Count() > 0)
                {
                    _translatedText = _search.First().TranslatedText;
                }
            }

            return _translatedText;
        }

        public string TranslateTextByIdentifier(string pTextIdentifier, int pLanguageId)
        {
            string _translatedText = string.Empty;

            if (_languageFile != null)
            {
                var _search = _languageFile.Where(x => x.TextIdentifier == pTextIdentifier);
                if (_search.Count() > 0)
                {
                    _translatedText = _search.First().TranslatedText;
                }
            }

            return _translatedText;
        }


        private IEnumerable<TranslatorFileModelDTO> GetFileModel(int pLanguageId)
        {
            string completePath = string.Format("{0}_Resource_{1}.json", HttpContext.Current.Server.MapPath("~/App_Data/"), pLanguageId);
            IEnumerable<TranslatorFileModelDTO> _languageFile = null;

            try
            {
                string data = System.IO.File.ReadAllText(completePath);

                if (!string.IsNullOrEmpty(data))
                {
                    return _languageFile = JsonConvert.DeserializeObject<IEnumerable<TranslatorFileModelDTO>>(data);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }

        
    }
}