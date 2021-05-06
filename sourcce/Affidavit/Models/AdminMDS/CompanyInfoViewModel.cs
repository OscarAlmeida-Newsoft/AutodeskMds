using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models.AdminMDS
{
    public class CompanyInfoViewModel
    {
        public int CompanyID { get; set; }

        public short CampaignID { get; set; }

        public string LeadID { get; set; }

        public int? LogFileID { get; set; }

        public DateTime? FolderCreationDate { get; set; }

        public int? TotalQuantityOfEmployees { get; set; }

        [Display(Name = "Desktops and laptops with Windows O.S")]
        public short? TotalQuantityOfDesktops { get; set; }

        public short? TotalQuantityOfLaptops { get; set; }

        [Display(Name = "Desktops and laptops with other O.S")]
        public short? TotalQuantityPCWithOtherOS { get; set; }

        public short? TotalQuantityOfPhysicalServers { get; set; }

        public short? TotalQuantityOfVirtualServers { get; set; }

        public short? AcademicQttyTeachersPartialTime { get; set; }

        public short? AcademicQttyTeachersFullTime { get; set; }

        public short? AcademicQttyAdmEmpPartialTime { get; set; }

        public short? AcademicQttyAdmEmpFullTime { get; set; }

        public string ExactNameInInvoicing { get; set; }

        public string VolumeLicenceNumber1 { get; set; }

        public string VolumeLicenceNumber2 { get; set; }

        public string VolumeLicenceNumber3 { get; set; }

        public string VolumeLicenceNumber4 { get; set; }

        public string VolumeLicenceNumber5 { get; set; }

        public string VolumeLicenceNumber6 { get; set; }

        public string VolumeLicenceNumber7 { get; set; }

        public string VolumeLicenceNumber8 { get; set; }

        public string VolumeLicenceNumber9 { get; set; }

        public bool? SoftwareAssuranceFlag { get; set; }

        public string StatusRenewalAndContratDetails { get; set; }

        public bool? PlansToPurchaseNewLicensesFlag { get; set; }

        public string PlansToPurchaseNewLicensesComments { get; set; }

        public bool? PlansToUpgradeCurrentlyOwnedLicensesFlag { get; set; }

        public string PlansToUpgradeCurrentlyOwnedLicensesComments { get; set; }

        public bool? AuthorizedMicrosoftChannelFlag { get; set; }

        public string AuthorizedChannel { get; set; }

        public string MicrosoftPartnerContactName { get; set; }

        public string MicrosoftPartnerPhoneNumber { get; set; }

        public string MicrosoftPartnerEmail { get; set; }

        public string CustomOrBasicsApplications { get; set; }

        public string DevelopersPartnersApplicationsType { get; set; }

        public string DevelopersPartnersMicrosofTools { get; set; }

        public string DevelopersPartnersProjectsTypes { get; set; }

        public byte? LicenseStatusResponseID { get; set; }

        public bool IsFinalVersion { get; set; }

        public bool? AzureFlag { get; set; }
    }
}