namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_PROBLEMPRELOADED : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblProblemPreLoaded",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessPreLoadedId = c.Int(nullable: false),
                        TranslatorAdministratorProblemId = c.Int(nullable: false),
                        TranslatorAdministratorOpportunityId = c.Int(nullable: false),
                        TranslatorAdministratorTechnologyId = c.Int(nullable: false),
                        TranslatorAdministratorInventoryId = c.Int(nullable: false),
                        TranslatorAdministratorSolutionId = c.Int(),
                        Priority = c.Int(nullable: false),
                        Exist = c.Boolean(nullable: false),
                        Microsoft = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblProcessPreLoaded", t => t.ProcessPreLoadedId, cascadeDelete: true)
                .Index(t => t.ProcessPreLoadedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblProblemPreLoaded", "ProcessPreLoadedId", "dbo.NS_TblProcessPreLoaded");
            DropIndex("dbo.NS_TblProblemPreLoaded", new[] { "ProcessPreLoadedId" });
            DropTable("dbo.NS_TblProblemPreLoaded");
        }
    }
}
