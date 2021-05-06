namespace SOMSightRepositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_COMPANY_NAME_AND_COMPANY_CONTACT_FIELD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ContactName");
            DropColumn("dbo.AspNetUsers", "CompanyName");
        }
    }
}
