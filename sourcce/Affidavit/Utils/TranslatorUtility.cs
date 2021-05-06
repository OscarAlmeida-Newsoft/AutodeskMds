using DTOs;
using DTOs.Utils;
using IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Affidavit.Utils
{
    public class TranslatorUtility : ITranslatorUtility
    {
        private TranslatorFileModelDTO _languageFile { get; set; }
        public ISessionState _sessionState;

        public TranslatorUtility(ISessionState pSessionState)
        {
            _sessionState = pSessionState;

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

        public void UpdateFileModel()
        {
            int _langId = 1;
            var _lang = _sessionState.Get(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY);
            if (_lang != null)
            {
                _langId = (int)_lang;
            }

            if (_languageFile == null || (_languageFile!= null && _languageFile.LanguageId != _langId))
            {
                _languageFile = GetFileModel(_langId);
            }

        }

        public void SaveJsonLanguageFile(int pLanguageId, IEnumerable<TranslatorTextModelDTO> pLanguageFileObject)
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
                var _search = _languageFile.LanguageFile.Where(x => x.Id == pTextId);
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
                var _search = _languageFile.LanguageFile.Where(x => x.TextIdentifier == pTextIdentifier);
                if (_search.Count() > 0)
                {
                    _translatedText = _search.First().TranslatedText;
                }
            }

            return _translatedText;
        }


        private TranslatorFileModelDTO GetFileModel(int pLanguageId)
        {
            string completePath = string.Format("{0}_Resource_{1}.json", HttpContext.Current.Server.MapPath("~/App_Data/"), pLanguageId);
            IEnumerable<TranslatorTextModelDTO> _languageFile = null;

            try
            {
                string data = System.IO.File.ReadAllText(completePath);

                if (!string.IsNullOrEmpty(data))
                {
                     _languageFile = JsonConvert.DeserializeObject<IEnumerable<TranslatorTextModelDTO>>(data);
                    return new TranslatorFileModelDTO { LanguageId = pLanguageId, LanguageFile = _languageFile };
                }
            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }

        public IEnumerable<ImplementedCulturesDTO> GetImplementedCultures()
        {
            string completePath = string.Format("{0}_Implementedcultures.json", HttpContext.Current.Server.MapPath("~/App_Data/"));
            IEnumerable<ImplementedCulturesDTO> _implementedCultures = null;

            try
            {
                string data = System.IO.File.ReadAllText(completePath);

                if (!string.IsNullOrEmpty(data))
                {
                    return _implementedCultures = JsonConvert.DeserializeObject<IEnumerable<ImplementedCulturesDTO>>(data);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }


        public int GetCultureIdentifier(string pCultureCode)
        {
            var cultures = GetImplementedCultures();

            int cultureId = 2;

            if (cultures.Count() > 0)
            {
                var data = cultures.Where(x => x.Culture.CultureCode.ToUpper() == pCultureCode.ToUpper());

                if (data.Count() > 0)
                {
                    cultureId = data.FirstOrDefault().Culture.CultureIdentifier;
                }
            }

            return cultureId;

        }

    }
}