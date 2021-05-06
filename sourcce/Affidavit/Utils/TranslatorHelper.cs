using Affidavit.Helpers;
using DTOs;
using DTOs.Utils;
using IServices;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Utils
{
    public static class TranslatorHelper
    {
        private static System.Resources.ResourceManager _langResource = new System.Resources.ResourceManager("Resources.Resources", typeof(Resources.Resources).Assembly);

        private static ISessionState _sessionState
        {
            get
            {
                ISessionState _sessionStateResolve = DependencyResolver.Current.GetService<ISessionState>();
                return _sessionStateResolve;
            }
        }

        private static ITranslatorUtility _translatorUtility
        {
            get
            {
                ITranslatorUtility translatorUtility = DependencyResolver.Current.GetService<ITranslatorUtility>();
                return translatorUtility;
            }
        }


        public static string T(string key)
        {
            string _translatedWord = _langResource.GetString(key, CultureInfo.CreateSpecificCulture(CultureHelper.GetCurrentCulture()));
            return string.IsNullOrWhiteSpace(_translatedWord) ? key : _translatedWord;
        }

        //http://stackoverflow.com/questions/388483/how-do-you-create-a-dropdownlist-from-an-enum-in-asp-net-mvc
        //Convierte enumeración a SelectList traducida
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))

                         select new { Id = e, Name = T(e.ToString()) };

            SelectList _result = new SelectList(values, "Id", "Name", enumObj);
            return _result;
        }

        public static string TranslateTextByIdentifier(string pTextIdentifier)
        {
            int languageId = (int)_sessionState.Get(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY);

            return _translatorUtility.TranslateTextByIdentifier(pTextIdentifier, languageId);
        }

        public static string TranslateTextByIdentifier(string pTextIdentifier, int pLanguageId)
        {
            return _translatorUtility.TranslateTextByIdentifier(pTextIdentifier, pLanguageId);
        }

        public static string TranslateTextById(int pTextId)
        {
            int languageId = (int)_sessionState.Get(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY);

            return _translatorUtility.TranslateTextById(pTextId, languageId);
        }

        public static string TranslateTextById(int pTextId, int pLanguageId)
        {
            return _translatorUtility.TranslateTextById(pTextId, pLanguageId);
        }


        public static int GetCultureIdentifier(string pCultureCode)
        {
            return _translatorUtility.GetCultureIdentifier(pCultureCode);
        }

        public static void SetCulture(int pCultureIdentifier)
        {
            _sessionState.Store(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY, pCultureIdentifier);
        }

        public static IEnumerable<ImplementedCulturesDTO> GetImplementedCultures()
        {
            return _translatorUtility.GetImplementedCultures();
        }

        public static void UpdateTranslatorUtilityFileModel()
        {
            _translatorUtility.UpdateFileModel();
        }



    }
}