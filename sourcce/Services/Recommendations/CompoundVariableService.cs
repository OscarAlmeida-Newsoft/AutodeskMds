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
    public class CompoundVariableService : ICompoundVariableService
    {
        protected ICompoundVariableRepository compoundVariableRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public CompoundVariableService(ICompoundVariableRepository pCompoundVariableRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            compoundVariableRepository = pCompoundVariableRepository;
            uow = pUnitOfWork;
        }

        public void Add(CompoundVariableDTO entity)
        {
            compoundVariableRepository.Add(Mapper.Map<CompoundVariableDTO, NS_tblCompoundVariable>(entity));
            compoundVariableRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<CompoundVariableDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompoundVariableDTO> Find(Expression<Func<CompoundVariableDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CompoundVariableDTO Get(int id)
        {
            return Mapper.Map<NS_tblCompoundVariable, CompoundVariableDTO>(compoundVariableRepository.Get(id));
        }

        /// <summary>
        ///  Obtiene la lista de las variables
        /// </summary>
        /// <returns>Lista con la información de las variables</returns>
        public IEnumerable<CompoundVariableDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblCompoundVariable>,IEnumerable<CompoundVariableDTO>>(compoundVariableRepository.GetAll());
        }

        public IEnumerable<CompoundVariableDTO> GetAllByVariableID(int pVariableID)
        {
            return Mapper.Map<IEnumerable<NS_tblCompoundVariable>, IEnumerable<CompoundVariableDTO>>(compoundVariableRepository.GetByVariableID(pVariableID));
        }

        public CompoundVariableDTO GetByVariableIDAndAssociatedVariable(int pVariableID, int AssociatedVariableID)
        {
            return Mapper.Map<NS_tblCompoundVariable,CompoundVariableDTO>(compoundVariableRepository.GetByVariableIDAndAssociatedVariable(pVariableID, AssociatedVariableID));
        }

        public void Remove(CompoundVariableDTO entity)
        {
            //NS_tblCompoundVariable _compoundVariable = Mapper.Map<CompoundVariableDTO, NS_tblCompoundVariable>(entity);
            var variable = compoundVariableRepository.Get(entity.CompoundVariableExpressionID);
            compoundVariableRepository.Remove(variable);
        }

        public void RemoveRange(IEnumerable<CompoundVariableDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
