namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureRedesign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departmets",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Deaneries",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departmets", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SiteUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departmets", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        YearNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudyingPersons", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Rank = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkingPersons", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Id", "dbo.WorkingPersons");
            DropForeignKey("dbo.Students", "Id", "dbo.StudyingPersons");
            DropForeignKey("dbo.Inventories", "Id", "dbo.Objects");
            DropForeignKey("dbo.Faculties", "Id", "dbo.Departmets");
            DropForeignKey("dbo.Deaneries", "Id", "dbo.Departmets");
            DropForeignKey("dbo.Departmets", "Id", "dbo.Objects");
            DropIndex("dbo.Teachers", new[] { "Id" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Inventories", new[] { "Id" });
            DropIndex("dbo.Faculties", new[] { "Id" });
            DropIndex("dbo.Deaneries", new[] { "Id" });
            DropIndex("dbo.Departmets", new[] { "Id" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Inventories");
            DropTable("dbo.Faculties");
            DropTable("dbo.Deaneries");
            DropTable("dbo.Departmets");
        }
    }
}
