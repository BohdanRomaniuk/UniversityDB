using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
