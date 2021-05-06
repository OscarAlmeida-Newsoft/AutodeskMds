using ISharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SharedRepositories
{
    public class AssessmentQuestionsRepository : IAssessmentQuestionsRepository
    {
        public IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId)
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<NS_TblAssessmentQuestion> _rowMapper = MapBuilder<NS_TblAssessmentQuestion>.BuildAllProperties();
            IRowMapper<NS_TblAssessmentQuestionOption> _rowMapperQuestionOptions = MapBuilder<NS_TblAssessmentQuestionOption>.BuildAllProperties();

            List<NS_TblAssessmentQuestion> _questions = null;
            IEnumerable<NS_TblAssessmentQuestionOption> _questionOptions = null;
            

            try
            {
                Database _db = _factory.Create("SharedContext");
                _questions = _db.ExecuteSprocAccessor<NS_TblAssessmentQuestion>("NS_spGetActiveAssessmentQuestions", _rowMapper, pAssessmentTypeId).ToList();

                

                _questionOptions = _db.ExecuteSprocAccessor<NS_TblAssessmentQuestionOption>("NS_spGetAssessmentQuestionsOptions", _rowMapperQuestionOptions, pAssessmentTypeId);

                _questions.Select(c => { c.AssessmentQuestionOptions = _questionOptions.Where(y => y.AssessmentQuestionId == c.Id); return c; }).ToList();

                foreach (var question in _questions)
                {
                    var _qOptions = _questionOptions.Where(x => x.AssessmentQuestionId == question.Id);
                    _questions.Where(x => x.Id == question.Id).First().AssessmentQuestionOptions = _qOptions;
                }
                
            }
            catch (Exception)
            {

                throw;
            }

            return _questions;
        }
    }
}
 