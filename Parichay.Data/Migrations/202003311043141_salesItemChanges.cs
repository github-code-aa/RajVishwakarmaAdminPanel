namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salesItemChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesItems", "Warranty", c => c.Double(nullable: false));
            AlterColumn("dbo.SalesItems", "Qunatity", c => c.Double(nullable: false));
            AlterColumn("dbo.SalesItems", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesItems", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.SalesItems", "Qunatity", c => c.Int(nullable: false));
            AlterColumn("dbo.SalesItems", "Warranty", c => c.Int(nullable: false));
        }
    }
}
