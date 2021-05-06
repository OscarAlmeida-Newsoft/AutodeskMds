using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface ICompanyContactsRepository : IRepository<NS_tblCompanyContacts>
    {
        /// <summary>
        /// obtiene información general de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        NS_tblCompanyContacts GetByIDandCampaign(int pCompanyID, short pCampaignID);
    }
}
