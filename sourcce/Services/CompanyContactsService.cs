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
    public class CompanyContactsService : ICompanyContactsService
    {
        protected ICompanyContactsRepository companyContactsRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public CompanyContactsService(ICompanyContactsRepository pCompanyContactsRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            companyContactsRepository = pCompanyContactsRepository;
            uow = pUnitOfWork;
        }

        /// <summary>
        ///     Guarda un registro en la tabla CompanyContacts
        /// </summary>
        /// <param name="entity"></param>
        public void Add(CompanyContactsDTO entity)
        {
            NS_tblCompanyContacts _companycontacts = Mapper.Map<CompanyContactsDTO, NS_tblCompanyContacts>(entity);

            companyContactsRepository.Add(_companycontacts);

            //companyContactsRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<CompanyContactsDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanyContactsDTO> Find(Expression<Func<CompanyContactsDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CompanyContactsDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanyContactsDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene la información general de una compañia
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public CompanyContactsDTO GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            return Mapper.Map<NS_tblCompanyContacts, CompanyContactsDTO>(companyContactsRepository.GetByIDandCampaign(pCompanyID, pCampaignID));
        }

        public void Remove(CompanyContactsDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CompanyContactsDTO> entities)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Actualiza la información general de un compañia.
        /// </summary>
        /// <param name="pCompanyContacts">Viewmodel con la información a actualizar</param>
        public void UpdateCompanyContacts(CompanyContactsDTO pCompanyContacts)
        {
            int _companyID = pCompanyContacts.CompanyID;
            short _campaignID = pCompanyContacts.CampaignID;

            NS_tblCompanyContacts _companyContacts = companyContactsRepository.GetByIDandCampaign(_companyID, _campaignID);

            _companyContacts.ContactName = pCompanyContacts.ContactName;
            _companyContacts.CompanyEmail = pCompanyContacts.CompanyEmail;
            _companyContacts.CompanyPhone = pCompanyContacts.CompanyPhone;

            //companyContactsRepository.SaveChanges();
            //uow.Complete();
        }
    }
}
