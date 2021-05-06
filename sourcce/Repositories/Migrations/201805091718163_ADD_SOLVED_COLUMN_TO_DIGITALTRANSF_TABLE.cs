namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_SOLVED_COLUMN_TO_DIGITALTRANSF_TABLE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NS_TblDigitalTransformation", "Solved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NS_TblDigitalTransformation", "Solved");
        }
    }
}
