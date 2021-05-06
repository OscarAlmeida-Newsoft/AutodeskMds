

namespace Repositories
{
    using Entities;
    using IRepositories;

    public class ProductFamilyRepository : Repository<NS_tblProductFamily>, IProductFamilyRepository
    {
        public ProductFamilyRepository(AffidavitContext context) : base(context)
        {
        }
    }
}
