using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices;
using IRepositories;
using DTOs.EmailQueueDTO;
using IRepositories.EmailQueue;
using AutoMapper;
using Entities.NS_tblEmailQueue;

namespace Services
{
    public class SMTPService : ISMTPService
    {
        protected IUnitOfWork.IUnitOfWork unitOfWork;
        readonly ISMTPRepository SMTPRepository;
        readonly IEmailQueueRepository emailQueueRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pUnitOfWork"></param>
        /// <param name="pSMTPRepository"></param>
        public SMTPService(IUnitOfWork.IUnitOfWork pUnitOfWork, ISMTPRepository pSMTPRepository, IEmailQueueRepository pEmailQueueRepository)
        {
            unitOfWork = pUnitOfWork;
            SMTPRepository = pSMTPRepository;
            emailQueueRepository = pEmailQueueRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTo"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        public string EnviarCorreo(string pTo, string pSubject, string pBody)
        {
            string mensaje = SMTPRepository.EnviarCorreo(pTo, pSubject, pBody);

            if (mensaje != "")
            {
                EmailQueueDTO _emailQueueDTO = new EmailQueueDTO();
                _emailQueueDTO.ToEmail = pTo;
                _emailQueueDTO.FromEmail = mensaje;
                _emailQueueDTO.SubjectEmail = pSubject;
                _emailQueueDTO.BodyEmail = pBody;

                NS_tblEmailQueue _emailQueue = Mapper.Map<EmailQueueDTO, NS_tblEmailQueue>(_emailQueueDTO);

                emailQueueRepository.Add(_emailQueue);
            }

            return mensaje;
        }
    }
}
