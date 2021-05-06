using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IQuestionxProductFamilyService : IBaseService<QuestionxProductFamilyDTO>
    {
        /// <summary>
        /// Obtiene un registro por la clave compuesta
        /// </summary>
        /// <param name="pProductFamilyID"></param>
        /// <param name="pQuestionID"></param>
        /// <returns></returns>
        QuestionxProductFamilyDTO GetByCompositeKey(byte pProductFamilyID, int pQuestionID);

        /// <summary>
        /// Actualiza un registro de la tabla
        /// </summary>
        /// <param name="pQuestionxLanguage"></param>
        void UpdateQuestionxProductFamily(QuestionxProductFamilyDTO pQuestionProductFamilyDTO);
    }
}
