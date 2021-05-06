namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_PROCESSPRELOADED : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblProcessPreLoaded",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndustryId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        TranslatorAdministratorNameId = c.Int(nullable: false),
                        TranslatorAdministratorDescriptionId = c.Int(nullable: false),
                        Industry_IndustryID = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblActivity", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.NS_tblIndustry", t => t.Industry_IndustryID)
                .Index(t => t.ActivityId)
                .Index(t => t.Industry_IndustryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblProcessPreLoaded", "Industry_IndustryID", "dbo.NS_tblIndustry");
            DropForeignKey("dbo.NS_TblProcessPreLoaded", "ActivityId", "dbo.NS_TblActivity");
            DropIndex("dbo.NS_TblProcessPreLoaded", new[] { "Industry_IndustryID" });
            DropIndex("dbo.NS_TblProcessPreLoaded", new[] { "ActivityId" });
            DropTable("dbo.NS_TblProcessPreLoaded");
        }
    }
}
