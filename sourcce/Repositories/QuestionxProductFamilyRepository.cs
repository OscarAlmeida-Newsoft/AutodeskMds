using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class QuestionxProductFamilyRepository : Repository<NS_tblQuestionxProductFamily>, IQuestionxProductFamilyRepository
    {
        public QuestionxProductFamilyRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        /// Obtiene un registro por la clave compuesta
        /// </summary>
        /// <param name="pProductFamilyID"></param>
        /// <param name="pQuestionID"></param>
        /// <returns></returns>
        public NS_tblQuestionxProductFamily GetByCompositeKey(byte pProductFamilyID, int pQuestionID)
        {
            return base.Context.Set<NS_tblQuestionxProductFamily>().Where(x => x.ProductFamilyID == pProductFamilyID && x.QuestionID == pQuestionID).FirstOrDefault();
        }
    }
}
