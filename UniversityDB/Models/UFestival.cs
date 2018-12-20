using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Festivals")]
    public class UFestival : UEvent
    {
        public string Duration { get; set; }

        public UFestival():
            base()
        {
        }

        public UFestival(string _name, DateTime _date, string _location, string _duration, int _classId) :
            base(_name, _date, _location, _classId)
        {
            Duration = _duration;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UFestival).Duration = Duration;
        }
    }
}
