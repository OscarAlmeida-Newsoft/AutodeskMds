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
    public class PartnerCapabilityCompanyService : IPartnerCapabilityCompanyService
    {
        protected IPartnerCapabilityCompanyRepository partnerCapabilityCompanyRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public PartnerCapabilityCompanyService(IPartnerCapabilityCompanyRepository pPartnerCapabilityCompanyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            partnerCapabilityCompanyRepository = pPartnerCapabilityCompanyRepository;
            uow = pUnitOfWork;
        }

        /// <summary>
        ///     guarda la info de las competencias de un partner
        /// </summary>
        /// <param name="entity"></param>
        public void Add(PartnerCapabilityCompanyDTO entity)
        {
            NS_tblPartnerCapabilityCompany _partnerCapabilityCompany = Mapper.Map<PartnerCapabilityCompanyDTO, NS_tblPartnerCapabilityCompany>(entity);

            partnerCapabilityCompanyRepository.Add(_partnerCapabilityCompany);

            //partnerCapabilityCompanyRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<PartnerCapabilityCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<PartnerCapabilityCompanyDTO> Find(Expression<Func<PartnerCapabilityCompanyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public PartnerCapabilityCompanyDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerCapabilityCompanyDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene los competencias de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public IEnumerable<PartnerCapabilityCompanyDTO> GetByCompositeKey(int pCompanyID, short pCampaignID)
        {
            IEnumerable <NS_tblPartnerCapabilityCompany> _partnerCapabilityCompany = partnerCapabilityCompanyRepository.GetByCompositeKey(pCompanyID,pCampaignID);
            IEnumerable<PartnerCapabilityCompanyDTO> _partnerCapabilityCompanyDTO = Mapper.Map<IEnumerable<NS_tblPartnerCapabilityCompany>, IEnumerable<PartnerCapabilityCompanyDTO>>(_partnerCapabilityCompany);

            return _partnerCapabilityCompanyDTO;
        }

        /// <summary>
        ///     Obtiene una capability de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pPartnerCapabilityID">Id del Partner Capability </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public PartnerCapabilityCompanyDTO GetByFullCompositeKey(int pCompanyID, short pPartnerCapabilityID, short pCampaignID)
        {
            return Mapper.Map<NS_tblPartnerCapabilityCompany, PartnerCapabilityCompanyDTO>(partnerCapabilityCompanyRepository.GetByFullCompositeKey(pCompanyID, pPartnerCapabilityID, pCampaignID));
        }

        public void Remove(PartnerCapabilityCompanyDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<PartnerCapabilityCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblPartnerCapabilityCompany
        /// </summary>
        /// <param name="pPartnerCapabilityCompany">ViewModel con informacion a actualizar</param>
        public void UpdatePartnerCapabilitycompany(PartnerCapabilityCompanyDTO pPartnerCapabilityCompany)
        {
            int _companyID = pPartnerCapabilityCompany.CompanyID;
            short _campaignID = pPartnerCapabilityCompany.CampaignID;
            short _partnerCapabilityID = pPartnerCapabilityCompany.PartnerCapabilityID;

            NS_tblPartnerCapabilityCompany _partnerCapabilityCompany = partnerCapabilityCompanyRepository.GetByFullCompositeKey(_companyID, _partnerCapabilityID, _campaignID);

            _partnerCapabilityCompany.IDPartner = pPartnerCapabilityCompany.IDPartner;
            _partnerCapabilityCompany.RenovationDate = pPartnerCapabilityCompany.RenovationDate;
            _partnerCapabilityCompany.Category = pPartnerCapabilityCompany.Category;

            //partnerCapabilityCompanyRepository.SaveChanges();
        }
    }
}
