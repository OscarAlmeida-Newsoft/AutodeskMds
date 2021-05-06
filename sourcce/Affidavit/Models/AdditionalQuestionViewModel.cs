using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class ServersViewModel
    {
        public List<AdditionalQuestionViewModel> AdditionalQuestion { get; set; }
        public List<AdditionalQuestion> AnswerQuestions { get; set; }
        public int ServersNumber { get; set; }
    }

    public class AdditionalQuestionViewModel
    {
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public int? ResDataTypeLength { get; set; }
        public string ProductFamilyName { get; set; }
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int ResDataType { get; set; }
        public short ProductFamilyID { get; set; }
        public int IsActive { get; set; }
    }

    public class AdditionalQuestion
    {
        public short ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int FamilyID { get; set; }
        public int IsVLS { get; set; }
        public int IsOEM { get; set; }
        public int IsWEB { get; set; }
        public int IsFPP { get; set; }
        public int IsInstalledLicenses { get; set; }
        public int Posicion { get; set; }
        public string Answer { get; set; }
        public int CompanyID { get; set; }
        public short CampaignID { get; set; }
        public int QuestionID { get; set; }
        public int IsActive { get; set; }
        public List<Server> ServersSQL { get; set; }
        public List<Server> ServersWindows { get; set; }
    }

    public class AnswerQuestion
    {
        public List<AdditionalQuestion> AnswerQuestions { get; set; }
    }

    public class Server
    {
        public short ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int FamilyID { get; set; }
        public int IsVLS { get; set; }
        public int IsOEM { get; set; }
        public int IsWEB { get; set; }
        public int IsFPP { get; set; }
        public int IsInstalledLicenses { get; set; }
        public bool IsRealProductID { get; set; }
        public int Posicion { get; set; }
        public bool IsVirtual { get; set; }
        public string Answer { get; set; }
        public int CompanyID { get; set; }
        public short CampaignID { get; set; }
        public int QuestionID { get; set; }        
    }
}