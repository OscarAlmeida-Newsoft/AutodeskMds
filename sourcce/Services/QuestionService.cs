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
    public class QuestionService : IQuestionService
    {
        protected IQuestionRepository questionRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public QuestionService(IQuestionRepository pQuestionRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            questionRepository = pQuestionRepository;
            uow = pUnitOfWork;
        }

        /// <summary>
        ///  adiciona una nueva pregunta
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Add(QuestionDTO entity)
        {
            NS_tblQuestion _question = Mapper.Map<QuestionDTO, NS_tblQuestion>(entity);

            questionRepository.Add(_question);

            questionRepository.SaveChanges();           
            
        }

        /// <summary>
        ///     Inserta una nueva pregunta en la tabla question
        /// </summary>
        /// <param name="pQuestionDTO">objeto a insertar</param>
        /// <returns></returns>
        public int AddQuestion(QuestionDTO pQuestionDTO)
        {
            NS_tblQuestion _question = Mapper.Map<QuestionDTO, NS_tblQuestion>(pQuestionDTO);

            questionRepository.Add(_question);

            questionRepository.SaveChanges();

            return _question.QuestionID;
        }

        /// <summary>
        /// Actualiza un registro de la tabla
        /// </summary>
        /// <param name="pQuestionDTO"></param>
        public void UpdateQuestion(QuestionDTO pQuestionDTO)
        {
            var _questionID = pQuestionDTO.QuestionID;

            NS_tblQuestion _question = questionRepository.Get(_questionID);

            _question.ResponseDataTypeID = pQuestionDTO.ResponseDataTypeID;
            _question.ReletedQuestionID = pQuestionDTO.ReletedQuestionID;
            _question.RelatedQuestionIDResponse = pQuestionDTO.RelatedQuestionIDResponse;

            questionRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<QuestionDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionDTO> Find(Expression<Func<QuestionDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public QuestionDTO Get(int id)
        {
            return Mapper.Map<NS_tblQuestion, QuestionDTO>(questionRepository.Get(id));
        }

        public IEnumerable<QuestionDTO> GetAll()
        {
            var _results = questionRepository.GetAll();

            return Mapper.Map<IEnumerable<NS_tblQuestion>, IEnumerable<QuestionDTO>>(questionRepository.GetAll());
        }

        /// <summary>
        /// Elimina un dato de la tabla
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(QuestionDTO entity)
        {
            NS_tblQuestion _question =  Mapper.Map<QuestionDTO, NS_tblQuestion>(entity);

            var question = questionRepository.GetAll().Where(s => s.QuestionID == _question.QuestionID).FirstOrDefault();
            questionRepository.Remove(question);

            questionRepository.SaveChanges();
        }

        public void RemoveRange(IEnumerable<QuestionDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
