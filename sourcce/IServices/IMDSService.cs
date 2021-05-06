using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IMDSService
    {

        void CommitChangesAffidavit();
        void ResetAll();
        DbContextTransaction BeginTx();
        void CommitTx(DbContextTransaction pActualTransaction);
        void RollbackTx(DbContextTransaction pActualTransaction);
    }
}
