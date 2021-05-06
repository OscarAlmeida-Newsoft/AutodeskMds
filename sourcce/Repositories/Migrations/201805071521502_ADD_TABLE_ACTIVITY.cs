namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_ACTIVITY : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblActivity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeActivityId = c.Int(nullable: false),
                        TranslatorAdministratorDescriptionId = c.Int(nullable: false),
                        TranslatorAdministratorDefinitionId = c.Int(nullable: false),
                        TranslatorAdministratorExampleId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblTypeActivity", t => t.TypeActivityId, cascadeDelete: true)
                .Index(t => t.TypeActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblActivity", "TypeActivityId", "dbo.NS_TblTypeActivity");
            DropIndex("dbo.NS_TblActivity", new[] { "TypeActivityId" });
            DropTable("dbo.NS_TblActivity");
        }
    }
}
