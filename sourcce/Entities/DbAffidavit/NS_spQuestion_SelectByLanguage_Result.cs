//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities.DbAffidavit
{
    using System;
    
    public partial class NS_spQuestion_SelectByLanguage_Result
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public byte ProductFamilyID { get; set; }
        public string ProductFamilyName { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<int> ReletedQuestionID { get; set; }
        public string RelatedQuestionIDResponse { get; set; }
        public int ResponseDataTypeID { get; set; }
        public string ResponseDataTypeDescription { get; set; }
        public int IsActive { get; set; }
    }
}
