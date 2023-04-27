namespace Crud_Operation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "category_CategoryId" });
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "category_CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "category_CategoryId", c => c.Int());
            DropColumn("dbo.Products", "CategoryId");
            CreateIndex("dbo.Products", "category_CategoryId");
            AddForeignKey("dbo.Products", "category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
