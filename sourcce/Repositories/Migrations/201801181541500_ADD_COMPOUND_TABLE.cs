namespace Repositories
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_COMPOUND_TABLE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NS_tblCompoundVariable",
                c => new
                    {
                        CompoundVariableExpressionID = c.Short(nullable: false, identity: true),
                        VariableID = c.Short(),
                        Order = c.Short(),
                        MathOperator = c.String(),
                        StaticValue = c.Double(),
                        InitialBrackets = c.Boolean(nullable: false),
                        FinalBrackets = c.Boolean(nullable: false),
                        AassociateVariableID = c.Short(),
                    })
                .PrimaryKey(t => t.CompoundVariableExpressionID)
                .ForeignKey("dbo.NS_tblVariable", t => t.AassociateVariableID)
                .ForeignKey("dbo.NS_tblVariable", t => t.VariableID)
                .Index(t => t.VariableID)
                .Index(t => t.AassociateVariableID);
            
            AddColumn("dbo.NS_tblVariable", "IsMathExpression", c => c.Boolean());
            AddColumn("dbo.NS_tblVariable", "MathExpression", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NS_tblCompoundVariable", "VariableID", "dbo.NS_tblVariable");
            DropForeignKey("dbo.NS_tblCompoundVariable", "AassociateVariableID", "dbo.NS_tblVariable");
            DropIndex("dbo.NS_tblCompoundVariable", new[] { "AassociateVariableID" });
            DropIndex("dbo.NS_tblCompoundVariable", new[] { "VariableID" });
            DropColumn("dbo.NS_tblVariable", "MathExpression");
            DropColumn("dbo.NS_tblVariable", "IsMathExpression");
            DropTable("dbo.NS_tblCompoundVariable");
        }
    }
}
