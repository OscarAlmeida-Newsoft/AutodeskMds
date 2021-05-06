using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ISOMSightRepositories;
using SOMSightModels;
using SOMSightModels.DTOs;

namespace SOMSightRepositories
{
    public class AssessmentQuestionRepository : IAssessmentQuestionRepository
    {

        private SOMSightContext dbContext;

        public AssessmentQuestionRepository(SOMSightContext context) 
        {
            dbContext = context;
        }

        public IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId)
        {
            IEnumerable<NS_TblAssessmentQuestion> _data = null;// dbContext.NS_AssessmentQuestion.Where(x => x.Capability.AssessmentTypeId == pAssessmentTypeId);


            return _data;
        }

        public IEnumerable<NS_TblAnswerXAssessment> GetAnswersXAssessment(Guid pUserId)
        {
            IEnumerable<NS_TblAnswerXAssessment> _data = null;
            _data = dbContext.NS_AnswerXAssessment.Where(x=> x.UserId == pUserId);
            
            return _data;
        }

        public void DeleteAllAnswersXAssessment(Guid pUserId, int pAssessmentTypeId)
        {
            var _dataToRemove = dbContext.NS_AnswerXAssessment.Where(x => x.UserId == pUserId && x.AssessmentTypeId == pAssessmentTypeId);

            dbContext.NS_AnswerXAssessment.RemoveRange(_dataToRemove);
        }

        public void SaveAnswersXAssessment(IEnumerable<NS_TblAnswerXAssessment> pAnswersXAssessment)
        {
            dbContext.NS_AnswerXAssessment.AddRange(pAnswersXAssessment);
        }

        public AssessmentRecommendationsDTO AssessmentScoringModel(int pAssessmentTypeId, Guid pUserId
            ,IEnumerable<NS_TblRecommendation> pAssessmentRecommendations)
        {
            NS_TblAssessmentSummary _summary = dbContext.NS_AssessmentSummary.Where(x => x.AssessmentTypeId == pAssessmentTypeId && x.UserId == pUserId).FirstOrDefault();


            IEnumerable<NS_TblRecommendation> _recommendations =  pAssessmentRecommendations.Where(x => x.AssessmentTypeId == pAssessmentTypeId && x.MaturityLevelId == _summary.GlobalMaturityLevelId);

            AssessmentRecommendationsDTO _model = new AssessmentRecommendationsDTO();

            _model.TitleTranslationId = _recommendations.First().TranslatorAdministratorNameId;
            _model.TextTranslationsIds = new List<int>();

            foreach (var item in _recommendations)
            {
                _model.TextTranslationsIds.Add(item.TranslatorAdministratorTextId);
            }
           
            return _model;
        }
    }
}
