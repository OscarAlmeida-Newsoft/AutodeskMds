using Affidavit.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Recommendations
{
    public class VariableListViewModel
    {
        public short VariableID { get; set; }
        
        [TranslatorDisplay("Old_NewLabelVariableName")]
        public string VariableName { get; set; }

        [TranslatorDisplay("Old_NewLabelVariableDescription")]
        public string VariableDescription { get; set; }

        [TranslatorDisplay("Old_NewLabelVariableType")]
        public string VariableType { get; set; }
    }
}