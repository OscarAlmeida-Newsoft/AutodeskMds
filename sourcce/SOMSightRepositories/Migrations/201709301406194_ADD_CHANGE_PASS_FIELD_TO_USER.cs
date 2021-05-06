namespace SOMSightRepositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_CHANGE_PASS_FIELD_TO_USER : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ChangePassword", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ChangePassword");
        }
    }
}
