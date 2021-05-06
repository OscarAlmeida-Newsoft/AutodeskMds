namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_INDUSTRYINDINS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_tblIndustryIndIns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TranslatorAdministratorDescriptionId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NS_tblIndustryIndIns");
        }
    }
}
