using Entities.SurveyQuestionResponse;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SurveyQuestionResponseRepository : Repository<SurveyQuestionResponse>, ISurveyQuestionResponseRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SurveyQuestionResponseRepository(AffidavitContext context) : base(context)
        {
        }

        /// <summary>
        /// Save a Survey Question Response
        /// </summary>
        /// <param name="pSurveyQuestionResponse">Survey Question Response Object</param>
        public void CreateSurveyQuestionResponse(SurveyQuestionResponse pSurveyQuestionResponse)
        {
            base.Add(pSurveyQuestionResponse);
        }

        /// <summary>
        /// Get a survey question by LeadId
        /// </summary>
        /// <param name="pLeadID">Lead Id for the survey</param>
        /// <returns></returns>
        public SurveyQuestionResponse GetSurveyQuestionByLeadId(Guid pLeadID)
        {
            return base.Context.Set<SurveyQuestionResponse>().Where(x => x.LeadId == pLeadID).FirstOrDefault();
        }
    }
}
