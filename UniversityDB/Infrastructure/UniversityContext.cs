using System.Data.Entity;
using UniversityDB.Models;

namespace UniversityDB.Infrastructure
{
    public class UniversityContext: DbContext
    {
        public UniversityContext():
            base("UniversityDB")
        {
        }

        public DbSet<UObject> Objects { get; set; }
    }
}
