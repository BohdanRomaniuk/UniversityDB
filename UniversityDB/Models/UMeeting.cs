using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Meetings")]
    public class UMeeting : UEvent
    {
        public string MeetingType { get; set; }

        public UMeeting():
            base()
        {
        }

        public UMeeting(string _name, DateTime _date, string _location, string _meetingType, int _classId) :
            base(_name, _date, _location, _classId)
        {
            MeetingType = _meetingType;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UMeeting).MeetingType = MeetingType;
        }
    }
}
