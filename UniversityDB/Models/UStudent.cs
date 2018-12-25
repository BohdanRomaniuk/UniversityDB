using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Students")]
    public class UStudent : UStudyingPerson
    {
        public int YearNumber { get; set; }

        public UStudent() :
            base()
        {
        }

        public UStudent(string _name, DateTime _birthday, string _address, double _avgMark, int _year, int _class)
            : base(_name, _birthday, _address, _avgMark, _class)
        {
            YearNumber = _year;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UStudent).YearNumber = YearNumber;
        }
    }
}
