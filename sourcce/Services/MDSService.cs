using IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MDSService : IMDSService
    {
        protected IUnitOfWork.IUnitOfWork uow;
        public MDSService(IUnitOfWork.IUnitOfWork pUow)
        {
            uow = pUow;
        }

        public void CommitChangesAffidavit()
        {
            int dat = uow.Complete();
        }

        public DbContextTransaction BeginTx()
        {
            DbContextTransaction dat = uow.BeginTransaction();
            return dat;
        }
        public void CommitTx(DbContextTransaction pActualTransaction)
        {
            uow.Commit(pActualTransaction);
        }

        public void RollbackTx(DbContextTransaction pActualTransaction)
        {
            uow.Rollback(pActualTransaction);
        }

        public void ResetAll()
        {
            uow.Reset();
        }
    }
}
