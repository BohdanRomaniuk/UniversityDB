namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomsAndInventory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objects", "RoomNumber", c => c.Int());
            AddColumn("dbo.Objects", "Discriminator", c => c.String(maxLength: 128));
            AddColumn("dbo.Inventories", "InventoryNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "InventoryNumber");
            DropColumn("dbo.Objects", "Discriminator");
            DropColumn("dbo.Objects", "RoomNumber");
        }
    }
}
