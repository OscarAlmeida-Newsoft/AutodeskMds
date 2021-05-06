namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_PROCESSVERSIONS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblProcessVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(nullable: false),
                        AssessmentSummaryId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        User = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblActivity", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.NS_TblAssessmentSummary", t => t.AssessmentSummaryId, cascadeDelete: true)
                .ForeignKey("dbo.NS_TblProcess", t => t.ProcessId, cascadeDelete: false)
                .Index(t => t.ProcessId)
                .Index(t => t.AssessmentSummaryId)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblProcessVersions", "ProcessId", "dbo.NS_TblProcess");
            DropForeignKey("dbo.NS_TblProcessVersions", "AssessmentSummaryId", "dbo.NS_TblAssessmentSummary");
            DropForeignKey("dbo.NS_TblProcessVersions", "ActivityId", "dbo.NS_TblActivity");
            DropIndex("dbo.NS_TblProcessVersions", new[] { "ActivityId" });
            DropIndex("dbo.NS_TblProcessVersions", new[] { "AssessmentSummaryId" });
            DropIndex("dbo.NS_TblProcessVersions", new[] { "ProcessId" });
            DropTable("dbo.NS_TblProcessVersions");
        }
    }
}
