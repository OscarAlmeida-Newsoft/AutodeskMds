namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_TABLE_DIGITALTRANSFORMATIONVERSIONS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_TblDigitalTransformationVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DigitalTransformationId = c.Int(nullable: false),
                        ProcessId = c.Int(nullable: false),
                        Pillar = c.String(),
                        Technology = c.String(),
                        Brand = c.String(),
                        Comment = c.String(),
                        Solution = c.String(),
                        Priority = c.Int(nullable: false),
                        Exist = c.Boolean(nullable: false),
                        User = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NS_TblDigitalTransformation", t => t.DigitalTransformationId, cascadeDelete: true)
                .ForeignKey("dbo.NS_TblProcess", t => t.ProcessId, cascadeDelete: false)
                .Index(t => t.DigitalTransformationId)
                .Index(t => t.ProcessId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_TblDigitalTransformationVersions", "ProcessId", "dbo.NS_TblProcess");
            DropForeignKey("dbo.NS_TblDigitalTransformationVersions", "DigitalTransformationId", "dbo.NS_TblDigitalTransformation");
            DropIndex("dbo.NS_TblDigitalTransformationVersions", new[] { "ProcessId" });
            DropIndex("dbo.NS_TblDigitalTransformationVersions", new[] { "DigitalTransformationId" });
            DropTable("dbo.NS_TblDigitalTransformationVersions");
        }
    }
}
