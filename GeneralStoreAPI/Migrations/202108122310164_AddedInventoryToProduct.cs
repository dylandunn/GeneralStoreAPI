namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInventoryToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NumberInInventory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NumberInInventory");
        }
    }
}
