using Entities;
using Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Data.SqlClient;
using Entities.Recommendations;

namespace IRepositories
{
    public class VariableProductRepository : Repository<NS_tblVariableProduct>, IVariableProductRepository
    {
        AffidavitContext _context;

        public VariableProductRepository(AffidavitContext context) : base(context)
        {
            _context = context;
        }
       

        public void AddVariableProduct(NS_tblVariableProduct pVariableID)
        {
            var _parameter = new SqlParameter[]
            {
                new SqlParameter{ParameterName="@VariableID", Value=pVariableID.Variable.VariableID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                new SqlParameter{ParameterName="@ProductID", Value=pVariableID.Product.ProductID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
            };
            try
            {
                _context.Database.CommandTimeout = 1200;
                _context.Database.ExecuteSqlCommand("NS_spVariableProduct_Insert @VariableID, @ProductID", _parameter);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<NS_tblVariableProduct> GetByVariableID(short pVariableID)
        {
            return base.Context.Set<NS_tblVariableProduct>().Where(x => x.VariableID == pVariableID);
        }
    }
}
