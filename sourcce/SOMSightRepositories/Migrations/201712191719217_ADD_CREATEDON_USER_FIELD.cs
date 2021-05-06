namespace SOMSightRepositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_CREATEDON_USER_FIELD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreatedOn");
        }
    }
}
