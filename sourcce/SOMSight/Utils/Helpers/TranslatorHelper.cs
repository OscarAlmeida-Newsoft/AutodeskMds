
using ISOMSightServices;
using Microsoft.Practices.Unity;
using SOMSightModels.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOMSight.Utils
{
    public static class TranslatorHelper
    {

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


        public static string TranslateTextByIdentifier(string pTextIdentifier)
        {
            int languageId = (int)_sessionState.Get(ConstantsStringKeys.SELECTED_LANGUAGE_ID__KEY);
            
            return _translatorUtility.TranslateTextByIdentifier(pTextIdentifier, languageId);
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
    }
}