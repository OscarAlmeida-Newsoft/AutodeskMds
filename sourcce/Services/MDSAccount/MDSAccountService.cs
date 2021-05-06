using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using IServices;

namespace Services
{
    public class MDSAccountService : IMDSAccountService
    {
        readonly ILeadService leadService;

        public MDSAccountService(ILeadService pLeadService)
        {
            leadService = pLeadService;
        }
        public MDSAccountDTO GetMDSAccountInformation(Guid pLeadId)
        {
            var _leadInfo = leadService.GetByLeadID(pLeadId);

            MDSAccountDTO _data = new MDSAccountDTO()
            {
                AccountUserName = _leadInfo.CompanySAMLiveUserName,
                CompanyContact = _leadInfo.LeadUser,
                CompanyName = _leadInfo.CompanyName,
                ConsultantContact = _leadInfo.MicrosoftConsultantEmail,
                ConsultantEmail = _leadInfo.MicrosoftConsultantEmail,
                SubscriptionDate = _leadInfo.CreateDate

            };

            return _data;
        }
    }
}
