namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NEW_CHANGES : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_tblProductCompanyFile",
                c => new
                    {
                        CompanyID = c.Int(nullable: false),
                        ProductID = c.Short(nullable: false),
                        CampaignID = c.Short(nullable: false),
                        FileNumber = c.Short(nullable: false),
                        AlternativeName = c.String(),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => new { t.CompanyID, t.ProductID, t.CampaignID, t.FileNumber })
                .ForeignKey("dbo.NS_tblCampaign", t => t.CampaignID, cascadeDelete: true)
                .ForeignKey("dbo.NS_tblCompany", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.NS_tblProduct", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.ProductID)
                .Index(t => t.CampaignID);
            
            AddColumn("dbo.NS_tblCompanyInfo", "AzureFlag", c => c.Boolean());
            AddColumn("dbo.NS_tblProductFamily", "OrderFamily", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_tblProductCompanyFile", "ProductID", "dbo.NS_tblProduct");
            DropForeignKey("dbo.NS_tblProductCompanyFile", "CompanyID", "dbo.NS_tblCompany");
            DropForeignKey("dbo.NS_tblProductCompanyFile", "CampaignID", "dbo.NS_tblCampaign");
            DropIndex("dbo.NS_tblProductCompanyFile", new[] { "CampaignID" });
            DropIndex("dbo.NS_tblProductCompanyFile", new[] { "ProductID" });
            DropIndex("dbo.NS_tblProductCompanyFile", new[] { "CompanyID" });
            DropColumn("dbo.NS_tblProductFamily", "OrderFamily");
            DropColumn("dbo.NS_tblCompanyInfo", "AzureFlag");
            DropTable("dbo.NS_tblProductCompanyFile");
        }
    }
}
