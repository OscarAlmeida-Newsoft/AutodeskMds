using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Affidavit.Utils;

namespace Affidavit.Attribute
{
    public class TranslatorDisplayAttribute : DisplayNameAttribute
    {
        private readonly string textIdentifier;
        public TranslatorDisplayAttribute(string textIdentifier)
        : base()
        {
            this.textIdentifier = textIdentifier;
        }

        public override string DisplayName
        {
            get
            {
                return TranslatorHelper.TranslateTextByIdentifier(this.textIdentifier);
            }
        }
    }
}