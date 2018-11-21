using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using UniversityDB.Infrastructure;

namespace UniversityDB.Models
{
    [Table("Persons")]
    public class UPerson : UObject
    {
        public DateTime Birthday { get; set; }
        public string Address { get; set; }

        protected override void CreateActions()
        {
            base.CreateActions();
            Actions.Add(new ContextAction() { Name = "Адреса проживання", Action = new Command(PersonLocation) });
        }

        public UPerson():
            base()
        {
        }

        private void PersonLocation(object parameter)
        {
            MessageBox.Show(Name + " проживає за адресою " + Address, Name, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public UPerson(string _name, DateTime _birthday, string _address, int _class) :
            base(_name, _class)
        {
            Birthday = _birthday;
            Address = _address;
        }
    }
}
