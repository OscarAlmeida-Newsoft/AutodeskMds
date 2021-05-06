using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Entities;
using IRepositories;
using DTOs.Utils;

namespace Repositories
{
    public class IndustryInsightsRepository : IIndustryInsightsRepository
    {
        private AffidavitContext dbContext;
        private readonly ITranslatorUtility translatorUtility;

        public IndustryInsightsRepository(AffidavitContext context, ITranslatorUtility pTranslatorUtility) {
            dbContext = context;
            translatorUtility = pTranslatorUtility;
        }

        public IEnumerable<NS_tblIndustryIndIns> GetAllIndustries()
        {
            IEnumerable<NS_tblIndustryIndIns> data = dbContext.NS_tblIndustryIndIns.OrderBy(d => d.Order);

            return data;
        }

        public IEnumerable<IndustryInsightsAdminDTO> GetAllIndustryInsightsAssessments(int pPageNumber, int pPageSize, int IdAssessmentType)
        {
            List<IndustryInsightsAdminDTO> info = new List<IndustryInsightsAdminDTO>();

            var data = (from asessment in dbContext.NS_AssessmentSummary
                        join userinfo in dbContext.LeadInformation
                            on asessment.CompanyId equals userinfo.LeadId
                        join industry in dbContext.NS_tblIndustryIndIns
                            on userinfo.IndustryIndInsId equals industry.Id
                        where asessment.AssessmentTypeId == IdAssessmentType
                        && asessment.ResponseStartDate != null
                        select new
                        {
                            IdAssessmentSummary = asessment.Id,
                            Email = userinfo.LeadUser,
                            CompanyName = userinfo.CompanyName,
                            IndustryName = translatorUtility.TranslateTextById(industry.TranslatorAdministratorDescriptionId,2),
                            ContactName = userinfo.CompanyName,
                            StartDate = asessment.ResponseStartDate,
                            EndDate = asessment.ResponseEndDate
                        }).OrderByDescending(x => x.EndDate).ThenByDescending(x => x.StartDate).Skip(
                            (pPageNumber - 1) * pPageSize
                        ).Take(pPageSize);



            foreach (var i in data)
            {

                info.Add(new IndustryInsightsAdminDTO()
                {
                    IdAssessmentSummary = i.IdAssessmentSummary,
                    Email = i.Email,
                    CompanyName = i.CompanyName,
                    IndustryName = i.IndustryName,
                    ContactName = i.ContactName,
                    Status = i.EndDate == null ? "In Progress" : "Finished",
                    StartDate = i.StartDate,
                    EndDate = i.EndDate
                });
            }

            return info;
        }

        public NS_tblIndustryIndIns GetIndustryById(int Id)
        {
            NS_tblIndustryIndIns industry = dbContext.NS_tblIndustryIndIns.SingleOrDefault(d => d.Id == Id);

            return industry;
        }

        public int GetNumbreAllIndustryInsightsAssessments(int IdAsessmentIndIns)
        {
            int NumberRegisters = dbContext.NS_AssessmentSummary.Count(d => d.AssessmentTypeId == IdAsessmentIndIns && d.ResponseStartDate != null);

            return NumberRegisters;
        }
    }
}
