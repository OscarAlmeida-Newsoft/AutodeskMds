using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LanguageRepository : Repository<NS_tblLanguage>, ILanguageRepository
    {
        public LanguageRepository(AffidavitContext context) : base(context)
        {

        }
    }
}
