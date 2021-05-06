using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using IRepositories;
using AutoMapper;
using Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        protected ICompanyRepository companyRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public CompanyService(ICompanyRepository pCompanyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            companyRepository = pCompanyRepository;
            uow = pUnitOfWork;
        }

        /// <summary>
        ///     Guarda una registro en la tabla company
        /// </summary>
        /// <param name="entity"></param>
        public void Add(CompanyDTO entity)
        {
            NS_tblCompany _company = Mapper.Map<CompanyDTO, NS_tblCompany>(entity);

            companyRepository.Add(_company);

            //companyRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<CompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanyDTO> Find(Expression<Func<CompanyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CompanyDTO Get(int id)
        {
            return Mapper.Map<NS_tblCompany, CompanyDTO>(companyRepository.Get(id));
        }

        public IEnumerable<CompanyDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblCompany>, IEnumerable<CompanyDTO>>(companyRepository.GetAll());
        }

        /// <summary>
        /// Obtiene una compañia por el account number
        /// </summary>
        /// <param name="pAccountNumberID">account number id</param>
        /// <returns></returns>
        public CompanyDTO GetCompanyByAccountNumber(string pAccountNumberID)
        {
            NS_tblCompany _company = companyRepository.GetCompanyByAccountNumber(pAccountNumberID);
            CompanyDTO _companyDTO = Mapper.Map<NS_tblCompany, CompanyDTO>(_company);

            return _companyDTO;
        }

        /// <summary>
        /// Obtiene una compañia por el Id
        /// </summary>
        /// <param name="pCompanyID">company id</param>
        /// <returns></returns>
        public CompanyDTO GetCompanyByID(int pCompanyID)
        {
            return Mapper.Map<NS_tblCompany, CompanyDTO>(companyRepository.GetCompanyByID(pCompanyID));
        }

        /// <summary>
        /// Obtiene una compañia por el nombre
        /// </summary>
        /// <param name="pAccountNumberID">Nombre de la compañia</param>
        /// <returns></returns>
        public CompanyDTO GetCompanyByName(string pCompanyName)
        {
            NS_tblCompany _company = companyRepository.GetCompanyByName(pCompanyName);
            CompanyDTO _companyDTO = Mapper.Map<NS_tblCompany, CompanyDTO>(_company);

            return _companyDTO;
        }

        public void Remove(CompanyDTO entity)
        {

                NS_tblCompany _company = companyRepository.Get(entity.CompanyID);
                if (_company != null)
                {
                    companyRepository.Remove(_company);
                }
                //companyRepository.SaveChanges();



            
        }

        public void RemoveRange(IEnumerable<CompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Actualiza la información de un compañia en la tabla NS_tblCompany.
        /// </summary>
        /// <param name="pCompany">Viewmodel con la información a actualizar</param>
        public void UpdateCompany(CompanyDTO pCompanyDTO)
        {
            int _companyID = pCompanyDTO.CompanyID;
            NS_tblCompany _company = companyRepository.Get(_companyID);

            _company.CompanyName = pCompanyDTO.CompanyName;
            _company.IndustryID = pCompanyDTO.IndustryID;
            _company.CompanyTypeID = pCompanyDTO.CompanyTypeID;
            _company.UpdatedFromCRMFlag = pCompanyDTO.UpdatedFromCRMFlag;
            _company.PurchaseAndInvoicingMode = pCompanyDTO.PurchaseAndInvoicingMode;

            //companyRepository.SaveChanges();

        }
    }
}
