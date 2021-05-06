using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IProblemPreLoadedRepository
    {
        IEnumerable<NS_TblProblemPreLoaded> GetAllProblemPreLoadedByProcess(int ProcessPreLoadedId);
    }
}
