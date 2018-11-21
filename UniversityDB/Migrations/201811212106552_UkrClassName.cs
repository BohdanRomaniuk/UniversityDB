namespace UniversityDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UkrClassName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "UkrName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "UkrName");
        }
    }
}
