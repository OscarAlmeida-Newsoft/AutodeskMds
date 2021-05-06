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
    public class PartnerProgramCompanyService : IPartnerProgramCompanyService
    {
        protected IPartnerProgramCompanyRepository partnerProgramCompanyRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public PartnerProgramCompanyService(IPartnerProgramCompanyRepository pPartnerProgramCompanyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            partnerProgramCompanyRepository = pPartnerProgramCompanyRepository;
            uow = pUnitOfWork;
        }

        /// <summary>
        ///     Guarda información referente a partner program
        /// </summary>
        /// <param name="entity"></param>
        public void Add(PartnerProgramCompanyDTO entity)
        {
            NS_tblPartnerProgramCompany _partnerProgramCompany = Mapper.Map<PartnerProgramCompanyDTO, NS_tblPartnerProgramCompany>(entity);

            partnerProgramCompanyRepository.Add(_partnerProgramCompany);

            //partnerProgramCompanyRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<PartnerProgramCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerProgramCompanyDTO> Find(Expression<Func<PartnerProgramCompanyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public PartnerProgramCompanyDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerProgramCompanyDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene los programas de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public IEnumerable<PartnerProgramCompanyDTO> GetByCompositeKey(int pCompanyID, short pCampaignID)
        {
            IEnumerable<NS_tblPartnerProgramCompany> _partnerProgramCompany = partnerProgramCompanyRepository.GetByCompositeKey(pCompanyID, pCampaignID);
            IEnumerable<PartnerProgramCompanyDTO> _partnerProgramCompanyDTO = Mapper.Map<IEnumerable<NS_tblPartnerProgramCompany>, IEnumerable<PartnerProgramCompanyDTO>>(_partnerProgramCompany);

            return _partnerProgramCompanyDTO;
        }

        /// <summary>
        ///     Obtiene una Program de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerProgramID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public PartnerProgramCompanyDTO GetByFullCompositeKey(int pCompanyID, short pPartnerProgramID, short pCampaignID)
        {
            return Mapper.Map<NS_tblPartnerProgramCompany, PartnerProgramCompanyDTO>(partnerProgramCompanyRepository.GetByFullCompositeKey(pCompanyID, pPartnerProgramID, pCampaignID));
        }

        public void Remove(PartnerProgramCompanyDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<PartnerProgramCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblPartnerProgramCompany
        /// </summary>
        /// <param name="pPartnerProgramCompany">ViewModel con informacion a actualizar</param>
        public void UpdatePartnerProgramCompany(PartnerProgramCompanyDTO pPartnerProgramCompany)
        {
            int _companyID = pPartnerProgramCompany.CompanyID;
            short _campaignID = pPartnerProgramCompany.CampaignID;
            short _partnerProgramID = pPartnerProgramCompany.PartnerProgramID;

            NS_tblPartnerProgramCompany _partnerProgramCompany = partnerProgramCompanyRepository.GetByFullCompositeKey(_companyID, _partnerProgramID, _campaignID);

            _partnerProgramCompany.IDPartner = pPartnerProgramCompany.IDPartner;
            _partnerProgramCompany.ExpirationDate = pPartnerProgramCompany.ExpirationDate;

            //partnerProgramCompanyRepository.SaveChanges();
        }
    }
}
