using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IProcessPreLoadedRepository
    {
        IEnumerable<NS_TblProcessPreLoaded> GetAllProccessPreLoadedByIndustry(int IdIndustry);
    }
}
