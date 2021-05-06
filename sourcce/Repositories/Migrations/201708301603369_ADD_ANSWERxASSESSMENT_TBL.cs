namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_ANSWERxASSESSMENT_TBL : DbMigration
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
                    Response = c.String(maxLength: 1, unicode: false),
                    Points = c.Int(nullable: false),
                    UpdatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropTable("dbo.NS_TblAnswerXAssessment");
        }
    }
}
