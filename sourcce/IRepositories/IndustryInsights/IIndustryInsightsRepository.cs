using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DTOs;

namespace IRepositories
{
    public interface IIndustryInsightsRepository
    {
        IEnumerable<NS_tblIndustryIndIns> GetAllIndustries();
        NS_tblIndustryIndIns GetIndustryById(int Id);
        IEnumerable<IndustryInsightsAdminDTO> GetAllIndustryInsightsAssessments(int pPageNumber, int pPageSize, int IdAssessmentType);
        int GetNumbreAllIndustryInsightsAssessments(int IdAsessmentIndIns);
    }
}
