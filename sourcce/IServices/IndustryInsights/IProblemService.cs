using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IProblemService
    {
        IEnumerable<ProblemDTO> GetAllProblemByProcess(int ProcessId);
        void InsertProblem(ProblemDTO newProblem);
        void UpdateProblem(ProblemDTO problem);
        void DeleteProblem(int IdProblem);
        ProblemDTO GetProblemById(int IdProblem);
    }
}
