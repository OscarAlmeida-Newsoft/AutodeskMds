using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IProcessService
    {
        IEnumerable<ProcessDTO> GetProcessesByActivity(int pActivityId, int pAssessmentSummaryId);
        void InsertProcess(ProcessDTO newProcess);
        void UpdateProcess(ProcessDTO process);
        void DeleteProcess(int IdProcess);
        ProcessDTO GetProcessById(int pId);
    }
}
