namespace Parichay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        AddressName = c.String(),
                        AddressLineOne = c.String(),
                        AddressLineTwo = c.String(),
                        State = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        pincode = c.String(),
                        DefaultAddress = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        EmailId = c.String(),
                        GstNumber = c.String(),
                        MobileNumberOne = c.String(),
                        MobileNumbertwo = c.String(),
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
                "dbo.SecurePasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        Purpose = c.String(),
                        Password = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserMsts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        LoginID = c.String(),
                        Password = c.String(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        EmailID = c.String(),
                        PhoneNumber = c.String(),
                        LoggedInIp = c.String(),
                        LastLoginDateTime = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.Addresses", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Addresses", new[] { "Customer_Id" });
            DropTable("dbo.UserMsts");
            DropTable("dbo.SecurePasswords");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
