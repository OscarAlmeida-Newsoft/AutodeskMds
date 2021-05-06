using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IQuestionxProductFamilyRepository : IRepository<NS_tblQuestionxProductFamily>
    {
        /// <summary>
        /// Obtiene un registro por la clave compuesta
        /// </summary>
        /// <param name="pProductFamilyID"></param>
        /// <param name="pQuestionID"></param>
        /// <returns></returns>
        NS_tblQuestionxProductFamily GetByCompositeKey(byte pProductFamilyID, int pQuestionID);
    }
}
