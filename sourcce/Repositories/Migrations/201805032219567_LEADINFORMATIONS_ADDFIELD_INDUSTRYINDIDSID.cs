namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LEADINFORMATIONS_ADDFIELD_INDUSTRYINDIDSID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadInformations", "IndustryIndInsId", c => c.Int());
            CreateIndex("dbo.LeadInformations", "IndustryIndInsId");
            AddForeignKey("dbo.LeadInformations", "IndustryIndInsId", "dbo.NS_tblIndustryIndIns", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeadInformations", "IndustryIndInsId", "dbo.NS_tblIndustryIndIns");
            DropIndex("dbo.LeadInformations", new[] { "IndustryIndInsId" });
            DropColumn("dbo.LeadInformations", "IndustryIndInsId");
        }
    }
}
