using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using Entities;
using AutoMapper;
using IRepositories;

namespace Services
{
    public class ProductFamilyCompanyService : IProductFamilyCompanyService
    {
        protected IProductFamilyCompanyRepository productFamilyCompanyRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public ProductFamilyCompanyService(IProductFamilyCompanyRepository pProductFamilyCompanyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            productFamilyCompanyRepository = pProductFamilyCompanyRepository;
            uow = pUnitOfWork;
        }

        /// <summary>
        ///     Guarda información referente a product family company
        /// </summary>
        /// <param name="entity"></param>
        public void Add(ProductFamilyCompanyDTO entity)
        {
            NS_tblProductFamilyCompany _productFamilyCompany = Mapper.Map<ProductFamilyCompanyDTO, NS_tblProductFamilyCompany>(entity);

            productFamilyCompanyRepository.Add(_productFamilyCompany);

            //productFamilyCompanyRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<ProductFamilyCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductFamilyCompanyDTO> Find(Expression<Func<ProductFamilyCompanyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ProductFamilyCompanyDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductFamilyCompanyDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene una product family company de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductFamilyID">Id de la familia de productos </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public ProductFamilyCompanyDTO GetByCompositeKey(int pCompanyID, short pProductFamilyID, short pCampaignID)
        {
            return Mapper.Map<NS_tblProductFamilyCompany, ProductFamilyCompanyDTO>(productFamilyCompanyRepository.GetByCompositeKey(pCompanyID, pProductFamilyID, pCampaignID));
        }

        /// <summary>
        ///     Obtiene la lista de comentarios adicionales para una familia de productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de de comentarios de productos que posee la compañia en una campaña</returns>
        public IEnumerable<ProductFamilyCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            IEnumerable<NS_tblProductFamilyCompany> _productFamilyCompany = productFamilyCompanyRepository.GetByIDAndCampaign(pCompanyID, pCampaignID);
            IEnumerable<ProductFamilyCompanyDTO> _productFamilyCompanyDTO = Mapper.Map<IEnumerable<NS_tblProductFamilyCompany>, IEnumerable<ProductFamilyCompanyDTO>>(_productFamilyCompany);

            return _productFamilyCompanyDTO;
        }

        public void Remove(ProductFamilyCompanyDTO entity)
        {
            NS_tblProductFamilyCompany current = productFamilyCompanyRepository.GetByCompositeKey(entity.CompanyID, entity.ProductFamilyID, entity.CampaignID);
            productFamilyCompanyRepository.Remove(current);
        }

        public void RemoveRange(IEnumerable<ProductFamilyCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblProductFamilyCompany
        /// </summary>
        /// <param name="pProductFamilyCompany">ViewModel con informacion a actualizar</param>
        public void UpdateProductFamilycompany(ProductFamilyCompanyDTO pProductFamilyCompany)
        {
            int _companyID = pProductFamilyCompany.CompanyID;
            short _campaignID = pProductFamilyCompany.CampaignID;
            short _productFamilyID = pProductFamilyCompany.ProductFamilyID;

            NS_tblProductFamilyCompany _productFamilyCompany = productFamilyCompanyRepository.GetByCompositeKey(_companyID, _productFamilyID, _campaignID);

            _productFamilyCompany.AdditionalComments = pProductFamilyCompany.AdditionalComments;

            //productFamilyCompanyRepository.SaveChanges();
        }
    }
}
