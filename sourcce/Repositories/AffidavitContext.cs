namespace Repositories
{
    using System.Data;
    using System.Data.Entity;
    using IRepositories;
    using Entities;
    using Entities.Landing;
    using Entities.SurveyQuestionResponse;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Entities.Users;
    using System;
    using Entities.NS_tblEmailQueue;
    using Entities.Recommendations;

    public partial class AffidavitContext : IdentityDbContext<User, MyRole, Guid, MyLogin, UserRole, MyClaim>, IAffidavitContext
    {
        public AffidavitContext()
            : base("name=AffidavitModelsConnection")
        {
            Database.SetInitializer<AffidavitContext>(new MigrateDatabaseToLatestVersion<AffidavitContext, Configuration>(true));
        }

        public virtual DbSet<NS_tblCampaign> NS_tblCampaign { get; set; }
        public virtual DbSet<NS_tblCompany> NS_tblCompany { get; set; }
        public virtual DbSet<NS_tblCompanyContacts> NS_tblCompanyContacts { get; set; }
        public virtual DbSet<NS_tblCompanyInfo> NS_tblCompanyInfo { get; set; }
        public virtual DbSet<NS_tblCompanyType> NS_tblCompanyType { get; set; }
        public virtual DbSet<NS_tblIndustry> NS_tblIndustry { get; set; }
        public virtual DbSet<NS_tblLead> NS_tblLead { get; set; }
        public virtual DbSet<NS_tblLicenseStatusResponse> NS_tblLicenseStatusResponse { get; set; }
        public virtual DbSet<NS_tblPartnerCapability> NS_tblPartnerCapability { get; set; }
        public virtual DbSet<NS_tblPartnerCapabilityCompany> NS_tblPartnerCapabilityCompany { get; set; }
        public virtual DbSet<NS_tblPartnerProgram> NS_tblPartnerProgram { get; set; }
        public virtual DbSet<NS_tblPartnerProgramCompany> NS_tblPartnerProgramCompany { get; set; }
        public virtual DbSet<NS_tblProduct> NS_tblProduct { get; set; }
        public virtual DbSet<NS_tblProductCompany> NS_tblProductCompany { get; set; }
        public virtual DbSet<NS_tblProductFamily> NS_tblProductFamily { get; set; }
        public virtual DbSet<NS_tblProductFamilyCompany> NS_tblProductFamilyCompany { get; set; }
        public virtual DbSet<NS_tblIndustryGroup> NS_tblIndustryGroup { get; set; }
        public virtual DbSet<NS_tblCountry> NS_tblCountry { get; set; }
        public virtual DbSet<NS_tblLanguage> NS_tblLanguage { get; set; }
        public virtual DbSet<NS_tblQuestion> NS_tblQuestion { get; set; }
        public virtual DbSet<NS_tblQuestionxLanguage> NS_tblQuestionxLanguage { get; set; }
        public virtual DbSet<NS_tblQuestionxProductFamily> NS_tblQuestionxProductFamily { get; set; }
        public virtual DbSet<NS_tblResponseDataType> NS_tblResponseDataType { get; set; }
        public virtual DbSet<NS_tblAnswerCompany> NS_tblAnswerCompany { get; set; }
        public virtual DbSet<NS_tblRegion> NS_tblRegion { get; set; }

        //tablas que eran de SomSight
        public virtual DbSet<LandingCampaign> LandingCampaign { get; set; }
        public virtual DbSet<LeadInformation> LeadInformation { get; set; }
        public virtual DbSet<SurveyQuestionResponse> SurveyQuestionResponse { get; set; }

        //Tabla que guarda correos pendientes por enviar
        public virtual DbSet<NS_tblEmailQueue> NS_tblEmailQueue { get; set; }
        public virtual DbSet<NS_TblAnswerXAssessment> NS_AnswerXAssessment { get; set; }
        public virtual DbSet<NS_TblAssessmentSummary> NS_AssessmentSummary { get; set; }

        //Tabla para guardar archivos de pruebas de licenciamiento
        public virtual DbSet<NS_tblProductCompanyFile> NS_tblProductCompanyFile { get; set; }

        //Tablas para las recomendaciones
        public virtual DbSet<NS_tblVariable> NS_tblVariable { get; set; }
        public virtual DbSet<NS_tblVariableProduct> NS_tblVariableProduct { get; set; }
        public virtual DbSet<NS_tblVariableProductFamily> NS_tblVariableProductFamily { get; set; }
        public virtual DbSet<NS_tblCompoundVariable> NS_tblCompoundVariable { get; set; }

        //Tablas para industry insights
        public virtual DbSet<NS_tblIndustryIndIns> NS_tblIndustryIndIns { get; set; }
        public virtual DbSet<NS_TblTypeActivity> NS_tblTypeActivity { get; set; }
        public virtual DbSet<NS_TblActivity> NS_tblTActivity { get; set; }
        public virtual DbSet<NS_TblProcessPreLoaded> NS_tblProcessPreloaded { get; set; }
        public virtual DbSet<NS_TblProblemPreLoaded> NS_tblProblemPreloaded { get; set; }
        public virtual DbSet<NS_TblDigitalTransformationPreLoaded> NS_tblDigitalTransformationPreloaded { get; set; }
        public virtual DbSet<NS_TblProcess> NS_tblProcess { get; set; }
        public virtual DbSet<NS_TblProcessVersions> NS_tblProcessVersions { get; set; }
        public virtual DbSet<NS_TblProblem> NS_tblProblem { get; set; }
        public virtual DbSet<NS_TblProblemVersions> NS_tblProblemVersions { get; set; }
        public virtual DbSet<NS_TblDigitalTransformation> NS_tblDigitalTransformation { get; set; }
        public virtual DbSet<NS_TblDigitalTransformationVersions> NS_tblDigitalTransformationVersions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NS_tblCampaign>()
                .Property(e => e.CRMCampaignID)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCampaign>()
                .HasMany(e => e.NS_tblCompanyContacts)
                .WithRequired(e => e.NS_tblCampaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCampaign>()
                .HasMany(e => e.NS_tblCompanyInfo)
                .WithRequired(e => e.NS_tblCampaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCampaign>()
                .HasMany(e => e.NS_tblPartnerCapabilityCompany)
                .WithRequired(e => e.NS_tblCampaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCampaign>()
                .HasMany(e => e.NS_tblPartnerProgramCompany)
                .WithRequired(e => e.NS_tblCampaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCampaign>()
                .HasMany(e => e.NS_tblProductCompany)
                .WithRequired(e => e.NS_tblCampaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCampaign>()
                .HasMany(e => e.NS_tblProductFamilyCompany)
                .WithRequired(e => e.NS_tblCampaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCompany>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompany>()
                .HasMany(e => e.NS_tblCompanyContacts)
                .WithRequired(e => e.NS_tblCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCompany>()
                .HasMany(e => e.NS_tblCompanyInfo)
                .WithRequired(e => e.NS_tblCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCompany>()
                .HasMany(e => e.NS_tblPartnerCapabilityCompany)
                .WithRequired(e => e.NS_tblCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCompany>()
                .HasMany(e => e.NS_tblPartnerProgramCompany)
                .WithRequired(e => e.NS_tblCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCompany>()
                .HasMany(e => e.NS_tblProductCompany)
                .WithRequired(e => e.NS_tblCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCompany>()
                .HasMany(e => e.NS_tblProductFamilyCompany)
                .WithRequired(e => e.NS_tblCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.LeadID)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber1)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber2)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber3)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber4)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber5)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber6)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber7)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber8)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.VolumeLicenceNumber9)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.MicrosoftPartnerPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyInfo>()
                .Property(e => e.CustomOrBasicsApplications)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblCompanyType>()
                .Property(e => e.CompanyTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblIndustry>()
                .HasMany(e => e.NS_tblCompany)
                .WithRequired(e => e.NS_tblIndustry)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblLead>()
                .Property(e => e.CRMLeadID)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblPartnerCapability>()
                .Property(e => e.PartnerCapabilityName)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblPartnerCapability>()
                .HasMany(e => e.NS_tblPartnerCapabilityCompany)
                .WithRequired(e => e.NS_tblPartnerCapability)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblPartnerCapabilityCompany>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblPartnerCapabilityCompany>()
                .Property(e => e.IDPartner)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblPartnerProgram>()
                .Property(e => e.PartnerProgramName)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblPartnerProgram>()
                .HasMany(e => e.NS_tblPartnerProgramCompany)
                .WithRequired(e => e.NS_tblPartnerProgram)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblPartnerProgramCompany>()
                .Property(e => e.IDPartner)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblProduct>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblProduct>()
                .Property(e => e.ProductVersion)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblProduct>()
                .HasMany(e => e.NS_tblProductCompany)
                .WithRequired(e => e.NS_tblProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NS_tblProductFamily>()
                .Property(e => e.ProductFamilyName)
                .IsUnicode(false);

            modelBuilder.Entity<NS_tblProductFamily>()
                .HasMany(e => e.NS_tblProductFamilyCompany)
                .WithRequired(e => e.NS_tblProductFamily)
                .WillCascadeOnDelete(false);


            //modelBuilder.Entity<User>()
            //    .HasKey<Guid>(e => e.Id);

            //modelBuilder.Entity<MyRole>()
            //    .HasKey<Guid>(e => e.Id);

            //modelBuilder.Entity<MyLogin>()
            //    .HasKey<Guid>(e => e.UserId);

            //modelBuilder.Entity<UserRole>()
            //    .HasKey(e => new { e.RoleId, e.UserId })
            //    .HasRequired(p=> new { p.UserId, p.RoleId });




        }
    }
}
