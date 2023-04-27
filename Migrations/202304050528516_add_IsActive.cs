namespace Crud_Operation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_IsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsActive");
        }
    }
}
