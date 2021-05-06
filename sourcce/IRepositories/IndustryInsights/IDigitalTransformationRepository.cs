using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IDigitalTransformationRepository
    {
        void InsertDigitalTransformation(NS_TblDigitalTransformation newDigitalTransformation);
        IEnumerable<NS_TblDigitalTransformation> GetAllDigitalTransformationByProcess(int ProcessId);
        void UpdateDigitalTransformation(NS_TblDigitalTransformation digitalTransformation);
        void DeleteDigitalTransformation(int IdDigitalTransformation);
        void DeleteDigitalTransformationsByProcess(int ProcessId);
        NS_TblDigitalTransformation GetDigitalTransformationById(int IdDigitalTransformation);
    }
}
