namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_discountinsales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "Discount");
        }
    }
}
