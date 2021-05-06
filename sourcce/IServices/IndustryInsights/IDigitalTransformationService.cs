using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IDigitalTransformationService
    {
        IEnumerable<DigitalTransformationDTO> GetAllDigitalTransformationByProcess(int ProcessId);
        void InsertDigitalTransformation(DigitalTransformationDTO newDigitalTransformation);
        void UpdateDigitalTransformation(DigitalTransformationDTO digitalTransformation);
        void DeleteDigitalTransformation(int IdDigitalTransformation);
        DigitalTransformationDTO GetDigitalTransformationById(int IdDigitalTransformation);
    }
}
