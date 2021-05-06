using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using IRepositories;
using Entities;
using AutoMapper;

namespace Services
{
    public class PartnerCapabilityService : IPartnerCapabilityService
    {
        protected IPartnerCapabilityRepository partnerCapabilityRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public PartnerCapabilityService(IPartnerCapabilityRepository pPartnerCapabilityRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            partnerCapabilityRepository = pPartnerCapabilityRepository;
            uow = pUnitOfWork;
        }

        public void Add(PartnerCapabilityDTO entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<PartnerCapabilityDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerCapabilityDTO> Find(Expression<Func<PartnerCapabilityDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public PartnerCapabilityDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PartnerCapabilityDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblPartnerCapability>, IEnumerable<PartnerCapabilityDTO>>(partnerCapabilityRepository.GetAll());
        }

        public void Remove(PartnerCapabilityDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<PartnerCapabilityDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
