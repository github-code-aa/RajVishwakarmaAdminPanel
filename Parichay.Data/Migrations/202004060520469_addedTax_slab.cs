namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTax_slab : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "TaxSlab", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "TaxSlab");
        }
    }
}
