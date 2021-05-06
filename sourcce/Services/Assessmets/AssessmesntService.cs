using AutoMapper;
using DTOs;
using Entities;
using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AssessmenstService : IAssessmentService
    {
        private string _message;

        protected IUnitOfWork.IUnitOfWork unitOfWork;
        readonly IAssessmentSummaryRepository assessmentSummaryRepository;

           
        public AssessmenstService(IUnitOfWork.IUnitOfWork pUnitOfWork, IAssessmentSummaryRepository pAssessmentSummeryRepository)
        {
            unitOfWork = pUnitOfWork;
            assessmentSummaryRepository = pAssessmentSummeryRepository;
        }

        public IEnumerable<AssessmentSummaryDTO> GetAssessmentsSummary(Guid pLeadID, Guid pCampaignID)
        {
            IEnumerable<NS_TblAssessmentType>_assessmensType = GetAssessmentType();
            IEnumerable<NS_TblMaturityLevel> _maturityLevels = GetAssessmentMaturityLevels();

            //first check the assessments status
            if (CheckAssessments(pLeadID, pCampaignID, _assessmensType))
            {
                unitOfWork.Complete();

                IEnumerable<AssessmentSummaryDTO> _data = Mapper.Map<IEnumerable<NS_TblAssessmentSummary>,IEnumerable<AssessmentSummaryDTO>>
                    (assessmentSummaryRepository.GetAssessmentsSummary(pLeadID, pCampaignID));
                _data = _data.Join(_assessmensType,
                    data => data.AssessmentTypeId,
                    assessmentType => assessmentType.Id,
                    (data, assessmentType) => new AssessmentSummaryDTO {
                        Id = data.Id,
                        AssessmentTypeId = data.AssessmentTypeId,
                        AssessmentTypeTraslatorId = assessmentType.TranslatorAdministratorId,
                        AssessmentDescriptionTranslatorId = assessmentType.TranslatorAdministratorDescriptionId,
                        CampaignId = data.CampaignId,
                        CompanyId = data.CompanyId,
                        CreatedDate = data.CreatedDate,
                        GlobalMaturityLevelId = data.GlobalMaturityLevelId,
                        ResponseEndDate = data.ResponseEndDate,
                        ResponseStartDate = data.ResponseStartDate,
                        IconName = assessmentType.IconName,
                        GlobalMaturityLevelTranslatorId = GetMaturityLevelTranslatorId(data.GlobalMaturityLevelId, _maturityLevels),
                        IsIndustryInsights = assessmentType.IsIndustryInsights
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
        private bool CheckAssessments(Guid pLeadID, Guid pCampaignID, IEnumerable<NS_TblAssessmentType> pAssessmentsType)
        {
            

            bool result = assessmentSummaryRepository.CheckAssessmentsSummary(pLeadID, pCampaignID, pAssessmentsType);

            if (result)
            {
                return true;
            }
            else
            {
                _message = assessmentSummaryRepository.Message;

                return false;
            }
        }

        public AssessmentSummaryDTO GetAssessmentsSummaryById(int pAssessmentSummaryId)
        {
            
            NS_TblAssessmentSummary _summary = assessmentSummaryRepository.GetAssessmentsSummaryById(pAssessmentSummaryId);

            NS_TblAssessmentType _assessmensType = GetAssessmentType().Where(x => x.Id == _summary.AssessmentTypeId).FirstOrDefault();

            IEnumerable<NS_TblMaturityLevel> _maturityLevels = GetAssessmentMaturityLevels();

            AssessmentSummaryDTO _summaryDTO = Mapper.Map<NS_TblAssessmentSummary, AssessmentSummaryDTO>(assessmentSummaryRepository.GetAssessmentsSummaryById(pAssessmentSummaryId));
            _summaryDTO.AssessmentTypeTraslatorId = _assessmensType.TranslatorAdministratorId;
            _summaryDTO.IconName = _assessmensType.IconName;
            _summaryDTO.IsIndustryInsights = _assessmensType.IsIndustryInsights;

            if (_summaryDTO.GlobalMaturityLevelId != null)
            {
                _summaryDTO.GlobalMaturityLevelTranslatorId = _maturityLevels.Where(x => x.MaturityLevelId == _summaryDTO.GlobalMaturityLevelId).FirstOrDefault().TranslatorAdministratorId;
            }

            

            return _summaryDTO;
        }

        public int GetActiveAssessmentsCount()
        {
            AssessmentExternalService.AssessmentServicesClient _client = new AssessmentExternalService.AssessmentServicesClient();
            int _activeAssessmentsCount = _client.GetActiveAssessmentCount();

            return _activeAssessmentsCount;
        }

        public IEnumerable<NS_TblAssessmentType> GetAssessmentType()
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
                    IconName = item.IconName,
                    IsIndustryInsights = item.IsIndustryInsights
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

        public void MarkAssessmentSummaryAsDraft(Guid pUserId, Guid pCampaignId, int pAssessmentSummaryId)
        {
            assessmentSummaryRepository.MarkAssessmentSummaryAsDraft(pUserId, pCampaignId, pAssessmentSummaryId);
        }

        public void SaveNextSteps(int pAssessmentSummaryId, string pNextSteps)
        {
            assessmentSummaryRepository.SaveNextSteps(pAssessmentSummaryId, pNextSteps);
            unitOfWork.Complete();
        }

        public void UnlockAssessment(int pAssessmentSummaryId)
        {
            assessmentSummaryRepository.UnlockAssessment(pAssessmentSummaryId);
            unitOfWork.Complete();
        }
    }
}
