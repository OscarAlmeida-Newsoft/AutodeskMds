using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using Entities;
using AutoMapper;
using IRepositories;

namespace Services
{
    public class PartnerProgramService : IPartnerProgramService
    {
        protected IPartnerProgramRepository partnerProgramRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public PartnerProgramService(IPartnerProgramRepository pPartnerProgramRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            partnerProgramRepository = pPartnerProgramRepository;
            uow = pUnitOfWork;
        }

        public void Add(PartnerProgramDTO entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<PartnerProgramDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerProgramDTO> Find(Expression<Func<PartnerProgramDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public PartnerProgramDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerProgramDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblPartnerProgram>, IEnumerable<PartnerProgramDTO>>(partnerProgramRepository.GetAll());
        }

        public void Remove(PartnerProgramDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<PartnerProgramDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
