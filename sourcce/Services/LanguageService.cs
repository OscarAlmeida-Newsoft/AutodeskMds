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
    public class LanguageService : ILanguageService
    {
        protected ILanguageRepository languageRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public LanguageService(ILanguageRepository pLanguageRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            languageRepository = pLanguageRepository;
            uow = pUnitOfWork;
        }

        public void Add(LanguageDTO entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<LanguageDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LanguageDTO> Find(Expression<Func<LanguageDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public LanguageDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Obtiene la lista de lenguajes del affidavit
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LanguageDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblLanguage>, IEnumerable<LanguageDTO>>(languageRepository.GetAll());
        }

        public void Remove(LanguageDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<LanguageDTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
