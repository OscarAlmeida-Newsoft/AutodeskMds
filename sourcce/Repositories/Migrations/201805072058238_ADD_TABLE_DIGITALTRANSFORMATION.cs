namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_DIGITALTRANSFORMATION : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblDigitalTransformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(nullable: false),
                        Pillar = c.String(),
                        Technology = c.String(),
                        Brand = c.String(),
                        Comment = c.String(),
                        Solution = c.String(),
                        Priority = c.Int(nullable: false),
                        Exist = c.Boolean(nullable: false),
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
            DropForeignKey("dbo.NS_TblDigitalTransformation", "ProcessId", "dbo.NS_TblProcess");
            DropIndex("dbo.NS_TblDigitalTransformation", new[] { "ProcessId" });
            DropTable("dbo.NS_TblDigitalTransformation");
        }
    }
}
