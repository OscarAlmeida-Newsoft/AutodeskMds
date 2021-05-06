namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_UPDATE_AND_DELETE_ROW_SP : DbMigration
    {
        private const string CREATE_SP = "CREATE PROCEDURE [dbo].[NS_spAnswerCompany_UpdateAndDeleteRow_LicenseID] "
 + "@CompanyID as int, "
 + "@CampaignID as smallint, "
 + "@IsVirtual as bit, "
 + "@LicenseID as int, "
 + "@FamilyID as smallint "
 + "AS "
 + "BEGIN "
 + "SET NOCOUNT ON; "
 + "DECLARE @serverNumber as INT "
 + "set @serverNumber =(SELECT COUNT(DISTINCT ans.LicenseID) "
 + "FROM [dbo].[NS_tblAnswerCompany] as ans "
 + "LEFT JOIN [dbo].[NS_tblProduct] as prod "
 + "ON ans.ProductID = prod.ProductID "
 + "WHERE	CompanyID = @CompanyID	AND  "
 + "CampaignID =@CampaignID	AND  "
 + "IsVirtual = @IsVirtual AND "
 + "prod.ProductFamilyID = @FamilyID); "
 + "DELETE ans "
 + "FROM [dbo].[NS_tblAnswerCompany] as ans "
 + "LEFT JOIN [dbo].[NS_tblProduct] as prod "
 + "ON ans.ProductID = prod.ProductID "
 + "WHERE	CompanyID = @CompanyID			AND  "
 + "CampaignID =@CampaignID			AND  "
 + "IsVirtual = @IsVirtual			AND "
 + "prod.ProductFamilyID = @FamilyID AND "
 + "LicenseID = @LicenseID "
 + "IF (@serverNumber > 1) AND ((@LicenseID+1) < > @serverNumber) BEGIN "
 + "IF(@LicenseID+1 = 1) BEGIN "
 + "UPDATE ans  "
 + "SET ans.LicenseID = (ans.LicenseID-1) "
 + "FROM [dbo].[NS_tblAnswerCompany] as ans "
 + "LEFT JOIN [dbo].[NS_tblProduct] as prod "
 + "ON ans.ProductID = prod.ProductID "
 + "WHERE	CompanyID = @CompanyID			AND  "
 + "CampaignID =@CampaignID			AND  "
 + "IsVirtual = @IsVirtual			AND "
 + "prod.ProductFamilyID = @FamilyID  "
 + "END "
 + "ELSE BEGIN "
 + "UPDATE ans  "
 + "SET ans.LicenseID = (ans.LicenseID-1) "
 + "FROM [dbo].[NS_tblAnswerCompany] as ans "
 + "LEFT JOIN [dbo].[NS_tblProduct] as prod "
 + "ON ans.ProductID = prod.ProductID "
 + "WHERE	CompanyID = @CompanyID			AND  "
 + "CampaignID =@CampaignID			AND  "
 + "IsVirtual = @IsVirtual			AND "
 + "prod.ProductFamilyID = @FamilyID AND "
 + "LicenseID > @LicenseID "
 + "END "
 + "END "
 + "END";


        private const string DELETE_SP = " IF (OBJECT_ID('[dbo].[NS_spAnswerCompany_UpdateAndDeleteRow_LicenseID]') IS NOT NULL) DROP PROCEDURE [dbo].[NS_spAnswerCompany_UpdateAndDeleteRow_LicenseID]";


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
