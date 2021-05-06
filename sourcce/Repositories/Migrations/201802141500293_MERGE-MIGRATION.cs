namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MERGEMIGRATION : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblAnswerXAssessment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Guid(nullable: false),
                        CampaignId = c.Guid(nullable: false),
                        AssessmentId = c.Int(nullable: false),
                        AssessmentTypeId = c.Int(nullable: false),
                        Response = c.String(maxLength: 1, unicode: false),
                        Points = c.Int(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NS_TblAssessmentSummary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Guid(nullable: false),
                        CampaignId = c.Guid(nullable: false),
                        AssessmentTypeId = c.Int(nullable: false),
                        Code = c.String(maxLength: 1, unicode: false),
                        GlobalMaturityLevelId = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ResponseStartDate = c.DateTime(),
                        ResponseEndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LeadInformations", "CompanySAMLiveUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadInformations", "CompanySAMLiveUserName");
            DropTable("dbo.NS_TblAssessmentSummary");
            DropTable("dbo.NS_TblAnswerXAssessment");
        }
    }
}
