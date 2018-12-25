using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDB.Models
{
    [Table("Repairs")]
    public class URepair : UProcess
    {
        public string RepairObject { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public URepair() :
            base()
        {
            From = Convert.ToDateTime("2018-01-01");
            To = Convert.ToDateTime("2019-01-01");
        }

        public URepair(string _name, string _type, string _object, DateTime _from, DateTime _to, int _classId) :
            base(_name, _type,  _classId)
        {
            RepairObject = _object;
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
            (another as URepair).RepairObject = RepairObject;
            (another as URepair).From = From;
            (another as URepair).To = To;
        }
    }
}
