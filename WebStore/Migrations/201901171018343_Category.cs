namespace WebStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategories", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategories", "Name", c => c.String());
        }
    }
}
