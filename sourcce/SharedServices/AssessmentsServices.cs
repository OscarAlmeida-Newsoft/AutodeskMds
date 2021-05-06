using ISharedRepositories;
using SharedEntities;
using System;
using System.Collections.Generic;

namespace SharedServices
{
    public class AssessmentsServices 
    {
        readonly IAssessmentTypeRepository _assessmentTypeRepository;
        readonly IAssessmentQuestionsRepository _assessmentQuestionRepository;
        readonly IAssessmentRecommendations _assessmentRecommendations;
        readonly IAssessmentMaturityLevelRepository _assessmentMaturityLevelRepository;

        public AssessmentsServices(IAssessmentTypeRepository pAssesmentTypeRepository, IAssessmentQuestionsRepository pAssessmentQuestionRepository
            , IAssessmentRecommendations pAssessmentRecommendations, IAssessmentMaturityLevelRepository pAssessmentMaturityLevelRepository)
        {
            _assessmentTypeRepository = pAssesmentTypeRepository;
            _assessmentQuestionRepository = pAssessmentQuestionRepository;
            _assessmentRecommendations = pAssessmentRecommendations;
            _assessmentMaturityLevelRepository = pAssessmentMaturityLevelRepository;
        }

        public IEnumerable<NS_TblAssessmentType> GetAssessmentType()
        {
            return _assessmentTypeRepository.GetAssessmentsType();
        }

        public IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId)
        {
            return _assessmentQuestionRepository.GetAssessmentQuestions(pAssessmentTypeId);
        }

        public IEnumerable<NS_TblRecommendation> GetAssessmentRecommendations()
        {
            return _assessmentRecommendations.GetAssessmentRecommendations();
        }

        public IEnumerable<NS_TblMaturityLevel> GetAssessmentsMaturityLevels()
        {
            return _assessmentMaturityLevelRepository.GetAssessmentsMaturityLevels();
        }

        public int GetActiveAssessmentsCount()
        {
            return _assessmentTypeRepository.GetActiveAssessmentsCount();
        }
    }
}
