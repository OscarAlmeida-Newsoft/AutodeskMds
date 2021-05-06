using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IServices.Files
{
    public interface IFileManagerService
    {
        /// <summary>
        /// Sube pruebas de licenciamiento para determinada compañia
        /// </summary>
        /// <param name="file">Archivo a subir</param>
        /// <param name="pLeadID">Lead de la compañia</param>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        /// <param name="pFileNumber">Id del archivo</param>
        string SaveFile(HttpPostedFileBase file, Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID, short pFileNumber);

        /// <summary>
        /// Elimina alguna prueba de licenciamiento para determinada compañia
        /// </summary>
        /// <param name="pLeadID">Lead de la compañia</param>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        /// <param name="pFileNumber">Id del archivo</param>
        string DeleteProductFile(Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID, short pFileNumber);

        /// <summary>
        /// Elimina todas prueba de licenciamiento para determinado producto de una compañia
        /// </summary>
        /// <param name="pLeadID">Lead de la compañia</param>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pProductID">Id del producto</param>
        string DeleteAllFilesByProduct(Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID);

    }
}
