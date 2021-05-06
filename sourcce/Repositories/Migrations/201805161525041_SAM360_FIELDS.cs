namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SAM360_FIELDS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeadInformations", "SAM360OrganisationId", c => c.Int());
            AddColumn("dbo.LeadInformations", "SAM360UserId", c => c.Int());
            AddColumn("dbo.LeadInformations", "SAM360CompanyUser", c => c.String());
            AddColumn("dbo.LeadInformations", "SAM360CompanyPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeadInformations", "SAM360CompanyPassword");
            DropColumn("dbo.LeadInformations", "SAM360CompanyUser");
            DropColumn("dbo.LeadInformations", "SAM360UserId");
            DropColumn("dbo.LeadInformations", "SAM360OrganisationId");
        }
    }
}
