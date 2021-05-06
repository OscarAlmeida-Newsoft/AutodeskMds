using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DigitalTransformationRepository : IDigitalTransformationRepository
    {
        private AffidavitContext dbContext;

        public DigitalTransformationRepository(AffidavitContext context)
        {
            dbContext = context;
        }

        public void DeleteDigitalTransformation(int IdDigitalTransformation)
        {
            IEnumerable<NS_TblDigitalTransformationVersions> versions = dbContext.NS_tblDigitalTransformationVersions.Where(d => d.DigitalTransformationId == IdDigitalTransformation);
            dbContext.NS_tblDigitalTransformationVersions.RemoveRange(versions);

            NS_TblDigitalTransformation digitalTransformation = dbContext.NS_tblDigitalTransformation.SingleOrDefault(d => d.Id == IdDigitalTransformation);
            dbContext.NS_tblDigitalTransformation.Remove(digitalTransformation);
            dbContext.SaveChanges();
        }

        public void DeleteDigitalTransformationsByProcess(int ProcessId)
        {
            IEnumerable<NS_TblDigitalTransformation> digitalTransformations = GetAllDigitalTransformationByProcess(ProcessId);

            foreach (NS_TblDigitalTransformation dt in digitalTransformations)
            {
                IEnumerable<NS_TblDigitalTransformationVersions> versions = dbContext.NS_tblDigitalTransformationVersions.Where(d => d.DigitalTransformationId == dt.Id);
                dbContext.NS_tblDigitalTransformationVersions.RemoveRange(versions);
            }

            dbContext.NS_tblDigitalTransformation.RemoveRange(digitalTransformations);
            dbContext.SaveChanges();
        }

        public IEnumerable<NS_TblDigitalTransformation> GetAllDigitalTransformationByProcess(int ProcessId)
        {
            IEnumerable<NS_TblDigitalTransformation> data = dbContext.NS_tblDigitalTransformation.Where(d => d.ProcessId == ProcessId);

            return data;
        }

        public NS_TblDigitalTransformation GetDigitalTransformationById(int IdDigitalTransformation)
        {
            return dbContext.NS_tblDigitalTransformation.SingleOrDefault(d => d.Id == IdDigitalTransformation);
        }

        public void InsertDigitalTransformation(NS_TblDigitalTransformation newDigitalTransformation)
        {
            dbContext.NS_tblDigitalTransformation.Add(newDigitalTransformation);
            dbContext.SaveChanges();

            NS_TblDigitalTransformationVersions version = new NS_TblDigitalTransformationVersions();
            version.DigitalTransformationId = newDigitalTransformation.Id;
            version.ProcessId = newDigitalTransformation.ProcessId;
            version.Pillar = newDigitalTransformation.Pillar;
            version.Technology = newDigitalTransformation.Technology;
            version.Brand = newDigitalTransformation.Brand;
            version.Comment = newDigitalTransformation.Comment;
            version.Priority = newDigitalTransformation.Priority;
            version.Exist = newDigitalTransformation.Exist;
            version.User = newDigitalTransformation.UserCreation;
            version.Date = newDigitalTransformation.DateCreation;
            version.Solution = newDigitalTransformation.Solution;
            version.Solved = newDigitalTransformation.Solved;

            dbContext.NS_tblDigitalTransformationVersions.Add(version);
            dbContext.SaveChanges();
        }

        public void UpdateDigitalTransformation(NS_TblDigitalTransformation digitalTransformation)
        {
            dbContext.NS_tblDigitalTransformation.Attach(digitalTransformation);
            dbContext.Entry(digitalTransformation).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();

            NS_TblDigitalTransformationVersions version = new NS_TblDigitalTransformationVersions();
            version.DigitalTransformationId = digitalTransformation.Id;
            version.ProcessId = digitalTransformation.ProcessId;
            version.Pillar = digitalTransformation.Pillar;
            version.Technology = digitalTransformation.Technology;
            version.Brand = digitalTransformation.Brand;
            version.Comment = digitalTransformation.Comment;
            version.Priority = digitalTransformation.Priority;
            version.Exist = digitalTransformation.Exist;
            version.User = digitalTransformation.UserLastModification;
            version.Date = digitalTransformation.DateLastModification;
            version.Solution = digitalTransformation.Solution;
            version.Solved = digitalTransformation.Solved;
            dbContext.NS_tblDigitalTransformationVersions.Add(version);
            dbContext.SaveChanges();
        }
    }
}
