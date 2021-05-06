using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DTOs;

namespace Services
{
    public class CRMService : ICRMService
    {
        private readonly ICRMRepository CRMRepository;

        public CRMService(ICRMRepository pCRMRepository)
        {
            CRMRepository = pCRMRepository;
        }

        /// <summary>
        ///     Servicio de prueba para consultas a CRM
        /// </summary>
        /// <returns></returns>
        public string GetCRMEntityValue()
        {
            return CRMRepository.GetCRMEntityValue();
        }

        /// <summary>
        ///     Service para consultar un lead por su id.
        /// </summary>
        /// <param name="pLeadID">Id del lead a consultar</param>
        /// <returns></returns>
        public CRMDataDTO GetLeadByID(Guid pLeadID)
        {
            return CRMRepository.GetLeadByID(pLeadID);
        }

        /// <summary>
        /// Get lead information for survey question consultant page
        /// </summary>
        /// <param name="pCompanyName">Name of the company</param>
        /// <returns></returns>
        public List<CRMDataDTO> GetLeadInfoByCompanyName(string pCompanyName)
        {
            return CRMRepository.GetLeadInfoByCompanyName(pCompanyName);
        }

        /// <summary>
        /// Get lead information for survey question consultant page
        /// </summary>
        /// <param name="pCompanyName">Name of the company</param>
        /// <param name="pConsultant">Name of the consultant</param>
        /// <returns></returns>
        public List<CRMDataDTO> GetLeadInfoByCompanyNameAndConsultant(string pCompanyName, string pConsultant)
        {
            return CRMRepository.GetLeadInfoByCompanyNameAndConsultant(pCompanyName, pConsultant);
        }

        /// <summary>
        /// check if exist a campaign in the CRM
        /// </summary>
        /// <param name="pCampaignName">Campaign Name to verified</param>
        /// <returns></returns>
        public bool CheckCampaign(string pCampaignName)
        {
            return CRMRepository.CheckCampaign(pCampaignName);
        }

        /// <summary>
        /// Get the consultant job title 
        /// </summary>
        /// <param name="pConsultantName">consultant name</param>
        /// <returns></returns>
        public string GetConsultantTitle(Guid pLeadID)
        {
            return CRMRepository.GetConsultantTitle(pLeadID);
        }

        /// <summary>
        /// Get the consultant authorize region by email
        /// </summary>
        /// <param name="pEmailconsultant">Email consultant</param>
        /// <returns></returns>
        public string GetUserRegionByEmail(string pEmailconsultant)
        {
            return CRMRepository.GetUserRegionByEmail(pEmailconsultant);
        }
    }
}
