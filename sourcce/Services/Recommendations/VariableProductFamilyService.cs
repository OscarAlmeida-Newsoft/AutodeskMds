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
    public class VariableProductFamilyService : IVariableProductFamilyService
    {
        protected IVariableProductFamilyRepository variableProductFamilyRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public VariableProductFamilyService(IVariableProductFamilyRepository pVariableProductFamilyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            variableProductFamilyRepository = pVariableProductFamilyRepository;
            uow = pUnitOfWork;
        }

        public void Add(VariableProductFamilyDTO pVariableProductFamily)
        {            
            NS_tblVariableProductFamily _variable = Mapper.Map<VariableProductFamilyDTO, NS_tblVariableProductFamily>(pVariableProductFamily);
            variableProductFamilyRepository.AddVariableProductFamily(_variable);
        }

        public VariableProductFamilyDTO Get(int id)
        {
            return Mapper.Map<NS_tblVariableProductFamily, VariableProductFamilyDTO>(variableProductFamilyRepository.Get(id));
        }

        public IEnumerable<VariableProductFamilyDTO> GetByVariableID(short pVariableID)
        {
            return Mapper.Map<IEnumerable<NS_tblVariableProductFamily>, IEnumerable<VariableProductFamilyDTO>>(variableProductFamilyRepository.GetByVariableID(pVariableID));
        }

        /// <summary>
        ///  Obtiene la lista de las variables
        /// </summary>
        /// <returns>Lista con la información de las variables</returns>
        public IEnumerable<VariableProductFamilyDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblVariableProductFamily>,IEnumerable<VariableProductFamilyDTO>>(variableProductFamilyRepository.GetAll());
        }

        public void Remove(VariableProductFamilyDTO entity)
        {
            var variableProductFamily = variableProductFamilyRepository.Get(entity.VariableProductFamilyID);
            variableProductFamilyRepository.Remove(variableProductFamily);
        }

        public void Dispose()
        {
            uow.Dispose();
        }



        public void AddRange(IEnumerable<VariableProductFamilyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VariableProductFamilyDTO> Find(Expression<Func<VariableProductFamilyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public void RemoveRange(IEnumerable<VariableProductFamilyDTO> entities)
        {
            throw new NotImplementedException();
        }


    }
}
