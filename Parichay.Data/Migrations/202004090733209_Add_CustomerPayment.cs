namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CustomerPayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customerpayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        SalesId = c.String(),
                        PaymentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customerpayments");
        }
    }
}
