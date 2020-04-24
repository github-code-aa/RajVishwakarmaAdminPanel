namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        SalesId = c.String(),
                        CustomerId = c.Int(nullable: false),
                        DefaultCustomerAddressId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalesItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        Warranty = c.Int(nullable: false),
                        Qunatity = c.Int(nullable: false),
                        HSN = c.String(),
                        Amount = c.Int(nullable: false),
                        SalesId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sales", t => t.SalesId, cascadeDelete: true)
                .Index(t => t.SalesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesItems", "SalesId", "dbo.Sales");
            DropIndex("dbo.SalesItems", new[] { "SalesId" });
            DropTable("dbo.SalesItems");
            DropTable("dbo.Sales");
        }
    }
}
