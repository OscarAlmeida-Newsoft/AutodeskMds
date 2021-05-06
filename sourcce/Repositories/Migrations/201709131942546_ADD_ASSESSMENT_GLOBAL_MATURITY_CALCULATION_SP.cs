namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_ASSESSMENT_GLOBAL_MATURITY_CALCULATION_SP : DbMigration
    {
        private const string CREATE_SP = "CREATE PROCEDURE [dbo].[NS_spCalculateAssesstmenGlobalMaturityLevel]  "
            + "@AssessmentTypeId INT, "
            + "@CampaignId UNIQUEIDENTIFIER, "
            + "@CompanyId UNIQUEIDENTIFIER "
            + "AS "
            + "BEGIN "
            + "DECLARE @GlobalMaturityLevel INT "
            + ";WITH WEIGHT_BY_CAPABILITY AS ( "
            + "SELECT CAP.Id [CapabilityId] "
            + "    , CAP.Weight [Weight] "
            + "    , COUNT(1) [Questions] "
            + "FROM [NS_dbSOMSightShared].[dbo].[NS_TblAssessmentQuestion] AQ "
            + "INNER JOIN [NS_dbSOMSightShared].[dbo].[NS_TblCapability] CAP ON CAP.Id = AQ.CapabilityId "
            + "WHERE CAP.AssessmentTypeId = @AssessmentTypeId "
            + "GROUP BY CAP.Id, CAP.Weight "
            + ") "
            + ", RESULTS AS( "
            + " SELECT (WXC.Weight*0.01 /WXC.Questions) * AXA.Points [WeightByQuestion] "
            + "FROM [dbo].[NS_TblAnswerXAssessment] AXA "
            + "INNER JOIN [NS_dbSOMSightShared].[dbo].[NS_TblAssessmentQuestion] AQ ON AQ.Id = AXA.AssessmentId "
            + "INNER JOIN WEIGHT_BY_CAPABILITY WXC ON WXC.CapabilityId = AQ.CapabilityId "
            + "WHERE AXA.CampaignId = @CampaignId AND CompanyId = @CompanyId "
            + ") "
            + "SELECT @GlobalMaturityLevel =(ROUND(SUM(RESULTS.WeightByQuestion),0))  FROM RESULTS "
            + " "
            + "SELECT CASE @GlobalMaturityLevel  "
            + "     WHEN 1 THEN 'A' "
            + "     WHEN 2 THEN 'B' "
            + "     WHEN 3 THEN 'C' "
            + "     WHEN 4 THEN 'D' "
            + "END [GlobalMaturityLevel] "
            + "END ";

        private const string DELETE_SP = " IF (OBJECT_ID('[dbo].[NS_spCalculateAssessmentGlobalMaturityLevel]') IS NOT NULL) DROP PROCEDURE [dbo].[NS_spCalculateAssessmentGlobalMaturityLevel]";


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
