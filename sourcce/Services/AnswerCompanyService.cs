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
    public class AnswerCompanyService : IAnswerCompanyService
    {
        protected IAnswerCompanyRepository answerCompanyRepository;
        protected IProductCompanyRepository productCompanyRespository;
        protected ICompanyInfoRepository companyInfoRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public AnswerCompanyService(IAnswerCompanyRepository pAnswerCompanyRepository, IUnitOfWork.IUnitOfWork pUnitOfWork, IProductCompanyRepository pProductCompanyRepository, ICompanyInfoRepository pCompanyInfoRespository)
        {
            answerCompanyRepository = pAnswerCompanyRepository;
            productCompanyRespository = pProductCompanyRepository;
            companyInfoRepository = pCompanyInfoRespository;
            uow = pUnitOfWork;
        }

        public void Add(AnswerCompanyDTO entity)
        {
            NS_tblAnswerCompany _answerCompany = Mapper.Map<AnswerCompanyDTO, NS_tblAnswerCompany>(entity);

            answerCompanyRepository.Add(_answerCompany);

            //answerCompanyRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<AnswerCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnswerCompanyDTO> Find(Expression<Func<AnswerCompanyDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public AnswerCompanyDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnswerCompanyDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblAnswerCompany>, IEnumerable<AnswerCompanyDTO>>(answerCompanyRepository.GetAll());
        }

        /// <summary>
        ///     Obtiene una capability de una compañia que es partner o developer partner de  una campaña por su clave compuesta.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// /// <param name="pQuestionID">Id de la pregunta </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pLicenseID">el número del servidor debido a la cantidad de licences que hayan</param>
        /// <param name="pProductID">Id del producto al que se le está respondiendo las preguntas</param>
        /// <param name="pIsVLS"></param>
        /// <param name="pIsOEM"></param>
        /// <param name="pIsWEB"></param>
        /// <param name="pIsFPP"></param>
        /// <returns></returns>
        public AnswerCompanyDTO GetByCompositeKey(int pCompanyID, int pQuestionID, short pCampaignID, int pLicenseID, short pProductID, bool pIsVirtual)
        {
            return Mapper.Map<NS_tblAnswerCompany, AnswerCompanyDTO>(answerCompanyRepository.GetByCompositeKey(pCompanyID, pQuestionID, pCampaignID, pLicenseID, pProductID,pIsVirtual));
        }

        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        public IEnumerable<AnswerCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID, int pFamilyID)
        {
            return Mapper.Map<IEnumerable<NS_tblAnswerCompany>, IEnumerable<AnswerCompanyDTO>>(answerCompanyRepository.GetByIDandCampaign(pCompanyID, pCampaignID, pFamilyID));
        }

        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        public IEnumerable<AnswerCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID)
        {
            return Mapper.Map<IEnumerable<NS_tblAnswerCompany>, IEnumerable<AnswerCompanyDTO>>(answerCompanyRepository.GetByIDandCampaign(pCompanyID, pCampaignID));
        }

        public void Remove(AnswerCompanyDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<AnswerCompanyDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Actualiza las respuestas ingresadas a las preguntas de sql server y windows server para una compañia.
        /// </summary>
        /// <param name="pAnswerCompany">Viewmodel con la información a actualizar</param>
        public void UpdateAnswerCompany(AnswerCompanyDTO pAnswerCompany)
        {
            var _companyID = pAnswerCompany.CompanyID;
            var _campaignID = pAnswerCompany.CampaignID;
            var _questionID = pAnswerCompany.QuestionID;
            var _licenseID = pAnswerCompany.LicenseID;
            var _productID = pAnswerCompany.ProductID;
            var _isInstalledLicenses = pAnswerCompany.IsInstalledLicenses;
            var _isVirtual = pAnswerCompany.IsVirtual;

            NS_tblAnswerCompany _answerCompany = answerCompanyRepository.GetByCompositeKey(_companyID, _questionID, _campaignID, _licenseID, _productID, _isVirtual);

            _answerCompany.Answer = pAnswerCompany.Answer;

            //answerCompanyRepository.SaveChanges();
        }


        /// <summary>
        /// Actualiza el productId para todas las preguntas de un servidor
        /// </summary>
        /// <param name="pCompanyID"></param>Id de la compañia
        /// /// <param name="pCampaignID"></param>Id de la campaña
        /// /// <param name="pLicenseID"></param>Id del servidor
        /// /// <param name="OldProductID"></param>Id del producto viejo
        /// /// /// <param name="newProductID"></param>Id del nuevo producto
        /// 
        public void UpdateProductIDAnswerCompany(int pCompanyID, short pCampaignID, int pLicenseID, short OldProductID, short newProductID, bool pIsVirtual, bool pIsRealProductId)
        {
            answerCompanyRepository.UpdateProductID(pCompanyID, pCampaignID, pLicenseID, OldProductID, newProductID, pIsVirtual, pIsRealProductId);
        }

        /// <summary>
        /// Elimina una columna de respuesta
        /// </summary>
        /// <param name="pCompanyId">Id de la compañia</param>
        /// <param name="pCampaignId">Id de la campaña</param>
        /// <param name="pProductId">Id del producto</param>
        /// <param name="pIsVLS">Es VLS True/False</param>
        /// <param name="pIsOEM">Es OEM True/False</param>
        /// <param name="pIsFPP">Es FPP True/False</param>
        /// <param name="pIsWeb">Es Web True/False</param>
        public void RemoveAnswer(int pCompanyId, short pCampaignId, short pProductId, bool pIsVLS, bool pIsOEM, bool pIsFPP, bool pIsWeb, bool pIsIntalledLicenses)
        {
            //En primer lugar se actualizan los valores del product company
            var _productCompany = productCompanyRespository.GetByFullCompositeKey(pCompanyId, pProductId, pCampaignId);
            _productCompany.FPP = (pIsFPP && _productCompany.FPP !=null) ? Convert.ToInt16(_productCompany.FPP -1) : _productCompany.FPP;
            _productCompany.OEM = (pIsOEM && _productCompany.OEM != null) ? Convert.ToInt16(_productCompany.OEM -1) : _productCompany.OEM;
            _productCompany.VLS = (pIsVLS && _productCompany.VLS != null) ? Convert.ToInt16(_productCompany.VLS -1) : _productCompany.VLS;
            _productCompany.WEB = (pIsWeb && _productCompany.WEB != null) ? Convert.ToInt16(_productCompany.WEB -1) : _productCompany.WEB;
            _productCompany.InstalledLicenses = (pIsIntalledLicenses && _productCompany.InstalledLicenses != null) ? Convert.ToInt16(_productCompany.InstalledLicenses -  1) : _productCompany.InstalledLicenses;

            //luego se procede a eliminar la fila de la respuesta
            var _maxLicense = answerCompanyRepository.GetAll()
                .Where(x => x.CompanyID == pCompanyId && x.CampaignID == pCampaignId && x.ProductID == pProductId
                && x.IsFPP == pIsFPP && x.IsOEM == pIsOEM && x.IsVLS == pIsVLS && x.IsWEB == pIsWeb
                && x.IsInstalledLicenses == pIsIntalledLicenses).Max(x => x.LicenseID);

            var _answer = answerCompanyRepository.GetAll()
                .Where(x => x.CompanyID == pCompanyId && x.CampaignID == pCampaignId && x.ProductID == pProductId
                && x.IsFPP == pIsFPP && x.IsOEM == pIsOEM && x.IsVLS == pIsVLS && x.IsWEB == pIsWeb && x.LicenseID == _maxLicense
                && x.IsInstalledLicenses == pIsIntalledLicenses);

            answerCompanyRepository.RemoveRange(_answer);


            productCompanyRespository.SaveChanges();
            answerCompanyRepository.SaveChanges();
        }





        /// <summary>
        /// Obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia para el nuevo modelo
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        /// <param name="pIsVirtual">Es virtual el servidor?</param>
        public IEnumerable<AnswerCompanyDTO> GetRegisteredServers(int pCompanyID, short pCampaignID, int pFamilyID, bool pIsVirtual)
        {
            return Mapper.Map<IEnumerable<NS_tblAnswerCompany>, IEnumerable<AnswerCompanyDTO>>(answerCompanyRepository.GetRegisteredServers(pCompanyID,pCampaignID, pFamilyID, pIsVirtual));
        }


        /// <summary>
        /// Elimina un servidor de los registrados por una compañia
        /// </summary>
        /// <param name="pCompanyId">Id de la compañia</param>
        /// <param name="pCampaignId">Id de la campaña</param>
        /// <param name="pIsVirtual">Variable para saber si el servidor es virtual o fisico<param>
        /// <param name="pCurrentServerId">Id del servidor</param>
        /// <param name="pFamilyId">Id de la familya a la que pertenece el servidor</param>
        /// <param name="pIsColumn">Id del servidor que se borrara</param>
        public void RemoveServerByIdAndTypeAndFamily(int pCompanyId, int pCampaingId, bool pIsVirtual, int pCurrentServerId, int pFamilyId, bool pIsColumn)
        {

            if (!pIsColumn)
            {
                var _answerCompany = answerCompanyRepository.GetByIdAndTypeAndFamily(pCompanyId, pCampaingId, pIsVirtual, pCurrentServerId, pFamilyId);

                foreach (var item in _answerCompany)
                {
                    answerCompanyRepository.Remove(item);
                }
            }
            else
            {
                answerCompanyRepository.RemoveServerByIdAndTypeAndFamily(pCompanyId, pCampaingId, pIsVirtual, pCurrentServerId, pFamilyId);
               
            }
            //Si es fisico  y windowsademas de borrar se quita el inside de los virtuales que lo tengan asociado
            if (!pIsVirtual && pFamilyId != 4)
            {
                //Si es un servidor fisico busco todos los virtuales
                var allVirtuals = answerCompanyRepository.GetRegisteredServers(pCompanyId, Convert.ToInt16(pCampaingId), pFamilyId, true);

                foreach (var virt in allVirtuals.Where(d => d.IsInside == pCurrentServerId))
                {
                    virt.IsInside = -1;
                }
            }
        }

        public void UpdateIsInsideServer(int pCompanyID, short pCampaignID, int pLicenseID, string IsInside)
        {
            //answerCompanyRepository.UpdateIsInsideServer(pCompanyID, pCampaignID, pLicenseID, IsInside);
            IEnumerable<NS_tblAnswerCompany> _answerCompany = answerCompanyRepository.GetByCompositeKeyLessProductID(pCompanyID, pCampaignID, pLicenseID, true);

            foreach (var item in _answerCompany)
            {
                item.IsInside = short.Parse(IsInside);
            }
            //answerCompanyRepository.SaveChanges();
        }
    }
}
