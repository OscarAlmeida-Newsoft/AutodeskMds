using IServices;
using System;
using System.Collections.Generic;
using DTOs;
using System.Linq.Expressions;
using IRepositories;
using Entities;
using AutoMapper;

namespace Services
{
    public class ProductCompanyFileService : IProductCompanyFileService
    {
        protected IProductCompanyFileRepository productCompanyFileRepository;

        public ProductCompanyFileService(IProductCompanyFileRepository pProductCompanyFileRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            productCompanyFileRepository = pProductCompanyFileRepository;
        }


        public void Add(ProductCompanyFileDTO entity)
        {
            NS_tblProductCompanyFile _productFile = Mapper.Map<ProductCompanyFileDTO, NS_tblProductCompanyFile>(entity);
            productCompanyFileRepository.Add(_productFile);
        }

        public void AddRange(IEnumerable<ProductCompanyFileDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductCompanyFileDTO> Find(Expression<Func<ProductCompanyFileDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ProductCompanyFileDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductCompanyFileDTO> GetAll()
        {
            throw new NotImplementedException();
        }
        public void RemoveRange(IEnumerable<ProductCompanyFileDTO> entities)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///     Obtiene una product company family de una compañia por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pProductID">Id del producto </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// /// <param name="pFileNumber">numero del archivo </param>
        /// <returns></returns>
        public ProductCompanyFileDTO GetByFullCompositeKey(int pCompanyID, short pProductID, short pCampaignID, short pFileNumber)
        {
            return Mapper.Map<NS_tblProductCompanyFile, ProductCompanyFileDTO>(productCompanyFileRepository.GetByFullCompositeKey(pCompanyID, pProductID, pCampaignID, pFileNumber));
        }

        /// <summary>
        ///     Obtiene la lista de imagenes de productos productos para una compañia y una campaña especificas
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña</returns>
        public IEnumerable<ProductCompanyFileDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            return Mapper.Map<IEnumerable<NS_tblProductCompanyFile>, IEnumerable<ProductCompanyFileDTO>>(productCompanyFileRepository.GetByIDAndCampaign(pCompanyID, pCampaignID));
        }

        /// <summary>
        ///     Obtiene la lista de imagenes de productos productos para una compañia, campaña y productos especificos
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña por producto</returns>
        public IEnumerable<ProductCompanyFileDTO> GetByIDAndCampaignAndProduct(int pCompanyID, short pCampaignID, short pProductID)
        {
            return Mapper.Map<IEnumerable<NS_tblProductCompanyFile>, IEnumerable<ProductCompanyFileDTO>>(productCompanyFileRepository.GetByIDAndCampaignAndProduct(pCompanyID, pCampaignID, pProductID));
        }

        /// <summary>
        ///     Obtiene la lista de imagenes de productos productos para una compañia, campaña y productos especificos
        /// </summary>
        /// <param name="entity">datos del archivo que se borrara</param>
        public void Remove(ProductCompanyFileDTO entity)
        {

            NS_tblProductCompanyFile _productFile = productCompanyFileRepository.GetByFullCompositeKey(entity.CompanyID, entity.ProductID, entity.CampaignID, entity.FileNumber);
            if (_productFile!=null)
            {
                productCompanyFileRepository.Remove(_productFile);
            }

        }

        /// <summary>
        ///     Borra la lista de archivos de productos para una compañia, campaña y productos especificos
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        /// <returns>Lista de productos productos que posee la compañia en una campaña por producto</returns>
        public void RemoveByIDAndCampaignAndProduct(int pCompanyID, short pCampaignID, short pProductID)
        {
            IEnumerable<NS_tblProductCompanyFile> _productFiles = productCompanyFileRepository.GetByIDAndCampaignAndProduct(pCompanyID, pCampaignID, pProductID);

            if (_productFiles!=null)
            {
                foreach (NS_tblProductCompanyFile actual in _productFiles)
                {
                    productCompanyFileRepository.Remove(actual);
                }
            }
        }



    }
}
