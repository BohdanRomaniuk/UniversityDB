namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Objects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Class = c.Int(nullable: false),
                        UObject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.UObject_Id)
                .Index(t => t.UObject_Id);
            
            CreateTable(
                "dbo.Metherials",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StudyingPersons",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AvarageMark = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.WorkingPersons",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkingPersons", "Id", "dbo.Persons");
            DropForeignKey("dbo.StudyingPersons", "Id", "dbo.Persons");
            DropForeignKey("dbo.Persons", "Id", "dbo.Objects");
            DropForeignKey("dbo.Metherials", "Id", "dbo.Objects");
            DropForeignKey("dbo.Objects", "UObject_Id", "dbo.Objects");
            DropIndex("dbo.WorkingPersons", new[] { "Id" });
            DropIndex("dbo.StudyingPersons", new[] { "Id" });
            DropIndex("dbo.Persons", new[] { "Id" });
            DropIndex("dbo.Metherials", new[] { "Id" });
            DropIndex("dbo.Objects", new[] { "UObject_Id" });
            DropTable("dbo.WorkingPersons");
            DropTable("dbo.StudyingPersons");
            DropTable("dbo.Persons");
            DropTable("dbo.Metherials");
            DropTable("dbo.Objects");
        }
    }
}
