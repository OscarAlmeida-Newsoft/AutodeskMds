namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_ASSESSMENT_SCORING_MODEL_SP : DbMigration
    {
        private const string CREATE_SP = "CREATE PROCEDURE [dbo].[NS_spGetAssessmentsScoringModelResults] "
            + "@AssessmentTypeID INT, "
            + "@CampaignId UNIQUEIDENTIFIER, "
            + "@LeadId UNIQUEIDENTIFIER "
            + "AS "
            + "BEGIN "
            + ";WITH AVGPOINTS AS ( "
            + "     SELECT C.Id 'CapabilityId' "
            + "     ,AVG(AXA.Points) 'PointsAVG' "
            + "     FROM [NS_dbSOMSightShared].[dbo].[NS_TblCapability] C "
            + "     INNER JOIN [NS_dbSOMSightShared].[dbo].[NS_TblAssessmentQuestion] AQ ON C.Id = AQ.CapabilityId AND C.AssessmentTypeId= @AssessmentTypeID "
            + "     INNER JOIN [dbo].[NS_TblAnswerXAssessment] AXA ON AXA.AssessmentId = AQ.Id AND AXA.CampaignId = @CampaignId AND AXA.CompanyId = @LeadId "
            + "     GROUP BY C.Id "
            + ")"
            + "SELECT CapabilityId, "
            + " CASE PointsAVG "
            + "     WHEN 1 THEN 'A' "
            + "     WHEN 2 THEN 'B' "
            + "     WHEN 3 THEN 'C' "
            + "     WHEN 4 THEN 'D' "
            + " END 'PointsAVG' "
            + "FROM AVGPOINTS "
            + "END "
            ;


        private const string DELETE_SP = " IF (OBJECT_ID('[dbo].[NS_spGetAssessmentsScoringModelResults]') IS NOT NULL) DROP PROCEDURE [dbo].[NS_spGetAssessmentsScoringModelResults]";
        public override void Up()
        {
            Sql(DELETE_SP);
            Sql(CREATE_SP);
        }

        public override void Down()
        {
            Sql(DELETE_SP);
        }
    }
}
