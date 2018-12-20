using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using UniversityDB.Infrastructure;

namespace UniversityDB.Models
{
    [Table("StudyingPersons")]
    public class UStudyingPerson: UPerson
    {
        public double AvarageMark { get; set; }

        public UStudyingPerson():
            base()
        {
        }

        public UStudyingPerson(string _name, DateTime _birthday, string _address, double _avgMark, int _class)
            :base(_name, _birthday, _address, _class)
        {
            AvarageMark = _avgMark;
        }

        protected override void CreateActions()
        {
            base.CreateActions();
            Actions.Add(new ContextAction() { Name = "Сер. оцінка", Action = new Command(ShowAvgMark) });
        }

        private void ShowAvgMark(object parameters)
        {
            MessageBox.Show(AvarageMark.ToString(), "Cередня оцінка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UStudyingPerson).AvarageMark = AvarageMark;
        }
    }
}
