namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_PROCESS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblProcess",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssessmentSummaryId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        UserCreation = c.Guid(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        UserLastModification = c.Guid(nullable: false),
                        DateLastModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblActivity", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.NS_TblAssessmentSummary", t => t.AssessmentSummaryId, cascadeDelete: true)
                .Index(t => t.AssessmentSummaryId)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblProcess", "AssessmentSummaryId", "dbo.NS_TblAssessmentSummary");
            DropForeignKey("dbo.NS_TblProcess", "ActivityId", "dbo.NS_TblActivity");
            DropIndex("dbo.NS_TblProcess", new[] { "ActivityId" });
            DropIndex("dbo.NS_TblProcess", new[] { "AssessmentSummaryId" });
            DropTable("dbo.NS_TblProcess");
        }
    }
}
