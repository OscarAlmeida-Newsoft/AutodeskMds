using IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICampaignRepository Campaigns { get; }
        IProductRepository Products { get; }
        IProductFamilyRepository ProductsFamilies { get; }
        int Complete();
        void Reset();
        DbContextTransaction BeginTransaction();
        void Commit(DbContextTransaction pActualTransaction);
        void Rollback(DbContextTransaction pActualTransaction);
    }
}
