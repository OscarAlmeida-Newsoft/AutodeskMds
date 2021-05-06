namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_SAMLIVEUSER_COLUMN : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadInformations", "CompanySAMLiveUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadInformations", "CompanySAMLiveUserName");
        }
    }
}
