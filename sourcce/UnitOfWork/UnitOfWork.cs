namespace UnitOfWork
{
    using IRepositories;
    using IUnitOfWork;
    using Repositories;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }


        public ICampaignRepository Campaigns
        {
            get; private set;
        }

        public IProductRepository Products
        {
            get; private set;
        }

        public IProductFamilyRepository ProductsFamilies
        {
            get; private set;
        }

        public ICompanyInfoRepository CompanyInfo
        {
            get; private set;
        }

        public ICompanyContactsRepository CompanyContacts
        {
            get; private set;
        }

        public ICompanyRepository Company
        {
            get; private set;
        }

        public IIndustryRepository IndustryRepository
        {
            get; private set;
        }

        public ILanguageRepository LanguageRepository
        {
            get; private set;
        }

        public IPartnerCapabilityRepository PartnerCapability
        {
            get; private set;
        }

        public IPartnerProgramRepository PartnerProgram
        {
            get; private set;
        }

        public IProductCompanyRepository ProductCompany
        {
            get; private set;
        }

        public IQuestionRepository Questions
        {
            get; private set;
        }

        public IQuestionxLanguageRepository QuestionsxLanguage
        {
            get; private set;
        }

        public IQuestionxProductFamilyRepository QuestionsxProductFamily
        {
            get; private set;
        }

        public IResponseDataTypeRepository ResponseDataTypeRepository
        {
            get; private set;
        } 

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context !=null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
        public DbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Commit(DbContextTransaction pActualTransaction)
        {
            pActualTransaction.Commit();
        }
        public void Rollback(DbContextTransaction pActualTransaction)
        {
            pActualTransaction.Rollback();
        }

        public virtual void Reset()
        {
            foreach (DbEntityEntry entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }



    }
}
