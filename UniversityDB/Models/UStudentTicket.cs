using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("StudentTickets")]
    public class UStudentTicket : UDocument
    {
        public DateTime ValidDate { get; set; }

        public UStudentTicket(): 
            base()
        {
            ValidDate = Convert.ToDateTime("01-01-2019");
        }

        public UStudentTicket(string _name, long _regNumber, DateTime _date, int _classId) :
            base(_name, _regNumber, _classId)
        {
            ValidDate = _date;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UStudentTicket).ValidDate = ValidDate;
        }
    }
}
