namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_salesDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "SalesDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "SalesDate");
        }
    }
}
