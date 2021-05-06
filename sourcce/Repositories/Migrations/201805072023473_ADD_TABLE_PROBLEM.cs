namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_PROBLEM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblProblem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(nullable: false),
                        ProblemDescription = c.String(),
                        Opportunity = c.String(),
                        Technology = c.String(),
                        Inventory = c.String(),
                        Priority = c.Int(nullable: false),
                        Exist = c.Boolean(nullable: false),
                        Microsoft = c.Boolean(nullable: false),
                        Solution = c.String(),
                        UserCreation = c.Guid(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        UserLastModification = c.Guid(nullable: false),
                        DateLastModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblProcess", t => t.ProcessId, cascadeDelete: true)
                .Index(t => t.ProcessId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblProblem", "ProcessId", "dbo.NS_TblProcess");
            DropIndex("dbo.NS_TblProblem", new[] { "ProcessId" });
            DropTable("dbo.NS_TblProblem");
        }
    }
}
