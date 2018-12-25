using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Newspapers")]
    public class UNewspaper : UPrintEdition
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public UNewspaper():
            base()
        {
            From = Convert.ToDateTime("2018-01-01");
            To = Convert.ToDateTime("2018-02-01");
        }

        public UNewspaper(string _name, int _count, DateTime _from, DateTime _to, int _classId) :
            base(_name, _count, _classId)
        {
            From = _from;
            To = _to;
        }

        //---------Hierarchy of Behavior
        protected override void CreateActions()
        {
            base.CreateActions();
        }

        public override void CopyPropertiesTo(UObject another)
        {
            base.CopyPropertiesTo(another);
            (another as UNewspaper).From = From;
            (another as UNewspaper).To = To;
        }
    }
}
