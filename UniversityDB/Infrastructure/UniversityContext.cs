using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using UniversityDB.Models;

namespace UniversityDB.Infrastructure
{
    public class UniversityContext: DbContext, INotifyPropertyChanged
    {
        public UniversityContext():
            base("UniversityDB")
        {
        }

        public DbSet<UObject> Objects { get; set; }
        public override int SaveChanges()
        {
            OnPropertyChanged(nameof(Objects));
            return base.SaveChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
