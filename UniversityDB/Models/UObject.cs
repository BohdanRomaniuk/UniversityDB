using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace UniversityDB.Models
{
    [Table("Objects")]
    public class UObject : INotifyPropertyChanged
    {
        private string name;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Class { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public UObject Parent { get; set; }
        public ObservableCollection<UObject> Childrens { get; set; }

        public UObject()
        {
            Childrens = new ObservableCollection<UObject>();
        }

        public UObject(string _name, int _class)
        {
            Name = _name;
            Class = _class;
            Childrens = new ObservableCollection<UObject>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
