using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace SOMSight.Utils.Helpers
{
    public class CultureHelper
    {
        // Valid cultures
        private static readonly IList<string> _validCultures = new List<string> { "af", "af-ZA", "sq", "sq-AL", "gsw-FR", "am-ET", "ar", "ar-DZ", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-AE", "ar-YE", "hy", "hy-AM", "as-IN", "az", "az-Cyrl-AZ", "az-Latn-AZ", "ba-RU", "eu", "eu-ES", "be", "be-BY", "bn-BD", "bn-IN", "bs-Cyrl-BA", "bs-Latn-BA", "br-FR", "bg", "bg-BG", "ca", "ca-ES", "zh-HK", "zh-MO", "zh-CN", "zh-Hans", "zh-SG", "zh-TW", "zh-Hant", "co-FR", "hr", "hr-HR", "hr-BA", "cs", "cs-CZ", "da", "da-DK", "prs-AF", "div", "div-MV", "nl", "nl-BE", "nl-NL", "en", "en-AU", "en-BZ", "en-CA", "en-029", "en-IN", "en-IE", "en-JM", "en-MY", "en-NZ", "en-PH", "en-SG", "en-ZA", "en-TT", "en-GB", "en-US", "en-ZW", "et", "et-EE", "fo", "fo-FO", "fil-PH", "fi", "fi-FI", "fr", "fr-BE", "fr-CA", "fr-FR", "fr-LU", "fr-MC", "fr-CH", "fy-NL", "gl", "gl-ES", "ka", "ka-GE", "de", "de-AT", "de-DE", "de-LI", "de-LU", "de-CH", "el", "el-GR", "kl-GL", "gu", "gu-IN", "ha-Latn-NG", "he", "he-IL", "hi", "hi-IN", "hu", "hu-HU", "is", "is-IS", "ig-NG", "id", "id-ID", "iu-Latn-CA", "iu-Cans-CA", "ga-IE", "xh-ZA", "zu-ZA", "it", "it-IT", "it-CH", "ja", "ja-JP", "kn", "kn-IN", "kk", "kk-KZ", "km-KH", "qut-GT", "rw-RW", "sw", "sw-KE", "kok", "kok-IN", "ko", "ko-KR", "ky", "ky-KG", "lo-LA", "lv", "lv-LV", "lt", "lt-LT", "wee-DE", "lb-LU", "mk", "mk-MK", "ms", "ms-BN", "ms-MY", "ml-IN", "mt-MT", "mi-NZ", "arn-CL", "mr", "mr-IN", "moh-CA", "mn", "mn-MN", "mn-Mong-CN", "ne-NP", "no", "nb-NO", "nn-NO", "oc-FR", "or-IN", "ps-AF", "fa", "fa-IR", "pl", "pl-PL", "pt", "pt-BR", "pt-PT", "pa", "pa-IN", "quz-BO", "quz-EC", "quz-PE", "ro", "ro-RO", "rm-CH", "ru", "ru-RU", "smn-FI", "smj-NO", "smj-SE", "se-FI", "se-NO", "se-SE", "sms-FI", "sma-NO", "sma-SE", "sa", "sa-IN", "sr", "sr-Cyrl-BA", "sr-Cyrl-SP", "sr-Latn-BA", "sr-Latn-SP", "nso-ZA", "tn-ZA", "si-LK", "sk", "sk-SK", "sl", "sl-SI", "es", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-DO", "es-EC", "es-SV", "es-GT", "es-HN", "es-MX", "es-NI", "es-PA", "es-PY", "es-PE", "es-PR", "es-ES", "es-US", "es-UY", "es-VE", "sv", "sv-FI", "sv-SE", "syr", "syr-SY", "tg-Cyrl-TJ", "tzm-Latn-DZ", "ta", "ta-IN", "tt", "tt-RU", "te", "te-IN", "th", "th-TH", "bo-CN", "tr", "tr-TR", "tk-TM", "ug-CN", "uk", "uk-UA", "wen-DE", "ur", "ur-PK", "uz", "uz-Cyrl-UZ", "uz-Latn-UZ", "vi", "vi-VN", "cy-GB", "wo-SN", "sah-RU", "ii-CN", "yo-NG" };

        // Include ONLY cultures you are implementing
        public static readonly IList<string> CulturesImplemented = new List<string> {
            "en-US",// la primera cultura es por defecto
            "es-CO"
        };
        public static string GetImplementedCulture(string name)
        {
            //Si es nula
            if (string.IsNullOrEmpty(name))
            {
                return GetDefaultCulture(); //Retorna cultura por defecto "es-CO"
            }

            //Si es una cultura inválida
            if (_validCultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() == 0)
            {
                return GetDefaultCulture(); //Retorna cultura por defecto "es-CO"
            }

            //Si se implementa la cultura
            if (CulturesImplemented.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
            {
                return name; // accept it
            }

            else
            {
                // Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
                // the function will return closes match that is "en-US" because at least the language is the same (ie English)  
                var n = GetNeutralCulture(name);
                foreach (var c in CulturesImplemented)
                {
                    if (c.StartsWith(n))
                    {
                        return c;
                    }
                }

                // else 
                // It is not implemented
                return GetDefaultCulture(); // return Default culture as no match found
            }
        }


        /// <summary>
        /// Retorna la primera cultura declarada en _cultures "es-CO"
        /// </summary>
        /// <returns>Cadena con el nombre de la cultura por defecto</returns>
        public static string GetDefaultCulture()
        {
            return CulturesImplemented[0]; //Retorna cultura por defecto "en-US"
        }

        /// <summary>
        /// Retorna la cultura actual
        /// </summary>
        /// <returns>Cadena con el nombre de la cultura actual</returns>
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        /// Retorna la cultura actual
        /// </summary>
        /// <returns>Cadena con el nombre de la cultura neutral actual</returns>
        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }


        public static string GetNeutralCulture(string name)
        {
            if (name.Length < 2)
            {
                return name;
            }
            else
            {
                return name.Substring(0, 2); // Lee los primeros caracteres
            }
        }

        public static List<Culture> GetCultureList()
        {
            List<Culture> _culturas = new List<Culture>();
            int _index = 0;
            foreach (var culture in CulturesImplemented)
            {
                _index++;
                _culturas.Add(new Culture { Key = _index, CultureName = culture });
            }

            return _culturas;
        }
    }

    public class Culture
    {
        public int Key { get; set; }
        public string CultureName { get; set; }
    }
}