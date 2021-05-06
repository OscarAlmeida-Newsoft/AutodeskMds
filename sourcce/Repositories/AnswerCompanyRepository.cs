using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AnswerCompanyRepository : Repository<NS_tblAnswerCompany>, IAnswerCompanyRepository
    {
        AffidavitContext _context;

        public AnswerCompanyRepository(AffidavitContext context) : base(context)
        {
            _context = context;
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
        public NS_tblAnswerCompany GetByCompositeKey(int pCompanyID, int pQuestionID, short pCampaignID, int pLicenseID, short pProductID, bool IsVirtual)
        {
            return base.Context.Set<NS_tblAnswerCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID
            && x.QuestionID == pQuestionID && x.LicenseID == pLicenseID && x.ProductID == pProductID && x.IsVirtual == IsVirtual).FirstOrDefault();
        }


        /// <summary>
        ///     Obtiene una capability de una compañia que es partner o developer partner de  una campaña por su clave compuesta menos el product id.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pLicenseID">el número del servidor debido a la cantidad de licences que hayan</param>
        /// /// <param name="pIsVirtual">el número del servidor debido a la cantidad de licences que hayan</param>
        /// <returns></returns>
        IEnumerable<NS_tblAnswerCompany> IAnswerCompanyRepository.GetByCompositeKeyLessProductID(int pCompanyID, short pCampaignID, int pLicenseID, bool pIsVirtual)
        {
            return base.Context.Set<NS_tblAnswerCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID
                    && x.LicenseID == pLicenseID && x.IsVirtual == pIsVirtual).ToList();
        }

        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        public IEnumerable<NS_tblAnswerCompany> GetByIDandCampaign(int pCompanyID, short pCampaignID, int pFamilyID)
        {
            return base.Context.Set<NS_tblAnswerCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID && x.Product.ProductFamilyID == pFamilyID);
        }

        /// <summary>
        /// obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        public IEnumerable<NS_tblAnswerCompany> GetByIDandCampaign(int pCompanyID, short pCampaignID)
        {
            return base.Context.Set<NS_tblAnswerCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID);
        }



        /// <summary>
        /// Obtiene listado de respuestas a las preguntas de sql server y windows server de una compañia para el nuevo modelo
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pFamilyID">Id de la familia</param>
        /// <param name="pIsVirtual">Es virtual el servidor?</param>
        public IEnumerable<NS_tblAnswerCompany> GetRegisteredServers(int pCompanyID, short pCampaignID, int pFamilyID, bool pIsVirtual)
        {
            return base.Context.Set<NS_tblAnswerCompany>().Where(x => x.CompanyID == pCompanyID && x.CampaignID == pCampaignID && x.IsVirtual == pIsVirtual && x.Product.ProductFamilyID == pFamilyID);
        }


        /// <summary>
        ///     Actualiza el Product Id de un servidor especifico
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia </param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <param name="pLicenseID">el número del servidor debido a la cantidad de licences que hayan</param>
        /// <param name="pProductID"></param>
        /// <param name="newProductID"></param>
        /// <returns></returns>
        void IAnswerCompanyRepository.UpdateProductID(int pCompanyID, short pCampaignID, int pLicenseID, short pProductID, short newProductID, bool pIsVirtual, bool pIsRealProductId)
        {
            var _parameter = new SqlParameter[]
            {
                new SqlParameter{ParameterName="@CompanyID", Value=pCompanyID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                new SqlParameter{ParameterName="@CampaignID", Value=pCampaignID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                new SqlParameter{ParameterName="@LicenseID", Value=pLicenseID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                new SqlParameter{ParameterName="@ProductIDOld", Value=pProductID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                new SqlParameter{ParameterName="@ProductIDNew", Value=newProductID, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                new SqlParameter{ParameterName="@IsVirtual", Value=pIsVirtual, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Boolean},
                new SqlParameter{ParameterName="@IsRealProductId", Value=pIsRealProductId, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Boolean},

            };
                _context.Database.CommandTimeout = 1200;
                _context.Database.ExecuteSqlCommand("NS_spAnswerCompany_UpdateRow @CompanyID, @CampaignID, @LicenseID, @ProductIDOld, @ProductIDNew, @IsVirtual, @IsRealProductId", _parameter);


        }

        /// <summary>
        /// Elimina un servidor de los registrados por una compañia
        /// </summary>
        /// <param name="pCompanyId">Id de la compañia</param>
        /// <param name="pCampaignId">Id de la campaña</param>
        /// <param name="pIsVirtual">Variable para saber si el servidor es virtual o fisico<param>
        /// <param name="pCurrentServerId">Id del servidor</param>
        /// <param name="pFamilyId">Id de la familya a la que pertenece el servidor</param>
        public IEnumerable<NS_tblAnswerCompany> GetByIdAndTypeAndFamily(int pCompanyId, int pCampaingId, bool pIsVirtual, int pCurrentServerId, int pFamilyId)
        {
            return base.Context.Set<NS_tblAnswerCompany>().Where(x => x.CompanyID == pCompanyId && x.CampaignID == pCampaingId && x.IsVirtual == pIsVirtual && x.Product.ProductFamilyID == pFamilyId && x.LicenseID == pCurrentServerId);
        }

        public void RemoveServerByIdAndTypeAndFamily(int pCompanyId, int pCampaingId, bool pIsVirtual, int pCurrentServerId, int pFamilyId)
        {

            var _parameter = new SqlParameter[]
            {
                    new SqlParameter{ParameterName="@CompanyID", Value=pCompanyId, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                    new SqlParameter{ParameterName="@CampaignID", Value=pCampaingId, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int16},
                    new SqlParameter{ParameterName="@IsVirtual", Value=pIsVirtual, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Boolean},
                    //new SqlParameter{ParameterName="@IsVirtual", Value=pIsVirtual==true?1:0, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                    new SqlParameter{ParameterName="@LicenseID", Value=pCurrentServerId, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int32},
                    new SqlParameter{ParameterName="@FamilyID", Value=pFamilyId, Direction = System.Data.ParameterDirection.Input, DbType=System.Data.DbType.Int16}
            };


            _context.Database.CommandTimeout = 10000;
            _context.Database.ExecuteSqlCommand("NS_spAnswerCompany_UpdateAndDeleteRow_LicenseID @CompanyID, @CampaignID,  @IsVirtual, @LicenseID, @FamilyID", _parameter);
        }
    }
}
