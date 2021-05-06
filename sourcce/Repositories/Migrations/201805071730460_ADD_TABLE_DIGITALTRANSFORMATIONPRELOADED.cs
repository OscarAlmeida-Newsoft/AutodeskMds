namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_DIGITALTRANSFORMATIONPRELOADED : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblDigitalTransformationPreLoaded",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessPreLoadedId = c.Int(nullable: false),
                        TranslatorAdministratorPillarId = c.Int(nullable: false),
                        TranslatorAdministratorTechnologyId = c.Int(nullable: false),
                        TranslatorAdministratorBrandId = c.Int(nullable: false),
                        TranslatorAdministratorCommentId = c.Int(nullable: false),
                        TranslatorAdministratorSolutionId = c.Int(),
                        Priority = c.Int(nullable: false),
                        Exist = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblProcessPreLoaded", t => t.ProcessPreLoadedId, cascadeDelete: true)
                .Index(t => t.ProcessPreLoadedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblDigitalTransformationPreLoaded", "ProcessPreLoadedId", "dbo.NS_TblProcessPreLoaded");
            DropIndex("dbo.NS_TblDigitalTransformationPreLoaded", new[] { "ProcessPreLoadedId" });
            DropTable("dbo.NS_TblDigitalTransformationPreLoaded");
        }
    }
}
