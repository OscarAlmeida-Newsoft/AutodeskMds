using ISharedRepositories;
using Microsoft.Practices.Unity;
using SharedEntities;
using SharedRepositories;
using SharedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFSharedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AssessmentServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AssessmentServices.svc or AssessmentServices.svc.cs at the Solution Explorer and start debugging.
    public class AssessmentServices : IAssessmentServices
    {
        private UnityContainer _unityContainer;
        private AssessmentsServices _assessmentServices;
        public AssessmentServices()
        {
            
            //Unity container instance
            if (_unityContainer == null)
            {
                _unityContainer = new UnityContainer();
            }

            //Register unity types
            _unityContainer.RegisterType<IAssessmentTypeRepository, AssessmentTypeRepository>();
            _unityContainer.RegisterType<IAssessmentQuestionsRepository, AssessmentQuestionsRepository>();
            _unityContainer.RegisterType<IAssessmentRecommendations, AssessmentRecommendations>();
            _unityContainer.RegisterType<IAssessmentMaturityLevelRepository, AssessmentMaturityLevelRepository>();

            //Resolve Assessment Services class
            if (_assessmentServices == null)
            {
                _assessmentServices = _unityContainer.Resolve<AssessmentsServices>();
            }


        }

        public IEnumerable<NS_TblAssessmentQuestion> GetAssessmentQuestions(int pAssessmentTypeId)
        {
            return _assessmentServices.GetAssessmentQuestions(pAssessmentTypeId);
        }

        public IEnumerable<NS_TblAssessmentType> GetAssessmentTypes()
        {

            return _assessmentServices.GetAssessmentType();

        }

        public IEnumerable<NS_TblRecommendation> GetAssessmentRecommendations()
        {
            return _assessmentServices.GetAssessmentRecommendations();
        }

        public IEnumerable<NS_TblMaturityLevel> GetAssessmentMaturityLevels()
        {
            return _assessmentServices.GetAssessmentsMaturityLevels();
        }

        public int GetActiveAssessmentCount()
        {
            return _assessmentServices.GetActiveAssessmentsCount();
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }

    
}
