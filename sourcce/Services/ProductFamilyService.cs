
namespace Services
{
    using AutoMapper;
    using DTOs;
    using Entities;
    using IRepositories;
    using IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductFamilyService : IProductFamilyService
    {
        protected IProductFamilyRepository productFamilyRepository;
        protected IUnitOfWork.IUnitOfWork uow;
        public ProductFamilyService(IProductFamilyRepository pProductFamilyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            productFamilyRepository = pProductFamilyRepository;
            uow = pUnitOfWork;
        }

        public void Add(ProductFamilyDTO pProductoFamily)
        {
            //NS_tblProduct _producto = Mapper.Map<ProductDTO, NS_tblProduct>(pProducto);
            //_producto.ProductID = 416;
            //productRepository.Add(_producto);

            //var _res =productRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<ProductFamilyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductFamilyDTO> Find(Expression<Func<ProductFamilyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ProductFamilyDTO Get(int id)
        {
            return Mapper.Map<NS_tblProductFamily, ProductFamilyDTO>(productFamilyRepository.Get(id));
        }

        public IEnumerable<ProductFamilyDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblProductFamily>, IEnumerable<ProductFamilyDTO>>(productFamilyRepository.GetAll());
        }

        public void Remove(ProductFamilyDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<ProductFamilyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
