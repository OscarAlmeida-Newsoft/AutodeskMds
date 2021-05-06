using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IProcessRepository
    {
        int InsertProcess(NS_TblProcess newProcess);

        IEnumerable<NS_TblProcess> GetProcessesByActivity(int pActivityId, int pAssessmentSummaryId);

        void UpdateProcess(NS_TblProcess process);

        void DeleteProcess(int IdProcess);

        NS_TblProcess GetProcessById(int pId);

        IEnumerable<NS_TblProcess> GetProcessesByAssesmentSummaryId(int pAssessmentSummaryId);
    }
}
