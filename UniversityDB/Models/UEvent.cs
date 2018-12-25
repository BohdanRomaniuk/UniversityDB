using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Events")]
    public class UEvent : UObject
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }

        public UEvent() :
            base()
        {
            Date = Convert.ToDateTime("01-01-2010");
        }

        public UEvent(string _name, DateTime _date, string _location, int _classId) :
            base(_name, _classId)
        {
            Date = _date;
            Location = _location;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UEvent).Date = Date;
            (another as UEvent).Location = Location;
        }
    }
}
