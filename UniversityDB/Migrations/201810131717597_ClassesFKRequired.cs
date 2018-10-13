namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassesFKRequired : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FormName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Objects", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Objects", "ClassId");
            AddForeignKey("dbo.Objects", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            DropColumn("dbo.Objects", "Class");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objects", "Class", c => c.Int(nullable: false));
            DropForeignKey("dbo.Objects", "ClassId", "dbo.Classes");
            DropIndex("dbo.Objects", new[] { "ClassId" });
            DropColumn("dbo.Objects", "ClassId");
            DropTable("dbo.Classes");
        }
    }
}
