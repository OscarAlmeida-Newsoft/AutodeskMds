using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class QuestionsViewModels
    {
        public List<ServersInformation> windowsServerInformation { get; set; }
        public List<ServersInformation> sqlServerInformation { get; set; }
    }


    public class ServersInformation
    {
        public int companyId { get; set; }
        public short campaignId { get; set; }
        public int productId { get; set; }
        public bool productIsVLS { get; set; }
        public bool productIsFPP { get; set; }
        public bool productIsOEM { get; set; }
        public bool productIsWeb { get; set; }
        public bool productInstalledLicenses { get; set; }
        public string productName { get; set; }
        public string productFamilyComplete { get; set; }
        public int Qty { get; set; }
        public string LeadID { get; set; }
        //Nuevo
        public int LicenseId { get; set; }
        public bool IsVirtual { get; set; }
        public short? IsInside { get; set; }
        public bool IsNew { get; set; }
    }

    public class DetailAnswers
    {
        public int questionId { get; set; }
        public string answer { get; set; }
        public int licenceId { get; set; }
    }

    public class AnswerquestionsViewModel
    {
        public int companyId { get; set; }
        public short campaignId { get; set; }
        public int productId { get; set; }
        public bool productIsVLS { get; set; }
        public bool productIsFPP { get; set; }
        public bool productIsOEM { get; set; }
        public bool productIsWeb { get; set; }
        public bool productInstalledLicenses { get; set; }
        public string productName { get; set; }
        public string productFamilyComplete { get; set; }
        public List<DetailAnswers> detailQuestions { get; set; }
        //Nuevo modelo
        public bool IsVirtual { get; set; }
        public bool IsNew { get; set; }
        public int LicenseId { get; set; }
        public short? IsInside { get; set; }
        public bool IsRealProductID { get; set; }
    }

    public class GoupServerInformation
    {
        public int productId { get; set; }
        public bool VLS { get; set; }
        public bool OEM { get; set; }
        public bool FPP { get; set; }
        public bool WEB { get; set; }
        public bool InstalledLicenses { get; set; }
        public string productName { get; set; }
        public string familyComplete { get; set; }
        public int count { get; set; }
        //Nuevo modelo
        public bool IsVirtual { get; set; }
        public bool IsNew { get; set; }
        public int LicenseId { get; set; }
        public short? IsInside { get; set; }
    }

    public class TableHeaderAnswerQuestion
    {
        public string familyComplete { get; set; }
        public string productName { get; set; }
        public string licenseType { get; set; }
        public int productId { get; set; }
        public int companyId { get; set; }
        public int campaignId { get; set; }
        public bool IsNew { get; set; }
        public bool IsVirtual { get; set; }
        public int LicenseId { get; set; }
        public short? IsInside { get; set; }
    }

    public class AdditionalQuestionCompleteViewModel
    {
        public List<AdditionalQuestionViewModel> questions { get; set; }
        public List<TableHeaderAnswerQuestion> headerServerByQuestions { get; set; }
        public List<AnswerquestionsViewModel> serverByQuestions { get; set; }
        public Guid LeadID { get; set; }
        public bool IsFinal { get; set; }

    }
}