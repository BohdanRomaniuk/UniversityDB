namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UkrName = c.String(),
                        FormName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Objects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClassId = c.Int(nullable: false),
                        ParentId = c.Int(),
                        PlanType = c.String(),
                        ApprovedBy = c.String(),
                        Discriminator = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.ParentId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.BotanicGardens",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PlantsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CampBuilding",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ClinicBuildings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        EmployeeCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SportBuildings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FootballFieldsCount = c.String(),
                        SwimingPool = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StudyingBuildings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LectorsRoomsCount = c.Int(nullable: false),
                        PracticeRoomsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.Id)
                .Index(t => t.Id);
            
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
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RegistoryNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ContractType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Gradebooks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AvgMark = c.Double(nullable: false),
                        GradeYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StudentTickets",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ValidDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.EducationalSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LecturesCount = c.Int(nullable: false),
                        PractiveCount = c.Int(nullable: false),
                        Reporting = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Excursions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Transport = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Festivals",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Duration = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MeetingType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        InventoryNumber = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Category = c.String(),
                        Varanty = c.Int(),
                        Type = c.String(),
                        Year = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
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
                "dbo.WorkingPersons",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.Id)
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
            
            CreateTable(
                "dbo.PrintedEditions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Magazines",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MagazineNumber = c.Int(nullable: false),
                        MagazineType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrintedEditions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Newspapers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrintedEditions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ProcessType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Repairs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RepairObject = c.String(),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ReportingType = c.String(),
                        Date = c.DateTime(nullable: false),
                        Mark = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RoomNumber = c.Int(nullable: false),
                        SeatsNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.DiningRooms",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PracticeRooms",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StudyingRooms",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Projector = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Symbols",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SymbolType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objects", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Symbols", "Id", "dbo.Objects");
            DropForeignKey("dbo.StudyingRooms", "Id", "dbo.Rooms");
            DropForeignKey("dbo.PracticeRooms", "Id", "dbo.Rooms");
            DropForeignKey("dbo.DiningRooms", "Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "Id", "dbo.Objects");
            DropForeignKey("dbo.Reports", "Id", "dbo.Objects");
            DropForeignKey("dbo.Repairs", "Id", "dbo.Processes");
            DropForeignKey("dbo.Processes", "Id", "dbo.Objects");
            DropForeignKey("dbo.Newspapers", "Id", "dbo.PrintedEditions");
            DropForeignKey("dbo.Magazines", "Id", "dbo.PrintedEditions");
            DropForeignKey("dbo.PrintedEditions", "Id", "dbo.Objects");
            DropForeignKey("dbo.Teachers", "Id", "dbo.WorkingPersons");
            DropForeignKey("dbo.WorkingPersons", "Id", "dbo.Persons");
            DropForeignKey("dbo.Students", "Id", "dbo.StudyingPersons");
            DropForeignKey("dbo.StudyingPersons", "Id", "dbo.Persons");
            DropForeignKey("dbo.Persons", "Id", "dbo.Objects");
            DropForeignKey("dbo.Metherials", "Id", "dbo.Objects");
            DropForeignKey("dbo.Inventories", "Id", "dbo.Objects");
            DropForeignKey("dbo.Meetings", "Id", "dbo.Events");
            DropForeignKey("dbo.Festivals", "Id", "dbo.Events");
            DropForeignKey("dbo.Excursions", "Id", "dbo.Events");
            DropForeignKey("dbo.Events", "Id", "dbo.Objects");
            DropForeignKey("dbo.EducationalSubjects", "Id", "dbo.Objects");
            DropForeignKey("dbo.StudentTickets", "Id", "dbo.Documents");
            DropForeignKey("dbo.Gradebooks", "Id", "dbo.Documents");
            DropForeignKey("dbo.Contracts", "Id", "dbo.Documents");
            DropForeignKey("dbo.Documents", "Id", "dbo.Objects");
            DropForeignKey("dbo.Faculties", "Id", "dbo.Departmets");
            DropForeignKey("dbo.Deaneries", "Id", "dbo.Departmets");
            DropForeignKey("dbo.Departmets", "Id", "dbo.Objects");
            DropForeignKey("dbo.StudyingBuildings", "Id", "dbo.Buildings");
            DropForeignKey("dbo.SportBuildings", "Id", "dbo.Buildings");
            DropForeignKey("dbo.ClinicBuildings", "Id", "dbo.Buildings");
            DropForeignKey("dbo.CampBuilding", "Id", "dbo.Buildings");
            DropForeignKey("dbo.BotanicGardens", "Id", "dbo.Buildings");
            DropForeignKey("dbo.Buildings", "Id", "dbo.Objects");
            DropForeignKey("dbo.Objects", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Objects", "ParentId", "dbo.Objects");
            DropForeignKey("dbo.ClassesRules", "ClassIdInside", "dbo.Classes");
            DropForeignKey("dbo.ClassesRules", "ClassId", "dbo.Classes");
            DropIndex("dbo.Symbols", new[] { "Id" });
            DropIndex("dbo.StudyingRooms", new[] { "Id" });
            DropIndex("dbo.PracticeRooms", new[] { "Id" });
            DropIndex("dbo.DiningRooms", new[] { "Id" });
            DropIndex("dbo.Rooms", new[] { "Id" });
            DropIndex("dbo.Reports", new[] { "Id" });
            DropIndex("dbo.Repairs", new[] { "Id" });
            DropIndex("dbo.Processes", new[] { "Id" });
            DropIndex("dbo.Newspapers", new[] { "Id" });
            DropIndex("dbo.Magazines", new[] { "Id" });
            DropIndex("dbo.PrintedEditions", new[] { "Id" });
            DropIndex("dbo.Teachers", new[] { "Id" });
            DropIndex("dbo.WorkingPersons", new[] { "Id" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.StudyingPersons", new[] { "Id" });
            DropIndex("dbo.Persons", new[] { "Id" });
            DropIndex("dbo.Metherials", new[] { "Id" });
            DropIndex("dbo.Inventories", new[] { "Id" });
            DropIndex("dbo.Meetings", new[] { "Id" });
            DropIndex("dbo.Festivals", new[] { "Id" });
            DropIndex("dbo.Excursions", new[] { "Id" });
            DropIndex("dbo.Events", new[] { "Id" });
            DropIndex("dbo.EducationalSubjects", new[] { "Id" });
            DropIndex("dbo.StudentTickets", new[] { "Id" });
            DropIndex("dbo.Gradebooks", new[] { "Id" });
            DropIndex("dbo.Contracts", new[] { "Id" });
            DropIndex("dbo.Documents", new[] { "Id" });
            DropIndex("dbo.Faculties", new[] { "Id" });
            DropIndex("dbo.Deaneries", new[] { "Id" });
            DropIndex("dbo.Departmets", new[] { "Id" });
            DropIndex("dbo.StudyingBuildings", new[] { "Id" });
            DropIndex("dbo.SportBuildings", new[] { "Id" });
            DropIndex("dbo.ClinicBuildings", new[] { "Id" });
            DropIndex("dbo.CampBuilding", new[] { "Id" });
            DropIndex("dbo.BotanicGardens", new[] { "Id" });
            DropIndex("dbo.Buildings", new[] { "Id" });
            DropIndex("dbo.Objects", new[] { "ParentId" });
            DropIndex("dbo.Objects", new[] { "ClassId" });
            DropIndex("dbo.ClassesRules", new[] { "ClassIdInside" });
            DropIndex("dbo.ClassesRules", new[] { "ClassId" });
            DropTable("dbo.Symbols");
            DropTable("dbo.StudyingRooms");
            DropTable("dbo.PracticeRooms");
            DropTable("dbo.DiningRooms");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reports");
            DropTable("dbo.Repairs");
            DropTable("dbo.Processes");
            DropTable("dbo.Newspapers");
            DropTable("dbo.Magazines");
            DropTable("dbo.PrintedEditions");
            DropTable("dbo.Teachers");
            DropTable("dbo.WorkingPersons");
            DropTable("dbo.Students");
            DropTable("dbo.StudyingPersons");
            DropTable("dbo.Persons");
            DropTable("dbo.Metherials");
            DropTable("dbo.Inventories");
            DropTable("dbo.Meetings");
            DropTable("dbo.Festivals");
            DropTable("dbo.Excursions");
            DropTable("dbo.Events");
            DropTable("dbo.EducationalSubjects");
            DropTable("dbo.StudentTickets");
            DropTable("dbo.Gradebooks");
            DropTable("dbo.Contracts");
            DropTable("dbo.Documents");
            DropTable("dbo.Faculties");
            DropTable("dbo.Deaneries");
            DropTable("dbo.Departmets");
            DropTable("dbo.StudyingBuildings");
            DropTable("dbo.SportBuildings");
            DropTable("dbo.ClinicBuildings");
            DropTable("dbo.CampBuilding");
            DropTable("dbo.BotanicGardens");
            DropTable("dbo.Buildings");
            DropTable("dbo.Objects");
            DropTable("dbo.ClassesRules");
            DropTable("dbo.Classes");
        }
    }
}
