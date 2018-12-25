using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Teachers")]
    public class UTeacher : UWorkingPerson
    {
        public string Rank { get; set; }

        public UTeacher() :
            base()
        {
        }

        public UTeacher(string _name, DateTime _birthday, string _address, double _salary, string _rank, int _class) :
            base(_name, _birthday, _address, _salary, _class)
        {
            Rank = _rank;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UTeacher).Rank = Rank;
        }
    }
}
