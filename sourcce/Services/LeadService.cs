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
    public class LeadService : ILeadService
    {
        protected IUnitOfWork.IUnitOfWork unitOfWork;
        readonly ILeadRepository leadRepository;
        protected ICodeGenerator codeGenetor;

        public LeadService(IUnitOfWork.IUnitOfWork pUnitOfWork, ILeadRepository pLeadRepository, ICodeGenerator pCodeGenerator)
        {
            unitOfWork = pUnitOfWork;
            leadRepository = pLeadRepository;
            codeGenetor = pCodeGenerator;
        }


        /// <summary>
        /// Save the disagree reason
        /// </summary>
        /// <param name="disagreeReasonDTO">Disagree reason object with data</param>
        public void SaveDisagreeReason(DisagreeReasonDTO pDisagreeReasonDTO)
        {
            LeadInformation _leadInfo = leadRepository.GetByLeadId(pDisagreeReasonDTO.LeadId);

            _leadInfo.AcceptLanding = false;
            _leadInfo.EditedDate = DateTime.Now;
            _leadInfo.CommentsNoAccept = pDisagreeReasonDTO.DisagreeReason;

            unitOfWork.Complete();
        }

        /// <summary>
        /// Save the lead information
        /// </summary>
        /// <param name="pLeadInformation">Lead information to save</param>
        public void SaveLeadInformation(LeadInformationDTO pLeadInformation)
        {
            LeadInformation _leadInfo = Mapper.Map<LeadInformationDTO, LeadInformation>(pLeadInformation);
            

            leadRepository.SaveLeadInformation(_leadInfo);
            unitOfWork.Complete();
        }

        /// <summary>
        /// Get lead information by ID
        /// </summary>
        /// <param name="pLeadId">Lead Id</param>
        /// <returns>Object with the lead information</returns>
        public LeadInformationDTO GetByLeadID(Guid pLeadID)
        {
            return Mapper.Map<LeadInformation, LeadInformationDTO>(leadRepository.GetByLeadId(pLeadID));
        }

        /// <summary>
        /// Update the information of a row
        /// </summary>
        /// <param name="pLeadID">Lead ID of the row to update</param>
        public void UpdateLeadInformation(Guid pLeadID, Guid? pLandingID, string pLandingText, string updateType)
        {
            LeadInformation _leadInfo = leadRepository.GetByLeadId(pLeadID);

            if (updateType.Equals("landingUpdate"))
            {
                _leadInfo.AcceptLanding = true;
                _leadInfo.IdLandingCampaig = pLandingID;
                _leadInfo.AcceptedLandingText = pLandingText;
            }
            else if (updateType.Equals("acceptLandingDateUpdate"))
            {
                _leadInfo.AcceptLandingDate = DateTime.UtcNow;
            }
            else if (updateType.Equals("noParticipateDateUpdate"))
            {
                _leadInfo.NoParticipateDate = DateTime.UtcNow;
            }
            else if (updateType.Equals("submittedDateUpdate"))
            {
                _leadInfo.SubmittedDate = DateTime.UtcNow;
            }


            //unitOfWork.Complete();

        }

        public void UpdateLeadInformationIndustryIndInsId(Guid pId, int IndustryIndInsId) {

            leadRepository.UpdateLeadInformationIndustryIndInsId(pId, IndustryIndInsId);
            unitOfWork.Complete();
        }

        /// <summary>
        /// Get Lead by Landing campaign id
        /// </summary>
        /// <param name="pLandingId">Landing campaign id</param>
        /// <returns></returns>
        public bool GetLeadByLandingCampaignId(Guid pLandingId)
        {
            return leadRepository.GetLeadByLandingCampaignId(pLandingId);
        }

        /// <summary>
        /// Get a Lead by LeadUsser 
        /// </summary>
        /// <param name="pEmailUser">User by the lead</param>
        /// <returns></returns>
        public IEnumerable<LeadInformationDTO> GetLeadByEmail(string pEmailUser)
        {
            return Mapper.Map<IEnumerable<LeadInformation>, IEnumerable<LeadInformationDTO>>(leadRepository.GetLeadByEmail(pEmailUser));
        }

        //TODO:Remove this method after create all company user names
        public void CreateAllCompanyUserNames()
        {
            //IEnumerable<LeadInformation> _leads = leadRepository.GetAll().Where(x => x.CompanySAMLiveUserName == null);

            //foreach (var item in _leads)
            //{
            //    leadRepository.UpdateLeadInformationUserName(item.Id, GetCompanyUserNameCode());
            //}
            //unitOfWork.Complete();
        }



        public IEnumerable<LeadInformationDTO> GetByUserName(string pCompanyName)
        {
            return Mapper.Map<IEnumerable<LeadInformation>, IEnumerable<LeadInformationDTO>>(leadRepository.GetByUserName(pCompanyName));
        }

    }
}
