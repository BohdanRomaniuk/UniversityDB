using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Windows.Input;
using UniversityDB.Forms;
using UniversityDB.Infrastructure;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

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

        public ObservableCollection<UObject> Childrens { get; set; }
        public ICommand ViewCommand { get; private set; }

        public UObject()
        {
            ViewCommand = new Command(View);
        }

        public UObject(string _name, int _class)
        {
            Name = _name;
            Class = _class;
        }

        private void View(object parametr)
        {
            UObjectWindow window = new UObjectWindow(Convert.ToInt32(parametr));
            window.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
