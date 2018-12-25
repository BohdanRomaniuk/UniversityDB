using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("WorkingPersons")]
    public class UWorkingPerson: UPerson
    {
        public double Salary { get; set; }

        public UWorkingPerson():
            base()
        {
        }

        public UWorkingPerson(string _name, DateTime _birthday, string _address, double _salary, int _class) :
            base(_name, _birthday, _address, _class)
        {
            Salary = _salary;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UWorkingPerson).Salary = Salary;
        }
    }
}
