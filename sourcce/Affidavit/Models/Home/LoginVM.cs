using Affidavit.Attribute;
using Microsoft.CookieCompliance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Home
{
    public class LoginVM
    {
        [TranslatorRequired("Old_FieldRequired")]
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        [TranslatorRequired("Old_FieldRequired")]
        public string Password { get; set; }

        public Guid leadID { get; set; }

        public bool RenderConsentBanner { get; set; }
        public ConsentMarkup ConsentMarkup { get; set; }
    }

    public class ForgetPassword
    {
        [TranslatorRequired("Old_FieldRequired")]
        public string Usuario { get; set; }
    }
}