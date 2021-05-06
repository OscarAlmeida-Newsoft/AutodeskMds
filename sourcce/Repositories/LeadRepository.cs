using DTOs;
using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LeadRepository : Repository<LeadInformation>, ILeadRepository
    {

        protected ICodeGenerator codeGenetor;

        public LeadRepository(AffidavitContext context, ICodeGenerator pCodeGenetor) : base(context)
        {
            codeGenetor = pCodeGenetor;
        }

        /// <summary>
        /// Save the lead information
        /// </summary>
        /// <param name="pLeadInformation">Lead information to save</param>

        public void SaveLeadInformation(LeadInformation pLeadInformation)
        {
            //var _lead = base.GetAll().Any(x => x.LeadId == pLeadInformation.LeadId);
            var _lead = base.Find(x => x.LeadId == pLeadInformation.LeadId).FirstOrDefault() == null ? false : true;

            if (!_lead)
            {
                pLeadInformation.Id = Guid.NewGuid();
                pLeadInformation.CreateDate = DateTime.Now;
                pLeadInformation.EditedDate = null;
                //Datos adicionales
                pLeadInformation.FirstLoginDate = DateTime.UtcNow;
                pLeadInformation.CompanySAMLiveUserName = GetCompanyUserNameCode();

                base.Add(pLeadInformation);
            }
        }

        /// <summary>
        /// Get lead information by ID
        /// </summary>
        /// <param name="pLeadId">Lead Id</param>
        /// <returns>Object with the lead information</returns>
        public LeadInformation GetByLeadId(Guid pLeadId)
        {
            return Context.Set<LeadInformation>().First(x => x.LeadId == pLeadId);
        }

        /// <summary>
        /// Get Lead by Landing campaign id
        /// </summary>
        /// <param name="pLandingId">Landing campaign id</param>
        /// <returns></returns>
        public bool GetLeadByLandingCampaignId(Guid pLandingId)
        {            
            return Context.Set<LeadInformation>().Any(x => x.IdLandingCampaig == pLandingId);
        }


        /// <summary>
        /// Get a Lead by LeadUsser 
        /// </summary>
        /// <param name="pEmailUser">User by the lead</param>
        /// <returns></returns>
        public IEnumerable<LeadInformation> GetLeadByEmail(string pEmailUser)
        {
            return base.Context.Set<LeadInformation>().Where(e => e.LeadUser == pEmailUser);
        }

        //TODO: Remove this method after masive lead information update for the username creation
        public void UpdateLeadInformationUserName(Guid pId, string pUserNameCode)
        {
            base.Context.Set<LeadInformation>().Find(pId).CompanySAMLiveUserName = pUserNameCode;
        }

        public void UpdateLeadInformationIndustryIndInsId(Guid pId, int IndustryIndInsId)
        {
            base.Context.Set<LeadInformation>().SingleOrDefault(d => d.LeadId == pId).IndustryIndInsId = IndustryIndInsId;
        }

        public IEnumerable<LeadInformation> GetByUserName(string pCompanyName)
        {
            return base.Context.Set<LeadInformation>().Where(e => e.CompanyName.Contains(pCompanyName)); 
        }


        public int GetLeadInformationProgress(Guid pCompanyId)
        {
            LeadInformation _leadInfo = base.Context.Set<LeadInformation>().Where(x => x.LeadId == pCompanyId).OrderByDescending(x => x.CreateDate).First();
            int _progress = 0;

            if (_leadInfo.AcceptLandingDate != null)
            {
                _progress = 50;
            }

            if (_leadInfo.SubmittedDate != null)
            {
                _progress = 100;
            }

            return _progress;
            
        }

        public bool ExistLeadByCompanyUserNameCode(string pCompanyUserNameCode)
        {
            var _lead = base.Find(x => x.CompanySAMLiveUserName == pCompanyUserNameCode);

            bool exist = false;
            var count = _lead.ToList().Count();

            if (_lead != null && count > 0)
                exist = true;

            return exist; 
        }


        private string GetCompanyUserNameCode()
        {
            string _codigo = codeGenetor.GenerateCode(LeadInformationDTO.USERCODE_PREFIX, 2, 4);

            while (ExistLeadByCompanyUserNameCode(_codigo))
            {
                _codigo = codeGenetor.GenerateCode(LeadInformationDTO.USERCODE_PREFIX, 2, 4);
            }

            return _codigo;
        }
    }
}
