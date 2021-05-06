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
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository processRepository;
        private readonly IProblemRepository problemRepisotory;
        private readonly IDigitalTransformationRepository digitalTransformationRepository;

        public ProcessService(IProcessRepository pProcessRepository,
            IProblemRepository pProblemRepository,
            IDigitalTransformationRepository pDigitalTransformationRepository)
        {
            processRepository = pProcessRepository;
            problemRepisotory = pProblemRepository;
            digitalTransformationRepository = pDigitalTransformationRepository;
        }

        public void DeleteProcess(int IdProcess)
        {
            //Elimino los problemas asociados
            problemRepisotory.DeleteProblemsByIdProcess(IdProcess);

            //Elimino las tranformaciones digitales
            digitalTransformationRepository.DeleteDigitalTransformationsByProcess(IdProcess);

            //Elimino el proceso
            processRepository.DeleteProcess(IdProcess);
        }

        public IEnumerable<ProcessDTO> GetProcessesByActivity(int pActivityId, int pAssessmentSummaryId)
        {
            IEnumerable<ProcessDTO> _data = null;
            var items = processRepository.GetProcessesByActivity(pActivityId, pAssessmentSummaryId);

            if (items != null)
            {
                _data = Mapper.Map<IEnumerable<NS_TblProcess>, IEnumerable<ProcessDTO>>(items);
            }
            return _data;
        }

        public void InsertProcess(ProcessDTO newProcess)
        {
            NS_TblProcess newP = Mapper.Map<ProcessDTO, NS_TblProcess>(newProcess);
            processRepository.InsertProcess(newP);

        }

        public void UpdateProcess(ProcessDTO process)
        {
            NS_TblProcess theProcess = Mapper.Map<ProcessDTO, NS_TblProcess>(process);
            processRepository.UpdateProcess(theProcess);
        }

        public ProcessDTO GetProcessById(int pId)
        {
            ProcessDTO _data = Mapper.Map<NS_TblProcess, ProcessDTO>(processRepository.GetProcessById(pId));
            return _data;
        }
    }
}
