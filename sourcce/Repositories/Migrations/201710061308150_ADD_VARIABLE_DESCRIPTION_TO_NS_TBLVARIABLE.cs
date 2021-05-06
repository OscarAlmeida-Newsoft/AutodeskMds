namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_VARIABLE_DESCRIPTION_TO_NS_TBLVARIABLE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NS_tblVariable", "Description", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NS_tblVariable", "Description");
        }
    }
}
