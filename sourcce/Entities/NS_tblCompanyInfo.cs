namespace Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class NS_tblCompanyInfo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CampaignID { get; set; }

        [StringLength(50)]
        public string LeadID { get; set; }

        public int? LogFileID { get; set; }

        public DateTime? FolderCreationDate { get; set; }

        public int? TotalQuantityOfEmployees { get; set; }

        public short? TotalQuantityOfDesktops { get; set; }

        public short? TotalQuantityOfLaptops { get; set; }

        public short? TotalQuantityPCWithOtherOS { get; set; }

        public short? TotalQuantityOfPhysicalServers { get; set; }

        public short? TotalQuantityOfVirtualServers { get; set; }

        public short? TotalQuantityOfSQLServers { get; set; }

        public short? AcademicQttyTeachersPartialTime { get; set; }

        public short? AcademicQttyTeachersFullTime { get; set; }

        public short? AcademicQttyAdmEmpPartialTime { get; set; }

        public short? AcademicQttyAdmEmpFullTime { get; set; }

        [StringLength(50)]
        public string ExactNameInInvoicing { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber1 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber2 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber3 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber4 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber5 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber6 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber7 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber8 { get; set; }

        [StringLength(15)]
        public string VolumeLicenceNumber9 { get; set; }

        public bool? SoftwareAssuranceFlag { get; set; }

        [StringLength(500)]
        public string StatusRenewalAndContratDetails { get; set; }

        public bool? PlansToPurchaseNewLicensesFlag { get; set; }

        [StringLength(500)]
        public string PlansToPurchaseNewLicensesComments { get; set; }

        public bool? PlansToUpgradeCurrentlyOwnedLicensesFlag { get; set; }

        [StringLength(500)]
        public string PlansToUpgradeCurrentlyOwnedLicensesComments { get; set; }

        public bool? AuthorizedMicrosoftChannelFlag { get; set; }

        [StringLength(50)]
        public string AuthorizedChannel { get; set; }

        [StringLength(50)]
        public string MicrosoftPartnerContactName { get; set; }

        [StringLength(50)]
        public string MicrosoftPartnerPhoneNumber { get; set; }

        [StringLength(50)]
        public string MicrosoftPartnerEmail { get; set; }

        [StringLength(1)]
        public string CustomOrBasicsApplications { get; set; }

        [StringLength(500)]
        public string DevelopersPartnersApplicationsType { get; set; }

        [StringLength(500)]
        public string DevelopersPartnersMicrosofTools { get; set; }

        [StringLength(500)]
        public string DevelopersPartnersProjectsTypes { get; set; }

        public bool IsFinalVersion { get; set; }

        public byte? LicenseStatusResponseID { get; set; }

        public virtual NS_tblCampaign NS_tblCampaign { get; set; }

        public virtual NS_tblCompany NS_tblCompany { get; set; }

        public bool? AzureFlag { get; set; }

        public virtual NS_tblLicenseStatusResponse NS_tblLicenseStatusResponse { get; set; }
    }
}
