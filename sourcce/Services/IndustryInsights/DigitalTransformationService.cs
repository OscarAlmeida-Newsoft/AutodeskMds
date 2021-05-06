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
    public class DigitalTransformationService : IDigitalTransformationService
    {
        private readonly IDigitalTransformationRepository digitalTransformationRepository;

        public DigitalTransformationService(IDigitalTransformationRepository pDigitalTransformationRepository)
        {
            digitalTransformationRepository = pDigitalTransformationRepository;
        }

        public void DeleteDigitalTransformation(int IdDigitalTransformation)
        {
            digitalTransformationRepository.DeleteDigitalTransformation(IdDigitalTransformation);
        }

        public IEnumerable<DigitalTransformationDTO> GetAllDigitalTransformationByProcess(int ProcessId)
        {
            IEnumerable<NS_TblDigitalTransformation> dt = digitalTransformationRepository.GetAllDigitalTransformationByProcess(ProcessId);
            IEnumerable<DigitalTransformationDTO> data = Mapper.Map<IEnumerable<NS_TblDigitalTransformation>, IEnumerable<DigitalTransformationDTO>>(dt);

            return data.OrderBy(x => x.Priority);
        }

        public DigitalTransformationDTO GetDigitalTransformationById(int IdDigitalTransformation)
        {
            NS_TblDigitalTransformation digitalTransformation = digitalTransformationRepository.GetDigitalTransformationById(IdDigitalTransformation);
            DigitalTransformationDTO theDigitalTransf = Mapper.Map<NS_TblDigitalTransformation, DigitalTransformationDTO>(digitalTransformation);
            return theDigitalTransf;
        }

        public void InsertDigitalTransformation(DigitalTransformationDTO newDigitalTransformation)
        {
            NS_TblDigitalTransformation digitTrans = Mapper.Map<DigitalTransformationDTO, NS_TblDigitalTransformation>(newDigitalTransformation);
            digitalTransformationRepository.InsertDigitalTransformation(digitTrans);
        }

        public void UpdateDigitalTransformation(DigitalTransformationDTO digitalTransformation)
        {
            NS_TblDigitalTransformation digitTrans = Mapper.Map<DigitalTransformationDTO, NS_TblDigitalTransformation>(digitalTransformation);
            digitalTransformationRepository.UpdateDigitalTransformation(digitTrans);
        }
    }
}
