using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IQuestionService : IBaseService<QuestionDTO>
    {
        /// <summary>
        ///     Inserta una nueva pregunta en la tabla question
        /// </summary>
        /// <param name="pQuestionDTO">objeto a insertar</param>
        /// <returns></returns>
        int AddQuestion(QuestionDTO pQuestionDTO);

        /// <summary>
        /// Actualiza un registro de la tabla
        /// </summary>
        /// <param name="pQuestionDTO"></param>
        void UpdateQuestion(QuestionDTO pQuestionDTO);
    }
}
