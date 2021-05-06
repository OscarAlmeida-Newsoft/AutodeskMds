namespace IRepositories
{
    using Entities;
    using System.Collections.Generic;

    public interface IProductRepository : IRepository<NS_tblProduct>
    {
        IEnumerable<NS_tblProduct> GetByFamilyID(int id);
    }
}
