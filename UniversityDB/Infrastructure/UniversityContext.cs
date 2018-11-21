using System.Data.Entity;
using UniversityDB.Models;

namespace UniversityDB.Infrastructure
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() :
            base("UniversityDB")
        {
        }

        public DbSet<UObject> Objects { get; set; }
        public DbSet<SClass> Classes { get; set; }
        public DbSet<SClassRules> ClassesRules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SClassRules>()
                .HasRequired(r=>r.ClassInside)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
