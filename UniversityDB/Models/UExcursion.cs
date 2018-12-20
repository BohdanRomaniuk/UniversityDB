using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Excursions")]
    public class UExcursion : UEvent
    {
        public string Transport { get; set; }

        public UExcursion():
            base()
        {
        }

        public UExcursion(string _name, DateTime _date, string _location, string _transport, int _classId) :
            base(_name, _date, _location, _classId)
        {
            Transport = _transport;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UExcursion).Transport = Transport;
        }
    }
}
