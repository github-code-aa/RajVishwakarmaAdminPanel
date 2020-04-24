namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salesItemChanges_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesItems", "Warranty", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesItems", "Warranty", c => c.Double(nullable: false));
        }
    }
}
