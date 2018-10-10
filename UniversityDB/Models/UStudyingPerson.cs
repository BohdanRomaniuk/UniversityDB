using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
