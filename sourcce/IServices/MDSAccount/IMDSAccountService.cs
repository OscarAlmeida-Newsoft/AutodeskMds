using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IMDSAccountService
    {
        MDSAccountDTO GetMDSAccountInformation(Guid pLeadId);
    }
}
