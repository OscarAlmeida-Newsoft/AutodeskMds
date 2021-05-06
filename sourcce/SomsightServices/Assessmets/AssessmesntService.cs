using AutoMapper;
using ISOMSightRepositories;
using ISOMSightRepositories.Common;
using ISOMSightServices;
using SOMSightModels;
using SOMSightModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightServices
{
    public class AssessmenstService : IAssessmentService
    {
        private string _message;

        protected IUnitOfWork unitOfWork;
        readonly IAssessmentSummaryRepository assessmentSummeryRepository;

           
        public AssessmenstService(IUnitOfWork pUnitOfWork, IAssessmentSummaryRepository pAssessmentSummeryRepository)
        {
            unitOfWork = pUnitOfWork;
            assessmentSummeryRepository = pAssessmentSummeryRepository;
        }

        public IEnumerable<AssessmentSummaryDTO> GetAssessmentsSummary(Guid pUserId)
        {
            IEnumerable<NS_TblAssessmentType>_assessmensType = GetAssessmentType();
            IEnumerable<NS_TblMaturityLevel> _maturityLevels = GetAssessmentMaturityLevels();

            //first check the assessments status
            if (CheckAssessments(pUserId, _assessmensType))
            {
                unitOfWork.Commit();

                IEnumerable<AssessmentSummaryDTO> _data = Mapper.Map<IEnumerable<NS_TblAssessmentSummary>,IEnumerable<AssessmentSummaryDTO>>
                    (assessmentSummeryRepository.GetAssessmentsSummary(pUserId));
                _data = _data.Join(_assessmensType,
                    data => data.AssessmentTypeId,
                    assessmentType => assessmentType.Id,
                    (data, assessmentType) => new AssessmentSummaryDTO {
                        Id = data.Id,
                        AssessmentTypeId = data.AssessmentTypeId,
                        AssessmentTypeTraslatorId = assessmentType.TranslatorAdministratorId,
                        AssessmentDescriptionTranslatorId = assessmentType.TranslatorAdministratorDescriptionId,
                        UserId = data.UserId,
                        CreatedDate = data.CreatedDate,
                        GlobalMaturityLevelId = data.GlobalMaturityLevelId,
                        ResponseEndDate = data.ResponseEndDate,
                        ResponseStartDate = data.ResponseStartDate,
                        IconName = assessmentType.IconName,
                        GlobalMaturityLevelTranslatorId = GetMaturityLevelTranslatorId(data.GlobalMaturityLevelId, _maturityLevels)
                    });

                return _data;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Check if there are Assessments to insert for the lead in the indicated campaign
        /// if there are pending assessments for registering to the client it will be inserted
        /// </summary>
        /// <param name="pLeadID">LeadId</param>
        /// <param name="pCampaignID"> Campaign Id</param>
        private bool CheckAssessments(Guid pUserId, IEnumerable<NS_TblAssessmentType> pAssessmentsType)
        {
            

            bool result = assessmentSummeryRepository.CheckAssessmentsSummary(pUserId, pAssessmentsType);

            if (result)
            {
                return true;
            }
            else
            {
                _message = assessmentSummeryRepository.Message;

                return false;
            }
        }

        public AssessmentSummaryDTO GetAssessmentsSummaryById(int pAssessmentSummaryId)
        {
            
            NS_TblAssessmentSummary _summary = assessmentSummeryRepository.GetAssessmentsSummaryById(pAssessmentSummaryId);

            NS_TblAssessmentType _assessmensType = GetAssessmentType().Where(x => x.Id == _summary.AssessmentTypeId).FirstOrDefault();

            IEnumerable<NS_TblMaturityLevel> _maturityLevels = GetAssessmentMaturityLevels();

            AssessmentSummaryDTO _summaryDTO = Mapper.Map<NS_TblAssessmentSummary, AssessmentSummaryDTO>(assessmentSummeryRepository.GetAssessmentsSummaryById(pAssessmentSummaryId));
            _summaryDTO.AssessmentTypeTraslatorId = _assessmensType.TranslatorAdministratorId;
            _summaryDTO.IconName = _assessmensType.IconName;

            if (_summaryDTO.GlobalMaturityLevelId != null)
            {
                _summaryDTO.GlobalMaturityLevelTranslatorId = _maturityLevels.Where(x => x.MaturityLevelId == _summaryDTO.GlobalMaturityLevelId).FirstOrDefault().TranslatorAdministratorId;
            }

            

            return _summaryDTO;
        }

        private IEnumerable<NS_TblAssessmentType> GetAssessmentType()
        {
            AssessmentExternalService.AssessmentServicesClient _client = new AssessmentExternalService.AssessmentServicesClient();
            List<NS_TblAssessmentType> _assessmentTypes = new List<NS_TblAssessmentType>();
            IEnumerable<AssessmentExternalService.NS_TblAssessmentType> _assessmentTypeExternal = _client.GetAssessmentTypes();
            //Convert the object to internalClass
            foreach (var item in _assessmentTypeExternal)
            {
                _assessmentTypes.Add(new NS_TblAssessmentType
                {
                    CreatedById = item.CreatedById,
                    CreatedDate = item.CreatedDate,
                    Description = item.Description,
                    Id = item.Id,
                    IndustryId = item.IndustryId,
                    Status = item.Status,
                    TranslatorAdministratorId = item.TranslatorAdministratorId,
                    TranslatorAdministratorDescriptionId = item.TranslatorAdministratorDescriptionId,
                    UpdatedById = item.UpdatedById,
                    UpdatedDate = item.UpdatedDate,
                    IconName = item.IconName
                    
                });
            }

            return _assessmentTypes;
        }

        private IEnumerable<NS_TblMaturityLevel> GetAssessmentMaturityLevels()
        {
            AssessmentExternalService.AssessmentServicesClient _client = new AssessmentExternalService.AssessmentServicesClient();
            List<NS_TblMaturityLevel> _assessmentMaturityLevels = new List<NS_TblMaturityLevel>();
            IEnumerable<AssessmentExternalService.NS_TblMaturityLevel> _assessmentMaturityLevelsExternal = _client.GetAssessmentMaturityLevels();
            //Convert the object to internalClass
            foreach (var item in _assessmentMaturityLevelsExternal)
            {
                _assessmentMaturityLevels.Add(new NS_TblMaturityLevel
                {
                    MaturityLevelId = item.MaturityLevelId,
                    Description = item.Description,
                    Points = item.Points,
                    TranslatorAdministratorId = item.TranslatorAdministratorId

                });
            }

            return _assessmentMaturityLevels;
        }

        private int? GetMaturityLevelTranslatorId(string pGlobalMaturityLevel, IEnumerable<NS_TblMaturityLevel> pMaturityLevelList)
        {
            var _levelTranslatorId = pMaturityLevelList.Where(x => x.MaturityLevelId == pGlobalMaturityLevel);

            if (_levelTranslatorId.Count()> 0)
            {
                return _levelTranslatorId.First().TranslatorAdministratorId;
            }


            return null;
        }
    }
}
