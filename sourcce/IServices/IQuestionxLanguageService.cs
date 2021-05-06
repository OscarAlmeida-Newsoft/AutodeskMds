using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IQuestionxLanguageService : IBaseService<QuestionxLanguageDTO>
    {
        /// <summary>
        /// Obtiene una fila de la tabla QuestionxLanguage 
        /// </summary>
        /// <param name="pQuestionID">Id de la pregunta relacionada</param>
        /// <param name="pLanguageID">Idioma de la pregunta relacionada</param>
        /// <returns></returns>
        QuestionxLanguageDTO GetByCompositeID(int pQuestionID, int pLanguageID);

        /// <summary>
        /// Actualiza un registro de la tabla
        /// </summary>
        /// <param name="pQuestionxLanguage"></param>
        void UpdateQuestionxLanguage(QuestionxLanguageDTO pQuestionxLanguage);
    }
}
