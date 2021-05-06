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
    public class VariableService : IVariableService
    {
        protected IVariableRepository variableRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public VariableService(IVariableRepository pVariableRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            variableRepository = pVariableRepository;
            uow = pUnitOfWork;
        }

        public int AddAndReturnID(VariableDTO pVariable)
        {            
            NS_tblVariable _variable = Mapper.Map<VariableDTO, NS_tblVariable>(pVariable);
            variableRepository.Add(_variable);
            uow.Complete();
            return _variable.VariableID;
        }

        public VariableDTO Get(int id)
        {
            return Mapper.Map<NS_tblVariable, VariableDTO>(variableRepository.Get(id));
        }

        /// <summary>
        ///  Obtiene la lista de las variables
        /// </summary>
        /// <returns>Lista con la información de las variables</returns>
        public IEnumerable<VariableDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblVariable>,IEnumerable<VariableDTO>>(variableRepository.GetAll());
        }

        public void Remove(VariableDTO entity)
        {
            NS_tblVariable _variable = Mapper.Map<VariableDTO, NS_tblVariable>(entity);
            var variable = variableRepository.Get(_variable.VariableID);
            variableRepository.Remove(variable);
        }


        /// <summary>
        /// Actualiza una fila de la tabla de variables
        /// </summary>
        /// <param name="pVariableDTO">Variable</param>
        public void UpdateVariable(VariableDTO pVariableDTO)
        {
            NS_tblVariable _variable = variableRepository.Get(pVariableDTO.VariableID);

            if (pVariableDTO.Type == 0)
            {
                _variable.CustomerVariable = pVariableDTO.CustomerVariable==null?null: pVariableDTO.CustomerVariable;
                _variable.Field = pVariableDTO.Field;
                _variable.IsCommercial = pVariableDTO.IsCommercial;
                _variable.IsCorporate = pVariableDTO.IsCorporate;
                _variable.IsSupported = pVariableDTO.IsSupported;
                _variable.Name = pVariableDTO.Name;
                _variable.Description = pVariableDTO.Description;
                _variable.SelectAllFamilies = pVariableDTO.SelectAllFamilies;
                _variable.SelectAllProducts = pVariableDTO.SelectAllProducts;
                _variable.Selector = pVariableDTO.Selector;
                _variable.Type = pVariableDTO.Type;
            }
            if(pVariableDTO.Type == 1)
            {
                _variable.CustomerVariable = pVariableDTO.CustomerVariable == null ? null : pVariableDTO.CustomerVariable;
                _variable.Field = pVariableDTO.Field;
                _variable.IsCommercial = pVariableDTO.IsCommercial;
                _variable.IsCorporate = pVariableDTO.IsCorporate;
                _variable.IsSupported = pVariableDTO.IsSupported;
                _variable.Name = pVariableDTO.Name;
                _variable.Description = pVariableDTO.Description;
                _variable.SelectAllFamilies = pVariableDTO.SelectAllFamilies;
                _variable.SelectAllProducts = pVariableDTO.SelectAllProducts;
                _variable.Selector = pVariableDTO.Selector;
                _variable.Type = pVariableDTO.Type;
                _variable.MathExpression = pVariableDTO.MathExpression;
                _variable.IsMathExpression = pVariableDTO.IsMathExpression;
            }
        }

        public void Dispose()
        {
            uow.Dispose();
        }



        public void AddRange(IEnumerable<VariableDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VariableDTO> Find(Expression<Func<VariableDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public void RemoveRange(IEnumerable<VariableDTO> entities)
        {
            throw new NotImplementedException();
        }

        public void Add(VariableDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
