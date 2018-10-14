using System;
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

        [Required]
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public SClass Class { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public UObject Parent { get; set; }

        public ObservableCollection<UObject> Childrens { get; set; }

        public UObject()
        {
            Childrens = new ObservableCollection<UObject>();
        }

        public UObject(string _name, int _classId)
        {
            Name = _name;
            ClassId = _classId;
            Childrens = new ObservableCollection<UObject>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void CopyPropertiesTo(UObject another)
        {
            another.Name = Name;
            another.ParentId = ParentId;
            another.ClassId = ClassId;
        }
    }
}
