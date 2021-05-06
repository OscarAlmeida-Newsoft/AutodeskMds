namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_PROBLEMVERSIONS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblProblemVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProblemId = c.Int(nullable: false),
                        ProcessId = c.Int(nullable: false),
                        ProblemDescription = c.String(),
                        Opportunity = c.String(),
                        Technology = c.String(),
                        Inventory = c.String(),
                        Solution = c.String(),
                        Priority = c.Int(nullable: false),
                        Exist = c.Boolean(nullable: false),
                        Microsoft = c.Boolean(nullable: false),
                        User = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblProblem", t => t.ProblemId, cascadeDelete: true)
                .ForeignKey("dbo.NS_TblProcess", t => t.ProcessId, cascadeDelete: false)
                .Index(t => t.ProblemId)
                .Index(t => t.ProcessId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblProblemVersions", "ProcessId", "dbo.NS_TblProcess");
            DropForeignKey("dbo.NS_TblProblemVersions", "ProblemId", "dbo.NS_TblProblem");
            DropIndex("dbo.NS_TblProblemVersions", new[] { "ProcessId" });
            DropIndex("dbo.NS_TblProblemVersions", new[] { "ProblemId" });
            DropTable("dbo.NS_TblProblemVersions");
        }
    }
}
