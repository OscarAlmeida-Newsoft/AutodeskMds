using AutoMapper;
using DTOs;
using Entities;
using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository problemRepository;

        public ProblemService(IProblemRepository pProblemRepository)
        {
            problemRepository = pProblemRepository;
        }

        public void DeleteProblem(int IdProblem)
        {
            problemRepository.DeleteProblem(IdProblem);
        }

        public IEnumerable<ProblemDTO> GetAllProblemByProcess(int ProcessId)
        {
            IEnumerable<NS_TblProblem> pr = problemRepository.GetAllProblemByProcess(ProcessId);
            IEnumerable<ProblemDTO> data = Mapper.Map<IEnumerable<NS_TblProblem>, IEnumerable<ProblemDTO>>(pr);

            return data.OrderBy(x => x.Priority);
        }

        public ProblemDTO GetProblemById(int IdProblem)
        {
            NS_TblProblem problem = problemRepository.GetProblemById(IdProblem);
            ProblemDTO theProblem = Mapper.Map<NS_TblProblem, ProblemDTO>(problem);
            return theProblem;
        }

        public void InsertProblem(ProblemDTO newProblem)
        {
            NS_TblProblem newP = Mapper.Map<ProblemDTO, NS_TblProblem>(newProblem);
            problemRepository.InsertProblem(newP);
        }

        public void UpdateProblem(ProblemDTO problem)
        {
            NS_TblProblem theProblem = Mapper.Map<ProblemDTO, NS_TblProblem>(problem);
            problemRepository.UpdateProblem(theProblem);
        }
    }
}
