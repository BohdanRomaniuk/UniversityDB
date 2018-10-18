namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassesRules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassesRules",
                c => new
                    {
                        SClassRulesId = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        ClassIdInside = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SClassRulesId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.ClassIdInside)
                .Index(t => t.ClassId)
                .Index(t => t.ClassIdInside);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassesRules", "ClassIdInside", "dbo.Classes");
            DropForeignKey("dbo.ClassesRules", "ClassId", "dbo.Classes");
            DropIndex("dbo.ClassesRules", new[] { "ClassIdInside" });
            DropIndex("dbo.ClassesRules", new[] { "ClassId" });
            DropTable("dbo.ClassesRules");
        }
    }
}
