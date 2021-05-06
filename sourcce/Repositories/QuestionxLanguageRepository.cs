using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class QuestionxLanguageRepository : Repository<NS_tblQuestionxLanguage>, IQuestionxLanguageRepository
    {
        public QuestionxLanguageRepository(AffidavitContext context) : base(context)
        {

        }

        /// <summary>
        /// Obtiene una fila de la tabla QuestionxLanguage 
        /// </summary>
        /// <param name="pQuestionID">Id de la pregunta relacionada</param>
        /// <param name="pLanguageID">Idioma de la pregunta relacionada</param>
        /// <returns></returns>
        public NS_tblQuestionxLanguage GetByCompositeID(int pQuestionID, int pLanguageID)
        {
            return Context.Set<NS_tblQuestionxLanguage>().Where(x => x.QuestionID == pQuestionID && x.LanguageID == pLanguageID).FirstOrDefault();
        }
    }
}
