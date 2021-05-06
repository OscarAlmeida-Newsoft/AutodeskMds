using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using IRepositories;
using AutoMapper;
using Entities;

namespace Services
{
    public class QuestionxLanguageService : IQuestionxLanguageService
    {
        protected IQuestionxLanguageRepository questionxLanguageRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public QuestionxLanguageService(IQuestionxLanguageRepository pQuestionxLanguageRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            questionxLanguageRepository = pQuestionxLanguageRepository;
            uow = pUnitOfWork;
        }

        /// <summary>
        /// guarda una fila en questionxLanguages
        /// </summary>
        /// <param name="entity"></param>
        public void Add(QuestionxLanguageDTO entity)
        {
            NS_tblQuestionxLanguage _questionxLanguages = Mapper.Map<QuestionxLanguageDTO, NS_tblQuestionxLanguage>(entity);

            questionxLanguageRepository.Add(_questionxLanguages);

            questionxLanguageRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<QuestionxLanguageDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionxLanguageDTO> Find(Expression<Func<QuestionxLanguageDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Obtiene una pregunta por el Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionxLanguageDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una fila de la tabla QuestionxLanguage 
        /// </summary>
        /// <param name="pQuestionID">Id de la pregunta relacionada</param>
        /// <param name="pLanguageID">Idioma de la pregunta relacionada</param>
        /// <returns></returns>
        public QuestionxLanguageDTO GetByCompositeID(int pQuestionID, int pLanguageID)
        {
            return Mapper.Map<NS_tblQuestionxLanguage, QuestionxLanguageDTO>(questionxLanguageRepository.GetByCompositeID(pQuestionID, pLanguageID));
        }

        /// <summary>
        /// obtiene una lista de las preguntas algunos de los productos.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<QuestionxLanguageDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblQuestionxLanguage>, IEnumerable<QuestionxLanguageDTO>>(questionxLanguageRepository.GetAll());
        }

        /// <summary>
        /// Actualiza un registro de la tabla
        /// </summary>
        /// <param name="pQuestionxLanguage"></param>
        public void UpdateQuestionxLanguage(QuestionxLanguageDTO pQuestionxLanguage)
        {
            var _questionID = pQuestionxLanguage.QuestionID;
            var _languageID = pQuestionxLanguage.LanguageID;            

            NS_tblQuestionxLanguage _questionxLanguage = questionxLanguageRepository.GetByCompositeID(_questionID, _languageID);

            _questionxLanguage.QuestionText= pQuestionxLanguage.QuestionText;

            questionxLanguageRepository.SaveChanges();
        }

        public void Remove(QuestionxLanguageDTO entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina un grupo de datos de la tabla
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<QuestionxLanguageDTO> entities)
        {
            IEnumerable<NS_tblQuestionxLanguage> _questionList = Mapper.Map<IEnumerable<QuestionxLanguageDTO>, IEnumerable<NS_tblQuestionxLanguage>>(entities);

            foreach (var item in _questionList)
            {
                var qxl = questionxLanguageRepository.GetAll().Where(s => s.QuestionID == item.QuestionID && s.LanguageID == item.LanguageID).FirstOrDefault();

                questionxLanguageRepository.Remove(qxl);

                questionxLanguageRepository.SaveChanges();
            }
        }
    }
}
