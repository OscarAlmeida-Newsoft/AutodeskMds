namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_ASSESSMENT_GLOBAL_MATURITY_CALCULATION_SP : DbMigration
    {

        private const string CREATE_SP = "CREATE PROCEDURE [dbo].[NS_spCalculateAssesstmenGlobalMaturityLevel]  "
            + "@AssessmentTypeId INT, "
            + "@CampaignId UNIQUEIDENTIFIER, "
            + "@CompanyId UNIQUEIDENTIFIER "
            + "AS "
            + "BEGIN "
            + "DECLARE @TotalPoints INT "
            + "DECLARE @GlobalMaturityLevelId VARCHAR(1) "
            + " "
            + ";WITH TOTAL_POINTS_SUM AS ( "
            + "SELECT SUM(AXA.Points) Points "
            + "FROM [NS_dbSOMSightShared].[dbo].[NS_TblAssessmentQuestion] AQ "
            + "INNER JOIN [dbo].[NS_TblAnswerXAssessment] AXA ON AQ.Id = AXA.AssessmentId "
            + "WHERE AQ.AssessmentTypeId = @AssessmentTypeId "
            + "AND AXA.CampaignId = @CampaignId AND CompanyId = @CompanyId "
            + ") "
            + "SELECT @TotalPoints = Points "
            + "FROM TOTAL_POINTS_SUM "
            + " "
            + "SELECT @GlobalMaturityLevelId = MaturityLevelId "
            + "FROM [NS_dbSOMSightShared].[dbo].[NS_tblScoringXRanges] SXR "
            + "WHERE SXR.AssessmentTypeId = @AssessmentTypeId "
            + "AND( "
            + " SXR.CeilingValue >= @TotalPoints "
            + " AND "
            + " SXR.FloorValue <= @TotalPoints "
            + ") "
            + " "
            + "SELECT @GlobalMaturityLevelId "
            + " "
            + "END ";

        private const string DELETE_SP = " IF (OBJECT_ID('[dbo].[NS_spCalculateAssesstmenGlobalMaturityLevel]') IS NOT NULL) DROP PROCEDURE [dbo].[NS_spCalculateAssesstmenGlobalMaturityLevel]";


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
