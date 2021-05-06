using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IAnswerCompanyService : IBaseService<AnswerCompanyDTO>
    {
        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        IEnumerable<AnswerCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID, int pFamilyID);

        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        IEnumerable<AnswerCompanyDTO> GetByIDAndCampaign(int pCompanyID, short pCampaignID);

        /// <summary>
        /// Obtiene listado de respuestas a las preguntas de sql server o windows server de una compañia para el nuevo modelo
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        /// <param name="pIsVirtual">Es virtual el servidor?</param>
        IEnumerable<AnswerCompanyDTO> GetRegisteredServers(int pCompanyID, short pCampaignID, int pFamilyID, bool pIsVirtual);

        /// <summary>
        /// Actualiza las respuestas ingresadas a las preguntas de sql server y windows server para una compañia.
        /// </summary>
        /// <param name="pAnswerCompany">Viewmodel con la información a actualizar</param>
        void UpdateAnswerCompany(AnswerCompanyDTO pAnswerCompany);



        /// <summary>
        /// Actualiza el productId para todas las preguntas de un servidor
        /// </summary>
        /// <param name="pCompanyID"></param>Id de la compañia
        /// /// <param name="pCampaignID"></param>Id de la campaña
        /// /// <param name="pLicenseID"></param>Id del servidor
        /// /// <param name="OldProductID"></param>Id del producto viejo
        /// /// /// <param name="newProductID"></param>Id del nuevo producto
        /// 
        void UpdateProductIDAnswerCompany(int pCompanyID, short pCampaignID, int pLicenseID, short OldProductID, short newProductID, bool pIsVirtual,bool pIsRealProductId);

        /// <summary>
        /// Actualiza el IsInside para un servidor virtual
        /// </summary>
        /// <param name="pCompanyID"></param>Id de la compañia
        /// /// <param name="pCampaignID"></param>Id de la campaña
        /// /// <param name="pLicenseID"></param>Id del servidor
        /// /// <param name="IsInside"></param>Id del producto viejo
        /// 
        void UpdateIsInsideServer(int pCompanyID, short pCampaignID, int pLicenseID, string IsInside);
        

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
        AnswerCompanyDTO GetByCompositeKey(int pCompanyID, int pQuestionID, short pCampaignID, int pLicenseID, short pProductID,bool pIsVirtual);



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
        void RemoveAnswer(int pCompanyId, short pCampaignId, short pProductId, bool pIsVLS, bool pIsOEM, bool pIsFPP, bool pIsWeb, bool pIsIntalledLicenses);



        /// <summary>
        /// Elimina un servidor de los registrados por una compañia
        /// </summary>
        /// <param name="pCompanyId">Id de la compañia</param>
        /// <param name="pCampaignId">Id de la campaña</param>
        /// <param name="pIsVirtual">Variable para saber si el servidor es virtual o fisico<param>
        /// <param name="pCurrentServerId">Id del servidor</param>
        /// <param name="pFamilyId">Id de la familya a la que pertenece el servidor</param>
        /// /// <param name="pIsColumn">Para saber si se eliminara una </param>
        void RemoveServerByIdAndTypeAndFamily(int pCompanyId, int pCampaingId, bool pIsVirtual, int pCurrentServerId, int pFamilyId, bool pIsColumn);
    }
}
