namespace Crud_Operation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_Isvalue_Column : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
