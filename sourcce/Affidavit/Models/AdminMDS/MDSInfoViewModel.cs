using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class MDSInfoViewModel
    {
        //public CompanyDTO Company { get; set; }
        //public CompanyInfoDTO CompanyInfo { get; set; }
        //public CompanyContactsDTO CompanyContact { get; set; }
        //public List<ProductCompanyDTO> ProductCompanyList { get; set; }
        //public List<ProductFamilyCompanyDTO> ProductFamilyCompanyList { get; set; }
        //public List<PartnerCapabilityCompanyDTO> PartnerCapabilityList { get; set; }
        //public List<PartnerProgramCompanyDTO> PartnerProgramList { get; set; }
        //public List<PartnerProgramViewModel> PartnerProgramVMList { get; set; }
        //public List<PartnerCapabilityViewModel> PartnerCapabilityVMList { get; set; }
        //public List<ProductsViewModel> ProductsVMList { get; set; }
        //public List<AdditionalCommentsViewModel> AdditionalCommentsVMList { get; set; }
        //public string TipoCliente { get; set; }
        //public string IndustryName { get; set; }


        public CompanyInfoSearchViewModel Company { get; set; }
        public CompanyInfoViewModel CompanyInfo { get; set; }
        public CompanyContactsViewModel CompanyContact { get; set; }
        public List<ProductFamilyCompanyViewModel> ProductFamilyCompanyList { get; set; }
        public List<PartnerProgramListViewModel> PartnerProgramVMList { get; set; }
        public List<PartnerCapabilityListViewModel> PartnerCapabilityVMList { get; set; }
        public List<ProductListaViewModel> ProductsVMList { get; set; }
        public List<AdditonalCommentsViewModel> AdditionalCommentsVMList { get; set; }
        public string TipoCliente { get; set; }
        public string IndustryName { get; set; }
        public bool IsFinalVersion { get; set; }
        public bool HaveFiles { get; set; }

        public CompanyInfoCRMViewModel CompanyInfoCRM { get; set; }

        public string SharepointURL { get; set; }

        //public string CustomOrBasicsApplications { get; set; }
        //public string DevelopersPartnersApplicationsType { get; set; }
        //public string DevelopersPartnersMicrosofTools { get; set; }
        //public string DevelopersPartnersProjectsTypes { get; set; }

        //public short? AcademicQttyTeachersPartialTime { get; set; }
        //public short? AcademicQttyTeachersFullTime { get; set; }
        //public short? AcademicQttyAdmEmpPartialTime { get; set; }
        //public short? AcademicQttyAdmEmpFullTime { get; set; }

    }
}