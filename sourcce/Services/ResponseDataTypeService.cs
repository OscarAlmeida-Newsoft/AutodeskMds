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

namespace Services
{
    public class ResponseDataTypeService : IResponseDataTypeService
    {
        protected IResponseDataTypeRepository responseDataTypeRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public ResponseDataTypeService(IResponseDataTypeRepository pResponseDataTypeRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            responseDataTypeRepository = pResponseDataTypeRepository;
            uow = pUnitOfWork;
        }

        public void Add(ResponseDataTypeDTO entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<ResponseDataTypeDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ResponseDataTypeDTO> Find(Expression<Func<ResponseDataTypeDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ResponseDataTypeDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Obtiene los tipos de datos para las preguntas de SQL Server y Windows Server.
        /// </summary>
        /// <returns>Una lista con los tipos de datos</returns>
        public IEnumerable<ResponseDataTypeDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblResponseDataType>, IEnumerable<ResponseDataTypeDTO>>(responseDataTypeRepository.GetAll());
        }

        public void Remove(ResponseDataTypeDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<ResponseDataTypeDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
