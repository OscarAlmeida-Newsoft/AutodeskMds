using IServices;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IRepositories;
using DTOs;
using AutoMapper;
using Entities.Recommendations;

namespace Services
{
    public class VariableProductService : IVariableProductService
    {
        protected IVariableProductRepository variableProductRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public VariableProductService(IVariableProductRepository pVariableProductRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            variableProductRepository = pVariableProductRepository;
            uow = pUnitOfWork;
        }

        public void Add(VariableProductDTO pVariableProduct)
        {            
            NS_tblVariableProduct _variableProduct = Mapper.Map<VariableProductDTO, NS_tblVariableProduct>(pVariableProduct);
            variableProductRepository.AddVariableProduct(_variableProduct);
        }

        public VariableProductDTO Get(int id)
        {
            return Mapper.Map<NS_tblVariableProduct, VariableProductDTO>(variableProductRepository.Get(id));
        }

        public IEnumerable<VariableProductDTO> GetByVariableID(short pVariableID)
        {
            return Mapper.Map<IEnumerable<NS_tblVariableProduct>, IEnumerable<VariableProductDTO>>(variableProductRepository.GetByVariableID(pVariableID));
        }

        /// <summary>
        ///  Obtiene la lista de las variables
        /// </summary>
        /// <returns>Lista con la información de las variables</returns>
        public IEnumerable<VariableProductDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblVariableProduct>,IEnumerable<VariableProductDTO>>(variableProductRepository.GetAll());
        }

        public void Remove(VariableProductDTO entity)
        {
            var variable = variableProductRepository.Get(entity.VariableProductID);
            variableProductRepository.Remove(variable);
        }

        public void Dispose()
        {
            uow.Dispose();
        }



        public void AddRange(IEnumerable<VariableProductDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VariableProductDTO> Find(Expression<Func<VariableProductDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public void RemoveRange(IEnumerable<VariableProductDTO> entities)
        {
            throw new NotImplementedException();
        }


    }
}
