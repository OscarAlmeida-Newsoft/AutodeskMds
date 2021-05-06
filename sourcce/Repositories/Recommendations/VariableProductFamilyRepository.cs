using Entities;
using Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Data.SqlClient;
using Entities.Recommendations;

namespace IRepositories
{
    public class VariableProductFamilyRepository : Repository<NS_tblVariableProductFamily>, IVariableProductFamilyRepository
    {
        AffidavitContext _context;
        public VariableProductFamilyRepository(AffidavitContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<NS_tblVariableProductFamily> GetByVariableID(short pVariableID)
        {
            return base.Context.Set<NS_tblVariableProductFamily>().Where(x => x.VariableID == pVariableID);
        }

        public void AddVariableProductFamily(NS_tblVariableProductFamily pVariableID)
        {
            var _parameter = new SqlParameter[]
            {
                new SqlParameter{ParameterName="@VariableID", Value=pVariableID.Variable.VariableID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                new SqlParameter{ParameterName="@ProductFamilyID", Value=pVariableID.ProductFamily.ProductFamilyID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
            };
            try
            {
                _context.Database.CommandTimeout = 1200;
                _context.Database.ExecuteSqlCommand("NS_spVariableProductFamily_Insert @VariableID, @ProductFamilyID", _parameter);
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
    }
}
