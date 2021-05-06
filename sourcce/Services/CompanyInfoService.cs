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
    public class CompanyInfoService : ICompanyInfoService
    {
        protected ICompanyInfoRepository companyInfoRepository;
        protected IUnitOfWork.IUnitOfWork uow;

        public CompanyInfoService(ICompanyInfoRepository pCompanyInfoRepository, IUnitOfWork.IUnitOfWork pUnitOfWork)
        {
            companyInfoRepository = pCompanyInfoRepository;
            uow = pUnitOfWork;
        }

        public void Add(CompanyInfoDTO entity)
        {
            NS_tblCompanyInfo _company = Mapper.Map<CompanyInfoDTO, NS_tblCompanyInfo>(entity);

            companyInfoRepository.Add(_company);

            //companyInfoRepository.SaveChanges();
        }

        public void AddRange(IEnumerable<CompanyInfoDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanyInfoDTO> Find(Expression<Func<CompanyInfoDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CompanyInfoDTO Get(int id)
        {
            return Mapper.Map<NS_tblCompanyInfo, CompanyInfoDTO>(companyInfoRepository.Get(id));
        }

        public IEnumerable<CompanyInfoDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<NS_tblCompanyInfo>, IEnumerable<CompanyInfoDTO>>(companyInfoRepository.GetAll());
        }

        /// <summary>
        /// obtiene info de una compañia por su id y id de la campaña.
        /// </summary>
        /// <param name="pCompanyID">Id de la compañia</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        public CompanyInfoDTO GetCompanyInfoByIDandCampaign(int pCompanyID, short pCampaignID)
        {
            return Mapper.Map<NS_tblCompanyInfo, CompanyInfoDTO>(companyInfoRepository.GetCompanyInfoByIDandCampaign(pCompanyID, pCampaignID));
        }

        /// <summary>
        /// Obitene el id una compañía de la tabla companyInfo por la campaña y el lead
        /// </summary>
        /// <param name="pLeadId">Id del lead</param>
        /// <param name="pCampaignID">Id de la campaña</param>
        /// <returns></returns>
        public int GetCompanyIDByLeadAndCampaign(Guid pLeadId, short pCampaignID)
        {
            CompanyInfoDTO _companyInfoDTO = Mapper.Map<NS_tblCompanyInfo, CompanyInfoDTO>(companyInfoRepository.GetCompanyIDByLeadAndCampaign(pLeadId, pCampaignID));

            int _companyID = (_companyInfoDTO != null) ? _companyInfoDTO.CompanyID : 0;

            return _companyID;
        }

        public void Remove(CompanyInfoDTO entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CompanyInfoDTO> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Actualiza la información de un compañia.
        /// </summary>
        /// <param name="pCompanyInfo">Viewmodel con la información a actualizar</param>
        public void UpdateCompanyInfo(CompanyInfoDTO pCompanyInfo)
        {
            int _companyID = pCompanyInfo.CompanyID;
            short _campaignID = pCompanyInfo.CampaignID;

            NS_tblCompanyInfo _companyInfo = companyInfoRepository.GetCompanyInfoByIDandCampaign(_companyID, _campaignID);

            _companyInfo.CustomOrBasicsApplications = pCompanyInfo.CustomOrBasicsApplications;
            _companyInfo.DevelopersPartnersApplicationsType = pCompanyInfo.DevelopersPartnersApplicationsType;
            _companyInfo.DevelopersPartnersMicrosofTools = pCompanyInfo.DevelopersPartnersMicrosofTools;
            _companyInfo.DevelopersPartnersProjectsTypes = pCompanyInfo.DevelopersPartnersProjectsTypes;
            _companyInfo.TotalQuantityOfEmployees = pCompanyInfo.TotalQuantityOfEmployees;
            _companyInfo.TotalQuantityOfLaptops = pCompanyInfo.TotalQuantityOfLaptops;
            _companyInfo.TotalQuantityOfDesktops = pCompanyInfo.TotalQuantityOfDesktops;
            _companyInfo.TotalQuantityOfPhysicalServers = pCompanyInfo.TotalQuantityOfPhysicalServers;
            _companyInfo.TotalQuantityOfVirtualServers = pCompanyInfo.TotalQuantityOfVirtualServers;
            _companyInfo.TotalQuantityOfSQLServers = pCompanyInfo.TotalQuantityOfSQLServers;
            _companyInfo.TotalQuantityPCWithOtherOS = pCompanyInfo.TotalQuantityPCWithOtherOS;
            _companyInfo.ExactNameInInvoicing = pCompanyInfo.ExactNameInInvoicing;
            _companyInfo.FolderCreationDate = pCompanyInfo.FolderCreationDate;
            _companyInfo.SoftwareAssuranceFlag = pCompanyInfo.SoftwareAssuranceFlag;
            _companyInfo.StatusRenewalAndContratDetails = pCompanyInfo.StatusRenewalAndContratDetails;
            _companyInfo.PlansToPurchaseNewLicensesFlag = pCompanyInfo.PlansToPurchaseNewLicensesFlag;
            _companyInfo.PlansToPurchaseNewLicensesComments = pCompanyInfo.PlansToPurchaseNewLicensesComments;
            _companyInfo.PlansToUpgradeCurrentlyOwnedLicensesFlag = pCompanyInfo.PlansToUpgradeCurrentlyOwnedLicensesFlag;
            _companyInfo.PlansToUpgradeCurrentlyOwnedLicensesComments = pCompanyInfo.PlansToUpgradeCurrentlyOwnedLicensesComments;
            _companyInfo.AuthorizedMicrosoftChannelFlag = pCompanyInfo.AuthorizedMicrosoftChannelFlag;
            _companyInfo.AuthorizedChannel = pCompanyInfo.AuthorizedChannel;
            _companyInfo.MicrosoftPartnerContactName = pCompanyInfo.MicrosoftPartnerContactName;
            _companyInfo.MicrosoftPartnerPhoneNumber = pCompanyInfo.MicrosoftPartnerPhoneNumber;
            _companyInfo.MicrosoftPartnerEmail = pCompanyInfo.MicrosoftPartnerEmail;
            _companyInfo.IsFinalVersion = pCompanyInfo.IsFinalVersion;
            _companyInfo.LicenseStatusResponseID = pCompanyInfo.LicenseStatusResponseID;
            _companyInfo.VolumeLicenceNumber1 = pCompanyInfo.VolumeLicenceNumber1;
            _companyInfo.VolumeLicenceNumber2 = pCompanyInfo.VolumeLicenceNumber2;
            _companyInfo.VolumeLicenceNumber3 = pCompanyInfo.VolumeLicenceNumber3;
            _companyInfo.VolumeLicenceNumber4 = pCompanyInfo.VolumeLicenceNumber4;
            _companyInfo.VolumeLicenceNumber5 = pCompanyInfo.VolumeLicenceNumber5;
            _companyInfo.VolumeLicenceNumber6 = pCompanyInfo.VolumeLicenceNumber6;
            _companyInfo.VolumeLicenceNumber7 = pCompanyInfo.VolumeLicenceNumber7;
            _companyInfo.VolumeLicenceNumber8 = pCompanyInfo.VolumeLicenceNumber8;
            _companyInfo.VolumeLicenceNumber9 = pCompanyInfo.VolumeLicenceNumber9;
            _companyInfo.AcademicQttyAdmEmpFullTime = pCompanyInfo.AcademicQttyAdmEmpFullTime;
            _companyInfo.AcademicQttyAdmEmpPartialTime = pCompanyInfo.AcademicQttyAdmEmpPartialTime;
            _companyInfo.AcademicQttyTeachersFullTime = pCompanyInfo.AcademicQttyTeachersFullTime;
            _companyInfo.AcademicQttyTeachersPartialTime = pCompanyInfo.AcademicQttyTeachersPartialTime;
            _companyInfo.AzureFlag = pCompanyInfo.AzureFlag;

            //companyInfoRepository.SaveChanges();
            //uow.Complete();
        }

        /// <summary>
        /// Cambia el estado de un affidavit (MDS)
        /// </summary>
        /// <param name="pIsFinalVersion">estado del affidavit (MDS)</param>
        /// <param name="pCompanyId">id de la compañia</param>
        /// <param name="pCampaignID">id de la campaña</param>
        public void UpdateState(bool pIsFinalVersion, int pCompanyID, short pCampaignID)
        {
            NS_tblCompanyInfo _companyInfo = companyInfoRepository.GetCompanyInfoByIDandCampaign(pCompanyID, pCampaignID);

            _companyInfo.IsFinalVersion = pIsFinalVersion;

            companyInfoRepository.SaveChanges();
        }
    }
}
