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
    public class QuestionxProductFamilyService : IQuestionxProductFamilyService
    {
        protected IQuestionxProductFamilyRepository questionxProductFamilyRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public QuestionxProductFamilyService(IQuestionxProductFamilyRepository pQuestionxProductFamilyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            questionxProductFamilyRepository = pQuestionxProductFamilyRepository;
            uow = pUnitOfWork;
        }

        public void Add(QuestionxProductFamilyDTO entity)
        {
            NS_tblQuestionxProductFamily _questionxProductFamily = Mapper.Map<QuestionxProductFamilyDTO, NS_tblQuestionxProductFamily>(entity);

            questionxProductFamilyRepository.Add(_questionxProductFamily);

            questionxProductFamilyRepository.SaveChanges();
        }

        /// <summary>
        /// Actualiza un registro de la tabla
        /// </summary>
        /// <param name="pQuestionxLanguage"></param>
        public void UpdateQuestionxProductFamily(QuestionxProductFamilyDTO pQuestionProductFamilyDTO)
        {
            var _questionID = pQuestionProductFamilyDTO.QuestionID;
            var _productFamilyID = pQuestionProductFamilyDTO.ProductFamilyID;

            NS_tblQuestionxProductFamily _questionxProductFamily = questionxProductFamilyRepository.GetByCompositeKey(_productFamilyID, _questionID);

            _questionxProductFamily.ProductFamilyID = pQuestionProductFamilyDTO.ProductFamilyID;
            _questionxProductFamily.DisplayOrder = pQuestionProductFamilyDTO.DisplayOrder;
            _questionxProductFamily.IsActive = pQuestionProductFamilyDTO.IsActive;

            questionxProductFamilyRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<QuestionxProductFamilyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionxProductFamilyDTO> Find(Expression<Func<QuestionxProductFamilyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene un registro por la clave compuesta
        /// </summary>
        /// <param name="pProductFamilyID"></param>
        /// <param name="pQuestionID"></param>
        /// <returns></returns>
        public QuestionxProductFamilyDTO GetByCompositeKey(byte pProductFamilyID, int pQuestionID)
        {
            return Mapper.Map<NS_tblQuestionxProductFamily, QuestionxProductFamilyDTO>(questionxProductFamilyRepository.GetByCompositeKey(pProductFamilyID, pQuestionID));
        }

        public QuestionxProductFamilyDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionxProductFamilyDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblQuestionxProductFamily>, IEnumerable<QuestionxProductFamilyDTO>>(questionxProductFamilyRepository.GetAll());
        }

        public void Remove(QuestionxProductFamilyDTO entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina un grupo de datos de la tabla
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<QuestionxProductFamilyDTO> entities)
        {
            IEnumerable<NS_tblQuestionxProductFamily> _questionList = Mapper.Map<IEnumerable<QuestionxProductFamilyDTO>, IEnumerable<NS_tblQuestionxProductFamily>>(entities);

            foreach (var item in _questionList)
            {
                var qxpf = questionxProductFamilyRepository.GetAll().Where(s => s.QuestionID == item.QuestionID && s.ProductFamilyID == item.ProductFamilyID).FirstOrDefault();

                questionxProductFamilyRepository.Remove(qxpf);

                questionxProductFamilyRepository.SaveChanges();
            }
        }
    }
}
