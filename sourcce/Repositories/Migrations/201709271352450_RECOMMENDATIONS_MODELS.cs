namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RECOMMENDATIONS_MODELS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_tblVariable",
                c => new
                    {
                        VariableID = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.Short(nullable: false),
                        SelectAllFamilies = c.Boolean(),
                        SelectAllProducts = c.Boolean(),
                        Selector = c.String(maxLength: 50),
                        Field = c.String(maxLength: 50),
                        IsCorporate = c.Boolean(),
                        IsCommercial = c.Boolean(),
                        IsSupported = c.Boolean(),
                        CustomerVariable = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.VariableID);
            
            CreateTable(
                "dbo.NS_tblVariableProduct",
                c => new
                    {
                        VariableProductID = c.Short(nullable: false, identity: true),
                        VariableID = c.Short(nullable: false),
                        ProductID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.VariableProductID)
                .ForeignKey("dbo.NS_tblProduct", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.NS_tblVariable", t => t.VariableID, cascadeDelete: true)
                .Index(t => t.VariableID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.NS_tblVariableProductFamily",
                c => new
                    {
                        VariableProductFamilyID = c.Short(nullable: false, identity: true),
                        VariableID = c.Short(nullable: false),
                        ProductFamilyID = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.VariableProductFamilyID)
                .ForeignKey("dbo.NS_tblProductFamily", t => t.ProductFamilyID, cascadeDelete: true)
                .ForeignKey("dbo.NS_tblVariable", t => t.VariableID, cascadeDelete: true)
                .Index(t => t.VariableID)
                .Index(t => t.ProductFamilyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_tblVariableProductFamily", "VariableID", "dbo.NS_tblVariable");
            DropForeignKey("dbo.NS_tblVariableProductFamily", "ProductFamilyID", "dbo.NS_tblProductFamily");
            DropForeignKey("dbo.NS_tblVariableProduct", "VariableID", "dbo.NS_tblVariable");
            DropForeignKey("dbo.NS_tblVariableProduct", "ProductID", "dbo.NS_tblProduct");
            DropIndex("dbo.NS_tblVariableProductFamily", new[] { "ProductFamilyID" });
            DropIndex("dbo.NS_tblVariableProductFamily", new[] { "VariableID" });
            DropIndex("dbo.NS_tblVariableProduct", new[] { "ProductID" });
            DropIndex("dbo.NS_tblVariableProduct", new[] { "VariableID" });
            DropTable("dbo.NS_tblVariableProductFamily");
            DropTable("dbo.NS_tblVariableProduct");
            DropTable("dbo.NS_tblVariable");
        }
    }
}
