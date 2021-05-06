using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using DTOs;

namespace Repositories
{
    public class AssessmentQuestionRepository : Repository<NS_TblAssessmentQuestion>, IAssessmentQuestionRepository
    {

        private AffidavitContext dbContext;

        public AssessmentQuestionRepository(AffidavitContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId)
        {
            IEnumerable<NS_TblAssessmentQuestion> _data = null;// dbContext.NS_AssessmentQuestion.Where(x => x.Capability.AssessmentTypeId == pAssessmentTypeId);


            return _data;
        }

        public IEnumerable<NS_TblAnswerXAssessment> GetAnswersXAssessment(Guid pLeadId, Guid pCampaignID)
        {
            IEnumerable<NS_TblAnswerXAssessment> _data = null;
            _data = dbContext.NS_AnswerXAssessment.Where(x=> x.CompanyId == pLeadId && x.CampaignId == pCampaignID);
            
            return _data;
        }

        public void DeleteAllAnswersXAssessment(Guid pLeadId, Guid pCampaignID, int pAssessmentTypeId)
        {
            var _dataToRemove = dbContext.NS_AnswerXAssessment.Where(x => x.CompanyId == pLeadId && x.CampaignId == pCampaignID && x.AssessmentTypeId == pAssessmentTypeId);

            dbContext.NS_AnswerXAssessment.RemoveRange(_dataToRemove);
        }

        public void SaveAnswersXAssessment(IEnumerable<NS_TblAnswerXAssessment> pAnswersXAssessment)
        {
            dbContext.NS_AnswerXAssessment.AddRange(pAnswersXAssessment);
        }

        public AssessmentRecommendationsDTO AssessmentScoringModel(int pAssessmentTypeId, Guid pLeadId, Guid pCampaingId
            ,IEnumerable<NS_TblRecommendation> pAssessmentRecommendations)
        {
            //NS_TblAssessmentSummary _summary = dbContext.NS_AssessmentSummary.Where(x => x.AssessmentTypeId == pAssessmentTypeId && x.CampaignId == pCampaingId && x.CompanyId == pLeadId).FirstOrDefault();
            //IEnumerable<NS_TblRecommendation> _recommendations =  pAssessmentRecommendations.Where(x => x.AssessmentTypeId == pAssessmentTypeId && x.MaturityLevelId == _summary.GlobalMaturityLevelId);

            IEnumerable<NS_TblRecommendation> _recommendations = pAssessmentRecommendations.Where(x => x.AssessmentTypeId == pAssessmentTypeId );

            AssessmentRecommendationsDTO _model = new AssessmentRecommendationsDTO();

            _model.TitleTranslationId = _recommendations.First().TranslatorAdministratorNameId;
            //_model.TextTranslationsIds = new List<int>();
            _model.TextTranslationsIds = new List<RecommendationDTO>();

            foreach (var item in _recommendations)
            {
                //_model.TextTranslationsIds.Add(new item.TranslatorAdministratorTextId);
                _model.TextTranslationsIds.Add(
                    new RecommendationDTO {
                        TextTranslationsId = item.TranslatorAdministratorTextId,
                        AssessmentQuestionId = item.AssessmentQuestionId,
                        MaturityLevelId = item.MaturityLevelId
                    });
            }

            //var _parameters = new SqlParameter[] {
            //    new SqlParameter { ParameterName = "@AssessmentTypeID", Value = pAssessmentTypeId, Direction = ParameterDirection.Input, DbType = DbType.Int32 },
            //    new SqlParameter { ParameterName = "@CampaignId", Value = pCampaingId, Direction = ParameterDirection.Input, DbType = DbType.Guid },
            //    new SqlParameter { ParameterName = "@LeadId", Value = pLeadId, Direction = ParameterDirection.Input, DbType = DbType.Guid }
            //};

            //dbContext.Database.CommandTimeout = 1200;

            //IEnumerable<ScoringModelResultsXCapabilityDTO> _data = dbContext.Database
            //    .SqlQuery<ScoringModelResultsXCapabilityDTO>("[dbo].[NS_spGetAssessmentsScoringModelResults] @AssessmentTypeID, @CampaignId, @LeadId", _parameters).ToList();


            //IEnumerable<AssessmentRecommendationsDTO> _assessmentRecommendations = (from Average in _data
            //                                                                        join Recommendations in pAssessmentRecommendations on
            //                                                                        new
            //                                                                        {
            //                                                                            Key1 = Average.CapabilityId,
            //                                                                            Key2 = Average.PointsAVG
            //                                                                        }
            //                                                                        equals
            //                                                                        new
            //                                                                        {
            //                                                                            Key1 = Recommendations.CapabilityId,
            //                                                                            Key2 = Recommendations.MaturityLevelId
            //                                                                        }
            //                                                                        into Results
            //                                                                        from r in Results.DefaultIfEmpty()
            //                                                                        select new AssessmentRecommendationsDTO
            //                                                                        {
            //                                                                            CapabilityTranslationId = r.CapabilityTranslationId,
            //                                                                            TitleTranslationId = r.TranslatorAdministratorNameId,
            //                                                                            TextTranslationId = r.TranslatorAdministratorTextId
            //                                                                        });

            //return _assessmentRecommendations;
            return _model;
        }
    }
}
