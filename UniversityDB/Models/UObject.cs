using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using UniversityDB.Infrastructure;

namespace UniversityDB.Models
{
    [Table("Objects")]
    public class UObject: INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Class { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public UObject Parent { get; set; }
        public ObservableCollection<UObject> Childrens { get; set; }

        public UObject()
        {
            ViewCommand = new Command(View);
        }

        private void View(object parametr)
        {
            MessageBox.Show(parametr.ToString(), "Інфо");
        }

        public ICommand ViewCommand { get; private set; }

        public UObject(string _name, int _class)
        {
            Name = _name;
            Class = _class;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
