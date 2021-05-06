using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IProblemRepository
    {
        void InsertProblem(NS_TblProblem newProblem);
        IEnumerable<NS_TblProblem> GetAllProblemByProcess(int ProcessId);
        void UpdateProblem(NS_TblProblem problem);
        void DeleteProblem(int IdProblem);
        void DeleteProblemsByIdProcess(int IdProcess);
        NS_TblProblem GetProblemById(int IdProblem);
    }
}
