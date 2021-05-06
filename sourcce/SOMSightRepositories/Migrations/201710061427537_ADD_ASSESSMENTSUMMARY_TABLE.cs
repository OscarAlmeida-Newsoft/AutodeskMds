namespace SOMSightRepositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_ASSESSMENTSUMMARY_TABLE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblAssessmentSummary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        AssessmentTypeId = c.Int(nullable: false),
                        Code = c.String(maxLength: 1, unicode: false),
                        GlobalMaturityLevelId = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ResponseStartDate = c.DateTime(),
                        ResponseEndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NS_TblAssessmentSummary");
        }
    }
}
