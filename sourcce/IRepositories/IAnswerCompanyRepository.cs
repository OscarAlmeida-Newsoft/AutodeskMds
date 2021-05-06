using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IAnswerCompanyRepository : IRepository<NS_tblAnswerCompany>
    {
        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        IEnumerable<NS_tblAnswerCompany> GetByIDandCampaign(int pCompanyID, short pCampaignID, int pFamilyID);


        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        IEnumerable<NS_tblAnswerCompany> GetByIDandCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        /// Obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia para el nuevo modelo
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        /// <param name="pIsVirtual">Es virtual el servidor?</param>
        IEnumerable<NS_tblAnswerCompany> GetRegisteredServers(int pCompanyID, short pCampaignID, int pFamilyID, bool pIsVirtual);

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
        NS_tblAnswerCompany GetByCompositeKey(int pCompanyID, int pQuestionID, short pCampaignID, int pLicenseID, short pProductID, bool IsVirtual);

        /// <summary>
        ///     Obtiene una capability de una compañia que es partner o developer partner de  una campaña por su clave compuesta menos el product id.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pLicenseID">el número del servidor debido a la cantidad de licences que hayan</param>
        /// /// <param name="pIsVirtual">el número del servidor debido a la cantidad de licences que hayan</param>
        /// <returns></returns>
        IEnumerable<NS_tblAnswerCompany> GetByCompositeKeyLessProductID(int pCompanyID, short pCampaignID, int pLicenseID, bool pIsVirtual);

        /// <summary>
        ///     Actualiza el Product Id de un servidor especifico
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pLicenseID">el número del servidor debido a la cantidad de licences que hayan</param>
        /// <param name="pProductID"></param>
        /// <param name="newProductID"></param>
        /// <returns></returns>
        void UpdateProductID(int pCompanyID, short pCampaignID, int pLicenseID, short pProductID, short newProductID, bool pIsVirtual,bool pIsRealProductId);



        /// <summary>
        /// Obtiene un servidor de los registrados por una compañia
        /// </summary>
        /// <param name="pCompanyId">Id de la compañia</param>
        /// <param name="pCampaignId">Id de la campaña</param>
        /// <param name="pIsVirtual">Variable para saber si el servidor es virtual o fisico<param>
        /// <param name="pCurrentServerId">Id del servidor</param>
        /// <param name="pFamilyId">Id de la familya a la que pertenece el servidor</param>
        IEnumerable<NS_tblAnswerCompany> GetByIdAndTypeAndFamily(int pCompanyId, int pCampaingId, bool pIsVirtual, int pCurrentServerId, int pFamilyId);

        /// <summary>
        /// Elimina un servidor de los registrados por una compañia
        /// </summary>
        /// <param name="pCompanyId">Id de la compañia</param>
        /// <param name="pCampaignId">Id de la campaña</param>
        /// <param name="pIsVirtual">Variable para saber si el servidor es virtual o fisico<param>
        /// <param name="pCurrentServerId">Id del servidor</param>
        /// <param name="pFamilyId">Id de la familya a la que pertenece el servidor</param>
        void RemoveServerByIdAndTypeAndFamily(int pCompanyId, int pCampaingId, bool pIsVirtual, int pCurrentServerId, int pFamilyId);
    }
}
