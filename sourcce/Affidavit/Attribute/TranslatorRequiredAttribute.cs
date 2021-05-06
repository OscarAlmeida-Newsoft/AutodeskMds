using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Affidavit.Utils;
using System.ComponentModel.DataAnnotations;

namespace Affidavit.Attribute
{
    public class TranslatorRequiredAttribute : RequiredAttribute
    {
        public TranslatorRequiredAttribute(string textIdentifier)
        {
            this.ErrorMessage = TranslatorHelper.TranslateTextByIdentifier(textIdentifier);
        }
    }
}