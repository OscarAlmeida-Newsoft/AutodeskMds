namespace Repositories
{
    using Entities;
    using IRepositories;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System;

    public class ProductRepository : Repository<NS_tblProduct>, IProductRepository
    {
        public ProductRepository(AffidavitContext context) : base(context)
        {

        }

        public override IEnumerable<NS_tblProduct> GetAll()
        {
            return Context.Set<NS_tblProduct>().OrderBy(s => s.ProductFamilyID).ThenByDescending(n => n.ProductVersion);
        }

        public IEnumerable<NS_tblProduct> GetByFamilyID(int id)
        {
            return Context.Set<NS_tblProduct>().Where(x => x.ProductFamilyID == id);
        }
    }
}
