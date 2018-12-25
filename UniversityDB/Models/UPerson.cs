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

        public UPerson():
            base()
        {
            Birthday = Convert.ToDateTime("01.01.2010");
        }

        public UPerson(string _name, DateTime _birthday, string _address, int _class) :
            base(_name, _class)
        {
            Birthday = _birthday;
            Address = _address;
        }

        protected override void CreateActions()
        {
            base.CreateActions();
            Actions.Add(new ContextAction() { Name = "Адреса проживання", Action = new Command(PersonLocation) });
        }

        private void PersonLocation(object parameter)
        {
            MessageBox.Show(Name + " проживає за адресою " + Address, Name, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UPerson).Birthday = Birthday;
            (another as UPerson).Address = Address;
        }
    }
}
