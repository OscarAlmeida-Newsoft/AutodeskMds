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
    public class ProductCompanyService : IProductCompanyService
    {
        protected IProductCompanyRepository productCompanyRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public ProductCompanyService(IProductCompanyRepository pProductCompanyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            productCompanyRepository = pProductCompanyRepository;
            uow = pUnitOfWork;
        }

        public void Add(ProductCompanyDTO entity)
        {
            NS_tblProductCompany _partnerProgramCompany = Mapper.Map<ProductCompanyDTO, NS_tblProductCompany>(entity);

            productCompanyRepository.Add(_partnerProgramCompany);

            //productCompanyRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<ProductCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductCompanyDTO> Find(Expression<Func<ProductCompanyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ProductCompanyDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene la lista de productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de productos que posee la compañia en una campaña</returns>
        public IEnumerable<ProductCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            return Mapper.Map<IEnumerable<NS_tblProductCompany>, IEnumerable<ProductCompanyDTO>>(productCompanyRepository.GetByIDAndCampaign(pCompanyID, pCampaignID));
        }

        /// <summary>
        ///     Obtiene todos los productos de la tabla NS_tblProductCompany
        /// </summary>
        /// <returns>lista con todos los products company</returns>
        public IEnumerable<ProductCompanyDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblProductCompany>, IEnumerable<ProductCompanyDTO>>(productCompanyRepository.GetAll());
        }

        public void Remove(ProductCompanyDTO entity)
        {
            NS_tblProductCompany _product = Mapper.Map<ProductCompanyDTO, NS_tblProductCompany>(entity);

            var _productResult = productCompanyRepository.Find(x => x.ProductID == _product.ProductID &&
                                                                x.CompanyID == _product.CompanyID &&
                                                                x.CampaignID == _product.CampaignID).FirstOrDefault();
            productCompanyRepository.Remove(_productResult);

            //productCompanyRepository.SaveChanges();
        }

        public void RemoveRange(IEnumerable<ProductCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene una product company de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductID">Id del producto </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public ProductCompanyDTO GetByFullCompositeKey(int pCompanyID, short pProductID, short pCampaignID)
        {
            NS_tblProductCompany _productCompany = productCompanyRepository.GetByFullCompositeKey(pCompanyID, pProductID, pCampaignID);
            ProductCompanyDTO _productCompanyDTO = Mapper.Map<NS_tblProductCompany, ProductCompanyDTO>(_productCompany);

            return _productCompanyDTO;
        }

        /// <summary>
        ///     Actualiza un campo de la tabla NS_tblProductCompany
        /// </summary>
        /// <param name="pProductCompany">ViewModel con informacion a actualizar</param>
        public void UpdateProductcompany(ProductCompanyDTO pProductCompany)
        {
            int _companyID = pProductCompany.CompanyID;
            short _campaignID = pProductCompany.CampaignID;
            short _productID = pProductCompany.ProductID;

            NS_tblProductCompany _productCompany = productCompanyRepository.GetByFullCompositeKey(_companyID, _productID, _campaignID);

            _productCompany.InstalledLicenses = pProductCompany.InstalledLicenses;
            _productCompany.VLS = pProductCompany.VLS;
            _productCompany.FPP = pProductCompany.FPP;
            _productCompany.OEM = pProductCompany.OEM;
            _productCompany.WEB = pProductCompany.WEB;
            _productCompany.Agreement = pProductCompany.Agreement;

            //productCompanyRepository.SaveChanges();
        }

        /// <summary>
        ///     Actualiza
        /// </summary>
        /// <param name="model"></param>
        //public void UpdateLicenses(List<ProductCompanyDTO> model)
        //{

        //}
    }
}
