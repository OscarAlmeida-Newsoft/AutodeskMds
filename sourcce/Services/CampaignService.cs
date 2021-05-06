using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using IRepositories;
using Entities;
using AutoMapper;

namespace Services
{
    public class CampaignService : ICampaignService
    {
        protected ICampaignRepository campaignRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public CampaignService(ICampaignRepository pCampaignRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            campaignRepository = pCampaignRepository;
            uow = pUnitOfWork;
        }
        public void Add(CampaignDTO entity)
        {
            NS_tblCampaign _company = Mapper.Map<CampaignDTO, NS_tblCampaign>(entity);

            campaignRepository.Add(_company);

            //campaignRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<CampaignDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CampaignDTO> Find(Expression<Func<CampaignDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CampaignDTO Get(int id)
        {
            return Mapper.Map<NS_tblCampaign, CampaignDTO>(campaignRepository.Get(id));
        }

        public IEnumerable<CampaignDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene la información de una campaña por su CRMCampaignId
        /// </summary>
        /// <param name="pCRMcampaignID">Id de la campaña en el CRM</param>
        /// <returns>Información de una campaña</returns>
        public CampaignDTO GetByCRMCampaignID(string pCRMcampaignID)
        {
            NS_tblCampaign _campaign = campaignRepository.GetByCRMCampaignID(pCRMcampaignID);
            CampaignDTO _campaignDTO = Mapper.Map<NS_tblCampaign, CampaignDTO>(_campaign);

            return _campaignDTO;
        }

        public void Remove(CampaignDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CampaignDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
