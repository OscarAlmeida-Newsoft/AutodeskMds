namespace Services
{
    using IServices;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using IRepositories;
    using DTOs;
    using AutoMapper;

    public class ProductService : IProductService
    {
        protected IProductRepository productRepository;
        protected IUnitOfWork.IUnitOfWork uow;
        public ProductService(IProductRepository pProductRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            productRepository = pProductRepository;
            uow = pUnitOfWork;
        }

        public void Add(ProductDTO pProducto)
        {            
            NS_tblProduct _producto = Mapper.Map<ProductDTO, NS_tblProduct>(pProducto);
            //_producto.ProductID = 416;
            productRepository.Add(_producto);

            productRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<ProductDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDTO> Find(Expression<Func<ProductDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ProductDTO Get(int id)
        {
            return Mapper.Map<NS_tblProduct, ProductDTO>(productRepository.Get(id));
        }

        public IEnumerable<ProductDTO> GetByFamilyID(int id)
        {
            return Mapper.Map<IEnumerable<NS_tblProduct>, IEnumerable<ProductDTO>>(productRepository.GetByFamilyID(id));
        }

        /// <summary>
        ///  Obtiene la lista de los productos
        /// </summary>
        /// <returns>Lista con la información de los productos</returns>
        public IEnumerable<ProductDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblProduct>,IEnumerable<ProductDTO>>(productRepository.GetAll());
        }

        public void Remove(ProductDTO entity)
        {
            NS_tblProduct _product = Mapper.Map<ProductDTO, NS_tblProduct>(entity);

            var product = productRepository.Get(_product.ProductID);
            productRepository.Remove(product);

            productRepository.SaveChanges();
        }

        public void RemoveRange(IEnumerable<ProductDTO> entities)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            uow.Dispose();
        }

        /// <summary>
        /// Actualiza una fila de la tabla de productos
        /// </summary>
        /// <param name="pProductDTO">Id del producto</param>
        public void UpdateProduct(ProductDTO pProductDTO)
        {

            NS_tblProduct _product = productRepository.Get(pProductDTO.ProductID);

            _product.ProductName = pProductDTO.ProductName;
            _product.ProductVersion = pProductDTO.ProductVersion;
            _product.ProductFamilyID = pProductDTO.ProductFamilyID;
            _product.ProductVersionGroup = pProductDTO.ProductVersionGroup;
            _product.OEMFlag = pProductDTO.OEMFlag;
            _product.IsActive = pProductDTO.IsActive;
            _product.OrderVersion = pProductDTO.OrderVersion;

            productRepository.SaveChanges();
        }
    }
}
