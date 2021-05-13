namespace CustomerApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclient : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CustomerCustomerFiles", newName: "CustomerFileCustomers");
            DropPrimaryKey("dbo.CustomerFileCustomers");
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Mobile = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        UID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddPrimaryKey("dbo.CustomerFileCustomers", new[] { "CustomerFile_CustomerFileId", "Customer_CustomerId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CustomerFileCustomers");
            DropTable("dbo.Clients");
            AddPrimaryKey("dbo.CustomerFileCustomers", new[] { "Customer_CustomerId", "CustomerFile_CustomerFileId" });
            RenameTable(name: "dbo.CustomerFileCustomers", newName: "CustomerCustomerFiles");
        }
    }
}
