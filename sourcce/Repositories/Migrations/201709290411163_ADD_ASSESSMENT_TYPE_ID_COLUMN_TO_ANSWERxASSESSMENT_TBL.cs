namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_ASSESSMENT_TYPE_ID_COLUMN_TO_ANSWERxASSESSMENT_TBL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NS_TblAnswerXAssessment", "AssessmentTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NS_TblAnswerXAssessment", "AssessmentTypeId");
        }
    }
}
