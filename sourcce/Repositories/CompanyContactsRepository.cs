using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CompanyContactsRepository : Repository<NS_tblCompanyContacts>, ICompanyContactsRepository
    {
        public CompanyContactsRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        /// obtiene información general de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
       public  NS_tblCompanyContacts GetByIDandCampaign(int pCompanyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblCompanyContacts>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID).FirstOrDefault();
        }

    }
}
