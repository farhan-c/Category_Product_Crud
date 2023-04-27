namespace Crud_Operation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        products_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Products", t => t.products_ProductId)
                .Index(t => t.products_ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "products_ProductId", "dbo.Products");
            DropIndex("dbo.Categories", new[] { "products_ProductId" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
