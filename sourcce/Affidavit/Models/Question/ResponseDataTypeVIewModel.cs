using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.Question
{
    public class ResponseDataTypeVIewModel
    {
        public int ResponseDataTypeID { get; set; }

        public string ResponseDataTypeDescription { get; set; }

        public int? ResponseDataTypeLength { get; set; }
    }
}