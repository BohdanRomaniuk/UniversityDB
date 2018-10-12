namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SelfFKNullable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Objects", name: "UObject_Id", newName: "ParentId");
            RenameIndex(table: "dbo.Objects", name: "IX_UObject_Id", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Objects", name: "IX_ParentId", newName: "IX_UObject_Id");
            RenameColumn(table: "dbo.Objects", name: "ParentId", newName: "UObject_Id");
        }
    }
}
