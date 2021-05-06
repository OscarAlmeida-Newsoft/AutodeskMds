namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_ASSESSMENTSUMMARY_ADD_NEXTSTEPS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NS_TblAssessmentSummary", "NextSteps", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NS_TblAssessmentSummary", "NextSteps");
        }
    }
}
