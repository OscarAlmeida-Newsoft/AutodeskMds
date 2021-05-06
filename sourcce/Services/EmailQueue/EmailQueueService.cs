using IServices.EmailQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.EmailQueueDTO;
using System.Linq.Expressions;
using IRepositories.EmailQueue;
using Entities.NS_tblEmailQueue;
using AutoMapper;

namespace Services.EmailQueue
{
    public class EmailQueueService : IEmailQueueService
    {
        protected IEmailQueueRepository emailQueueRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public EmailQueueService(IEmailQueueRepository pEmailQueueRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            emailQueueRepository = pEmailQueueRepository;
            uow = pUnitOfWork;
        }


        public void Add(EmailQueueDTO entity)
        {
            NS_tblEmailQueue _emailQueue = Mapper.Map<EmailQueueDTO, NS_tblEmailQueue>(entity);
            emailQueueRepository.Add(_emailQueue);
            uow.Complete();
        }

        public void AddRange(IEnumerable<EmailQueueDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmailQueueDTO> Find(Expression<Func<EmailQueueDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public EmailQueueDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmailQueueDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(EmailQueueDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<EmailQueueDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
