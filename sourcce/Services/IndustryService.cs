using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using AutoMapper;
using Entities;
using IRepositories;

namespace Services
{
    public class IndustryService : IIndustryService
    {
        protected IIndustryRepository industryRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public IndustryService(IIndustryRepository pIndustryctRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            industryRepository = pIndustryctRepository;
            uow = pUnitOfWork;
        }

        public void Add(IndustryDTO entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<IndustryDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndustryDTO> Find(Expression<Func<IndustryDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IndustryDTO Get(int id)
        {
            return Mapper.Map<NS_tblIndustry, IndustryDTO>(industryRepository.Get(id));
        }

        /// <summary>
        ///     Obtiene la lista de industrias
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IndustryDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblIndustry>, IEnumerable<IndustryDTO>>(industryRepository.GetAll()); ;
        }

        /// <summary>
        ///     Obtiene un registro de la tabla industry por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        public IndustryDTO GetByName(string pName)
        {
            NS_tblIndustry _industry = industryRepository.GetByName(pName);
            IndustryDTO _industryDTO = Mapper.Map<NS_tblIndustry, IndustryDTO>(_industry);

            return _industryDTO;
        }

        public void Remove(IndustryDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<IndustryDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
