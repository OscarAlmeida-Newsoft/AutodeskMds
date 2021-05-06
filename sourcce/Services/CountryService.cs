using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using System.Linq.Expressions;
using IRepositories;
using Entities;
using AutoMapper;

namespace Services
{
    public class CountryService : ICountryService
    {
        protected ICountryRepository countryRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public CountryService(ICountryRepository pCountryRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            countryRepository = pCountryRepository;
            uow = pUnitOfWork;
        }

        public void Add(CountryDTO entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<CountryDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CountryDTO> Find(Expression<Func<CountryDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CountryDTO Get(int id)
        {
            return Mapper.Map<NS_tblCountry, CountryDTO>(countryRepository.Get(id));
        }

        public IEnumerable<CountryDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtiene un registro de la tabla country por su nombre
        /// </summary>
        /// <param name="pName">Nombre de la industria a consultar.</param>
        /// <returns></returns>
        public CountryDTO GetByName(string pName)
        {
            NS_tblCountry _country = countryRepository.GetByName(pName);
            CountryDTO _countryDTO = Mapper.Map<NS_tblCountry, CountryDTO>(_country);

            return _countryDTO;
        }

        public void Remove(CountryDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CountryDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene la lista de países de la base de datos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var _data = countryRepository.GetAllCountries();

            return Mapper.Map<IEnumerable<NS_tblCountry>, IEnumerable<CountryDTO>>(_data);
        }
    }
}
