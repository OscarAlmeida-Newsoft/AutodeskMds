using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IQuestionxLanguageRepository : IRepository<NS_tblQuestionxLanguage>
    {
        /// <summary>
        /// Obtiene una fila de la tabla QuestionxLanguage 
        /// </summary>
        /// <param name="pQuestionID">Id de la pregunta relacionada</param>
        /// <param name="pLanguageID">Idioma de la pregunta relacionada</param>
        /// <returns></returns>
        NS_tblQuestionxLanguage GetByCompositeID(int pQuestionID, int pLanguageID);
    }
}
